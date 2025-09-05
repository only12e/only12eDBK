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
    /// 博客友情链接管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客友情链接管理")]
    public class Blog_FriendlyLinkController : BaseApiController
    {
        #region DI

        public Blog_FriendlyLinkController(IBlog_FriendlyLinkBusiness friendlyLinkBus)
        {
            _friendlyLinkBus = friendlyLinkBus;
        }

        IBlog_FriendlyLinkBusiness _friendlyLinkBus { get; }

        #endregion

        #region 友情链接管理

        /// <summary>
        /// 获取友情链接分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Blog_FriendlyLink>> GetDataList(BlogFriendlyLinkQueryInputDTO input)
        {
            return await _friendlyLinkBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 获取友情链接详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog_FriendlyLink> GetTheData(IdInputDTO input)
        {
            return await _friendlyLinkBus.GetTheDataAsync(input.id.ToInt());
        }

        /// <summary>
        /// 获取所有启用的友情链接
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_FriendlyLink>> GetEnabledLinks()
        {
            return await _friendlyLinkBus.GetEnabledLinksAsync();
        }

        /// <summary>
        /// 获取友情链接列表（按排序权重）
        /// </summary>
        /// <param name="status">状态筛选，null表示获取所有</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_FriendlyLink>> GetSortedLinks(int? status = null)
        {
            return await _friendlyLinkBus.GetSortedLinksAsync(status);
        }

        /// <summary>
        /// 保存友情链接
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveData(BlogFriendlyLinkInputDTO input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                await _friendlyLinkBus.UpdateDataAsync(input);
            }
            else
            {
                await _friendlyLinkBus.AddDataAsync(input);
            }
        }

        /// <summary>
        /// 删除友情链接
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteData(List<int> ids)
        {
            await _friendlyLinkBus.DeleteDataAsync(ids);
        }

        /// <summary>
        /// 批量更新状态
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task BatchUpdateStatus(BlogFriendlyLinkBatchStatusInputDTO input)
        {
            await _friendlyLinkBus.BatchUpdateStatusAsync(input);
        }

        /// <summary>
        /// 更新排序
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task UpdateSortOrder(BlogFriendlyLinkSortInputDTO input)
        {
            await _friendlyLinkBus.UpdateSortOrderAsync(input);
        }

        /// <summary>
        /// 检查链接名称是否存在
        /// </summary>
        /// <param name="name">链接名称</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> NameExists(string name, int? excludeId = null)
        {
            return await _friendlyLinkBus.NameExistsAsync(name, excludeId);
        }

        /// <summary>
        /// 检查URL是否存在
        /// </summary>
        /// <param name="url">链接地址</param>
        /// <param name="excludeId">排除的ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> UrlExists(string url, int? excludeId = null)
        {
            return await _friendlyLinkBus.UrlExistsAsync(url, excludeId);
        }

        /// <summary>
        /// 切换友情链接状态
        /// </summary>
        /// <param name="id">友情链接ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task ToggleStatus(int id)
        {
            await _friendlyLinkBus.ToggleStatusAsync(id);
        }

        #endregion
    }
}