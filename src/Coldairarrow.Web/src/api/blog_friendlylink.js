import { Axios as axios } from '@/utils/plugin/axios-plugin'

const api = {
  dataList: '/Blog_Manage/Blog_FriendlyLink/GetDataList',
  theData: '/Blog_Manage/Blog_FriendlyLink/GetTheData',
  enabledLinks: '/Blog_Manage/Blog_FriendlyLink/GetEnabledLinks',
  sortedLinks: '/Blog_Manage/Blog_FriendlyLink/GetSortedLinks',
  saveData: '/Blog_Manage/Blog_FriendlyLink/SaveData',
  deleteData: '/Blog_Manage/Blog_FriendlyLink/DeleteData',
  batchUpdateStatus: '/Blog_Manage/Blog_FriendlyLink/BatchUpdateStatus',
  updateSortOrder: '/Blog_Manage/Blog_FriendlyLink/UpdateSortOrder',
  nameExists: '/Blog_Manage/Blog_FriendlyLink/NameExists',
  urlExists: '/Blog_Manage/Blog_FriendlyLink/UrlExists',
  toggleStatus: '/Blog_Manage/Blog_FriendlyLink/ToggleStatus'
}

export function GetDataList (parameter) {
  return axios({
    url: api.dataList,
    method: 'post',
    data: parameter
  })
}

export function GetTheData (parameter) {
  return axios({
    url: api.theData,
    method: 'post',
    data: parameter
  })
}

export function GetEnabledLinks () {
  return axios({
    url: api.enabledLinks,
    method: 'get'
  })
}

export function GetSortedLinks (status = null) {
  return axios({
    url: api.sortedLinks,
    method: 'get',
    params: { status }
  })
}

export function SaveData (parameter) {
  return axios({
    url: api.saveData,
    method: 'post',
    data: parameter
  })
}

export function DeleteData (parameter) {
  return axios({
    url: api.deleteData,
    method: 'post',
    data: parameter
  })
}

export function BatchUpdateStatus (parameter) {
  return axios({
    url: api.batchUpdateStatus,
    method: 'post',
    data: parameter
  })
}

export function UpdateSortOrder (parameter) {
  return axios({
    url: api.updateSortOrder,
    method: 'post',
    data: parameter
  })
}

export function NameExists (name, excludeId = null) {
  return axios({
    url: api.nameExists,
    method: 'get',
    params: { name, excludeId }
  })
}

export function UrlExists (url, excludeId = null) {
  return axios({
    url: api.urlExists,
    method: 'get',
    params: { url, excludeId }
  })
}

export function ToggleStatus (id) {
  return axios({
    url: api.toggleStatus,
    method: 'post',
    params: { id }
  })
}