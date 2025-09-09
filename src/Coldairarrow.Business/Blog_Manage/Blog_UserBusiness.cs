using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.IBusiness;
using Coldairarrow.IBusiness.Blog_Manage;
using Coldairarrow.Util;
using EFCore.Sharding;
using LinqKit;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Coldairarrow.Business.Blog_Manage
{
    public class Blog_UserBusiness : BaseBusiness<Blog_User>, IBlog_UserBusiness, ITransientDependency
    {
        readonly IOperator _operator;

        public Blog_UserBusiness(IDbAccessor db, IOperator @operator)
            : base(db)
        {
            _operator = @operator;
        }

        #region 外部接口

        public async Task<PageResult<Blog_User>> GetDataListAsync(BlogUserQueryInputDTO input)
        {
            var q = GetIQueryable();

            // 关键词搜索
            if (!input.Keyword.IsNullOrEmpty())
            {
                q = q.Where(x => x.Username.Contains(input.Keyword) 
                             || x.Nickname.Contains(input.Keyword) 
                             || x.Email.Contains(input.Keyword));
            }

            // 角色筛选
            if (!input.Role.IsNullOrEmpty())
            {
                q = q.Where(x => x.Role == input.Role);
            }

            // 状态筛选
            if (input.Status.HasValue)
            {
                q = q.Where(x => x.Status == input.Status.Value);
            }

            // 时间范围筛选
            if (input.StartTime.HasValue)
            {
                q = q.Where(x => x.CreatedAt >= input.StartTime.Value);
            }
            if (input.EndTime.HasValue)
            {
                q = q.Where(x => x.CreatedAt <= input.EndTime.Value);
            }

            // 排序
            q = q.OrderByDescending(x => x.CreatedAt);

            return await q.GetPageResultAsync(input);
        }

        public async Task<Blog_User> GetTheDataAsync(int id)
        {
            return await GetEntityAsync(id);
        }

        public async Task AddDataAsync(BlogUserInputDTO input)
        {
            // 检查用户名是否已存在
            if (await CheckUsernameExistsAsync(input.Username))
            {
                throw new BusException("用户名已存在");
            }

            // 检查邮箱是否已存在
            if (!input.Email.IsNullOrEmpty() && await CheckEmailExistsAsync(input.Email))
            {
                throw new BusException("邮箱地址已存在");
            }

            // 新建用户必须提供密码
            if (input.Password.IsNullOrEmpty())
            {
                throw new BusException("新建用户必须设置密码");
            }

            var newData = new Blog_User
            {
                Username = input.Username,
                Password = HashPassword(input.Password),
                Email = input.Email,
                Nickname = input.Nickname ?? input.Username,
                Avatar = input.Avatar,
                Role = input.Role ?? "user",
                Status = input.Status,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await InsertAsync(newData);
        }

        public async Task UpdateDataAsync(BlogUserInputDTO input)
        {
            if (!input.Id.HasValue)
                throw new BusException("用户ID不能为空");

            var theData = await GetEntityAsync(input.Id.Value);
            if (theData == null)
                throw new BusException("用户不存在");

            // 检查用户名是否已存在（排除当前用户）
            if (await CheckUsernameExistsAsync(input.Username, input.Id.Value))
            {
                throw new BusException("用户名已存在");
            }

            // 检查邮箱是否已存在（排除当前用户）
            if (!input.Email.IsNullOrEmpty() && await CheckEmailExistsAsync(input.Email, input.Id.Value))
            {
                throw new BusException("邮箱地址已存在");
            }
            
            theData.Username = input.Username;
            theData.Email = input.Email;
            theData.Nickname = input.Nickname ?? input.Username;
            theData.Avatar = input.Avatar;
            theData.Role = input.Role ?? theData.Role;
            theData.Status = input.Status;
            theData.UpdatedAt = DateTime.Now;

            // 如果提供了新密码，则更新密码
            if (!input.Password.IsNullOrEmpty())
            {
                theData.Password = HashPassword(input.Password);
            }
            
            await UpdateAsync(theData);
        }

        public async Task DeleteDataAsync(List<int> ids)
        {
            var stringIds = ids.Select(x => x.ToString()).ToList();
            await DeleteAsync(stringIds);
        }

        public async Task<List<Blog_User>> GetLatestUsersAsync(int count = 10)
        {
            return await GetIQueryable()
                        .OrderByDescending(x => x.CreatedAt)
                        .Take(count)
                        .ToListAsync();
        }

        public async Task<List<Blog_User>> GetActiveUsersAsync(int count = 10)
        {
            return await GetIQueryable()
                        .Where(x => x.Status == 1)
                        .OrderByDescending(x => x.CreatedAt)
                        .Take(count)
                        .ToListAsync();
        }

        public async Task<int> GetUserCountByStatusAsync(int status)
        {
            return await GetIQueryable()
                        .Where(x => x.Status == status)
                        .CountAsync();
        }

        public async Task<int> GetUserCountByRoleAsync(string role)
        {
            return await GetIQueryable()
                        .Where(x => x.Role == role)
                        .CountAsync();
        }

        public async Task EnableUsersAsync(List<int> ids)
        {
            var users = await GetIQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            foreach (var user in users)
            {
                user.Status = 1;
                user.UpdatedAt = DateTime.Now;
            }

            await UpdateAsync(users);
        }

        public async Task DisableUsersAsync(List<int> ids)
        {
            var users = await GetIQueryable()
                .Where(x => ids.Contains(x.Id))
                .ToListAsync();

            foreach (var user in users)
            {
                user.Status = 0;
                user.UpdatedAt = DateTime.Now;
            }

            await UpdateAsync(users);
        }

        public async Task ResetPasswordAsync(int userId, string newPassword)
        {
            var user = await GetEntityAsync(userId);
            if (user == null)
                throw new BusException("用户不存在");

            user.Password = HashPassword(newPassword);
            user.UpdatedAt = DateTime.Now;

            await UpdateAsync(user);
        }

        public async Task<Blog_User> GetUserByUsernameAsync(string username)
        {
            return await GetIQueryable()
                        .Where(x => x.Username == username)
                        .FirstOrDefaultAsync();
        }

        public async Task<Blog_User> GetUserByEmailAsync(string email)
        {
            return await GetIQueryable()
                        .Where(x => x.Email == email)
                        .FirstOrDefaultAsync();
        }

        public async Task<Blog_User> GetUserByUsernameOrEmailAsync(string usernameOrEmail)
        {
            return await GetIQueryable()
                        .Where(x => x.Username == usernameOrEmail || x.Email == usernameOrEmail)
                        .FirstOrDefaultAsync();
        }

        public async Task UpdateLastLoginTimeAsync(int userId)
        {
            var user = await GetEntityAsync(userId);
            if (user != null)
            {
                await UpdateAsync(user);
            }
        }

        public async Task<bool> CheckUsernameExistsAsync(string username, int? excludeId = null)
        {
            var q = GetIQueryable().Where(x => x.Username == username);
            
            if (excludeId.HasValue)
            {
                q = q.Where(x => x.Id != excludeId.Value);
            }

            return await q.AnyAsync();
        }

        public async Task<bool> CheckEmailExistsAsync(string email, int? excludeId = null)
        {
            if (email.IsNullOrEmpty())
                return false;

            var q = GetIQueryable().Where(x => x.Email == email);
            
            if (excludeId.HasValue)
            {
                q = q.Where(x => x.Id != excludeId.Value);
            }

            return await q.AnyAsync();
        }

        #endregion

        #region 私有成员

        /// <summary>
        /// 密码哈希
        /// </summary>
        /// <param name="password">明文密码</param>
        /// <returns>哈希后的密码</returns>
        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "BlogSalt2025"));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        #endregion
    }
}