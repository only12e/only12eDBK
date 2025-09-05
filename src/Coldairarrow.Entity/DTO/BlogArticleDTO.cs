using Coldairarrow.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Coldairarrow.Entity.DTO
{
    /// <summary>
    /// 文章管理输入DTO
    /// </summary>
    public class BlogArticleInputDTO
    {
        /// <summary>
        /// 文章ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [Required(ErrorMessage = "文章标题不能为空")]
        [StringLength(200, ErrorMessage = "文章标题长度不能超过200个字符")]
        public string Title { get; set; }

        /// <summary>
        /// 文章摘要
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 文章内容
        /// </summary>
        [Required(ErrorMessage = "文章内容不能为空")]
        public string Content { get; set; }

        /// <summary>
        /// 封面图片URL
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// 文章状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 是否精选
        /// </summary>
        public int IsFeatured { get; set; }
    }

    /// <summary>
    /// 文章查询输入DTO
    /// </summary>
    public class BlogArticleQueryInputDTO : PageInput
    {
        /// <summary>
        /// 关键词搜索
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 分类ID
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// 状态筛选
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 是否精选
        /// </summary>
        public int? IsFeatured { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 文章发布输入DTO
    /// </summary>
    public class BlogArticlePublishInputDTO
    {
        /// <summary>
        /// 文章ID列表
        /// </summary>
        [Required(ErrorMessage = "文章ID不能为空")]
        public List<int> Ids { get; set; }

        /// <summary>
        /// 发布状态：published发布/draft草稿/hidden隐藏
        /// </summary>
        [Required(ErrorMessage = "发布状态不能为空")]
        public string Status { get; set; }
    }

    /// <summary>
    /// 项目管理输入DTO
    /// </summary>
    public class BlogProjectInputDTO
    {
        /// <summary>
        /// 项目ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required(ErrorMessage = "项目名称不能为空")]
        [StringLength(100, ErrorMessage = "项目名称长度不能超过100个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 项目简介描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 项目详细说明
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 项目封面图片URL
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// 项目演示地址
        /// </summary>
        public string DemoUrl { get; set; }

        /// <summary>
        /// GitHub仓库地址
        /// </summary>
        public string GithubUrl { get; set; }

        /// <summary>
        /// 其他相关网站地址
        /// </summary>
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// 项目状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 是否为热门项目
        /// </summary>
        public int IsFeatured { get; set; }

        /// <summary>
        /// 项目难度级别
        /// </summary>
        public string DifficultyLevel { get; set; }

        /// <summary>
        /// 排序权重
        /// </summary>
        public int SortOrder { get; set; }
    }

    /// <summary>
    /// 项目查询输入DTO
    /// </summary>
    public class BlogProjectQueryInputDTO : PageInput
    {
        /// <summary>
        /// 关键词搜索
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 状态筛选
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 是否精选
        /// </summary>
        public int? IsFeatured { get; set; }

        /// <summary>
        /// 难度级别
        /// </summary>
        public string DifficultyLevel { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 工具管理输入DTO
    /// </summary>
    public class BlogToolInputDTO
    {
        /// <summary>
        /// 工具ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 工具名称
        /// </summary>
        [Required(ErrorMessage = "工具名称不能为空")]
        [StringLength(100, ErrorMessage = "工具名称长度不能超过100个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 工具描述说明
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 工具详细介绍，富文本
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 工具封面图片
        /// </summary>
        public string CoverImage { get; set; }

        /// <summary>
        /// 工具图标URL
        /// </summary>
        public string IconUrl { get; set; }

        /// <summary>
        /// 官方网站地址
        /// </summary>
        public string OfficialUrl { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 工具分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 价格类型：free免费/paid付费/freemium免费增值
        /// </summary>
        public string PriceType { get; set; }

        /// <summary>
        /// 个人评分（0-5）
        /// </summary>
        public decimal Rating { get; set; }

        /// <summary>
        /// 是否推荐：1是/0否
        /// </summary>
        public int IsRecommended { get; set; }

        /// <summary>
        /// 排序权重
        /// </summary>
        public int SortOrder { get; set; }
    }

    /// <summary>
    /// 工具查询输入DTO
    /// </summary>
    public class BlogToolQueryInputDTO : PageInput
    {
        /// <summary>
        /// 关键词搜索
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 工具分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 价格类型
        /// </summary>
        public string PriceType { get; set; }

        /// <summary>
        /// 是否推荐
        /// </summary>
        public int? IsRecommended { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 技术栈管理输入DTO
    /// </summary>
    public class BlogTechnologyInputDTO
    {
        /// <summary>
        /// 技术栈ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 技术名称
        /// </summary>
        [Required(ErrorMessage = "技术名称不能为空")]
        [StringLength(50, ErrorMessage = "技术名称长度不能超过50个字符")]
        public string Name { get; set; }

        /// <summary>
        /// 技术Logo图片URL
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// 技术简短描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 技术栈详细介绍，富文本
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 技术分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 熟练度评分（0-100）
        /// </summary>
        public int ProficiencyLevel { get; set; }

        /// <summary>
        /// 推荐度评分（0-100）
        /// </summary>
        public int RecommendationLevel { get; set; }

        /// <summary>
        /// 是否在首页展示：1是/0否
        /// </summary>
        public int IsFeatured { get; set; }

        /// <summary>
        /// 排序权重
        /// </summary>
        public int SortOrder { get; set; }
    }

    /// <summary>
    /// 技术栈查询输入DTO
    /// </summary>
    public class BlogTechnologyQueryInputDTO : PageInput
    {
        /// <summary>
        /// 关键词搜索
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 技术分类
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 是否精选
        /// </summary>
        public int? IsFeatured { get; set; }

        /// <summary>
        /// 最低熟练度
        /// </summary>
        public int? MinProficiencyLevel { get; set; }

        /// <summary>
        /// 最低推荐度
        /// </summary>
        public int? MinRecommendationLevel { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 评论管理输入DTO
    /// </summary>
    public class BlogCommentInputDTO
    {
        /// <summary>
        /// 评论ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [Required(ErrorMessage = "评论内容不能为空")]
        public string Content { get; set; }

        /// <summary>
        /// 评论用户ID
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 评论目标类型：article文章/project项目/tool工具/technology技术栈
        /// </summary>
        [Required(ErrorMessage = "评论目标类型不能为空")]
        public string TargetType { get; set; }

        /// <summary>
        /// 评论目标ID
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// 父评论ID，用于回复功能
        /// </summary>
        public int? ParentId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 评论者IP地址
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        public string UserAgent { get; set; }
    }

    /// <summary>
    /// 评论查询输入DTO
    /// </summary>
    public class BlogCommentQueryInputDTO : PageInput
    {
        /// <summary>
        /// 关键词搜索（评论内容）
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 评论目标类型
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 评论目标ID
        /// </summary>
        public int? TargetId { get; set; }

        /// <summary>
        /// 审核状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 评论用户ID
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 是否只查询顶级评论（非回复）
        /// </summary>
        public bool? IsTopLevel { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 评论批量审核输入DTO
    /// </summary>
    public class BlogCommentBatchApprovalInputDTO
    {
        /// <summary>
        /// 评论ID列表
        /// </summary>
        [Required(ErrorMessage = "评论ID不能为空")]
        public List<int> Ids { get; set; }

        /// <summary>
        /// 操作类型：approve通过/reject拒绝
        /// </summary>
        [Required(ErrorMessage = "操作类型不能为空")]
        public string Action { get; set; }
    }

    /// <summary>
    /// 博客用户管理输入DTO
    /// </summary>
    public class BlogUserInputDTO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int? Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required(ErrorMessage = "用户名不能为空")]
        [StringLength(50, ErrorMessage = "用户名长度不能超过50个字符")]
        public string Username { get; set; }

        /// <summary>
        /// 密码（新建时必填，编辑时可选）
        /// </summary>
        [StringLength(255, ErrorMessage = "密码长度不能超过255个字符")]
        public string Password { get; set; }

        /// <summary>
        /// 邮箱地址
        /// </summary>
        [EmailAddress(ErrorMessage = "邮箱格式不正确")]
        [StringLength(100, ErrorMessage = "邮箱长度不能超过100个字符")]
        public string Email { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [StringLength(50, ErrorMessage = "昵称长度不能超过50个字符")]
        public string Nickname { get; set; }

        /// <summary>
        /// 头像URL地址
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 账号状态：1启用/0禁用
        /// </summary>
        public int Status { get; set; }
    }

    /// <summary>
    /// 博客用户查询输入DTO
    /// </summary>
    public class BlogUserQueryInputDTO : PageInput
    {
        /// <summary>
        /// 关键词搜索（用户名、昵称、邮箱）
        /// </summary>
        public string Keyword { get; set; }

        /// <summary>
        /// 用户角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 账号状态
        /// </summary>
        public int? Status { get; set; }

        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime? StartTime { get; set; }

        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime? EndTime { get; set; }
    }

    /// <summary>
    /// 博客用户批量操作输入DTO
    /// </summary>
    public class BlogUserBatchOperationInputDTO
    {
        /// <summary>
        /// 用户ID列表
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public List<int> Ids { get; set; }

        /// <summary>
        /// 操作类型：enable启用/disable禁用
        /// </summary>
        [Required(ErrorMessage = "操作类型不能为空")]
        public string Action { get; set; }
    }

    /// <summary>
    /// 重置密码输入DTO
    /// </summary>
    public class BlogUserResetPasswordInputDTO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [Required(ErrorMessage = "用户ID不能为空")]
        public int UserId { get; set; }

        /// <summary>
        /// 新密码
        /// </summary>
        [Required(ErrorMessage = "新密码不能为空")]
        [StringLength(255, MinimumLength = 6, ErrorMessage = "密码长度必须在6-255个字符之间")]
        public string NewPassword { get; set; }
    }
}