import { Axios as axios } from '@/utils/plugin/axios-plugin'

const api = {
  GetDataList: '/Blog_Manage/Blog_Tool/GetDataList',
  GetTheData: '/Blog_Manage/Blog_Tool/GetTheData',
  SaveData: '/Blog_Manage/Blog_Tool/SaveData',
  DeleteData: '/Blog_Manage/Blog_Tool/DeleteData',
  GetRecommendedTools: '/Blog_Manage/Blog_Tool/GetRecommendedTools',
  GetLatestTools: '/Blog_Manage/Blog_Tool/GetLatestTools',
  GetCategories: '/Blog_Manage/Blog_Tool/GetCategories'
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

export function GetRecommendedTools(count = 10) {
  return axios({
    url: api.GetRecommendedTools,
    method: 'get',
    params: { count }
  })
}

export function GetLatestTools(count = 10) {
  return axios({
    url: api.GetLatestTools,
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