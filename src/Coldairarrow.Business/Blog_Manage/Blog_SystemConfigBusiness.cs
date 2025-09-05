using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Blog_Manage
{
    /// <summary>
    /// 博客系统配置业务实现
    /// </summary>
    public class Blog_SystemConfigBusiness : BaseBusiness<Blog_SystemConfig>, IBlog_SystemConfigBusiness, ITransientDependency
    {
        public Blog_SystemConfigBusiness(IDbAccessor db) : base(db)
        {

        }

        #region 外部接口

        /// <summary>
        /// 获取配置分页列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PageResult<Blog_SystemConfig>> GetDataListAsync(BlogSystemConfigQueryInputDTO input)
        {
            var q = GetIQueryable();

            var where = LinqHelper.True<Blog_SystemConfig>();

            // 关键词搜索
            if (!input.Keyword.IsNullOrEmpty())
            {
                where = where.And(x => x.ConfigKey.Contains(input.Keyword) || x.Description.Contains(input.Keyword));
            }

            // 配置键精确匹配
            if (!input.ConfigKey.IsNullOrEmpty())
            {
                where = where.And(x => x.ConfigKey == input.ConfigKey);
            }

            // 时间范围
            if (input.StartTime.HasValue)
            {
                where = where.And(x => x.CreatedAt >= input.StartTime.Value);
            }
            if (input.EndTime.HasValue)
            {
                where = where.And(x => x.CreatedAt <= input.EndTime.Value);
            }

            return await q.Where(where).GetPageResultAsync(input);
        }

        /// <summary>
        /// 根据ID获取配置详情
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <returns></returns>
        public async Task<Blog_SystemConfig> GetTheDataAsync(int id)
        {
            return await GetEntityAsync(id);
        }

        /// <summary>
        /// 根据配置键获取配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns></returns>
        public async Task<string> GetConfigValueAsync(string configKey)
        {
            var config = await GetIQueryable().Where(x => x.ConfigKey == configKey).FirstOrDefaultAsync();
            return config?.ConfigValue;
        }

        /// <summary>
        /// 批量获取配置值
        /// </summary>
        /// <param name="configKeys">配置键列表</param>
        /// <returns></returns>
        public async Task<Dictionary<string, string>> GetConfigValuesAsync(List<string> configKeys)
        {
            var configs = await GetIQueryable()
                .Where(x => configKeys.Contains(x.ConfigKey))
                .Select(x => new { x.ConfigKey, x.ConfigValue })
                .ToListAsync();

            return configs.ToDictionary(x => x.ConfigKey, x => x.ConfigValue);
        }

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        public async Task<List<Blog_SystemConfig>> GetAllConfigsAsync()
        {
            return await GetIQueryable().OrderBy(x => x.ConfigKey).ToListAsync();
        }

        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="input">配置数据</param>
        /// <returns></returns>
        public async Task AddDataAsync(BlogSystemConfigInputDTO input)
        {
            // 检查配置键是否已存在
            if (await ConfigKeyExistsAsync(input.ConfigKey))
            {
                throw new BusException($"配置键 '{input.ConfigKey}' 已存在");
            }

            var entity = new Blog_SystemConfig
            {
                ConfigKey = input.ConfigKey,
                ConfigValue = input.ConfigValue,
                Description = input.Description,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await InsertAsync(entity);
        }

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="input">配置数据</param>
        /// <returns></returns>
        public async Task UpdateDataAsync(BlogSystemConfigInputDTO input)
        {
            if (!input.Id.HasValue)
            {
                throw new BusException("配置ID不能为空");
            }

            // 检查配置键是否已存在（排除当前记录）
            if (await ConfigKeyExistsAsync(input.ConfigKey, input.Id))
            {
                throw new BusException($"配置键 '{input.ConfigKey}' 已存在");
            }

            var entity = await GetTheDataAsync(input.Id.Value);
            if (entity == null)
            {
                throw new BusException("配置不存在");
            }

            entity.ConfigKey = input.ConfigKey;
            entity.ConfigValue = input.ConfigValue;
            entity.Description = input.Description;
            entity.UpdatedAt = DateTime.Now;

            await UpdateAsync(entity);
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="ids">配置ID列表</param>
        /// <returns></returns>
        public async Task DeleteDataAsync(List<int> ids)
        {
            var stringIds = ids.Select(x => x.ToString()).ToList();
            await DeleteAsync(stringIds);
        }

        /// <summary>
        /// 批量更新配置
        /// </summary>
        /// <param name="input">批量更新数据</param>
        /// <returns></returns>
        public async Task BatchUpdateAsync(BlogSystemConfigBatchUpdateInputDTO input)
        {
            var configKeys = input.Configs.Select(x => x.ConfigKey).ToList();
            var existingConfigs = await GetIQueryable()
                .Where(x => configKeys.Contains(x.ConfigKey))
                .ToListAsync();

            var updateList = new List<Blog_SystemConfig>();
            var insertList = new List<Blog_SystemConfig>();

            foreach (var item in input.Configs)
            {
                var existing = existingConfigs.FirstOrDefault(x => x.ConfigKey == item.ConfigKey);
                if (existing != null)
                {
                    // 更新现有配置
                    existing.ConfigValue = item.ConfigValue;
                    existing.UpdatedAt = DateTime.Now;
                    updateList.Add(existing);
                }
                else
                {
                    // 新增配置
                    insertList.Add(new Blog_SystemConfig
                    {
                        ConfigKey = item.ConfigKey,
                        ConfigValue = item.ConfigValue,
                        CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now
                    });
                }
            }

            if (updateList.Count > 0)
            {
                await UpdateAsync(updateList);
            }
            if (insertList.Count > 0)
            {
                await InsertAsync(insertList);
            }
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <param name="description">配置说明</param>
        /// <returns></returns>
        public async Task SetConfigValueAsync(string configKey, string configValue, string description = null)
        {
            var existing = await GetIQueryable().Where(x => x.ConfigKey == configKey).FirstOrDefaultAsync();
            if (existing != null)
            {
                // 更新现有配置
                existing.ConfigValue = configValue;
                if (!description.IsNullOrEmpty())
                {
                    existing.Description = description;
                }
                existing.UpdatedAt = DateTime.Now;
                await UpdateAsync(existing);
            }
            else
            {
                // 新增配置
                var entity = new Blog_SystemConfig
                {
                    ConfigKey = configKey,
                    ConfigValue = configValue,
                    Description = description,
                    CreatedAt = DateTime.Now,
                    UpdatedAt = DateTime.Now
                };
                await InsertAsync(entity);
            }
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns></returns>
        public async Task DeleteConfigAsync(string configKey)
        {
            var entity = await GetIQueryable().Where(x => x.ConfigKey == configKey).FirstOrDefaultAsync();
            if (entity != null)
            {
                await DeleteAsync(entity.Id.ToString());
            }
        }

        /// <summary>
        /// 检查配置键是否存在
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="excludeId">排除的ID（用于编辑时检查）</param>
        /// <returns></returns>
        public async Task<bool> ConfigKeyExistsAsync(string configKey, int? excludeId = null)
        {
            var query = GetIQueryable().Where(x => x.ConfigKey == configKey);
            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        #endregion

        #region 私有方法

        #endregion
    }
}