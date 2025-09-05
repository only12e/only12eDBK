using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness.Blog_Manage;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Blog_Manage
{
    /// <summary>
    /// 博客数据统计管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客数据统计管理")]
    public class Blog_StatisticsController : BaseApiController
    {
        #region DI

        public Blog_StatisticsController(IBlog_StatisticsBusiness statisticsBus)
        {
            _statisticsBus = statisticsBus;
        }

        IBlog_StatisticsBusiness _statisticsBus { get; }

        #endregion

        #region 数据统计接口

        /// <summary>
        /// 获取数据统计总览
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<BlogStatisticsOverviewDTO> GetOverview()
        {
            return await _statisticsBus.GetOverviewAsync();
        }

        /// <summary>
        /// 获取趋势统计数据
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BlogTrendStatisticsDTO> GetTrendStatistics(BlogTrendStatisticsQueryDTO query)
        {
            return await _statisticsBus.GetTrendStatisticsAsync(query);
        }

        /// <summary>
        /// 获取热门内容统计
        /// </summary>
        /// <param name="count">每类内容返回的数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BlogPopularContentDTO> GetPopularContent(int count = 10)
        {
            return await _statisticsBus.GetPopularContentAsync(count);
        }

        /// <summary>
        /// 获取文章统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ArticleStatistics> GetArticleStatistics()
        {
            return await _statisticsBus.GetArticleStatisticsAsync();
        }

        /// <summary>
        /// 获取项目统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ProjectStatistics> GetProjectStatistics()
        {
            return await _statisticsBus.GetProjectStatisticsAsync();
        }

        /// <summary>
        /// 获取工具统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ToolStatistics> GetToolStatistics()
        {
            return await _statisticsBus.GetToolStatisticsAsync();
        }

        /// <summary>
        /// 获取技术栈统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<TechnologyStatistics> GetTechnologyStatistics()
        {
            return await _statisticsBus.GetTechnologyStatisticsAsync();
        }

        /// <summary>
        /// 获取评论统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<CommentStatistics> GetCommentStatistics()
        {
            return await _statisticsBus.GetCommentStatisticsAsync();
        }

        /// <summary>
        /// 获取用户统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<UserStatistics> GetUserStatistics()
        {
            return await _statisticsBus.GetUserStatisticsAsync();
        }

        /// <summary>
        /// 获取访问统计详情
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<AccessStatistics> GetAccessStatistics()
        {
            return await _statisticsBus.GetAccessStatisticsAsync();
        }

        #endregion
    }
}