using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    public interface IBlog_TechnologyBusiness
    {
        Task<PageResult<Blog_Technology>> GetDataListAsync(BlogTechnologyQueryInputDTO input);
        Task<Blog_Technology> GetTheDataAsync(int id);
        Task AddDataAsync(BlogTechnologyInputDTO input);
        Task UpdateDataAsync(BlogTechnologyInputDTO input);
        Task DeleteDataAsync(List<int> ids);
        Task<List<Blog_Technology>> GetFeaturedTechnologiesAsync(int count = 10);
        Task<List<Blog_Technology>> GetLatestTechnologiesAsync(int count = 10);
        Task<List<string>> GetCategoriesAsync();
        Task<List<Blog_Technology>> GetTechnologiesByCategoryAsync(string category);
    }
}