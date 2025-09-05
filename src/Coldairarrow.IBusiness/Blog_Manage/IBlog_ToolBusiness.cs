using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    public interface IBlog_ToolBusiness
    {
        Task<PageResult<Blog_Tool>> GetDataListAsync(BlogToolQueryInputDTO input);
        Task<Blog_Tool> GetTheDataAsync(int id);
        Task AddDataAsync(BlogToolInputDTO input);
        Task UpdateDataAsync(BlogToolInputDTO input);
        Task DeleteDataAsync(List<int> ids);
        Task<List<Blog_Tool>> GetRecommendedToolsAsync(int count = 10);
        Task<List<Blog_Tool>> GetLatestToolsAsync(int count = 10);
        Task<List<string>> GetCategoriesAsync();
    }
}