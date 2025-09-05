using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 技术栈表
    /// </summary>
    [Table("blog_technologies")]
    public class Blog_Technology
    {
        /// <summary>
        /// 技术栈ID，主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 技术名称，如React、Vue等
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        /// <summary>
        /// 技术Logo图片URL
        /// </summary>
        [StringLength(255)]
        [Column("logo_url")]
        public string LogoUrl { get; set; }

        /// <summary>
        /// 技术简短描述，用于列表展示
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 技术栈详细介绍，富文本，支持文字+图片
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// 技术分类：frontend前端/backend后端/database数据库/devtools开发工具/other其他
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// 熟练度评分（0-100）
        /// </summary>
        [Column("proficiency_level")]
        public int ProficiencyLevel { get; set; } = 0;

        /// <summary>
        /// 推荐度评分（0-100）
        /// </summary>
        [Column("recommendation_level")]
        public int RecommendationLevel { get; set; } = 0;

        /// <summary>
        /// 是否在首页展示：1是/0否
        /// </summary>
        [Column("is_featured")]
        public int IsFeatured { get; set; } = 0;

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