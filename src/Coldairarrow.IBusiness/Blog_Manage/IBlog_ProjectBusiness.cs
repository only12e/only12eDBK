using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    public interface IBlog_ProjectBusiness
    {
        Task<PageResult<Blog_Project>> GetDataListAsync(BlogProjectQueryInputDTO input);
        Task<Blog_Project> GetTheDataAsync(int id);
        Task AddDataAsync(BlogProjectInputDTO input);
        Task UpdateDataAsync(BlogProjectInputDTO input);
        Task DeleteDataAsync(List<string> ids);
        Task DeleteDataAsync(List<int> ids);
        Task<List<Blog_Project>> GetFeaturedProjectsAsync(int count = 10);
        Task<List<Blog_Project>> GetLatestProjectsAsync(int count = 10);
        Task IncrementViewCountAsync(int id);
    }
}