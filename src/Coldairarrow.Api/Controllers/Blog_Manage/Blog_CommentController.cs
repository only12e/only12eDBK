using Coldairarrow.Entity;
using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Blog_Manage
{
    /// <summary>
    /// 博客评论管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客评论管理")]
    public class Blog_CommentController : BaseApiController
    {
        #region DI

        public Blog_CommentController(IBlog_CommentBusiness commentBus)
        {
            _commentBus = commentBus;
        }

        IBlog_CommentBusiness _commentBus { get; }

        #endregion

        #region 评论管理

        /// <summary>
        /// 获取评论分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Blog_Comment>> GetDataList(BlogCommentQueryInputDTO input)
        {
            return await _commentBus.GetDataListAsync(input);
        }

        /// <summary>
        /// 获取评论详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog_Comment> GetTheData(IdInputDTO input)
        {
            return await _commentBus.GetTheDataAsync(input.id.ToInt());
        }

        /// <summary>
        /// 保存评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiPermission("Blog_Comment.Edit")]
        public async Task SaveData(BlogCommentInputDTO input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                await _commentBus.UpdateDataAsync(input);
            }
            else
            {
                await _commentBus.AddDataAsync(input);
            }
        }

        /// <summary>
        /// 删除评论
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiPermission("Blog_Comment.Delete")]
        public async Task DeleteData(List<int> ids)
        {
            await _commentBus.DeleteDataAsync(ids);
        }

        /// <summary>
        /// 根据目标获取评论列表
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Comment>> GetCommentsByTarget(string targetType, int targetId, int count = 10)
        {
            return await _commentBus.GetCommentsByTargetAsync(targetType, targetId, count);
        }

        /// <summary>
        /// 获取最新评论列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Comment>> GetLatestComments(int count = 10)
        {
            return await _commentBus.GetLatestCommentsAsync(count);
        }

        /// <summary>
        /// 获取待审核评论列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Comment>> GetPendingComments(int count = 10)
        {
            return await _commentBus.GetPendingCommentsAsync(count);
        }

        /// <summary>
        /// 批量审核评论
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        [ApiPermission("Blog_Comment.Audit")]
        public async Task BatchApproval(BlogCommentBatchApprovalInputDTO input)
        {
            if (input.Action == "approve")
            {
                await _commentBus.ApproveCommentsAsync(input.Ids);
            }
            else if (input.Action == "reject")
            {
                await _commentBus.RejectCommentsAsync(input.Ids);
            }
            else
            {
                throw new BusException("无效的操作类型");
            }
        }

        /// <summary>
        /// 获取指定状态的评论数量
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> GetCommentCountByStatus(string status)
        {
            return await _commentBus.GetCommentCountByStatusAsync(status);
        }

        /// <summary>
        /// 获取指定评论的回复列表
        /// </summary>
        /// <param name="parentId">父评论ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_Comment>> GetRepliesByParentId(int parentId)
        {
            return await _commentBus.GetRepliesByParentIdAsync(parentId);
        }

        /// <summary>
        /// 点赞评论
        /// </summary>
        /// <param name="commentId">评论ID</param>
        /// <returns></returns>
        [HttpPost]
        public async Task LikeComment(int commentId)
        {
            await _commentBus.LikeCommentAsync(commentId);
        }

        #endregion
    }
}