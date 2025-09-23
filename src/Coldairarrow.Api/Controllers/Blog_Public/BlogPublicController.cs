using Microsoft.AspNetCore.Mvc;
using Coldairarrow.Business.Blog_Manage;
using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace Coldairarrow.Api.Controllers.Blog_Public
{
    /// <summary>
    /// 博客公共接口控制器 - 统一管理系统和博客系统的数据访问
    /// </summary>
    [Route("/Blog_Public/[controller]/[action]")]
    [ApiController]
    public class BlogPublicController : ControllerBase
    {
        #region DI
        private readonly IBlog_ArticleBusiness _articleBusiness;
        private readonly IBlog_ProjectBusiness _projectBusiness;
        private readonly IBlog_ToolBusiness _toolBusiness;
        private readonly IBlog_TechnologyBusiness _technologyBusiness;
        private readonly IBlog_CommentBusiness _commentBusiness;
        private readonly IBlog_LikeBusiness _likeBusiness;

        public BlogPublicController(
            IBlog_ArticleBusiness articleBusiness,
            IBlog_ProjectBusiness projectBusiness,
            IBlog_ToolBusiness toolBusiness,
            IBlog_TechnologyBusiness technologyBusiness,
            IBlog_CommentBusiness commentBusiness,
            IBlog_LikeBusiness likeBusiness)
        {
            _articleBusiness = articleBusiness;
            _projectBusiness = projectBusiness;
            _toolBusiness = toolBusiness;
            _technologyBusiness = technologyBusiness;
            _commentBusiness = commentBusiness;
            _likeBusiness = likeBusiness;
        }
        #endregion

        #region 文章相关
        [HttpPost]
        public async Task<AjaxResult> GetArticleList(BlogArticleQueryInputDTO input)
        {
            // 只返回已发布的文章
            input.Status = "published";
            var data = await _articleBusiness.GetDataListAsync(input);
            return Success(data);
        }

        [HttpPost]
        public async Task<AjaxResult> GetArticleDetail([FromBody] IdInputDTO input)
        {
            var data = await _articleBusiness.GetTheDataAsync(input.id.ToLong());
            return Success(data);
        }
        #endregion

        #region 项目相关
        [HttpPost]
        public async Task<AjaxResult> GetProjectList(BlogProjectQueryInputDTO input)
        {
            var data = await _projectBusiness.GetDataListAsync(input);
            return Success(data);
        }

        [HttpPost]
        public async Task<AjaxResult> GetProjectDetail([FromBody] IdInputDTO input)
        {
            var data = await _projectBusiness.GetTheDataAsync(input.id.ToLong());
            return Success(data);
        }
        #endregion

        #region 工具相关
        [HttpPost]
        public async Task<AjaxResult> GetToolList(BlogToolQueryInputDTO input)
        {
            var data = await _toolBusiness.GetDataListAsync(input);
            return Success(data);
        }

        [HttpPost]
        public async Task<AjaxResult> GetToolDetail([FromBody] IdInputDTO input)
        {
            var data = await _toolBusiness.GetTheDataAsync(input.id.ToInt());
            return Success(data);
        }
        #endregion

        #region 技术栈相关
        [HttpPost]
        public async Task<AjaxResult> GetTechnologyList(BlogTechnologyQueryInputDTO input)
        {
            var data = await _technologyBusiness.GetDataListAsync(input);
            return Success(data);
        }

        [HttpPost]
        public async Task<AjaxResult> GetTechnologyDetail([FromBody] IdInputDTO input)
        {
            var data = await _technologyBusiness.GetTheDataAsync(input.id.ToInt());
            return Success(data);
        }
        #endregion

        #region 统一的评论和点赞接口
        [HttpPost]
        public async Task<AjaxResult> GetComments(string targetType, long targetId, int pageIndex = 1, int pageSize = 10)
        {
            // 直接使用简单的方法获取评论，先确保基本功能工作
            var comments = await _commentBusiness.GetCommentsByTargetAsync(targetType, targetId, pageSize * pageIndex);
            return Success(comments);
        }

        [HttpPost]
        public async Task<AjaxResult> GetRepliesByParentId(long parentId)
        {
            var replies = await _commentBusiness.GetRepliesByParentIdAsync(parentId);
            return Success(replies);
        }

        [HttpPost]
        public async Task<AjaxResult> PostComment(BlogCommentInputDTO input)
        {
            await _commentBusiness.AddDataAsync(input);
            return Success("评论成功");
        }

        [HttpPost]
        public async Task<AjaxResult> ToggleLike(string targetType, long targetId)
        {
            var result = await _likeBusiness.ToggleLikeAsync("Anonymous", targetType, targetId);
            return Success(result);
        }

        [HttpGet]
        public async Task<AjaxResult> GetLikeCount(string targetType, long targetId)
        {
            var count = await _likeBusiness.GetLikeCountAsync(targetType, targetId);
            return Success(count);
        }
        #endregion

        #region 辅助方法
        private AjaxResult Success(object data = null, string msg = "操作成功")
        {
            return new AjaxResult { Success = true, Data = data, Msg = msg };
        }

        private AjaxResult Error(string msg = "操作失败")
        {
            return new AjaxResult { Success = false, Msg = msg };
        }
        #endregion
    }
}