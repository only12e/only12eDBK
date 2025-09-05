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
}