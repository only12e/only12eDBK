import { Axios as axios } from '@/utils/plugin/axios-plugin'

const api = {
  GetDataList: '/Blog_Manage/Blog_Project/GetDataList',
  GetTheData: '/Blog_Manage/Blog_Project/GetTheData',
  SaveData: '/Blog_Manage/Blog_Project/SaveData',
  DeleteData: '/Blog_Manage/Blog_Project/DeleteData',
  GetFeaturedProjects: '/Blog_Manage/Blog_Project/GetFeaturedProjects',
  GetLatestProjects: '/Blog_Manage/Blog_Project/GetLatestProjects'
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

export function GetFeaturedProjects(count = 10) {
  return axios({
    url: api.GetFeaturedProjects,
    method: 'get',
    params: { count }
  })
}

export function GetLatestProjects(count = 10) {
  return axios({
    url: api.GetLatestProjects,
    method: 'get',
    params: { count }
  })
}