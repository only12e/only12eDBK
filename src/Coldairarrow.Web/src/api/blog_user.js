import { Axios as axios } from '@/utils/plugin/axios-plugin'

const api = {
  GetDataList: '/Blog_Manage/Blog_User/GetDataList',
  GetTheData: '/Blog_Manage/Blog_User/GetTheData',
  SaveData: '/Blog_Manage/Blog_User/SaveData',
  DeleteData: '/Blog_Manage/Blog_User/DeleteData',
  GetLatestUsers: '/Blog_Manage/Blog_User/GetLatestUsers',
  GetActiveUsers: '/Blog_Manage/Blog_User/GetActiveUsers',
  GetUserCountByStatus: '/Blog_Manage/Blog_User/GetUserCountByStatus',
  GetUserCountByRole: '/Blog_Manage/Blog_User/GetUserCountByRole',
  BatchOperation: '/Blog_Manage/Blog_User/BatchOperation',
  ResetPassword: '/Blog_Manage/Blog_User/ResetPassword',
  CheckUsernameExists: '/Blog_Manage/Blog_User/CheckUsernameExists',
  CheckEmailExists: '/Blog_Manage/Blog_User/CheckEmailExists'
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

export function GetLatestUsers(count = 10) {
  return axios({
    url: api.GetLatestUsers,
    method: 'get',
    params: { count }
  })
}

export function GetActiveUsers(count = 10) {
  return axios({
    url: api.GetActiveUsers,
    method: 'get',
    params: { count }
  })
}

export function GetUserCountByStatus(status) {
  return axios({
    url: api.GetUserCountByStatus,
    method: 'get',
    params: { status }
  })
}

export function GetUserCountByRole(role) {
  return axios({
    url: api.GetUserCountByRole,
    method: 'get',
    params: { role }
  })
}

export function BatchOperation(parameter) {
  return axios({
    url: api.BatchOperation,
    method: 'post',
    data: parameter
  })
}

export function ResetPassword(parameter) {
  return axios({
    url: api.ResetPassword,
    method: 'post',
    data: parameter
  })
}

export function CheckUsernameExists(username, excludeId = null) {
  return axios({
    url: api.CheckUsernameExists,
    method: 'get',
    params: { username, excludeId }
  })
}

export function CheckEmailExists(email, excludeId = null) {
  return axios({
    url: api.CheckEmailExists,
    method: 'get',
    params: { email, excludeId }
  })
}