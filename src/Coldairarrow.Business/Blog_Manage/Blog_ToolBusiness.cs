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
    public class Blog_ToolBusiness : BaseBusiness<Blog_Tool>, IBlog_ToolBusiness, ITransientDependency
    {
        readonly IOperator _operator;

        public Blog_ToolBusiness(IDbAccessor db, IOperator @operator)
            : base(db)
        {
            _operator = @operator;
        }

        #region 外部接口

        public async Task<PageResult<Blog_Tool>> GetDataListAsync(BlogToolQueryInputDTO input)
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

            // 价格类型筛选
            if (!input.PriceType.IsNullOrEmpty())
            {
                q = q.Where(x => x.PriceType == input.PriceType);
            }

            // 是否推荐筛选
            if (input.IsRecommended.HasValue)
            {
                q = q.Where(x => x.IsRecommended == input.IsRecommended.Value);
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
                 .ThenByDescending(x => x.CreatedAt);

            return await q.GetPageResultAsync(input);
        }

        public async Task<Blog_Tool> GetTheDataAsync(int id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(BlogToolInputDTO input)
        {
            var newData = new Blog_Tool
            {
                Name = input.Name,
                Description = input.Description,
                Content = input.Content,
                CoverImage = input.CoverImage,
                IconUrl = input.IconUrl,
                OfficialUrl = input.OfficialUrl,
                DownloadUrl = input.DownloadUrl,
                Category = input.Category,
                PriceType = input.PriceType ?? "free",
                Rating = input.Rating,
                IsRecommended = input.IsRecommended,
                SortOrder = input.SortOrder,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await InsertAsync(newData);
        }

        public async Task UpdateDataAsync(BlogToolInputDTO input)
        {
            if (!input.Id.HasValue)
                throw new BusException("工具ID不能为空");

            var theData = await GetEntityAsync(input.Id.Value);
            if (theData == null)
                throw new BusException("工具不存在");
            
            theData.Name = input.Name;
            theData.Description = input.Description;
            theData.Content = input.Content;
            theData.CoverImage = input.CoverImage;
            theData.IconUrl = input.IconUrl;
            theData.OfficialUrl = input.OfficialUrl;
            theData.DownloadUrl = input.DownloadUrl;
            theData.Category = input.Category;
            theData.PriceType = input.PriceType ?? theData.PriceType;
            theData.Rating = input.Rating;
            theData.IsRecommended = input.IsRecommended;
            theData.SortOrder = input.SortOrder;
            theData.UpdatedAt = DateTime.Now;
            
            await UpdateAsync(theData);
        }

        public async Task DeleteDataAsync(List<int> ids)
        {
            var stringIds = ids.Select(x => x.ToString()).ToList();
            await DeleteAsync(stringIds);
        }

        public async Task<List<Blog_Tool>> GetRecommendedToolsAsync(int count = 10)
        {
            return await GetIQueryable()
                        .Where(x => x.IsRecommended == 1)
                        .OrderByDescending(x => x.SortOrder)
                        .ThenByDescending(x => x.Rating)
                        .Take(count)
                        .ToListAsync();
        }

        public async Task<List<Blog_Tool>> GetLatestToolsAsync(int count = 10)
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

        #endregion

        #region 私有成员

        #endregion
    }
}