using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    /// <summary>
    /// 博客系统配置业务接口
    /// </summary>
    public interface IBlog_SystemConfigBusiness
    {
        /// <summary>
        /// 获取配置分页列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PageResult<Blog_SystemConfig>> GetDataListAsync(BlogSystemConfigQueryInputDTO input);

        /// <summary>
        /// 根据ID获取配置详情
        /// </summary>
        /// <param name="id">配置ID</param>
        /// <returns></returns>
        Task<Blog_SystemConfig> GetTheDataAsync(int id);

        /// <summary>
        /// 根据配置键获取配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns></returns>
        Task<string> GetConfigValueAsync(string configKey);

        /// <summary>
        /// 批量获取配置值
        /// </summary>
        /// <param name="configKeys">配置键列表</param>
        /// <returns></returns>
        Task<Dictionary<string, string>> GetConfigValuesAsync(List<string> configKeys);

        /// <summary>
        /// 获取所有配置
        /// </summary>
        /// <returns></returns>
        Task<List<Blog_SystemConfig>> GetAllConfigsAsync();

        /// <summary>
        /// 新增配置
        /// </summary>
        /// <param name="input">配置数据</param>
        /// <returns></returns>
        Task AddDataAsync(BlogSystemConfigInputDTO input);

        /// <summary>
        /// 更新配置
        /// </summary>
        /// <param name="input">配置数据</param>
        /// <returns></returns>
        Task UpdateDataAsync(BlogSystemConfigInputDTO input);

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="ids">配置ID列表</param>
        /// <returns></returns>
        Task DeleteDataAsync(List<int> ids);

        /// <summary>
        /// 批量更新配置
        /// </summary>
        /// <param name="input">批量更新数据</param>
        /// <returns></returns>
        Task BatchUpdateAsync(BlogSystemConfigBatchUpdateInputDTO input);

        /// <summary>
        /// 设置配置值
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="configValue">配置值</param>
        /// <param name="description">配置说明</param>
        /// <returns></returns>
        Task SetConfigValueAsync(string configKey, string configValue, string description = null);

        /// <summary>
        /// 删除配置
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <returns></returns>
        Task DeleteConfigAsync(string configKey);

        /// <summary>
        /// 检查配置键是否存在
        /// </summary>
        /// <param name="configKey">配置键</param>
        /// <param name="excludeId">排除的ID（用于编辑时检查）</param>
        /// <returns></returns>
        Task<bool> ConfigKeyExistsAsync(string configKey, int? excludeId = null);
    }
}