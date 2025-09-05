using Coldairarrow.Entity;
using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.Api.Controllers.Blog_Manage
{
    /// <summary>
    /// 博客用户管理
    /// </summary>
    [Route("/Blog_Manage/[controller]/[action]")]
    [OpenApiTag("博客用户管理")]
    public class Blog_UserController : BaseApiController
    {
        #region DI

        public Blog_UserController(IBlog_UserBusiness userBus)
        {
            _userBus = userBus;
        }

        IBlog_UserBusiness _userBus { get; }

        #endregion

        #region 用户管理

        /// <summary>
        /// 获取用户分页列表
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<PageResult<Blog_User>> GetDataList(BlogUserQueryInputDTO input)
        {
            var result = await _userBus.GetDataListAsync(input);
            
            // 隐藏密码信息
            foreach (var user in result.Data)
            {
                user.Password = "******";
            }
            
            return result;
        }

        /// <summary>
        /// 获取用户详情
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<Blog_User> GetTheData(IdInputDTO input)
        {
            var user = await _userBus.GetTheDataAsync(input.id.ToInt());
            if (user != null)
            {
                // 隐藏密码信息
                user.Password = "******";
            }
            return user;
        }

        /// <summary>
        /// 保存用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveData(BlogUserInputDTO input)
        {
            if (input.Id.HasValue && input.Id.Value > 0)
            {
                await _userBus.UpdateDataAsync(input);
            }
            else
            {
                await _userBus.AddDataAsync(input);
            }
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task DeleteData(List<int> ids)
        {
            await _userBus.DeleteDataAsync(ids);
        }

        /// <summary>
        /// 获取最新注册用户列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_User>> GetLatestUsers(int count = 10)
        {
            var users = await _userBus.GetLatestUsersAsync(count);
            
            // 隐藏密码信息
            foreach (var user in users)
            {
                user.Password = "******";
            }
            
            return users;
        }

        /// <summary>
        /// 获取活跃用户列表
        /// </summary>
        /// <param name="count">获取数量</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<List<Blog_User>> GetActiveUsers(int count = 10)
        {
            var users = await _userBus.GetActiveUsersAsync(count);
            
            // 隐藏密码信息
            foreach (var user in users)
            {
                user.Password = "******";
            }
            
            return users;
        }

        /// <summary>
        /// 获取指定状态的用户数量
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> GetUserCountByStatus(int status)
        {
            return await _userBus.GetUserCountByStatusAsync(status);
        }

        /// <summary>
        /// 获取指定角色的用户数量
        /// </summary>
        /// <param name="role">角色</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<int> GetUserCountByRole(string role)
        {
            return await _userBus.GetUserCountByRoleAsync(role);
        }

        /// <summary>
        /// 批量操作用户
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task BatchOperation(BlogUserBatchOperationInputDTO input)
        {
            if (input.Action == "enable")
            {
                await _userBus.EnableUsersAsync(input.Ids);
            }
            else if (input.Action == "disable")
            {
                await _userBus.DisableUsersAsync(input.Ids);
            }
            else
            {
                throw new BusException("无效的操作类型");
            }
        }

        /// <summary>
        /// 重置用户密码
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task ResetPassword(BlogUserResetPasswordInputDTO input)
        {
            await _userBus.ResetPasswordAsync(input.UserId, input.NewPassword);
        }

        /// <summary>
        /// 检查用户名是否存在
        /// </summary>
        /// <param name="username">用户名</param>
        /// <param name="excludeId">排除的用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> CheckUsernameExists(string username, int? excludeId = null)
        {
            return await _userBus.CheckUsernameExistsAsync(username, excludeId);
        }

        /// <summary>
        /// 检查邮箱是否存在
        /// </summary>
        /// <param name="email">邮箱</param>
        /// <param name="excludeId">排除的用户ID</param>
        /// <returns></returns>
        [HttpGet]
        public async Task<bool> CheckEmailExists(string email, int? excludeId = null)
        {
            return await _userBus.CheckEmailExistsAsync(email, excludeId);
        }

        #endregion
    }
}