using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Blog_Manage
{
    /// <summary>
    /// 博客系统配置管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客系统配置管理")]
    public class Blog_SystemConfigController : BaseApiController
    {
        #region DI

        public Blog_SystemConfigController(IBlog_SystemConfigBusiness systemConfigBus)
        {
            _systemConfigBus = systemConfigBus;
        }

        IBlog_SystemConfigBusiness _systemConfigBus { get; }

        #endregion

        #region 系统配置管理

        /// <summary>
        /// 获取系统配置分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Blog_SystemConfig>> GetDataList(BlogSystemConfigQueryInputDTO input)
        {
            return await _systemConfigBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 获取系统配置详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog_SystemConfig> GetTheData(IdInputDTO input)
        {
            return await _systemConfigBus.GetTheDataAsync(input.id.ToInt());
        }

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_SystemConfig>> GetAllConfigs()
        {
            return await _systemConfigBus.GetAllConfigsAsync();
        }

        /// <summary>
        /// 根据配置键获取配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<string> GetConfigValue(string configKey)
        {
            return await _systemConfigBus.GetConfigValueAsync(configKey);
        }

        /// <summary>
        /// 批量获取配置值
        /// </summary>
        /// <param name="configKeys">配置键列表</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Dictionary<string, string>> GetConfigValues([FromBody] List<string> configKeys)
        {
            return await _systemConfigBus.GetConfigValuesAsync(configKeys);
        }

        /// <summary>
        /// 保存系统配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiPermission("Blog_Config.Edit")]
        public async Task SaveData(BlogSystemConfigInputDTO input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                await _systemConfigBus.UpdateDataAsync(input);
            }
            else
            {
                await _systemConfigBus.AddDataAsync(input);
            }
        }

        /// <summary>
        /// 删除系统配置
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiPermission("Blog_Config.Edit")]
        public async Task DeleteData(List<int> ids)
        {
            await _systemConfigBus.DeleteDataAsync(ids);
        }

        /// <summary>
        /// 批量更新配置
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiPermission("Blog_Config.Edit")]
        public async Task BatchUpdate(BlogSystemConfigBatchUpdateInputDTO input)
        {
            await _systemConfigBus.BatchUpdateAsync(input);
        }

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <param name="description">配置说明</param>
        /// <returns></returns>
        [HttpPost]
        [ApiPermission("Blog_Config.Edit")]
        public async Task SetConfigValue(string configKey, string configValue, string description = null)
        {
            await _systemConfigBus.SetConfigValueAsync(configKey, configValue, description);
        }

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns></returns>
        [HttpPost]
        [ApiPermission("Blog_Config.Edit")]
        public async Task DeleteConfig(string configKey)
        {
            await _systemConfigBus.DeleteConfigAsync(configKey);
        }

        /// <summary>
        /// 检查配置键是否存在
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> ConfigKeyExists(string configKey, int? excludeId = null)
        {
            return await _systemConfigBus.ConfigKeyExistsAsync(configKey, excludeId);
        }

        #endregion
    }
}