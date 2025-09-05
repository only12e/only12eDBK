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
    /// 博客文章管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客文章管理")]
    public class Blog_ArticleController : BaseApiController
    {
        #region DI

        public Blog_ArticleController(IBlog_ArticleBusiness articleBus)
        {
            _articleBus = articleBus;
        }

        IBlog_ArticleBusiness _articleBus { get; }

        #endregion

        #region 文章管理

        /// <summary>
        /// 获取文章分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Blog_Article>> GetDataList(BlogArticleQueryInputDTO input)
        {
            return await _articleBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog_Article> GetTheData(IdInputDTO input)
        {
            return await _articleBus.GetTheDataAsync(input.id.ToInt());
        }

        /// <summary>
        /// 保存文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveData(BlogArticleInputDTO input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                await _articleBus.UpdateDataAsync(input);
            }
            else
            {
                await _articleBus.AddDataAsync(input);
            }
        }

        /// <summary>
        /// 删除文章
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteData(List<int> ids)
        {
            await _articleBus.DeleteDataAsync(ids);
        }

        /// <summary>
        /// 批量发布/取消发布文章
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task PublishData(BlogArticlePublishInputDTO input)
        {
            await _articleBus.PublishDataAsync(input);
        }

        /// <summary>
        /// 获取精选文章列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Article>> GetFeaturedArticles(int count = 10)
        {
            return await _articleBus.GetFeaturedArticlesAsync(count);
        }

        /// <summary>
        /// 获取最新文章列表
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Article>> GetLatestArticles(int count = 10)
        {
            return await _articleBus.GetLatestArticlesAsync(count);
        }

        #endregion

        #region 文章分类管理

        /// <summary>
        /// 获取文章分类列表
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_ArticleCategory>> GetCategories()
        {
            return await _articleBus.GetCategoriesAsync();
        }

        /// <summary>
        /// 保存分类
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveCategory(Blog_ArticleCategory input)
        {
            if (input.Id > 0)
            {
                await _articleBus.UpdateCategoryAsync(input);
            }
            else
            {
                await _articleBus.AddCategoryAsync(input);
            }
        }

        /// <summary>
        /// 删除分类
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteCategory(List<int> ids)
        {
            await _articleBus.DeleteCategoryAsync(ids);
        }

        #endregion
    }
}