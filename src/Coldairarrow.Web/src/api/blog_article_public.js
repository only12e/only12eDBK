import { Axios as request } from '@/utils/plugin/axios-plugin'

// 管理端API - 文章管理相关接口（复用现有的管理端接口）
const api = {
  GetDataList: '/Blog_Manage/Blog_Article/GetDataList',
  GetTheData: '/Blog_Manage/Blog_Article/GetTheData',
  SaveData: '/Blog_Manage/Blog_Article/SaveData',
  DeleteData: '/Blog_Manage/Blog_Article/DeleteData',
  PublishData: '/Blog_Manage/Blog_Article/PublishData',
  GetFeaturedArticles: '/Blog_Manage/Blog_Article/GetFeaturedArticles',
  GetLatestArticles: '/Blog_Manage/Blog_Article/GetLatestArticles',
  GetCategories: '/Blog_Manage/Blog_Article/GetCategories',
  SaveCategory: '/Blog_Manage/Blog_Article/SaveCategory',
  DeleteCategory: '/Blog_Manage/Blog_Article/DeleteCategory'
}

// ============= 管理端API方法 =============

export function GetDataList(parameter) {
  return request({
    url: api.GetDataList,
    method: 'post',
    data: parameter
  })
}

export function GetTheData(parameter) {
  return request({
    url: api.GetTheData,
    method: 'post',
    data: parameter
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

export function PublishData(parameter) {
  return request({
    url: api.PublishData,
    method: 'post',
    data: parameter
  })
}

export function GetFeaturedArticles(count = 10) {
  return request({
    url: api.GetFeaturedArticles,
    method: 'get',
    params: { count }
  })
}

export function GetLatestArticles(count = 10) {
  return request({
    url: api.GetLatestArticles,
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

export function SaveCategory(parameter) {
  return request({
    url: api.SaveCategory,
    method: 'post',
    data: parameter
  })
}

export function DeleteCategory(parameter) {
  return request({
    url: api.DeleteCategory,
    method: 'post',
    data: parameter
  })
}

// ============= 公开页面使用的方法 =============

/**
 * 获取文章分页列表（公开页面使用）
 * @param {Object} parameter 查询参数
 * @param {string} parameter.keyword 关键词搜索
 * @param {number} parameter.categoryId 分类ID
 * @param {number} parameter.isFeatured 是否精选
 * @param {number} parameter.PageIndex 页码
 * @param {number} parameter.PageRows 每页数量
 */
export function GetArticleListForPublic(parameter) {
  // 添加只显示已发布文章的条件
  const params = {
    ...parameter,
    status: 'published' // 只显示已发布的文章
  }

  return request({
    url: api.GetDataList,
    method: 'post',
    data: params
  })
}

/**
 * 获取文章详情（公开页面使用）
 * @param {number} id 文章ID
 */
export function GetArticleDetailForPublic(id) {
  return request({
    url: api.GetTheData,
    method: 'post',
    data: { id }
  })
}

/**
 * 点赞文章
 * @param {number} articleId 文章ID
 */
export function LikeArticle(articleId) {
  return request({
    url: '/Blog_Manage/Blog_Article/LikeArticle',
    method: 'post',
    data: { articleId }
  })
}

/**
 * 增加文章访问量
 * @param {number} articleId 文章ID
 */
export function IncrementArticleViewCount(articleId) {
  return request({
    url: '/Blog_Manage/Blog_Article/IncrementViewCount',
    method: 'post',
    data: { articleId }
  })
}