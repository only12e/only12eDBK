using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Blog_Manage
{
    /// <summary>
    /// 博客数据统计业务实现
    /// </summary>
    public class Blog_StatisticsBusiness : IBlog_StatisticsBusiness, ITransientDependency
    {
        #region DI

        public Blog_StatisticsBusiness(IDbAccessor db)
        {
            _db = db;
        }

        IDbAccessor _db { get; }

        #endregion

        #region 外部接口

        /// <summary>
        /// 获取数据统计总览
        /// </summary>
        /// <returns></returns>
        public async Task<BlogStatisticsOverviewDTO> GetOverviewAsync()
        {
            var overview = new BlogStatisticsOverviewDTO
            {
                Articles = await GetArticleStatisticsAsync(),
                Projects = await GetProjectStatisticsAsync(),
                Tools = await GetToolStatisticsAsync(),
                Technologies = await GetTechnologyStatisticsAsync(),
                Comments = await GetCommentStatisticsAsync(),
                Access = await GetAccessStatisticsAsync()
            };

            return overview;
        }

        /// <summary>
        /// 获取趋势统计数据
        /// </summary>
        /// <param name="query">查询条件</param>
        /// <returns></returns>
        public async Task<BlogTrendStatisticsDTO> GetTrendStatisticsAsync(BlogTrendStatisticsQueryDTO query)
        {
            var result = new BlogTrendStatisticsDTO
            {
                Type = query.Type,
                Period = query.Period,
                DataPoints = new List<TrendDataPoint>()
            };

            // 设置默认时间范围
            var endTime = query.EndTime ?? DateTime.Now.Date.AddDays(1).AddSeconds(-1);
            var startTime = query.StartTime ?? GetDefaultStartTime(query.Period, endTime);

            var dataPoints = await GenerateTrendDataPointsAsync(query.Type, query.Period, startTime, endTime, query.Limit ?? 30);
            result.DataPoints = dataPoints;

            if (dataPoints.Any())
            {
                result.Total = dataPoints.Sum(x => x.Value);
                result.Average = Math.Round((decimal)dataPoints.Average(x => x.Value), 2);
                result.Maximum = dataPoints.Max(x => x.Value);
                result.Minimum = dataPoints.Min(x => x.Value);
            }

            return result;
        }

        /// <summary>
        /// 获取热门内容统计
        /// </summary>
        /// <param name="count">每类内容返回的数量</param>
        /// <returns></returns>
        public async Task<BlogPopularContentDTO> GetPopularContentAsync(int count = 10)
        {
            var result = new BlogPopularContentDTO();

            // 热门文章
            var articles = await _db.GetIQueryable<Blog_Article>()
                .Where(x => x.Status == "published")
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.LikeCount)
                .Take(count)
                .Select(x => new PopularItem
                {
                    Id = x.Id,
                    Title = x.Title,
                    ViewCount = x.ViewCount,
                    LikeCount = x.LikeCount,
                    CommentCount = x.CommentCount,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
            result.PopularArticles = articles;

            // 热门项目
            var projects = await _db.GetIQueryable<Blog_Project>()
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.LikeCount)
                .Take(count)
                .Select(x => new PopularItem
                {
                    Id = x.Id,
                    Title = x.Name,
                    ViewCount = x.ViewCount,
                    LikeCount = x.LikeCount,
                    CommentCount = x.CommentCount,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
            result.PopularProjects = projects;

            // 热门工具
            var tools = await _db.GetIQueryable<Blog_Tool>()
                .OrderByDescending(x => x.Rating)
                .ThenBy(x => x.SortOrder)
                .Take(count)
                .Select(x => new PopularItem
                {
                    Id = x.Id,
                    Title = x.Name,
                    ViewCount = 0, 
                    LikeCount = 0, 
                    CommentCount = 0,
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
            result.PopularTools = tools;

            // 热门技术栈
            var technologies = await _db.GetIQueryable<Blog_Technology>()
                .OrderByDescending(x => x.RecommendationLevel)
                .ThenByDescending(x => x.ProficiencyLevel)
                .Take(count)
                .Select(x => new PopularItem
                {
                    Id = x.Id,
                    Title = x.Name,
                    ViewCount = 0, // 技术栈表暂无浏览数字段
                    LikeCount = 0, // 技术栈表暂无点赞数字段
                    CommentCount = 0, // 技术栈表暂无评论数字段
                    CreatedAt = x.CreatedAt
                })
                .ToListAsync();
            result.PopularTechnologies = technologies;

            return result;
        }

        /// <summary>
        /// 获取文章统计详情
        /// </summary>
        /// <returns></returns>
        public async Task<ArticleStatistics> GetArticleStatisticsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var query = _db.GetIQueryable<Blog_Article>();

            return new ArticleStatistics
            {
                TotalCount = await query.CountAsync(),
                PublishedCount = await query.Where(x => x.Status == "published").CountAsync(),
                DraftCount = await query.Where(x => x.Status == "draft").CountAsync(),
                FeaturedCount = await query.Where(x => x.IsFeatured == 1).CountAsync(),
                TotalViews = await query.SumAsync(x => x.ViewCount),
                TotalLikes = await query.SumAsync(x => x.LikeCount),
                TodayCount = await query.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).CountAsync(),
                MonthCount = await query.Where(x => x.CreatedAt >= monthStart).CountAsync()
            };
        }

        /// <summary>
        /// 获取项目统计详情
        /// </summary>
        /// <returns></returns>
        public async Task<ProjectStatistics> GetProjectStatisticsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var query = _db.GetIQueryable<Blog_Project>();

            return new ProjectStatistics
            {
                TotalCount = await query.CountAsync(),
                ActiveCount = await query.Where(x => x.Status == "active").CountAsync(),
                CompletedCount = await query.Where(x => x.Status == "completed").CountAsync(),
                ArchivedCount = await query.Where(x => x.Status == "archived").CountAsync(),
                FeaturedCount = await query.Where(x => x.IsFeatured == 1).CountAsync(),
                TotalViews = await query.SumAsync(x => x.ViewCount),
                TotalLikes = await query.SumAsync(x => x.LikeCount),
                TodayCount = await query.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).CountAsync(),
                MonthCount = await query.Where(x => x.CreatedAt >= monthStart).CountAsync()
            };
        }

        /// <summary>
        /// 获取工具统计详情
        /// </summary>
        /// <returns></returns>
        public async Task<ToolStatistics> GetToolStatisticsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var query = _db.GetIQueryable<Blog_Tool>();

            return new ToolStatistics
            {
                TotalCount = await query.CountAsync(),
                RecommendedCount = await query.Where(x => x.IsRecommended == 1).CountAsync(),
                FreeCount = await query.Where(x => x.PriceType == "free").CountAsync(),
                PaidCount = await query.Where(x => x.PriceType == "paid").CountAsync(),
                TodayCount = await query.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).CountAsync(),
                MonthCount = await query.Where(x => x.CreatedAt >= monthStart).CountAsync()
            };
        }

        /// <summary>
        /// 获取技术栈统计详情
        /// </summary>
        /// <returns></returns>
        public async Task<TechnologyStatistics> GetTechnologyStatisticsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var query = _db.GetIQueryable<Blog_Technology>();

            var avgProficiency = await query.AverageAsync(x => (decimal?)x.ProficiencyLevel) ?? 0;
            var avgRecommendation = await query.AverageAsync(x => (decimal?)x.RecommendationLevel) ?? 0;

            return new TechnologyStatistics
            {
                TotalCount = await query.CountAsync(),
                FeaturedCount = await query.Where(x => x.IsFeatured == 1).CountAsync(),
                AverageProficiency = Math.Round(avgProficiency, 2),
                AverageRecommendation = Math.Round(avgRecommendation, 2),
                TodayCount = await query.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).CountAsync(),
                MonthCount = await query.Where(x => x.CreatedAt >= monthStart).CountAsync()
            };
        }

        /// <summary>
        /// 获取评论统计详情
        /// </summary>
        /// <returns></returns>
        public async Task<CommentStatistics> GetCommentStatisticsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var monthStart = new DateTime(today.Year, today.Month, 1);

            var query = _db.GetIQueryable<Blog_Comment>();

            return new CommentStatistics
            {
                TotalCount = await query.CountAsync(),
                PendingCount = await query.Where(x => x.Status == "pending").CountAsync(),
                ApprovedCount = await query.Where(x => x.Status == "approved").CountAsync(),
                RejectedCount = await query.Where(x => x.Status == "rejected").CountAsync(),
                TodayCount = await query.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).CountAsync(),
                MonthCount = await query.Where(x => x.CreatedAt >= monthStart).CountAsync()
            };
        }

        /// <summary>
        /// 获取访问统计详情
        /// </summary>
        /// <returns></returns>
        public async Task<AccessStatistics> GetAccessStatisticsAsync()
        {
            var today = DateTime.Today;
            var tomorrow = today.AddDays(1);
            var monthStart = new DateTime(today.Year, today.Month, 1);

            // 文章访问统计
            var articleQuery = _db.GetIQueryable<Blog_Article>();
            var projectQuery = _db.GetIQueryable<Blog_Project>();

            var totalViews = await articleQuery.SumAsync(x => x.ViewCount) + await projectQuery.SumAsync(x => x.ViewCount);
            var totalLikes = await articleQuery.SumAsync(x => x.LikeCount) + await projectQuery.SumAsync(x => x.LikeCount);

            // 今日访问数据：统计今天创建的内容的访问量（实际应该从访问日志表获取）
            var todayViews = await articleQuery.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).SumAsync(x => x.ViewCount) +
                            await projectQuery.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).SumAsync(x => x.ViewCount);
            var todayLikes = await articleQuery.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).SumAsync(x => x.LikeCount) +
                            await projectQuery.Where(x => x.CreatedAt >= today && x.CreatedAt < tomorrow).SumAsync(x => x.LikeCount);

            var monthViews = await articleQuery.Where(x => x.CreatedAt >= monthStart).SumAsync(x => x.ViewCount) +
                            await projectQuery.Where(x => x.CreatedAt >= monthStart).SumAsync(x => x.ViewCount);
            var monthLikes = await articleQuery.Where(x => x.CreatedAt >= monthStart).SumAsync(x => x.LikeCount) +
                            await projectQuery.Where(x => x.CreatedAt >= monthStart).SumAsync(x => x.LikeCount);

            return new AccessStatistics
            {
                TotalViews = totalViews,
                TotalLikes = totalLikes,
                TodayViews = todayViews,
                TodayLikes = todayLikes,
                MonthViews = monthViews,
                MonthLikes = monthLikes
            };
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 根据时间周期获取默认开始时间
        /// </summary>
        /// <param name="period"></param>
        /// <param name="endTime"></param>
        /// <returns></returns>
        private DateTime GetDefaultStartTime(string period, DateTime endTime)
        {
            return period.ToLower() switch
            {
                "day" => endTime.AddDays(-30),
                "week" => endTime.AddDays(-7 * 12), // 12周
                "month" => endTime.AddMonths(-12),
                "year" => endTime.AddYears(-5),
                _ => endTime.AddDays(-30)
            };
        }

        /// <summary>
        /// 生成趋势统计数据点
        /// </summary>
        /// <param name="type"></param>
        /// <param name="period"></param>
        /// <param name="startTime"></param>
        /// <param name="endTime"></param>
        /// <param name="limit"></param>
        /// <returns></returns>
        private async Task<List<TrendDataPoint>> GenerateTrendDataPointsAsync(string type, string period, DateTime startTime, DateTime endTime, int limit)
        {
            var dataPoints = new List<TrendDataPoint>();

            switch (type.ToLower())
            {
                case "article":
                    dataPoints = await GenerateArticleTrendDataAsync(period, startTime, endTime, limit);
                    break;
                case "project":
                    dataPoints = await GenerateProjectTrendDataAsync(period, startTime, endTime, limit);
                    break;
                case "tool":
                    dataPoints = await GenerateToolTrendDataAsync(period, startTime, endTime, limit);
                    break;
                case "technology":
                    dataPoints = await GenerateTechnologyTrendDataAsync(period, startTime, endTime, limit);
                    break;
                case "comment":
                    dataPoints = await GenerateCommentTrendDataAsync(period, startTime, endTime, limit);
                    break;
            }

            return dataPoints.OrderBy(x => x.Time).ToList();
        }

        private async Task<List<TrendDataPoint>> GenerateArticleTrendDataAsync(string period, DateTime startTime, DateTime endTime, int limit)
        {
            var query = _db.GetIQueryable<Blog_Article>()
                .Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime);

            return await GenerateTrendDataByPeriodAsync(query, x => x.CreatedAt, period, startTime, endTime, limit);
        }

        private async Task<List<TrendDataPoint>> GenerateProjectTrendDataAsync(string period, DateTime startTime, DateTime endTime, int limit)
        {
            var query = _db.GetIQueryable<Blog_Project>()
                .Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime);

            return await GenerateTrendDataByPeriodAsync(query, x => x.CreatedAt, period, startTime, endTime, limit);
        }

        private async Task<List<TrendDataPoint>> GenerateToolTrendDataAsync(string period, DateTime startTime, DateTime endTime, int limit)
        {
            var query = _db.GetIQueryable<Blog_Tool>()
                .Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime);

            return await GenerateTrendDataByPeriodAsync(query, x => x.CreatedAt, period, startTime, endTime, limit);
        }

        private async Task<List<TrendDataPoint>> GenerateTechnologyTrendDataAsync(string period, DateTime startTime, DateTime endTime, int limit)
        {
            var query = _db.GetIQueryable<Blog_Technology>()
                .Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime);

            return await GenerateTrendDataByPeriodAsync(query, x => x.CreatedAt, period, startTime, endTime, limit);
        }

        private async Task<List<TrendDataPoint>> GenerateCommentTrendDataAsync(string period, DateTime startTime, DateTime endTime, int limit)
        {
            var query = _db.GetIQueryable<Blog_Comment>()
                .Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime);

            return await GenerateTrendDataByPeriodAsync(query, x => x.CreatedAt, period, startTime, endTime, limit);
        }

        private async Task<List<TrendDataPoint>> GenerateTrendDataByPeriodAsync<T>(
            IQueryable<T> query, 
            System.Func<T, DateTime> dateSelector, 
            string period, 
            DateTime startTime, 
            DateTime endTime, 
            int limit) where T : class
        {
            var dataPoints = new List<TrendDataPoint>();
            var current = startTime;

            while (current <= endTime && dataPoints.Count < limit)
            {
                DateTime periodEnd;
                string label;

                switch (period.ToLower())
                {
                    case "day":
                        periodEnd = current.AddDays(1).AddSeconds(-1);
                        label = current.ToString("MM-dd");
                        break;
                    case "week":
                        periodEnd = current.AddDays(7).AddSeconds(-1);
                        label = $"{current:MM-dd} - {current.AddDays(6):MM-dd}";
                        break;
                    case "month":
                        periodEnd = current.AddMonths(1).AddSeconds(-1);
                        label = current.ToString("yyyy-MM");
                        break;
                    case "year":
                        periodEnd = current.AddYears(1).AddSeconds(-1);
                        label = current.ToString("yyyy");
                        break;
                    default:
                        periodEnd = current.AddDays(1).AddSeconds(-1);
                        label = current.ToString("MM-dd");
                        break;
                }

                // 优化：直接在数据库层面进行时间范围过滤，避免加载所有数据到内存
                var count = await GetCountByDateRangeAsync(query, current, periodEnd);

                dataPoints.Add(new TrendDataPoint
                {
                    Label = label,
                    Time = current,
                    Value = count
                });

                current = period.ToLower() switch
                {
                    "day" => current.AddDays(1),
                    "week" => current.AddDays(7),
                    "month" => current.AddMonths(1),
                    "year" => current.AddYears(1),
                    _ => current.AddDays(1)
                };
            }

            return dataPoints;
        }

        /// <summary>
        /// 根据日期范围获取数据数量（优化版本，直接在数据库层面过滤）
        /// </summary>
        private async Task<int> GetCountByDateRangeAsync<T>(IQueryable<T> query, DateTime startTime, DateTime endTime) where T : class
        {
            // 根据实体类型进行不同的处理
            if (typeof(T) == typeof(Blog_Article))
            {
                var articleQuery = query as IQueryable<Blog_Article>;
                return await articleQuery.Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime).CountAsync();
            }
            else if (typeof(T) == typeof(Blog_Project))
            {
                var projectQuery = query as IQueryable<Blog_Project>;
                return await projectQuery.Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime).CountAsync();
            }
            else if (typeof(T) == typeof(Blog_Tool))
            {
                var toolQuery = query as IQueryable<Blog_Tool>;
                return await toolQuery.Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime).CountAsync();
            }
            else if (typeof(T) == typeof(Blog_Technology))
            {
                var techQuery = query as IQueryable<Blog_Technology>;
                return await techQuery.Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime).CountAsync();
            }
            else if (typeof(T) == typeof(Blog_Comment))
            {
                var commentQuery = query as IQueryable<Blog_Comment>;
                return await commentQuery.Where(x => x.CreatedAt >= startTime && x.CreatedAt <= endTime).CountAsync();
            }

            return 0;
        }

        #endregion
    }
}