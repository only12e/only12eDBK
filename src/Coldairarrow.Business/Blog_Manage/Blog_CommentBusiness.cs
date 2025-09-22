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
    public class Blog_CommentBusiness : BaseBusiness<Blog_Comment>, IBlog_CommentBusiness, ITransientDependency
    {
        readonly IOperator _operator;

        public Blog_CommentBusiness(IDbAccessor db, IOperator @operator)
            : base(db)
        {
            _operator = @operator;
        }

        #region 外部接口

        public async Task<PageResult<Blog_Comment>> GetDataListAsync(BlogCommentQueryInputDTO input)
        {
            var q = GetIQueryable();

            // 关键词搜索
            if (!input.Keyword.IsNullOrEmpty())
            {
                q = q.Where(x => x.Content.Contains(input.Keyword));
            }

            // 目标类型筛选
            if (!input.TargetType.IsNullOrEmpty())
            {
                q = q.Where(x => x.TargetType == input.TargetType);
            }

            // 目标ID筛选
            if (input.TargetId.HasValue)
            {
                q = q.Where(x => x.TargetId == input.TargetId.Value);
            }

            // 审核状态筛选
            if (!input.Status.IsNullOrEmpty())
            {
                q = q.Where(x => x.Status == input.Status);
            }

            // 用户ID筛选
            if (input.UserId.HasValue)
            {
                // 将int类型的UserId转换为string进行比较（临时方案，实际应该修改前端传递string类型）
                var userIdString = input.UserId.Value.ToString();
                q = q.Where(x => x.UserId == userIdString);
            }

            // 是否只查询顶级评论
            if (input.IsTopLevel.HasValue)
            {
                if (input.IsTopLevel.Value)
                {
                    q = q.Where(x => x.ParentId == null);
                }
                else
                {
                    q = q.Where(x => x.ParentId != null);
                }
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
            q = q.Include(x => x.User).Include(x => x.ParentComment);

            // 排序
            q = q.OrderByDescending(x => x.CreatedAt);

            var result = await q.GetPageResultAsync(input);

            // 为顶级评论计算回复数量
            var topLevelComments = result.Data.Where(x => x.ParentId == null).ToList();
            if (topLevelComments.Any())
            {
                var commentIds = topLevelComments.Select(x => x.Id).ToList();
                var replyCounts = await GetIQueryable()
                    .Where(x => x.ParentId.HasValue && commentIds.Contains(x.ParentId.Value) && x.Status == "approved")
                    .GroupBy(x => x.ParentId)
                    .Select(g => new { ParentId = g.Key, Count = g.Count() })
                    .ToListAsync();

                foreach (var comment in topLevelComments)
                {
                    var replyCountInfo = replyCounts.FirstOrDefault(x => x.ParentId == comment.Id);
                    comment.ReplyCount = replyCountInfo?.Count ?? 0;
                }
            }

            return result;
        }

        public async Task<Blog_Comment> GetTheDataAsync(long id)
        {
            return await GetIQueryable()
                .Include(x => x.User)
                .Include(x => x.ParentComment)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task AddDataAsync(BlogCommentInputDTO input)
        {
            var newData = new Blog_Comment
            {
                Content = input.Content,
                UserId = input.UserId,
                TargetType = input.TargetType,
                TargetId = input.TargetId,
                ParentId = input.ParentId,
                Status = input.Status ?? "approved", // 默认直接通过，不需要审核
                IpAddress = input.IpAddress,
                UserAgent = input.UserAgent,
                LikeCount = 0,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await InsertAsync(newData);
        }

        public async Task UpdateDataAsync(BlogCommentInputDTO input)
        {
            if (!input.Id.HasValue)
                throw new BusException("评论ID不能为空");

            var theData = await GetEntityAsync(input.Id.Value);
            if (theData == null)
                throw new BusException("评论不存在");
            
            theData.Content = input.Content;
            theData.Status = input.Status ?? theData.Status;
            theData.UpdatedAt = DateTime.Now;
            
            await UpdateAsync(theData);
        }

        public async Task DeleteDataAsync(List<long> ids)
        {
            var stringIds = ids.Select(x => x.ToString()).ToList();
            await DeleteAsync(stringIds);
        }

        public async Task<List<Blog_Comment>> GetCommentsByTargetAsync(string targetType, long targetId, int count = 10)
        {
            return await GetIQueryable()
                        .Where(x => x.TargetType == targetType && x.TargetId == targetId && x.Status == "approved")
                        .Include(x => x.User)
                        .Include(x => x.ParentComment)
                        .OrderByDescending(x => x.CreatedAt)
                        .Take(count)
                        .ToListAsync();
        }

        public async Task<List<Blog_Comment>> GetLatestCommentsAsync(int count = 10)
        {
            return await GetIQueryable()
                        .Where(x => x.Status == "approved")
                        .Include(x => x.User)
                        .OrderByDescending(x => x.CreatedAt)
                        .Take(count)
                        .ToListAsync();
        }

        public async Task<List<Blog_Comment>> GetPendingCommentsAsync(int count = 10)
        {
            return await GetIQueryable()
                        .Where(x => x.Status == "pending")
                        .Include(x => x.User)
                        .OrderByDescending(x => x.CreatedAt)
                        .Take(count)
                        .ToListAsync();
        }

        public async Task ApproveCommentsAsync(List<long> ids)
        {
            var comments = await GetIQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            foreach (var comment in comments)
            {
                comment.Status = "approved";
                comment.UpdatedAt = DateTime.Now;
            }

            await UpdateAsync(comments);
        }

        public async Task RejectCommentsAsync(List<long> ids)
        {
            var comments = await GetIQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            foreach (var comment in comments)
            {
                comment.Status = "rejected";
                comment.UpdatedAt = DateTime.Now;
            }

            await UpdateAsync(comments);
        }

        public async Task<int> GetCommentCountByStatusAsync(string status)
        {
            return await GetIQueryable()
                        .Where(x => x.Status == status)
                        .CountAsync();
        }

        public async Task<List<Blog_Comment>> GetRepliesByParentIdAsync(long parentId)
        {
            return await GetIQueryable()
                        .Where(x => x.ParentId == parentId && x.Status == "approved")
                        .Include(x => x.User)
                        .OrderBy(x => x.CreatedAt)
                        .ToListAsync();
        }


        public async Task SetCommentsPendingAsync(List<long> ids)
        {
            var comments = await GetIQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            foreach (var comment in comments)
            {
                comment.Status = "pending";
                comment.UpdatedAt = DateTime.Now;
            }

            await UpdateAsync(comments);
        }

        public async Task<int> GetRepliesCountAsync(long parentId)
        {
            return await GetIQueryable()
                        .Where(x => x.ParentId == parentId && x.Status == "approved")
                        .CountAsync();
        }

        #endregion

        #region 私有成员

        #endregion
    }
}