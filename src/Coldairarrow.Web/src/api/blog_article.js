import { Axios as request } from '@/utils/plugin/axios-plugin'

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

export function GetDataList (parameter) {
  return request({
    url: api.GetDataList,
    method: 'post',
    data: parameter
  })
}

export function GetTheData (parameter) {
  return request({
    url: api.GetTheData,
    method: 'post',
    data: parameter
  })
}

export function SaveData (parameter) {
  return request({
    url: api.SaveData,
    method: 'post',
    data: parameter
  })
}

export function DeleteData (parameter) {
  return request({
    url: api.DeleteData,
    method: 'post',
    data: parameter
  })
}

export function PublishData (parameter) {
  return request({
    url: api.PublishData,
    method: 'post',
    data: parameter
  })
}

export function GetFeaturedArticles (count = 10) {
  return request({
    url: api.GetFeaturedArticles,
    method: 'get',
    params: { count }
  })
}

export function GetLatestArticles (count = 10) {
  return request({
    url: api.GetLatestArticles,
    method: 'get',
    params: { count }
  })
}

export function GetCategories () {
  return request({
    url: api.GetCategories,
    method: 'get'
  })
}

export function SaveCategory (parameter) {
  return request({
    url: api.SaveCategory,
    method: 'post',
    data: parameter
  })
}

export function DeleteCategory (parameter) {
  return request({
    url: api.DeleteCategory,
    method: 'post',
    data: parameter
  })
}