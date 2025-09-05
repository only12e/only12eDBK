using Coldairarrow.Entity.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    /// <summary>
    /// 博客数据统计业务接口
    /// </summary>
    public interface IBlog_StatisticsBusiness
    {
        /// <summary>
        /// 获取数据统计总览
        /// </summary>
        /// <returns></returns>
        Task<BlogStatisticsOverviewDTO> GetOverviewAsync();

        /// <summary>
        /// 获取趋势统计数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        Task<BlogTrendStatisticsDTO> GetTrendStatisticsAsync(BlogTrendStatisticsQueryDTO query);

        /// <summary>
        /// 获取热门内容统计
        /// </summary>
        /// <param name="count">每类内容返回的数量</param>
        /// <returns></returns>
        Task<BlogPopularContentDTO> GetPopularContentAsync(int count = 10);

        /// <summary>
        /// 获取文章统计详情
        /// </summary>
        /// <returns></returns>
        Task<ArticleStatistics> GetArticleStatisticsAsync();

        /// <summary>
        /// 获取项目统计详情
        /// </summary>
        /// <returns></returns>
        Task<ProjectStatistics> GetProjectStatisticsAsync();

        /// <summary>
        /// 获取工具统计详情
        /// </summary>
        /// <returns></returns>
        Task<ToolStatistics> GetToolStatisticsAsync();

        /// <summary>
        /// 获取技术栈统计详情
        /// </summary>
        /// <returns></returns>
        Task<TechnologyStatistics> GetTechnologyStatisticsAsync();

        /// <summary>
        /// 获取评论统计详情
        /// </summary>
        /// <returns></returns>
        Task<CommentStatistics> GetCommentStatisticsAsync();

        /// <summary>
        /// 获取用户统计详情
        /// </summary>
        /// <returns></returns>
        Task<UserStatistics> GetUserStatisticsAsync();

        /// <summary>
        /// 获取访问统计详情
        /// </summary>
        /// <returns></returns>
        Task<AccessStatistics> GetAccessStatisticsAsync();
    }
}