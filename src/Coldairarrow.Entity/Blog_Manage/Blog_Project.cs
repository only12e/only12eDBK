using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 项目表
    /// </summary>
    [Table("blog_projects")]
    public class Blog_Project
    {
        /// <summary>
        /// 项目ID，主键
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 项目名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 项目简介描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 项目详细说明，富文本
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 项目封面图片URL
        /// </summary>
        [StringLength(255)]
        [Column("cover_image")]
        public string CoverImage { get; set; }

        /// <summary>
        /// 项目演示地址
        /// </summary>
        [StringLength(255)]
        [Column("demo_url")]
        public string DemoUrl { get; set; }

        /// <summary>
        /// GitHub仓库地址
        /// </summary>
        [StringLength(255)]
        [Column("github_url")]
        public string GithubUrl { get; set; }

        /// <summary>
        /// 其他相关网站地址
        /// </summary>
        [StringLength(255)]
        [Column("website_url")]
        public string WebsiteUrl { get; set; }

        /// <summary>
        /// 项目状态：active进行中/completed已完成/archived已归档
        /// </summary>
        public string Status { get; set; } = "active";

        /// <summary>
        /// 是否为热门项目：1是/0否
        /// </summary>
        [Column("is_featured")]
        public int IsFeatured { get; set; } = 0;

        /// <summary>
        /// 项目难度级别
        /// </summary>
        [Column("difficulty_level")]
        public string DifficultyLevel { get; set; }

        /// <summary>
        /// 浏览次数
        /// </summary>
        [Column("view_count")]
        public int ViewCount { get; set; } = 0;

        /// <summary>
        /// 点赞数量
        /// </summary>
        [Column("like_count")]
        public int LikeCount { get; set; } = 0;

        /// <summary>
        /// 评论数量
        /// </summary>
        [Column("comment_count")]
        public int CommentCount { get; set; } = 0;

        /// <summary>
        /// 排序权重
        /// </summary>
        [Column("sort_order")]
        public int SortOrder { get; set; } = 0;

        /// <summary>
        /// 创建时间
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;
    }
}