using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Coldairarrow.Entity.Blog_Manage
{
    /// <summary>
    /// 博客用户表
    /// </summary>
    [Table("blog_users")]
    public class Blog_User
    {
        /// <summary>
        /// 用户ID，主键
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// 用户名，唯一，用于登录
        /// </summary>
        [Required]
        [StringLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// 密码，加密存储
        /// </summary>
        [Required]
        [StringLength(255)]
        public string Password { get; set; }

        /// <summary>
        /// 邮箱地址，唯一
        /// </summary>
        [StringLength(100)]
        public string Email { get; set; }

        /// <summary>
        /// 昵称，显示用
        /// </summary>
        [StringLength(50)]
        public string Nickname { get; set; }

        /// <summary>
        /// 头像URL地址
        /// </summary>
        [StringLength(255)]
        public string Avatar { get; set; }

        /// <summary>
        /// 用户角色：admin管理员/user普通用户
        /// </summary>
        public string Role { get; set; } = "user";

        /// <summary>
        /// 账号状态：1启用/0禁用
        /// </summary>
        public int Status { get; set; } = 1;

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

        /// <summary>
        /// 最后登录时间
        /// </summary>
        [Column("last_login_at")]
        public DateTime? LastLoginAt { get; set; }
    }
}