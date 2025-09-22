using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Coldairarrow.Entity.Base_Manage;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 用户点赞表
    /// </summary>
    [Table("blog_likes")]
    public class Blog_Like
    {
        /// <summary>
        /// 点赞ID，主键
        /// </summary>
        [Key]
        [Column("id")]
        public long Id { get; set; }

        /// <summary>
        /// 点赞用户ID
        /// </summary>
        [Required]
        [Column("user_id")]
        public string UserId { get; set; }

        /// <summary>
        /// 点赞目标类型：文章/项目/工具/技术栈/评论
        /// </summary>
        [Required]
        [StringLength(20)]
        [Column("target_type")]
        public string TargetType { get; set; }

        /// <summary>
        /// 点赞目标ID
        /// </summary>
        [Required]
        [Column("target_id")]
        public long TargetId { get; set; }

        /// <summary>
        /// 点赞时间
        /// </summary>
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        // 导航属性
        /// <summary>
        /// 用户信息
        /// </summary>
        [ForeignKey("UserId")]
        public virtual Base_User User { get; set; }
    }
}