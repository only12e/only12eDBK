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
    public class Blog_ProjectBusiness : BaseBusiness<Blog_Project>, IBlog_ProjectBusiness, ITransientDependency
    {
        readonly IOperator _operator;

        public Blog_ProjectBusiness(IDbAccessor db, IOperator @operator)
            : base(db)
        {
            _operator = @operator;
        }

        #region 外部接口

        public async Task<PageResult<Blog_Project>> GetDataListAsync(BlogProjectQueryInputDTO input)
        {
            var q = GetIQueryable();

            // 关键词搜索
            if (!input.Keyword.IsNullOrEmpty())
            {
                q = q.Where(x => x.Name.Contains(input.Keyword) 
                             || x.Description.Contains(input.Keyword) 
                             || x.Content.Contains(input.Keyword));
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

            // 难度级别筛选
            if (!input.DifficultyLevel.IsNullOrEmpty())
            {
                q = q.Where(x => x.DifficultyLevel == input.DifficultyLevel);
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
            q = q.OrderByDescending(x => x.SortOrder).ThenByDescending(x => x.CreatedAt);

            return await q.GetPageResultAsync(input);
        }

        public async Task<Blog_Project> GetTheDataAsync(int id)
        {
            return await GetIQueryable()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        [DataAddLog(UserLogType.博客管理, "Name", "项目")]
        [DataRepeatValidate(new string[] { "Name" }, new string[] { "项目名称" })]
        public async Task AddDataAsync(BlogProjectInputDTO input)
        {
            var project = new Blog_Project
            {
                Name = input.Name,
                Description = input.Description,
                Content = input.Content,
                CoverImage = input.CoverImage,
                DemoUrl = input.DemoUrl,
                GithubUrl = input.GithubUrl,
                WebsiteUrl = input.WebsiteUrl,
                Status = input.Status ?? "active",
                IsFeatured = input.IsFeatured,
                DifficultyLevel = input.DifficultyLevel,
                SortOrder = input.SortOrder,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await InsertAsync(project);
        }

        [DataEditLog(UserLogType.博客管理, "Name", "项目")]
        public async Task UpdateDataAsync(BlogProjectInputDTO input)
        {
            if (!input.Id.HasValue)
                throw new BusException("项目ID不能为空");

            var project = await GetEntityAsync(input.Id.Value);
            if (project == null)
                throw new BusException("项目不存在");

            project.Name = input.Name;
            project.Description = input.Description;
            project.Content = input.Content;
            project.CoverImage = input.CoverImage;
            project.DemoUrl = input.DemoUrl;
            project.GithubUrl = input.GithubUrl;
            project.WebsiteUrl = input.WebsiteUrl;
            project.Status = input.Status ?? project.Status;
            project.IsFeatured = input.IsFeatured;
            project.DifficultyLevel = input.DifficultyLevel;
            project.SortOrder = input.SortOrder;
            project.UpdatedAt = DateTime.Now;

            await UpdateAsync(project);
        }

        [DataDeleteLog(UserLogType.博客管理, "Name", "项目")]  
        public async Task DeleteDataAsync(List<string> ids)
        {
            await DeleteAsync(ids);
        }

        // 为前端提供的int类型ID删除方法
        public async Task DeleteDataAsync(List<int> ids)
        {
            var stringIds = ids.Select(x => x.ToString()).ToList();
            await DeleteDataAsync(stringIds);
        }

        public async Task<List<Blog_Project>> GetFeaturedProjectsAsync(int count = 10)
        {
            return await GetIQueryable()
                .Where(x => x.Status == "active" && x.IsFeatured == 1)
                .OrderByDescending(x => x.ViewCount)
                .ThenByDescending(x => x.LikeCount)
                .Take(count)
                .ToListAsync();
        }

        public async Task<List<Blog_Project>> GetLatestProjectsAsync(int count = 10)
        {
            return await GetIQueryable()
                .Where(x => x.Status == "active")
                .OrderByDescending(x => x.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        public async Task IncrementViewCountAsync(int id)
        {
            var project = await GetEntityAsync(id);
            if (project != null)
            {
                project.ViewCount++;
                project.UpdatedAt = DateTime.Now;
                await UpdateAsync(project);
            }
        }

        #endregion
    }
}