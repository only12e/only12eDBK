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
    /// 博客技术栈管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客技术栈管理")]
    public class Blog_TechnologyController : BaseApiController
    {
        #region DI

        public Blog_TechnologyController(IBlog_TechnologyBusiness technologyBus)
        {
            _technologyBus = technologyBus;
        }

        IBlog_TechnologyBusiness _technologyBus { get; }

        #endregion

        #region 技术栈管理

        /// <summary>
        /// 获取技术栈分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Blog_Technology>> GetDataList(BlogTechnologyQueryInputDTO input)
        {
            return await _technologyBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 获取技术栈详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog_Technology> GetTheData(IdInputDTO input)
        {
            return await _technologyBus.GetTheDataAsync(input.id.ToInt());
        }

        /// <summary>
        /// 保存技术栈
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveData(BlogTechnologyInputDTO input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                await _technologyBus.UpdateDataAsync(input);
            }
            else
            {
                await _technologyBus.AddDataAsync(input);
            }
        }

        /// <summary>
        /// 删除技术栈
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteData(List<int> ids)
        {
            await _technologyBus.DeleteDataAsync(ids);
        }

        /// <summary>
        /// 获取精选技术栈列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Technology>> GetFeaturedTechnologies(int count = 10)
        {
            return await _technologyBus.GetFeaturedTechnologiesAsync(count);
        }

        /// <summary>
        /// 获取最新技术栈列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Technology>> GetLatestTechnologies(int count = 10)
        {
            return await _technologyBus.GetLatestTechnologiesAsync(count);
        }

        /// <summary>
        /// 获取技术栈分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<string>> GetCategories()
        {
            return await _technologyBus.GetCategoriesAsync();
        }

        /// <summary>
        /// 根据分类获取技术栈列表
        /// </summary>
        /// <param name="category">技术分类</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Technology>> GetTechnologiesByCategory(string category)
        {
            return await _technologyBus.GetTechnologiesByCategoryAsync(category);
        }

        #endregion
    }
}