using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    /// <summary>
    /// 博客友情链接业务接口
    /// </summary>
    public interface IBlog_FriendlyLinkBusiness
    {
        /// <summary>
        /// 获取友情链接分页列表
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <returns></returns>
        Task<PageResult<Blog_FriendlyLink>> GetDataListAsync(BlogFriendlyLinkQueryInputDTO input);

        /// <summary>
        /// 根据ID获取友情链接详情
        /// </summary>
        /// <param name="id">友情链接ID</param>
        /// <returns></returns>
        Task<Blog_FriendlyLink> GetTheDataAsync(int id);

        /// <summary>
        /// 获取所有启用的友情链接
        /// </summary>
        /// <returns></returns>
        Task<List<Blog_FriendlyLink>> GetEnabledLinksAsync();

        /// <summary>
        /// 获取友情链接列表（按排序权重）
        /// </summary>
        /// <param name="status">状态筛选，null表示获取所有</param>
        /// <returns></returns>
        Task<List<Blog_FriendlyLink>> GetSortedLinksAsync(int? status = null);

        /// <summary>
        /// 新增友情链接
        /// </summary>
        /// <param name="input">友情链接数据</param>
        /// <returns></returns>
        Task AddDataAsync(BlogFriendlyLinkInputDTO input);

        /// <summary>
        /// 更新友情链接
        /// </summary>
        /// <param name="input">友情链接数据</param>
        /// <returns></returns>
        Task UpdateDataAsync(BlogFriendlyLinkInputDTO input);

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="ids">友情链接ID列表</param>
        /// <returns></returns>
        Task DeleteDataAsync(List<int> ids);

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="input">批量状态更新数据</param>
        /// <returns></returns>
        Task BatchUpdateStatusAsync(BlogFriendlyLinkBatchStatusInputDTO input);

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="input">排序更新数据</param>
        /// <returns></returns>
        Task UpdateSortOrderAsync(BlogFriendlyLinkSortInputDTO input);

        /// <summary>
        /// 检查链接名称是否存在
        /// </summary>
        /// <param name="name">链接名称</param>
        /// <param name="excludeId">排除的ID（用于编辑时检查）</param>
        /// <returns></returns>
        Task<bool> NameExistsAsync(string name, int? excludeId = null);

        /// <summary>
        /// 检查URL是否存在
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <param name="excludeId">排除的ID（用于编辑时检查）</param>
        /// <returns></returns>
        Task<bool> UrlExistsAsync(string url, int? excludeId = null);

        /// <summary>
        /// 切换友情链接状态
        /// </summary>
        /// <param name="id">友情链接ID</param>
        /// <returns></returns>
        Task ToggleStatusAsync(int id);
    }
}