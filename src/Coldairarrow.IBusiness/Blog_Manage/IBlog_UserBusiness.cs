using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    public interface IBlog_UserBusiness
    {
        Task<PageResult<Blog_User>> GetDataListAsync(BlogUserQueryInputDTO input);
        Task<Blog_User> GetTheDataAsync(int id);
        Task AddDataAsync(BlogUserInputDTO input);
        Task UpdateDataAsync(BlogUserInputDTO input);
        Task DeleteDataAsync(List<int> ids);
        Task<List<Blog_User>> GetLatestUsersAsync(int count = 10);
        Task<List<Blog_User>> GetActiveUsersAsync(int count = 10);
        Task<int> GetUserCountByStatusAsync(int status);
        Task<int> GetUserCountByRoleAsync(string role);
        Task EnableUsersAsync(List<int> ids);
        Task DisableUsersAsync(List<int> ids);
        Task ResetPasswordAsync(int userId, string newPassword);
        Task<Blog_User> GetUserByUsernameAsync(string username);
        Task<Blog_User> GetUserByEmailAsync(string email);
        Task<Blog_User> GetUserByUsernameOrEmailAsync(string usernameOrEmail);
        Task UpdateLastLoginTimeAsync(int userId);
        Task<bool> CheckUsernameExistsAsync(string username, int? excludeId = null);
        Task<bool> CheckEmailExistsAsync(string email, int? excludeId = null);
    }
}