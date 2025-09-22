using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Coldairarrow.Entity.Base_Manage;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 文章表
    /// </summary>
    [Table("blog_articles")]
    public class Blog_Article
    {
        /// <summary>
        /// 文章ID，主键
        /// </summary>
        [Key]
        public long Id { get; set; }

        /// <summary>
        /// 文章标题
        /// </summary>
        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        /// <summary>
        /// 文章摘要/简介
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// 文章内容，富文本
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 封面图片URL
        /// </summary>
        [StringLength(255)]
        [Column("cover_image")]
        public string CoverImage { get; set; }

        /// <summary>
        /// 文章分类ID
        /// </summary>
        [Column("category_id")]
        public int? CategoryId { get; set; }

        /// <summary>
        /// 作者ID，关联users表
        /// </summary>
        [Column("author_id")]
        public string AuthorId { get; set; }

        /// <summary>
        /// 文章状态：draft草稿/published已发布/hidden隐藏
        /// </summary>
        public string Status { get; set; } = "draft";

        /// <summary>
        /// 是否精选文章：1是/0否
        /// </summary>
        [Column("is_featured")]
        public int IsFeatured { get; set; } = 0;

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
        /// 发布时间
        /// </summary>
        [Column("published_at")]
        public DateTime? PublishedAt { get; set; }

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

        // 导航属性
        /// <summary>
        /// 分类信息
        /// </summary>
        [ForeignKey("CategoryId")]
        public virtual Blog_ArticleCategory Category { get; set; }

        /// <summary>
        /// 作者信息
        /// </summary>
        [ForeignKey("AuthorId")]
        public virtual Base_User Author { get; set; }

    }
}