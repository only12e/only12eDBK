using Coldairarrow.Entity.Blog_Manage;
using Coldairarrow.Entity.DTO;
using Coldairarrow.Util;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Coldairarrow.IBusiness.Blog_Manage
{
    public interface IBlog_LikeBusiness
    {
        /// <summary>
        /// 切换点赞状态（点赞/取消点赞）
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <returns>点赞状态结果</returns>
        Task<LikeStatusResult> ToggleLikeAsync(string userId, string targetType, long targetId);

        /// <summary>
        /// 检查用户是否已点赞
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <returns>是否已点赞</returns>
        Task<bool> IsLikedAsync(string userId, string targetType, long targetId);

        /// <summary>
        /// 获取目标的点赞数量
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <returns>点赞数量</returns>
        Task<int> GetLikeCountAsync(string targetType, long targetId);

        /// <summary>
        /// 获取用户的点赞列表
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="targetType">目标类型（可选）</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns>点赞列表</returns>
        Task<PageResult<Blog_Like>> GetUserLikesAsync(string userId, string targetType = null, int pageSize = 20, int pageIndex = 1);

        /// <summary>
        /// 获取目标的点赞用户列表
        /// </summary>
        /// <param name="targetType">目标类型</param>
        /// <param name="targetId">目标ID</param>
        /// <param name="pageSize">分页大小</param>
        /// <param name="pageIndex">页索引</param>
        /// <returns>点赞用户列表</returns>
        Task<PageResult<Blog_Like>> GetTargetLikesAsync(string targetType, long targetId, int pageSize = 20, int pageIndex = 1);

        /// <summary>
        /// 批量检查用户对多个目标的点赞状态
        /// </summary>
        /// <param name="userId">用户ID</param>
        /// <param name="targets">目标列表</param>
        /// <returns>点赞状态字典</returns>
        Task<Dictionary<string, bool>> BatchCheckLikeStatusAsync(string userId, List<LikeTarget> targets);

        /// <summary>
        /// 获取最近的点赞记录
        /// </summary>
        /// <param name="count">数量</param>
        /// <returns>最近点赞记录</returns>
        Task<List<Blog_Like>> GetRecentLikesAsync(int count = 10);
    }

    /// <summary>
    /// 点赞状态结果
    /// </summary>
    public class LikeStatusResult
    {
        /// <summary>
        /// 是否已点赞
        /// </summary>
        public bool IsLiked { get; set; }

        /// <summary>
        /// 当前点赞总数
        /// </summary>
        public int TotalLikes { get; set; }

        /// <summary>
        /// 操作消息
        /// </summary>
        public string Message { get; set; }
    }

    /// <summary>
    /// 点赞目标
    /// </summary>
    public class LikeTarget
    {
        /// <summary>
        /// 目标类型
        /// </summary>
        public string TargetType { get; set; }

        /// <summary>
        /// 目标ID
        /// </summary>
        public long TargetId { get; set; }

        /// <summary>
        /// 目标键（用于字典返回）
        /// </summary>
        public string Key => $"{TargetType}_{TargetId}";
    }
}