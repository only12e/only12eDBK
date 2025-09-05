using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    public interface IBlog_CommentBusiness
    {
        Task<PageResult<Blog_Comment>> GetDataListAsync(BlogCommentQueryInputDTO input);
        Task<Blog_Comment> GetTheDataAsync(int id);
        Task AddDataAsync(BlogCommentInputDTO input);
        Task UpdateDataAsync(BlogCommentInputDTO input);
        Task DeleteDataAsync(List<int> ids);
        Task<List<Blog_Comment>> GetCommentsByTargetAsync(string targetType, int targetId, int count = 10);
        Task<List<Blog_Comment>> GetLatestCommentsAsync(int count = 10);
        Task<List<Blog_Comment>> GetPendingCommentsAsync(int count = 10);
        Task ApproveCommentsAsync(List<int> ids);
        Task RejectCommentsAsync(List<int> ids);
        Task<int> GetCommentCountByStatusAsync(string status);
        Task<List<Blog_Comment>> GetRepliesByParentIdAsync(int parentId);
        Task LikeCommentAsync(int commentId);
    }
}