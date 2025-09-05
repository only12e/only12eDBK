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
using System.Threading.Tasks;

namespace Coldairarrow.Business.Blog_Manage
{
    /// <summary>
    /// 博客友情链接业务实现
    /// </summary>
    public class Blog_FriendlyLinkBusiness : BaseBusiness<Blog_FriendlyLink>, IBlog_FriendlyLinkBusiness, ITransientDependency
    {
        public Blog_FriendlyLinkBusiness(IDbAccessor db) : base(db)
        {

        }

        #region 外部接口

        /// <summary>
        /// 获取友情链接分页列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        public async Task<PageResult<Blog_FriendlyLink>> GetDataListAsync(BlogFriendlyLinkQueryInputDTO input)
        {
            var q = GetIQueryable();

            var where = LinqHelper.True<Blog_FriendlyLink>();

            // 关键词搜索
            if (!input.Keyword.IsNullOrEmpty())
            {
                where = where.And(x => x.Name.Contains(input.Keyword) || x.Description.Contains(input.Keyword));
            }

            // 状态筛选
            if (input.Status.HasValue)
            {
                where = where.And(x => x.Status == input.Status.Value);
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

            return await q.Where(where).OrderByDescending(x => x.SortOrder).ThenByDescending(x => x.CreatedAt).GetPageResultAsync(input);
        }

        /// <summary>
        /// 根据ID获取友情链接详情
        /// </summary>
        /// <param name="id">友情链接ID</param>
        /// <returns></returns>
        public async Task<Blog_FriendlyLink> GetTheDataAsync(int id)
        {
            return await GetIQueryable()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// 获取所有启用的友情链接
        /// </summary>
        /// <returns></returns>
        public async Task<List<Blog_FriendlyLink>> GetEnabledLinksAsync()
        {
            return await GetIQueryable()
                .Where(x => x.Status == 1)
                .OrderByDescending(x => x.SortOrder)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// 获取友情链接列表（按排序权重）
        /// </summary>
        /// <param name="status">状态筛选，null表示获取所有</param>
        /// <returns></returns>
        public async Task<List<Blog_FriendlyLink>> GetSortedLinksAsync(int? status = null)
        {
            var query = GetIQueryable();

            if (status.HasValue)
            {
                query = query.Where(x => x.Status == status.Value);
            }

            return await query
                .OrderByDescending(x => x.SortOrder)
                .ThenByDescending(x => x.CreatedAt)
                .ToListAsync();
        }

        /// <summary>
        /// 新增友情链接
        /// </summary>
        /// <param name="input">友情链接数据</param>
        /// <returns></returns>
        public async Task AddDataAsync(BlogFriendlyLinkInputDTO input)
        {
            // 检查名称是否已存在
            if (await NameExistsAsync(input.Name))
            {
                throw new BusException($"链接名称 '{input.Name}' 已存在");
            }

            // 检查URL是否已存在
            if (await UrlExistsAsync(input.Url))
            {
                throw new BusException($"链接地址 '{input.Url}' 已存在");
            }

            var entity = new Blog_FriendlyLink
            {
                Name = input.Name,
                Url = input.Url,
                Description = input.Description,
                LogoUrl = input.LogoUrl,
                Status = input.Status,
                SortOrder = input.SortOrder,
                CreatedAt = DateTime.Now
            };

            await InsertAsync(entity);
        }

        /// <summary>
        /// 更新友情链接
        /// </summary>
        /// <param name="input">友情链接数据</param>
        /// <returns></returns>
        public async Task UpdateDataAsync(BlogFriendlyLinkInputDTO input)
        {
            if (!input.Id.HasValue)
            {
                throw new BusException("友情链接ID不能为空");
            }

            // 检查名称是否已存在（排除当前记录）
            if (await NameExistsAsync(input.Name, input.Id))
            {
                throw new BusException($"链接名称 '{input.Name}' 已存在");
            }

            // 检查URL是否已存在（排除当前记录）
            if (await UrlExistsAsync(input.Url, input.Id))
            {
                throw new BusException($"链接地址 '{input.Url}' 已存在");
            }

            var entity = await GetTheDataAsync(input.Id.Value);
            if (entity == null)
            {
                throw new BusException("友情链接不存在");
            }

            entity.Name = input.Name;
            entity.Url = input.Url;
            entity.Description = input.Description;
            entity.LogoUrl = input.LogoUrl;
            entity.Status = input.Status;
            entity.SortOrder = input.SortOrder;

            await UpdateAsync(entity);
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="ids">友情链接ID列表</param>
        /// <returns></returns>
        public async Task DeleteDataAsync(List<int> ids)
        {
            var stringIds = ids.Select(x => x.ToString()).ToList();
            await DeleteAsync(stringIds);
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="input">批量状态更新数据</param>
        /// <returns></returns>
        public async Task BatchUpdateStatusAsync(BlogFriendlyLinkBatchStatusInputDTO input)
        {
            var entities = await GetIQueryable()
                .Where(x => input.Ids.Contains(x.Id))
                .ToListAsync();

            foreach (var entity in entities)
            {
                entity.Status = input.Status;
            }

            await UpdateAsync(entities);
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="input">排序更新数据</param>
        /// <returns></returns>
        public async Task UpdateSortOrderAsync(BlogFriendlyLinkSortInputDTO input)
        {
            var ids = input.Items.Select(x => x.Id).ToList();
            var entities = await GetIQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            foreach (var entity in entities)
            {
                var sortItem = input.Items.FirstOrDefault(x => x.Id == entity.Id);
                if (sortItem != null)
                {
                    entity.SortOrder = sortItem.SortOrder;
                }
            }

            await UpdateAsync(entities);
        }

        /// <summary>
        /// 检查链接名称是否存在
        /// </summary>
        /// <param name="name">链接名称</param>
        /// <param name="excludeId">排除的ID（用于编辑时检查）</param>
        /// <returns></returns>
        public async Task<bool> NameExistsAsync(string name, int? excludeId = null)
        {
            var query = GetIQueryable().Where(x => x.Name == name);
            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        /// <summary>
        /// 检查URL是否存在
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <param name="excludeId">排除的ID（用于编辑时检查）</param>
        /// <returns></returns>
        public async Task<bool> UrlExistsAsync(string url, int? excludeId = null)
        {
            var query = GetIQueryable().Where(x => x.Url == url);
            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }
            return await query.AnyAsync();
        }

        /// <summary>
        /// 切换友情链接状态
        /// </summary>
        /// <param name="id">友情链接ID</param>
        /// <returns></returns>
        public async Task ToggleStatusAsync(int id)
        {
            var entity = await GetTheDataAsync(id);
            if (entity == null)
            {
                throw new BusException("友情链接不存在");
            }

            entity.Status = entity.Status == 1 ? 0 : 1;
            await UpdateAsync(entity);
        }

        #endregion

        #region 私有方法

        #endregion
    }
}