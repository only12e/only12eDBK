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
    /// 博客项目管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客项目管理")]
    public class Blog_ProjectController : BaseApiController
    {
        #region DI

        public Blog_ProjectController(IBlog_ProjectBusiness projectBus)
        {
            _projectBus = projectBus;
        }

        IBlog_ProjectBusiness _projectBus { get; }

        #endregion

        #region 项目管理

        /// <summary>
        /// 获取项目分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Blog_Project>> GetDataList(BlogProjectQueryInputDTO input)
        {
            return await _projectBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 获取项目详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog_Project> GetTheData(IdInputDTO input)
        {
            return await _projectBus.GetTheDataAsync(input.id.ToInt());
        }

        /// <summary>
        /// 保存项目
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveData(BlogProjectInputDTO input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                await _projectBus.UpdateDataAsync(input);
            }
            else
            {
                await _projectBus.AddDataAsync(input);
            }
        }

        /// <summary>
        /// 删除项目
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteData(List<int> ids)
        {
            await _projectBus.DeleteDataAsync(ids);
        }

        /// <summary>
        /// 获取精选项目列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Project>> GetFeaturedProjects(int count = 10)
        {
            return await _projectBus.GetFeaturedProjectsAsync(count);
        }

        /// <summary>
        /// 获取最新项目列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Project>> GetLatestProjects(int count = 10)
        {
            return await _projectBus.GetLatestProjectsAsync(count);
        }

        #endregion
    }
}