using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 评论表（通用）
    /// </summary>
    [Table("blog_comments")]
    public class Blog_Comment
    {
        /// <summary>
        /// 评论ID，主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 评论内容
        /// </summary>
        [Required]
        public string Content { get; set; }

        /// <summary>
        /// 评论用户ID
        /// </summary>
        [Column("user_id")]
        public int UserId { get; set; }

        /// <summary>
        /// 评论目标类型：article文章/project项目/tool工具/technology技术栈
        /// </summary>
        [Required]
        [Column("target_type")]
        public string TargetType { get; set; }

        /// <summary>
        /// 评论目标ID
        /// </summary>
        [Column("target_id")]
        public int TargetId { get; set; }

        /// <summary>
        /// 父评论ID，用于回复功能
        /// </summary>
        [Column("parent_id")]
        public int? ParentId { get; set; }

        /// <summary>
        /// 评论点赞数
        /// </summary>
        [Column("like_count")]
        public int LikeCount { get; set; } = 0;

        /// <summary>
        /// 审核状态：approved通过/pending待审核/rejected拒绝
        /// </summary>
        public string Status { get; set; } = "approved";

        /// <summary>
        /// 评论者IP地址
        /// </summary>
        [StringLength(45)]
        [Column("ip_address")]
        public string IpAddress { get; set; }

        /// <summary>
        /// 浏览器信息
        /// </summary>
        [Column("user_agent")]
        public string UserAgent { get; set; }

        /// <summary>
        /// 评论时间
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 更新时间
        /// </summary>
        [Column("updated_at")]
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        /// <summary>
        /// 回复数量（计算属性，不存储在数据库中）
        /// </summary>
        [NotMapped]
        public int ReplyCount { get; set; } = 0;

        // 导航属性
        /// <summary>
        /// 评论用户信息
        /// </summary>
        [ForeignKey("UserId")]
        public virtual Blog_User User { get; set; }

        /// <summary>
        /// 父评论信息
        /// </summary>
        [ForeignKey("ParentId")]
        public virtual Blog_Comment ParentComment { get; set; }
    }
}