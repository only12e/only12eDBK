using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 友情链接表
    /// </summary>
    [Table("blog_friendly_links")]
    public class Blog_FriendlyLink
    {
        /// <summary>
        /// 链接ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 链接名称
        /// </summary>
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        /// <summary>
        /// 链接地址
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Url { get; set; }

        /// <summary>
        /// 链接描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Logo图片URL
        /// </summary>
        [StringLength(255)]
        [Column("logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// 状态：1启用/0禁用
        /// </summary>
        public int Status { get; set; } = 1;

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
    }
}