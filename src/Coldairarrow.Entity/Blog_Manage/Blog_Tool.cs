using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 工具表
    /// </summary>
    [Table("blog_tools")]
    public class Blog_Tool
    {
        /// <summary>
        /// 工具ID，主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 工具名称
        /// </summary>
        [Required]
        [StringLength(100)]
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
        [StringLength(255)]
        [Column("cover_image")]
        public string CoverImage { get; set; }

        /// <summary>
        /// 工具图标URL
        /// </summary>
        [StringLength(255)]
        [Column("icon_url")]
        public string IconUrl { get; set; }

        /// <summary>
        /// 官方网站地址
        /// </summary>
        [StringLength(255)]
        [Column("official_url")]
        public string OfficialUrl { get; set; }

        /// <summary>
        /// 下载地址
        /// </summary>
        [StringLength(255)]
        [Column("download_url")]
        public string DownloadUrl { get; set; }

        /// <summary>
        /// 工具分类，如编辑器、浏览器插件等
        /// </summary>
        [StringLength(50)]
        public string Category { get; set; }

        /// <summary>
        /// 价格类型：free免费/paid付费/freemium免费增值
        /// </summary>
        [Column("price_type")]
        public string PriceType { get; set; } = "free";

        /// <summary>
        /// 个人评分（0-5）
        /// </summary>
        public decimal Rating { get; set; } = 0;

        /// <summary>
        /// 是否推荐：1是/0否
        /// </summary>
        [Column("is_recommended")]
        public int IsRecommended { get; set; } = 0;

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