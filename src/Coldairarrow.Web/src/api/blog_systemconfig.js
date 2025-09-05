import { Axios as axios } from '@/utils/plugin/axios-plugin'

const api = {
  dataList: '/Blog_Manage/Blog_SystemConfig/GetDataList',
  theData: '/Blog_Manage/Blog_SystemConfig/GetTheData',
  allConfigs: '/Blog_Manage/Blog_SystemConfig/GetAllConfigs',
  configValue: '/Blog_Manage/Blog_SystemConfig/GetConfigValue',
  configValues: '/Blog_Manage/Blog_SystemConfig/GetConfigValues',
  saveData: '/Blog_Manage/Blog_SystemConfig/SaveData',
  deleteData: '/Blog_Manage/Blog_SystemConfig/DeleteData',
  batchUpdate: '/Blog_Manage/Blog_SystemConfig/BatchUpdate',
  setConfigValue: '/Blog_Manage/Blog_SystemConfig/SetConfigValue',
  deleteConfig: '/Blog_Manage/Blog_SystemConfig/DeleteConfig',
  configKeyExists: '/Blog_Manage/Blog_SystemConfig/ConfigKeyExists'
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

export function GetAllConfigs () {
  return axios({
    url: api.allConfigs,
    method: 'get'
  })
}

export function GetConfigValue (configKey) {
  return axios({
    url: api.configValue,
    method: 'get',
    params: { configKey }
  })
}

export function GetConfigValues (configKeys) {
  return axios({
    url: api.configValues,
    method: 'post',
    data: configKeys
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

export function BatchUpdate (parameter) {
  return axios({
    url: api.batchUpdate,
    method: 'post',
    data: parameter
  })
}

export function SetConfigValue (configKey, configValue, description = null) {
  return axios({
    url: api.setConfigValue,
    method: 'post',
    params: { configKey, configValue, description }
  })
}

export function DeleteConfig (configKey) {
  return axios({
    url: api.deleteConfig,
    method: 'post',
    params: { configKey }
  })
}

export function ConfigKeyExists (configKey, excludeId = null) {
  return axios({
    url: api.configKeyExists,
    method: 'get',
    params: { configKey, excludeId }
  })
}