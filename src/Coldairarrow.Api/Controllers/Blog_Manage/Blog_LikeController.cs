using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.IBusiness;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace Coldairarrow.Api.Controllers.Blog_Manage
{
    /// <summary>
    /// 博客点赞管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    public class Blog_LikeController : BaseApiController
    {
        #region DI

        public Blog_LikeController(IBlog_LikeBusiness blog_LikeBusiness, IOperator @operator)
        {
            _blog_LikeBusiness = blog_LikeBusiness;
            _operator = @operator;
        }

        IBlog_LikeBusiness _blog_LikeBusiness { get; }
        IOperator _operator { get; }

        #endregion

        #region 获取

        /// <summary>
        /// 获取用户点赞列表
        /// </summary>
        /// <param name="targetType">目标类型（可选）</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> GetUserLikes(string targetType = null, int pageSize = 20, int pageIndex = 1)
        {
            var userId = _operator.UserId;
            if (string.IsNullOrEmpty(userId))
                return Error("用户未登录");

            var result = await _blog_LikeBusiness.GetUserLikesAsync(userId, targetType, pageSize, pageIndex);

            // 转换为匿名对象以减少数据传输
            var data = result.Data.Select(x => new
            {
                x.Id,
                x.TargetType,
                x.TargetId,
                x.CreatedAt,
                User = new
                {
                    x.User?.Id,
                    x.User?.UserName,
                    x.User?.RealName
                }
            }).ToList();

            return Success(new PageResult<object>
            {
                Data = data.Cast<object>().ToList(),
                Total = result.Total
            });
        }

        /// <summary>
        /// 获取目标的点赞用户列表
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<AjaxResult> GetTargetLikes(string targetType, long targetId, int pageSize = 20, int pageIndex = 1)
        {
            if (string.IsNullOrEmpty(targetType) || targetId <= 0)
                return Error("参数无效");

            var result = await _blog_LikeBusiness.GetTargetLikesAsync(targetType, targetId, pageSize, pageIndex);

            // 转换为匿名对象
            var data = result.Data.Select(x => new
            {
                x.Id,
                x.CreatedAt,
                User = new
                {
                    x.User?.Id,
                    x.User?.UserName,
                    x.User?.RealName
                }
            }).ToList();

            return Success(new PageResult<object>
            {
                Data = data.Cast<object>().ToList(),
                Total = result.Total
            });
        }

        /// <summary>
        /// 检查用户是否已点赞
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> IsLiked(string targetType, long targetId)
        {
            var userId = _operator.UserId;
            if (string.IsNullOrEmpty(userId))
                return false;

            return await _blog_LikeBusiness.IsLikedAsync(userId, targetType, targetId);
        }

        /// <summary>
        /// 获取目标点赞数量
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> GetLikeCount(string targetType, long targetId)
        {
            return await _blog_LikeBusiness.GetLikeCountAsync(targetType, targetId);
        }

        /// <summary>
        /// 批量检查点赞状态
        /// </summary>
        /// <param name="targets">目标列表</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Dictionary<string, bool>> BatchCheckLikeStatus([FromBody] List<LikeTargetDto> targets)
        {
            var userId = _operator.UserId;
            if (string.IsNullOrEmpty(userId))
                return new Dictionary<string, bool>();

            var businessTargets = targets.Select(t => new LikeTarget
            {
                TargetType = t.TargetType,
                TargetId = t.TargetId
            }).ToList();

            return await _blog_LikeBusiness.BatchCheckLikeStatusAsync(userId, businessTargets);
        }

        /// <summary>
        /// 获取最近点赞记录
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<object>> GetRecentLikes(int count = 10)
        {
            var likes = await _blog_LikeBusiness.GetRecentLikesAsync(count);

            return likes.Select(x => new
            {
                x.Id,
                x.TargetType,
                x.TargetId,
                x.CreatedAt,
                User = new
                {
                    x.User?.Id,
                    x.User?.UserName,
                    x.User?.RealName
                }
            }).ToList<object>();
        }

        #endregion

        #region 提交

        /// <summary>
        /// 切换点赞状态
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<AjaxResult> ToggleLike(string targetType, long targetId)
        {
            try
            {
                var userId = _operator.UserId;
                if (string.IsNullOrEmpty(userId))
                    return Error("用户未登录");

                if (string.IsNullOrEmpty(targetType) || targetId <= 0)
                    return Error("参数无效");

                var result = await _blog_LikeBusiness.ToggleLikeAsync(userId, targetType, targetId);
                return Success(result, result.Message);
            }
            catch (Exception ex)
            {
                return Error(ex.Message);
            }
        }

        #endregion
    }

    /// <summary>
    /// 点赞目标DTO
    /// </summary>
    public class LikeTargetDto
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 目标ID
        /// </summary>
        public long TargetId { get; set; }
    }
}