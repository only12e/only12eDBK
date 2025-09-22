import { Axios as request } from '@/utils/plugin/axios-plugin'

// 管理端API - 项目管理相关接口（复用现有的管理端接口）
const api = {
  GetDataList: '/Blog_Manage/Blog_Project/GetDataList',
  GetTheData: '/Blog_Manage/Blog_Project/GetTheData',
  SaveData: '/Blog_Manage/Blog_Project/SaveData',
  DeleteData: '/Blog_Manage/Blog_Project/DeleteData',
  GetFeaturedProjects: '/Blog_Manage/Blog_Project/GetFeaturedProjects',
  GetLatestProjects: '/Blog_Manage/Blog_Project/GetLatestProjects'
}

// ============= 管理端API方法 =============

export function GetDataList(parameter) {
  return request({
    url: api.GetDataList,
    method: 'post',
    data: parameter
  })
}

export function GetTheData(id) {
  return request({
    url: api.GetTheData,
    method: 'post',
    data: { id }
  })
}

export function SaveData(parameter) {
  return request({
    url: api.SaveData,
    method: 'post',
    data: parameter
  })
}

export function DeleteData(parameter) {
  return request({
    url: api.DeleteData,
    method: 'post',
    data: parameter
  })
}

export function GetFeaturedProjects(count = 10) {
  return request({
    url: api.GetFeaturedProjects,
    method: 'get',
    params: { count }
  })
}

export function GetLatestProjects(count = 10) {
  return request({
    url: api.GetLatestProjects,
    method: 'get',
    params: { count }
  })
}

// ============= 公开页面使用的方法（参考技术文章页面模式）=============

/**
 * 获取项目分页列表（公开页面使用，类似技术文章页面）
 * @param {Object} parameter 查询参数
 * @param {string} parameter.keyword 关键词搜索
 * @param {string} parameter.difficultyLevel 难度级别
 * @param {number} parameter.isFeatured 是否精选
 * @param {number} parameter.PageIndex 页码
 * @param {number} parameter.PageRows 每页数量
 */
export function GetProjectListForPublic(parameter) {
  // 添加只显示活跃项目的条件，类似文章页面只显示已发布的文章
  const params = {
    ...parameter,
    status: 'active' // 只显示进行中的项目
  }
  
  return request({
    url: api.GetDataList,
    method: 'post',
    data: params
  })
}

/**
 * 获取项目详情（公开页面使用）
 * @param {number} id 项目ID
 */
export function GetProjectDetailForPublic(id) {
  return request({
    url: api.GetTheData,
    method: 'post',
    data: { id }
  })
}

/**
 * 点赞项目
 * @param {number} projectId 项目ID
 */
export function LikeProject(projectId) {
  return request({
    url: '/Blog_Manage/Blog_Project/LikeProject',
    method: 'post',
    data: { projectId }
  })
}

/**
 * 增加项目访问量
 * @param {number} projectId 项目ID
 */
export function IncrementProjectViewCount(projectId) {
  return request({
    url: '/Blog_Manage/Blog_Project/IncrementViewCount',
    method: 'post',
    data: { projectId }
  })
}