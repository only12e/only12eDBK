using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 文章分类表
    /// </summary>
    [Table("blog_article_categories")]
    public class Blog_ArticleCategory
    {
        /// <summary>
        /// 分类ID，主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 分类名称
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 分类描述
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 排序权重，数字越大越靠前
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