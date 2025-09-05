import { Axios as axios } from '@/utils/plugin/axios-plugin'

const api = {
  overview: '/Blog_Manage/Blog_Statistics/GetOverview',
  trendStatistics: '/Blog_Manage/Blog_Statistics/GetTrendStatistics',
  popularContent: '/Blog_Manage/Blog_Statistics/GetPopularContent',
  articleStatistics: '/Blog_Manage/Blog_Statistics/GetArticleStatistics',
  projectStatistics: '/Blog_Manage/Blog_Statistics/GetProjectStatistics',
  toolStatistics: '/Blog_Manage/Blog_Statistics/GetToolStatistics',
  technologyStatistics: '/Blog_Manage/Blog_Statistics/GetTechnologyStatistics',
  commentStatistics: '/Blog_Manage/Blog_Statistics/GetCommentStatistics',
  userStatistics: '/Blog_Manage/Blog_Statistics/GetUserStatistics',
  accessStatistics: '/Blog_Manage/Blog_Statistics/GetAccessStatistics'
}

export function GetOverview () {
  return axios({
    url: api.overview,
    method: 'get'
  })
}

export function GetTrendStatistics (parameter) {
  return axios({
    url: api.trendStatistics,
    method: 'post',
    data: parameter
  })
}

export function GetPopularContent (count = 10) {
  return axios({
    url: api.popularContent,
    method: 'get',
    params: { count }
  })
}

export function GetArticleStatistics () {
  return axios({
    url: api.articleStatistics,
    method: 'get'
  })
}

export function GetProjectStatistics () {
  return axios({
    url: api.projectStatistics,
    method: 'get'
  })
}

export function GetToolStatistics () {
  return axios({
    url: api.toolStatistics,
    method: 'get'
  })
}

export function GetTechnologyStatistics () {
  return axios({
    url: api.technologyStatistics,
    method: 'get'
  })
}

export function GetCommentStatistics () {
  return axios({
    url: api.commentStatistics,
    method: 'get'
  })
}

export function GetUserStatistics () {
  return axios({
    url: api.userStatistics,
    method: 'get'
  })
}

export function GetAccessStatistics () {
  return axios({
    url: api.accessStatistics,
    method: 'get'
  })
}