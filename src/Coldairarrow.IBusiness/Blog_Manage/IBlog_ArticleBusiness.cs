using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    public interface IBlog_ArticleBusiness
    {
        Task<PageResult<Blog_Article>> GetDataListAsync(BlogArticleQueryInputDTO input);
        Task<Blog_Article> GetTheDataAsync(int id);
        Task AddDataAsync(BlogArticleInputDTO input);
        Task UpdateDataAsync(BlogArticleInputDTO input);
        Task DeleteDataAsync(List<int> ids);
        Task PublishDataAsync(BlogArticlePublishInputDTO input);
        Task<List<Blog_Article>> GetFeaturedArticlesAsync(int count = 10);
        Task<List<Blog_Article>> GetLatestArticlesAsync(int count = 10);
        Task<List<Blog_ArticleCategory>> GetCategoriesAsync();
        Task AddCategoryAsync(Blog_ArticleCategory category);
        Task UpdateCategoryAsync(Blog_ArticleCategory category);
        Task DeleteCategoryAsync(List<int> ids);
    }
}