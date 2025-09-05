using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 系统配置表
    /// </summary>
    [Table("blog_system_configs")]
    public class Blog_SystemConfig
    {
        /// <summary>
        /// 配置ID
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 配置键名
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column("config_key")]
        public string ConfigKey { get; set; }

        /// <summary>
        /// 配置值
        /// </summary>
        [Column("config_value")]
        public string ConfigValue { get; set; }

        /// <summary>
        /// 配置说明
        /// </summary>
        [StringLength(255)]
        public string Description { get; set; }

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