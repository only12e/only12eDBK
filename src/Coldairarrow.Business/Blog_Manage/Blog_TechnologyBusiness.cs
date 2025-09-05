using Coldairarrow.Entity.Blog_Manage;
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
    public class Blog_TechnologyBusiness : BaseBusiness<Blog_Technology>, IBlog_TechnologyBusiness, ITransientDependency
    {
        readonly IOperator _operator;

        public Blog_TechnologyBusiness(IDbAccessor db, IOperator @operator)
            : base(db)
        {
            _operator = @operator;
        }

        #region 外部接口

        public async Task<PageResult<Blog_Technology>> GetDataListAsync(BlogTechnologyQueryInputDTO input)
        {
            var q = GetIQueryable();

            // 关键词搜索
            if (!input.Keyword.IsNullOrEmpty())
            {
                q = q.Where(x => x.Name.Contains(input.Keyword) 
                             || x.Description.Contains(input.Keyword) 
                             || x.Content.Contains(input.Keyword));
            }

            // 分类筛选
            if (!input.Category.IsNullOrEmpty())
            {
                q = q.Where(x => x.Category == input.Category);
            }

            // 是否精选筛选
            if (input.IsFeatured.HasValue)
            {
                q = q.Where(x => x.IsFeatured == input.IsFeatured.Value);
            }

            // 最低熟练度筛选
            if (input.MinProficiencyLevel.HasValue)
            {
                q = q.Where(x => x.ProficiencyLevel >= input.MinProficiencyLevel.Value);
            }

            // 最低推荐度筛选
            if (input.MinRecommendationLevel.HasValue)
            {
                q = q.Where(x => x.RecommendationLevel >= input.MinRecommendationLevel.Value);
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

            // 排序
            q = q.OrderByDescending(x => x.SortOrder)
                 .ThenByDescending(x => x.ProficiencyLevel)
                 .ThenByDescending(x => x.CreatedAt);

            return await q.GetPageResultAsync(input);
        }

        public async Task<Blog_Technology> GetTheDataAsync(int id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(BlogTechnologyInputDTO input)
        {
            var newData = new Blog_Technology
            {
                Name = input.Name,
                LogoUrl = input.LogoUrl,
                Description = input.Description,
                Content = input.Content,
                Category = input.Category,
                ProficiencyLevel = input.ProficiencyLevel,
                RecommendationLevel = input.RecommendationLevel,
                IsFeatured = input.IsFeatured,
                SortOrder = input.SortOrder,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await InsertAsync(newData);
        }

        public async Task UpdateDataAsync(BlogTechnologyInputDTO input)
        {
            if (!input.Id.HasValue)
                throw new BusException("技术栈ID不能为空");

            var theData = await GetEntityAsync(input.Id.Value);
            if (theData == null)
                throw new BusException("技术栈不存在");
            
            theData.Name = input.Name;
            theData.LogoUrl = input.LogoUrl;
            theData.Description = input.Description;
            theData.Content = input.Content;
            theData.Category = input.Category;
            theData.ProficiencyLevel = input.ProficiencyLevel;
            theData.RecommendationLevel = input.RecommendationLevel;
            theData.IsFeatured = input.IsFeatured;
            theData.SortOrder = input.SortOrder;
            theData.UpdatedAt = DateTime.Now;
            
            await UpdateAsync(theData);
        }

        public async Task DeleteDataAsync(List<int> ids)
        {
            var stringIds = ids.Select(x => x.ToString()).ToList();
            await DeleteAsync(stringIds);
        }

        public async Task<List<Blog_Technology>> GetFeaturedTechnologiesAsync(int count = 10)
        {
            return await GetIQueryable()
                        .Where(x => x.IsFeatured == 1)
                        .OrderByDescending(x => x.SortOrder)
                        .ThenByDescending(x => x.ProficiencyLevel)
                        .Take(count)
                        .ToListAsync();
        }

        public async Task<List<Blog_Technology>> GetLatestTechnologiesAsync(int count = 10)
        {
            return await GetIQueryable()
                        .OrderByDescending(x => x.CreatedAt)
                        .Take(count)
                        .ToListAsync();
        }

        public async Task<List<string>> GetCategoriesAsync()
        {
            return await GetIQueryable()
                        .Where(x => !string.IsNullOrEmpty(x.Category))
                        .Select(x => x.Category)
                        .Distinct()
                        .OrderBy(x => x)
                        .ToListAsync();
        }

        public async Task<List<Blog_Technology>> GetTechnologiesByCategoryAsync(string category)
        {
            return await GetIQueryable()
                        .Where(x => x.Category == category)
                        .OrderByDescending(x => x.SortOrder)
                        .ThenByDescending(x => x.ProficiencyLevel)
                        .ToListAsync();
        }

        #endregion

        #region 私有成员

        #endregion
    }
}