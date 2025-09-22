import { Axios as request } from '@/utils/plugin/axios-plugin'

const api = {
  ToggleLike: '/Blog_Manage/Blog_Like/ToggleLike',
  IsLiked: '/Blog_Manage/Blog_Like/IsLiked',
  GetLikeCount: '/Blog_Manage/Blog_Like/GetLikeCount',
  BatchCheckLikeStatus: '/Blog_Manage/Blog_Like/BatchCheckLikeStatus',
  GetUserLikes: '/Blog_Manage/Blog_Like/GetUserLikes',
  GetTargetLikes: '/Blog_Manage/Blog_Like/GetTargetLikes',
  GetRecentLikes: '/Blog_Manage/Blog_Like/GetRecentLikes'
}

/**
 * 博客点赞API
 */
export default {
  /**
   * 切换点赞状态
   * @param {string} targetType 目标类型 article/project/comment
   * @param {number} targetId 目标ID
   */
  toggleLike(targetType, targetId) {
    return request.post(api.ToggleLike, null, {
      params: { targetType, targetId }
    })
  },

  /**
   * 检查是否已点赞
   * @param {string} targetType 目标类型
   * @param {number} targetId 目标ID
   */
  isLiked(targetType, targetId) {
    return request.get(api.IsLiked, {
      params: { targetType, targetId }
    })
  },

  /**
   * 获取点赞数量
   * @param {string} targetType 目标类型
   * @param {number} targetId 目标ID
   */
  getLikeCount(targetType, targetId) {
    return request.get(api.GetLikeCount, {
      params: { targetType, targetId }
    })
  },

  /**
   * 批量检查点赞状态
   * @param {Array} targets 目标数组 [{targetType, targetId}]
   */
  batchCheckLikeStatus(targets) {
    return request.post(api.BatchCheckLikeStatus, targets)
  },

  /**
   * 获取用户点赞列表
   * @param {Object} params 查询参数
   */
  getUserLikes(params = {}) {
    return request.get(api.GetUserLikes, { params })
  },

  /**
   * 获取目标的点赞用户列表
   * @param {string} targetType 目标类型
   * @param {number} targetId 目标ID
   * @param {Object} params 分页参数
   */
  getTargetLikes(targetType, targetId, params = {}) {
    return request.get(api.GetTargetLikes, {
      params: { targetType, targetId, ...params }
    })
  },

  /**
   * 获取最近点赞记录
   * @param {number} count 数量
   */
  getRecentLikes(count = 10) {
    return request.get(api.GetRecentLikes, {
      params: { count }
    })
  }
}