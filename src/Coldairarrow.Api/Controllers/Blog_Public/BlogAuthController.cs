using Coldairarrow.Entity;
using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Blog_Public
{
    /// <summary>
    /// 博客公开API - 认证相关
    /// </summary>
    [Route("/BlogApi/[action]")]
    [OpenApiTag("博客公开API")]
    [AllowAnonymous]
    public class BlogAuthController : BaseApiController
    {
        #region DI

        public BlogAuthController(IBlog_UserBusiness userBus)
        {
            _userBus = userBus;
        }

        IBlog_UserBusiness _userBus { get; }

        #endregion

        #region 公开接口

        /// <summary>
        /// 博客用户登录
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BlogLoginResultDTO> Login(BlogLoginInputDTO input)
        {
            try
            {
                // 验证输入
                if (string.IsNullOrEmpty(input.Username))
                    throw new BusException("用户名不能为空");
                
                if (string.IsNullOrEmpty(input.Password))
                    throw new BusException("密码不能为空");
                
                // 查找用户
                var user = await _userBus.GetUserByUsernameOrEmailAsync(input.Username);
                if (user == null)
                    throw new BusException("用户名或密码错误");
                
                // 验证密码
                var hashedPassword = input.Password.ToMD5String();
                if (user.Password != hashedPassword)
                    throw new BusException("用户名或密码错误");
                
                // 检查用户状态
                if (user.Status != 1)
                    throw new BusException("账户已被禁用，请联系管理员");
                
                // 更新最后登录时间
                await _userBus.UpdateLastLoginTimeAsync(user.Id);
                
                // 生成访问令牌（这里应该使用JWT或其他安全的方式）
                var token = GenerateToken(user);
                
                return new BlogLoginResultDTO
                {
                    Success = true,
                    Token = token,
                    User = new BlogUserInfoDTO
                    {
                        Id = user.Id,
                        Username = user.Username,
                        Email = user.Email,
                        Avatar = user.Avatar,
                        Role = user.Role,
                        Status = user.Status,
                        CreatedAt = user.CreatedAt
                    },
                    Message = "登录成功"
                };
            }
            catch (BusException ex)
            {
                return new BlogLoginResultDTO
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new BlogLoginResultDTO
                {
                    Success = false,
                    Message = "登录失败，请稍后重试"
                };
            }
        }

        /// <summary>
        /// 博客用户注册
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BlogRegisterResultDTO> Register(BlogRegisterInputDTO input)
        {
            try
            {
                // 验证输入
                if (string.IsNullOrEmpty(input.Username))
                    throw new BusException("用户名不能为空");
                
                if (input.Username.Length < 3 || input.Username.Length > 20)
                    throw new BusException("用户名长度必须在3-20个字符之间");
                
                if (string.IsNullOrEmpty(input.Email))
                    throw new BusException("邮箱不能为空");
                
                if (!input.Email.IsEmail())
                    throw new BusException("邮箱格式不正确");
                
                if (string.IsNullOrEmpty(input.Password))
                    throw new BusException("密码不能为空");
                
                if (input.Password.Length < 8)
                    throw new BusException("密码长度至少8位");
                
                if (input.Password != input.ConfirmPassword)
                    throw new BusException("两次输入的密码不一致");
                
                // 检查用户名是否已存在
                if (await _userBus.CheckUsernameExistsAsync(input.Username))
                    throw new BusException("用户名已存在");
                
                // 检查邮箱是否已存在
                if (await _userBus.CheckEmailExistsAsync(input.Email))
                    throw new BusException("邮箱已被注册");
                
                // 创建用户
                var userInput = new BlogUserInputDTO
                {
                    Username = input.Username,
                    Email = input.Email,
                    Password = input.Password.ToMD5String(),
                    FirstName = input.FirstName,
                    LastName = input.LastName,
                    Role = "user", // 默认角色
                    Status = 1, // 启用状态
                    CreatedAt = DateTime.Now
                };
                
                await _userBus.AddDataAsync(userInput);
                
                return new BlogRegisterResultDTO
                {
                    Success = true,
                    Message = "注册成功"
                };
            }
            catch (BusException ex)
            {
                return new BlogRegisterResultDTO
                {
                    Success = false,
                    Message = ex.Message
                };
            }
            catch (Exception ex)
            {
                return new BlogRegisterResultDTO
                {
                    Success = false,
                    Message = "注册失败，请稍后重试"
                };
            }
        }

        /// <summary>
        /// 检查用户名是否可用
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BlogCheckResultDTO> CheckUsername(string username)
        {
            try
            {
                if (string.IsNullOrEmpty(username))
                    return new BlogCheckResultDTO { Available = false, Message = "用户名不能为空" };
                
                if (username.Length < 3 || username.Length > 20)
                    return new BlogCheckResultDTO { Available = false, Message = "用户名长度必须在3-20个字符之间" };
                
                var exists = await _userBus.CheckUsernameExistsAsync(username);
                
                return new BlogCheckResultDTO
                {
                    Available = !exists,
                    Message = exists ? "用户名已被占用" : "用户名可用"
                };
            }
            catch (Exception ex)
            {
                return new BlogCheckResultDTO
                {
                    Available = false,
                    Message = "检查失败，请稍后重试"
                };
            }
        }

        /// <summary>
        /// 检查邮箱是否可用
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<BlogCheckResultDTO> CheckEmail(string email)
        {
            try
            {
                if (string.IsNullOrEmpty(email))
                    return new BlogCheckResultDTO { Available = false, Message = "邮箱不能为空" };
                
                if (!email.IsEmail())
                    return new BlogCheckResultDTO { Available = false, Message = "邮箱格式不正确" };
                
                var exists = await _userBus.CheckEmailExistsAsync(email);
                
                return new BlogCheckResultDTO
                {
                    Available = !exists,
                    Message = exists ? "邮箱已被注册" : "邮箱可用"
                };
            }
            catch (Exception ex)
            {
                return new BlogCheckResultDTO
                {
                    Available = false,
                    Message = "检查失败，请稍后重试"
                };
            }
        }

        /// <summary>
        /// 验证令牌
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<BlogTokenValidationResultDTO> ValidateToken(string token)
        {
            try
            {
                // 这里应该实现实际的令牌验证逻辑
                // 简化实现，实际应该解析JWT或查询数据库
                if (string.IsNullOrEmpty(token))
                    throw new BusException("令牌不能为空");
                
                // 模拟令牌验证
                if (token.StartsWith("blog_token_"))
                {
                    return new BlogTokenValidationResultDTO
                    {
                        Valid = true,
                        Message = "令牌有效"
                    };
                }
                
                return new BlogTokenValidationResultDTO
                {
                    Valid = false,
                    Message = "令牌无效"
                };
            }
            catch (Exception ex)
            {
                return new BlogTokenValidationResultDTO
                {
                    Valid = false,
                    Message = "验证失败"
                };
            }
        }

        #endregion

        #region 私有方法

        /// <summary>
        /// 生成访问令牌
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        private string GenerateToken(Blog_User user)
        {
            // 简化实现，实际应该使用JWT
            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var tokenData = $"{user.Id}_{user.Username}_{timestamp}";
            var token = $"blog_token_{tokenData.ToMD5String()}";
            
            return token;
        }

        #endregion
    }

    #region DTO定义

    /// <summary>
    /// 博客登录输入DTO
    /// </summary>
    public class BlogLoginInputDTO
    {
        /// <summary>
        /// 用户名或邮箱
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 记住我
        /// </summary>
        public bool RememberMe { get; set; }
    }

    /// <summary>
    /// 博客登录结果DTO
    /// </summary>
    public class BlogLoginResultDTO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 访问令牌
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户信息
        /// </summary>
        public BlogUserInfoDTO User { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 博客注册输入DTO
    /// </summary>
    public class BlogRegisterInputDTO
    {
        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 确认密码
        /// </summary>
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// 名字
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// 姓氏
        /// </summary>
        public string LastName { get; set; }
    }

    /// <summary>
    /// 博客注册结果DTO
    /// </summary>
    public class BlogRegisterResultDTO
    {
        /// <summary>
        /// 是否成功
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 博客用户信息DTO
    /// </summary>
    public class BlogUserInfoDTO
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 头像
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 角色
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedAt { get; set; }
    }

    /// <summary>
    /// 博客检查结果DTO
    /// </summary>
    public class BlogCheckResultDTO
    {
        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Available { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 博客令牌验证结果DTO
    /// </summary>
    public class BlogTokenValidationResultDTO
    {
        /// <summary>
        /// 是否有效
        /// </summary>
        public bool Valid { get; set; }

        /// <summary>
        /// 消息
        /// </summary>
        public string Message { get; set; }
    }

    #endregion
}