using Coldairarrow.Entity;
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
    /// 博客工具管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客工具管理")]
    public class Blog_ToolController : BaseApiController
    {
        #region DI

        public Blog_ToolController(IBlog_ToolBusiness toolBus)
        {
            _toolBus = toolBus;
        }

        IBlog_ToolBusiness _toolBus { get; }

        #endregion

        #region 工具管理

        /// <summary>
        /// 获取工具分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Blog_Tool>> GetDataList(BlogToolQueryInputDTO input)
        {
            return await _toolBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 获取工具详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog_Tool> GetTheData(IdInputDTO input)
        {
            return await _toolBus.GetTheDataAsync(input.id.ToInt());
        }

        /// <summary>
        /// 保存工具
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveData(BlogToolInputDTO input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                await _toolBus.UpdateDataAsync(input);
            }
            else
            {
                await _toolBus.AddDataAsync(input);
            }
        }

        /// <summary>
        /// 删除工具
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteData(List<int> ids)
        {
            await _toolBus.DeleteDataAsync(ids);
        }

        /// <summary>
        /// 获取推荐工具列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Tool>> GetRecommendedTools(int count = 10)
        {
            return await _toolBus.GetRecommendedToolsAsync(count);
        }

        /// <summary>
        /// 获取最新工具列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Tool>> GetLatestTools(int count = 10)
        {
            return await _toolBus.GetLatestToolsAsync(count);
        }

        /// <summary>
        /// 获取工具分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<string>> GetCategories()
        {
            return await _toolBus.GetCategoriesAsync();
        }

        #endregion
    }
}