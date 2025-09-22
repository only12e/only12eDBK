using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.Base_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Blog_Manage
{
    public class Blog_ArticleBusiness : BaseBusiness<Blog_Article>, IBlog_ArticleBusiness, ITransientDependency
    {
        readonly IOperator _operator;

        public Blog_ArticleBusiness(IDbAccessor db, IOperator @operator)
            : base(db)
        {
            _operator = @operator;
        }

        #region 外部接口

        public async Task<PageResult<Blog_Article>> GetDataListAsync(BlogArticleQueryInputDTO input)
        {
            var q = GetIQueryable();

            // 关键词搜索
            if (!input.Keyword.IsNullOrEmpty())
            {
                q = q.Where(x => x.Title.Contains(input.Keyword) 
                             || x.Summary.Contains(input.Keyword) 
                             || x.Content.Contains(input.Keyword));
            }

            // 分类筛选
            if (input.CategoryId.HasValue)
            {
                q = q.Where(x => x.CategoryId == input.CategoryId.Value);
            }

            // 状态筛选
            if (!input.Status.IsNullOrEmpty())
            {
                q = q.Where(x => x.Status == input.Status);
            }

            // 精选筛选
            if (input.IsFeatured.HasValue)
            {
                q = q.Where(x => x.IsFeatured == input.IsFeatured.Value);
            }

            // 时间范围筛选
            if (input.StartTime.HasValue)
            {
                q = q.Where(x => x.CreatedAt >= input.StartTime.Value);
            }
            if (input.EndTime.HasValue)
            {
                q = q.Where(x => x.CreatedAt <= input.EndTime.Value);
            }

            // 包含关联数据
            q = q.Include(x => x.Category).Include(x => x.Author);

            // 排序
            q = q.OrderByDescending(x => x.CreatedAt);

            return await q.GetPageResultAsync(input);
        }

        public async Task<Blog_Article> GetTheDataAsync(long id)
        {
            return await GetIQueryable()
                .Include(x => x.Category)
                .Include(x => x.Author)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        [DataAddLog(UserLogType.博客管理, "Title", "文章")]
        [DataRepeatValidate(new string[] { "Title" }, new string[] { "文章标题" })]
        public async Task AddDataAsync(BlogArticleInputDTO input)
        {
            // 获取当前用户ID
            var authorId = string.IsNullOrEmpty(_operator.UserId) ? "Admin" : _operator.UserId;

            var article = new Blog_Article
            {
                Title = input.Title,
                Summary = input.Summary,
                Content = input.Content,
                CoverImage = input.CoverImage,
                CategoryId = input.CategoryId,
                AuthorId = authorId,
                Status = input.Status ?? "draft",
                IsFeatured = input.IsFeatured,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            if (input.Status == "published")
            {
                article.PublishedAt = DateTime.Now;
            }

            await InsertAsync(article);
        }

        [DataEditLog(UserLogType.博客管理, "Title", "文章")]
        public async Task UpdateDataAsync(BlogArticleInputDTO input)
        {
            if (!input.Id.HasValue)
                throw new BusException("文章ID不能为空");

            var article = await GetEntityAsync(input.Id.Value);
            if (article == null)
                throw new BusException("文章不存在");

            article.Title = input.Title;
            article.Summary = input.Summary;
            article.Content = input.Content;
            article.CoverImage = input.CoverImage;
            article.CategoryId = input.CategoryId;
            article.Status = input.Status ?? article.Status;
            article.IsFeatured = input.IsFeatured;
            article.UpdatedAt = DateTime.Now;

            // 如果从草稿变为发布状态，设置发布时间
            if (article.Status != "published" && input.Status == "published")
            {
                article.PublishedAt = DateTime.Now;
            }

            await UpdateAsync(article);
        }

        [DataDeleteLog(UserLogType.博客管理, "Title", "文章")]  
        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        // 为前端提供的long类型ID删除方法
        public async Task DeleteDataAsync(List<long> ids)
        {
            var stringIds = ids.Select(x => x.ToString()).ToList();
            await DeleteAsync(stringIds);
        }

        public async Task PublishDataAsync(BlogArticlePublishInputDTO input)
        {
            var articleIds = input.Ids.ToList(); // 确保列表被具体化
            var articles = await GetIQueryable().Where(x => articleIds.Contains(x.Id)).ToListAsync();
            
            foreach (var article in articles)
            {
                article.Status = input.Status;
                article.UpdatedAt = DateTime.Now;

                if (input.Status == "published" && article.PublishedAt == null)
                {
                    article.PublishedAt = DateTime.Now;
                }
            }

            await UpdateAsync(articles);
        }

        public async Task<List<Blog_Article>> GetFeaturedArticlesAsync(int count = 10)
        {
            return await GetIQueryable()
                .Where(x => x.Status == "published" && x.IsFeatured == 1)
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.LikeCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Blog_Article>> GetLatestArticlesAsync(int count = 10)
        {
            return await GetIQueryable()
                .Where(x => x.Status == "published")
                .OrderByDescending(x => x.PublishedAt ?? x.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task IncrementViewCountAsync(long id)
        {
            var article = await GetEntityAsync(id);
            if (article != null)
            {
                article.ViewCount++;
                article.UpdatedAt = DateTime.Now;
                await UpdateAsync(article);
            }
        }


        #endregion

        #region 文章分类管理

        public async Task<List<Blog_ArticleCategory>> GetCategoriesAsync()
        {
            return await Db.GetIQueryable<Blog_ArticleCategory>()
                .OrderByDescending(x => x.SortOrder)
                .ThenBy(x => x.Id)
                .ToListAsync();
        }

        public async Task AddCategoryAsync(Blog_ArticleCategory category)
        {
            category.CreatedAt = DateTime.Now;
            await Db.InsertAsync(category);
        }

        public async Task UpdateCategoryAsync(Blog_ArticleCategory category)
        {
            await Db.UpdateAsync(category);
        }

        public async Task DeleteCategoryAsync(List<long> ids)
        {
            // 检查是否有文章使用了这些分类
            var categoryIds = ids.ToList(); // 确保列表被具体化
            var hasArticles = await GetIQueryable()
                .AnyAsync(x => x.CategoryId.HasValue && categoryIds.Contains(x.CategoryId.Value));

            if (hasArticles)
                throw new BusException("存在文章使用了该分类，无法删除");

            await Db.DeleteAsync<Blog_ArticleCategory>(ids.Select(x => x.ToString()).ToList());
        }

        #endregion

        #region 私有成员

        #endregion
    }
}