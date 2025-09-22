using Coldairarrow.Business;
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
    public class Blog_LikeBusiness : BaseBusiness<Blog_Like>, IBlog_LikeBusiness, ITransientDependency
    {
        private readonly IBlog_ArticleBusiness _articleBusiness;
        private readonly IBlog_ProjectBusiness _projectBusiness;
        private readonly IBlog_CommentBusiness _commentBusiness;

        public Blog_LikeBusiness(IDbAccessor db,
            IBlog_ArticleBusiness articleBusiness,
            IBlog_ProjectBusiness projectBusiness,
            IBlog_CommentBusiness commentBusiness)
            : base(db)
        {
            _articleBusiness = articleBusiness;
            _projectBusiness = projectBusiness;
            _commentBusiness = commentBusiness;
        }

        public async Task<LikeStatusResult> ToggleLikeAsync(string userId, string targetType, long targetId)
        {
            if (string.IsNullOrEmpty(userId))
                throw new BusException("用户ID不能为空");

            if (string.IsNullOrEmpty(targetType))
                throw new BusException("目标类型不能为空");

            // 检查目标是否存在
            await ValidateTargetExistsAsync(targetType, targetId);

            // 查找现有点赞记录
            var existingLike = await GetIQueryable()
                .Where(x => x.UserId == userId && x.TargetType == targetType && x.TargetId == targetId)
                .FirstOrDefaultAsync();

            bool isLiked;
            string message;

            if (existingLike != null)
            {
                // 取消点赞
                await DeleteAsync(existingLike.Id.ToString());
                // 直接更新目标对象的点赞数量
                await UpdateTargetLikeCountAsync(targetType, targetId, -1);
                isLiked = false;
                message = "取消点赞成功";
            }
            else
            {
                // 添加点赞
                var newLike = new Blog_Like
                {
                    UserId = userId,
                    TargetType = targetType,
                    TargetId = targetId,
                    CreatedAt = DateTime.Now
                };
                await InsertAsync(newLike);
                // 直接更新目标对象的点赞数量
                await UpdateTargetLikeCountAsync(targetType, targetId, 1);
                isLiked = true;
                message = "点赞成功";
            }

            // 获取当前点赞总数
            var totalLikes = await GetLikeCountAsync(targetType, targetId);

            return new LikeStatusResult
            {
                IsLiked = isLiked,
                TotalLikes = totalLikes,
                Message = message
            };
        }

        public async Task<bool> IsLikedAsync(string userId, string targetType, long targetId)
        {
            if (string.IsNullOrEmpty(userId))
                return false;

            return await GetIQueryable()
                .AnyAsync(x => x.UserId == userId && x.TargetType == targetType && x.TargetId == targetId);
        }

        public async Task<int> GetLikeCountAsync(string targetType, long targetId)
        {
            return await GetIQueryable()
                .Where(x => x.TargetType == targetType && x.TargetId == targetId)
                .CountAsync();
        }

        public async Task<PageResult<Blog_Like>> GetUserLikesAsync(string userId, string targetType = null, int pageSize = 20, int pageIndex = 1)
        {
            var q = GetIQueryable()
                .Where(x => x.UserId == userId);

            if (!string.IsNullOrEmpty(targetType))
            {
                q = q.Where(x => x.TargetType == targetType);
            }

            var orderedQ = q.Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt);

            var input = new PageInput
            {
                PageRows = pageSize,
                PageIndex = pageIndex
            };

            return await orderedQ.GetPageResultAsync(input);
        }

        public async Task<PageResult<Blog_Like>> GetTargetLikesAsync(string targetType, long targetId, int pageSize = 20, int pageIndex = 1)
        {
            var orderedQ = GetIQueryable()
                .Where(x => x.TargetType == targetType && x.TargetId == targetId)
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt);

            var input = new PageInput
            {
                PageRows = pageSize,
                PageIndex = pageIndex
            };

            return await orderedQ.GetPageResultAsync(input);
        }

        public async Task<Dictionary<string, bool>> BatchCheckLikeStatusAsync(string userId, List<LikeTarget> targets)
        {
            if (string.IsNullOrEmpty(userId) || targets == null || !targets.Any())
                return new Dictionary<string, bool>();

            var result = new Dictionary<string, bool>();

            // 为了避免复杂的查询，这里采用分组查询的方式
            var groupedTargets = targets.GroupBy(x => x.TargetType);

            foreach (var group in groupedTargets)
            {
                var targetType = group.Key;
                var targetIds = group.Select(x => x.TargetId).ToList();

                var likedTargets = await GetIQueryable()
                    .Where(x => x.UserId == userId && x.TargetType == targetType && targetIds.Contains(x.TargetId))
                    .Select(x => x.TargetId)
                    .ToListAsync();

                foreach (var target in group)
                {
                    result[target.Key] = likedTargets.Contains(target.TargetId);
                }
            }

            return result;
        }

        public async Task<List<Blog_Like>> GetRecentLikesAsync(int count = 10)
        {
            return await GetIQueryable()
                .Include(x => x.User)
                .OrderByDescending(x => x.CreatedAt)
                .Take(count)
                .ToListAsync();
        }

        #region 私有方法

        /// <summary>
        /// 验证目标是否存在
        /// </summary>
        private async Task ValidateTargetExistsAsync(string targetType, long targetId)
        {
            switch (targetType.ToLower())
            {
                case "article":
                    var article = await _articleBusiness.GetTheDataAsync(targetId);
                    if (article == null)
                        throw new BusException("文章不存在");
                    break;
                case "project":
                    var project = await _projectBusiness.GetTheDataAsync(targetId);
                    if (project == null)
                        throw new BusException("项目不存在");
                    break;
                case "comment":
                    var comment = await _commentBusiness.GetTheDataAsync(targetId);
                    if (comment == null)
                        throw new BusException("评论不存在");
                    break;
                default:
                    throw new BusException($"不支持的目标类型: {targetType}");
            }
        }

        /// <summary>
        /// 更新目标对象的点赞数量
        /// </summary>
        private async Task UpdateTargetLikeCountAsync(string targetType, long targetId, int increment)
        {
            switch (targetType.ToLower())
            {
                case "article":
                    var article = await _articleBusiness.GetTheDataAsync(targetId);
                    if (article != null)
                    {
                        article.LikeCount = Math.Max(0, article.LikeCount + increment);
                        article.UpdatedAt = DateTime.Now;
                        await Db.UpdateAsync(article);
                    }
                    break;
                case "project":
                    var project = await _projectBusiness.GetTheDataAsync(targetId);
                    if (project != null)
                    {
                        project.LikeCount = Math.Max(0, project.LikeCount + increment);
                        project.UpdatedAt = DateTime.Now;
                        await Db.UpdateAsync(project);
                    }
                    break;
                case "comment":
                    var comment = await _commentBusiness.GetTheDataAsync(targetId);
                    if (comment != null)
                    {
                        comment.LikeCount = Math.Max(0, comment.LikeCount + increment);
                        comment.UpdatedAt = DateTime.Now;
                        await Db.UpdateAsync(comment);
                    }
                    break;
            }
        }

        #endregion
    }
}