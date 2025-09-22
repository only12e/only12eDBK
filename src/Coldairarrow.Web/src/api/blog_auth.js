import { Axios } from '@/utils/plugin/axios-plugin'

const API_BASE = '/BlogApi'

/**
 * 博客用户登录
 * @param {Object} parameter - 登录参数
 * @param {string} parameter.username - 用户名或邮箱
 * @param {string} parameter.password - 密码
 * @param {boolean} parameter.rememberMe - 是否记住我
 */
export function blogLogin(parameter) {
  return Axios({
    url: `${API_BASE}/Login`,
    method: 'post',
    data: parameter
  })
}

/**
 * 博客用户注册
 * @param {Object} parameter - 注册参数
 * @param {string} parameter.username - 用户名
 * @param {string} parameter.email - 邮箱
 * @param {string} parameter.password - 密码
 * @param {string} parameter.confirmPassword - 确认密码
 * @param {string} parameter.firstName - 名字
 * @param {string} parameter.lastName - 姓氏
 */
export function blogRegister(parameter) {
  return Axios({
    url: `${API_BASE}/Register`,
    method: 'post',
    data: parameter
  })
}

/**
 * 检查用户名是否可用
 * @param {string} username - 用户名
 */
export function checkUsername(username) {
  return Axios({
    url: `${API_BASE}/CheckUsername`,
    method: 'get',
    params: { username }
  })
}

/**
 * 检查邮箱是否可用
 * @param {string} email - 邮箱
 */
export function checkEmail(email) {
  return Axios({
    url: `${API_BASE}/CheckEmail`,
    method: 'get',
    params: { email }
  })
}

/**
 * 验证令牌
 * @param {string} token - 令牌
 */
export function validateToken(token) {
  return Axios({
    url: `${API_BASE}/ValidateToken`,
    method: 'post',
    data: { token }
  })
}

/**
 * 发送邮箱验证码
 * @param {Object} parameter - 发送参数
 * @param {string} parameter.email - 邮箱地址
 * @param {string} parameter.purpose - 用途（register/reset）
 */
export function sendVerificationCode(parameter) {
  return Axios({
    url: `${API_BASE}/SendVerificationCode`,
    method: 'post',
    data: parameter
  })
}

/**
 * 重置密码（使用验证码）
 * @param {Object} parameter - 重置参数
 * @param {string} parameter.email - 邮箱地址
 * @param {string} parameter.verificationCode - 验证码
 * @param {string} parameter.newPassword - 新密码
 * @param {string} parameter.confirmPassword - 确认密码
 */
export function resetPassword(parameter) {
  return Axios({
    url: `${API_BASE}/ResetPassword`,
    method: 'post',
    data: parameter
  })
}

/**
 * 发送密码重置邮件
 * @param {Object} parameter - 重置参数
 * @param {string} parameter.email - 邮箱地址
 */
export function sendResetEmail(parameter) {
  return Axios({
    url: `${API_BASE}/SendResetEmail`,
    method: 'post',
    data: parameter
  })
}

/**
 * 博客管理员获取管理系统token
 * @param {string} blogToken - 博客令牌
 */
export function getBlogAdminToken(blogToken) {
  return Axios({
    url: `${API_BASE}/GetAdminToken`,
    method: 'post',
    data: { BlogToken: blogToken }
  })
}

/**
 * 获取当前用户信息
 */
export function getCurrentUser() {
  const blogToken = localStorage.getItem('blog_token')
  return Axios({
    url: `${API_BASE}/GetCurrentUser`,
    method: 'get',
    headers: {
      'Authorization': `Bearer ${blogToken || ''}`
    }
  })
}

/**
 * 更新用户个人资料
 * @param {Object} parameter - 更新参数
 * @param {string} parameter.nickname - 昵称
 * @param {string} parameter.email - 邮箱
 * @param {string} parameter.avatar - 头像URL
 */
export function updateUserProfile(parameter) {
  const blogToken = localStorage.getItem('blog_token')
  return Axios({
    url: `${API_BASE}/UpdateProfile`,
    method: 'post',
    data: parameter,
    headers: {
      'Authorization': `Bearer ${blogToken || ''}`
    }
  })
}

/**
 * 修改密码
 * @param {Object} parameter - 修改参数
 * @param {string} parameter.currentPassword - 当前密码
 * @param {string} parameter.newPassword - 新密码
 * @param {string} parameter.confirmPassword - 确认密码
 */
export function changePassword(parameter) {
  const blogToken = localStorage.getItem('blog_token')
  return Axios({
    url: `${API_BASE}/ChangePassword`,
    method: 'post',
    data: parameter,
    headers: {
      'Authorization': `Bearer ${blogToken || ''}`
    }
  })
}

/**
 * 上传用户头像
 * @param {FormData} formData - 包含头像文件的FormData
 */
export function uploadAvatar(formData) {
  const blogToken = localStorage.getItem('blog_token')
  return Axios({
    url: `${API_BASE}/UploadAvatar`,
    method: 'post',
    data: formData,
    headers: {
      'Content-Type': 'multipart/form-data',
      'Authorization': `Bearer ${blogToken || ''}`
    }
  })
}