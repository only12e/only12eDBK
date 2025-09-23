import { Axios as axios } from '@/utils/plugin/axios-plugin'

const api = {
  GetDataList: '/Blog_Manage/Blog_Comment/GetDataList',
  GetTheData: '/Blog_Manage/Blog_Comment/GetTheData',
  SaveData: '/Blog_Manage/Blog_Comment/SaveData',
  DeleteData: '/Blog_Manage/Blog_Comment/DeleteData',
  GetCommentsByTarget: '/Blog_Manage/Blog_Comment/GetCommentsByTarget',
  GetLatestComments: '/Blog_Manage/Blog_Comment/GetLatestComments',
  GetPendingComments: '/Blog_Manage/Blog_Comment/GetPendingComments',
  BatchApproval: '/Blog_Manage/Blog_Comment/BatchApproval',
  GetCommentCountByStatus: '/Blog_Manage/Blog_Comment/GetCommentCountByStatus',
  GetRepliesByParentId: '/Blog_Manage/Blog_Comment/GetRepliesByParentId',
  LikeComment: '/Blog_Manage/Blog_Comment/LikeComment',
  PostComment: '/Blog_Manage/Blog_Comment/PostComment',
  GetArticleComments: '/Blog_Manage/Blog_Comment/GetArticleComments'
}

export function GetDataList(parameter) {
  return axios({
    url: api.GetDataList,
    method: 'post',
    data: parameter
  })
}

export function GetTheData(id) {
  return axios({
    url: api.GetTheData,
    method: 'post',
    data: { id }
  })
}

export function SaveData(parameter) {
  return axios({
    url: api.SaveData,
    method: 'post',
    data: parameter
  })
}

export function DeleteData(parameter) {
  return axios({
    url: api.DeleteData,
    method: 'post',
    data: parameter
  })
}

export function GetCommentsByTarget(targetType, targetId, count = 10) {
  return axios({
    url: api.GetCommentsByTarget,
    method: 'get',
    params: { targetType, targetId, count }
  })
}

export function GetLatestComments(count = 10) {
  return axios({
    url: api.GetLatestComments,
    method: 'get',
    params: { count }
  })
}

export function GetPendingComments(count = 10) {
  return axios({
    url: api.GetPendingComments,
    method: 'get',
    params: { count }
  })
}

export function BatchApproval(parameter) {
  return axios({
    url: api.BatchApproval,
    method: 'post',
    data: parameter
  })
}

export function GetCommentCountByStatus(status) {
  return axios({
    url: api.GetCommentCountByStatus,
    method: 'get',
    params: { status }
  })
}

export function GetRepliesByParentId(parentId) {
  return axios({
    url: api.GetRepliesByParentId, // 恢复使用原来的接口
    method: 'get',
    params: { parentId }
  })
}

export function LikeComment(commentId) {
  return axios({
    url: api.LikeComment,
    method: 'post',
    params: { commentId }
  })
}

export function PostComment(parameter) {
  return axios({
    url: api.SaveData, // 恢复使用原来的接口
    method: 'post',
    data: parameter
  })
}

export function GetArticleComments(articleId, page = 1, pageSize = 10) {
  return axios({
    url: api.GetCommentsByTarget, // 恢复使用原来的接口
    method: 'get',
    params: {
      targetType: 'article',
      targetId: articleId,
      count: pageSize
    }
  })
}