import { Axios as request } from '@/utils/plugin/axios-plugin'

// 管理端API - 工具管理相关接口（复用现有的管理端接口）
const api = {
  GetDataList: '/Blog_Manage/Blog_Tool/GetDataList',
  GetTheData: '/Blog_Manage/Blog_Tool/GetTheData',
  GetRecommendedTools: '/Blog_Manage/Blog_Tool/GetRecommendedTools',
  GetLatestTools: '/Blog_Manage/Blog_Tool/GetLatestTools',
  GetCategories: '/Blog_Manage/Blog_Tool/GetCategories'
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

export function GetRecommendedTools(count = 10) {
  return request({
    url: api.GetRecommendedTools,
    method: 'get',
    params: { count }
  })
}

export function GetLatestTools(count = 10) {
  return request({
    url: api.GetLatestTools,
    method: 'get',
    params: { count }
  })
}

export function GetCategories() {
  return request({
    url: api.GetCategories,
    method: 'get'
  })
}

// ============= 公开页面使用的方法（参考技术文章和项目页面模式）=============

/**
 * 获取工具分页列表（公开页面使用，类似技术文章和项目页面）
 * @param {Object} parameter 查询参数
 * @param {string} parameter.keyword 关键词搜索
 * @param {string} parameter.category 工具分类
 * @param {string} parameter.priceType 价格类型
 * @param {number} parameter.isRecommended 是否推荐
 * @param {number} parameter.PageIndex 页码
 * @param {number} parameter.PageRows 每页数量
 */
export function GetToolListForPublic(parameter) {
  // 不需要特殊的状态筛选，直接使用原始参数
  return request({
    url: api.GetDataList,
    method: 'post',
    data: parameter
  })
}

/**
 * 获取工具详情（公开页面使用）
 * @param {number} id 工具ID
 */
export function GetToolDetailForPublic(id) {
  return request({
    url: api.GetTheData,
    method: 'post',
    data: { id }
  })
}

/**
 * 获取工具分类列表（公开页面使用）
 */
export function GetToolCategoriesForPublic() {
  return request({
    url: api.GetCategories,
    method: 'get'
  })
}

/**
 * 获取推荐工具列表（公开页面使用）
 * @param {number} count 数量
 */
export function GetRecommendedToolsForPublic(count = 6) {
  return request({
    url: api.GetRecommendedTools,
    method: 'get',
    params: { count }
  })
}

/**
 * 获取最新工具列表（公开页面使用）
 * @param {number} count 数量
 */
export function GetLatestToolsForPublic(count = 6) {
  return request({
    url: api.GetLatestTools,
    method: 'get',
    params: { count }
  })
}