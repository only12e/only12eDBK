import { Axios as axios } from '@/utils/plugin/axios-plugin'

const api = {
  GetDataList: '/Blog_Manage/Blog_Technology/GetDataList',
  GetTheData: '/Blog_Manage/Blog_Technology/GetTheData',
  SaveData: '/Blog_Manage/Blog_Technology/SaveData',
  DeleteData: '/Blog_Manage/Blog_Technology/DeleteData',
  GetFeaturedTechnologies: '/Blog_Manage/Blog_Technology/GetFeaturedTechnologies',
  GetLatestTechnologies: '/Blog_Manage/Blog_Technology/GetLatestTechnologies',
  GetCategories: '/Blog_Manage/Blog_Technology/GetCategories',
  GetTechnologiesByCategory: '/Blog_Manage/Blog_Technology/GetTechnologiesByCategory'
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

export function GetFeaturedTechnologies(count = 10) {
  return axios({
    url: api.GetFeaturedTechnologies,
    method: 'get',
    params: { count }
  })
}

export function GetLatestTechnologies(count = 10) {
  return axios({
    url: api.GetLatestTechnologies,
    method: 'get',
    params: { count }
  })
}

export function GetCategories() {
  return axios({
    url: api.GetCategories,
    method: 'get'
  })
}

export function GetTechnologiesByCategory(category) {
  return axios({
    url: api.GetTechnologiesByCategory,
    method: 'get',
    params: { category }
  })
}