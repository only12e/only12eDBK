<template>
  <div class="user-profile-container">
    <!-- 背景动态效果 -->
    <div class="background-animation">
      <div class="background-image"></div>
      <div class="particles-container">
        <div class="particle" v-for="n in 20" :key="n"
             :style="{
               left: Math.random() * 100 + '%',
               top: Math.random() * 100 + '%',
               animationDelay: Math.random() * 20 + 's',
               animationDuration: (Math.random() * 10 + 15) + 's'
             }">
        </div>
      </div>
    </div>

    <!-- 顶部导航 -->
    <header class="blog-header">
      <div class="header-content">
        <div class="logo-section">
          <div class="logo" @click="goHome">
            <div class="logo-icon-wrapper">
              <a-icon type="global" class="logo-icon" />
            </div>
            <div class="logo-text">
              <span class="site-name">OnlyBlog</span>
              <p class="tagline">个人资料</p>
            </div>
          </div>
        </div>

        <div class="nav-links">
          <a-button type="link" @click="goHome">
            <a-icon type="left" />
            返回首页
          </a-button>
        </div>
      </div>
    </header>

    <!-- 主要内容区域 -->
    <main class="main-content" v-if="!loading">
      <div class="profile-container">
        <div class="profile-wrapper">
          <!-- 用户信息卡片 -->
          <div class="user-info-card glass-card">
            <div class="avatar-section">
              <div class="avatar-wrapper">
                <a-avatar :size="120" :src="userInfo.Avatar" class="user-avatar">
                  {{ getAvatarText(userInfo.Nickname || userInfo.Username) }}
                </a-avatar>
                <div class="avatar-upload-overlay" @click="showAvatarUpload">
                  <a-icon type="camera" />
                  <span>更换头像</span>
                </div>
              </div>
              <div class="user-basic-info">
                <h2 class="user-name">{{ userInfo.Nickname || userInfo.Username }}</h2>
                <p class="user-role">{{ getRoleText(userInfo.Role) }}</p>
                <p class="join-date">加入时间：{{ formatTime(userInfo.CreatedAt) }}</p>
              </div>
            </div>
          </div>

          <!-- 个人资料编辑 -->
          <div class="profile-edit-card glass-card">
            <div class="card-header">
              <h3>
                <a-icon type="user" />
                个人资料
              </h3>
            </div>
            <div class="card-content">
              <a-form-model ref="profileForm" :model="profileForm" :rules="profileRules" :label-col="{ span: 6 }" :wrapper-col="{ span: 18 }">
                <a-form-model-item label="用户名" prop="Username">
                  <a-input v-model="profileForm.Username" disabled>
                    <a-icon slot="prefix" type="user" />
                  </a-input>
                </a-form-model-item>

                <a-form-model-item label="昵称" prop="Nickname">
                  <a-input v-model="profileForm.Nickname" placeholder="请输入昵称">
                    <a-icon slot="prefix" type="idcard" />
                  </a-input>
                </a-form-model-item>

                <a-form-model-item label="邮箱" prop="Email">
                  <a-input v-model="profileForm.Email" placeholder="请输入邮箱地址">
                    <a-icon slot="prefix" type="mail" />
                  </a-input>
                </a-form-model-item>

                <a-form-model-item label="角色">
                  <a-input :value="getRoleText(userInfo.Role)" disabled>
                    <a-icon slot="prefix" type="crown" />
                  </a-input>
                </a-form-model-item>

                <a-form-model-item :wrapper-col="{ span: 18, offset: 6 }">
                  <a-button type="primary" @click="updateProfile" :loading="updating">
                    <a-icon type="save" />
                    保存资料
                  </a-button>
                </a-form-model-item>
              </a-form-model>
            </div>
          </div>

          <!-- 密码修改 -->
          <div class="password-change-card glass-card">
            <div class="card-header">
              <h3>
                <a-icon type="lock" />
                修改密码
              </h3>
            </div>
            <div class="card-content">
              <a-form-model ref="passwordForm" :model="passwordForm" :rules="passwordRules" :label-col="{ span: 6 }" :wrapper-col="{ span: 18 }">
                <a-form-model-item label="当前密码" prop="currentPassword">
                  <a-input-password v-model="passwordForm.currentPassword" placeholder="请输入当前密码">
                    <a-icon slot="prefix" type="lock" />
                  </a-input-password>
                </a-form-model-item>

                <a-form-model-item label="新密码" prop="newPassword">
                  <a-input-password v-model="passwordForm.newPassword" placeholder="请输入新密码">
                    <a-icon slot="prefix" type="key" />
                  </a-input-password>
                </a-form-model-item>

                <a-form-model-item label="确认密码" prop="confirmPassword">
                  <a-input-password v-model="passwordForm.confirmPassword" placeholder="请再次输入新密码">
                    <a-icon slot="prefix" type="safety-certificate" />
                  </a-input-password>
                </a-form-model-item>

                <a-form-model-item :wrapper-col="{ span: 18, offset: 6 }">
                  <a-button type="primary" @click="changeUserPassword" :loading="changingPassword">
                    <a-icon type="save" />
                    修改密码
                  </a-button>
                </a-form-model-item>
              </a-form-model>
            </div>
          </div>
        </div>
      </div>
    </main>

    <!-- 加载状态 -->
    <div v-else class="loading-state">
      <div class="loading-content">
        <a-spin size="large" />
        <p>正在加载用户信息...</p>
      </div>
    </div>

    <!-- 头像上传弹窗 -->
    <a-modal
      title="更换头像"
      :visible="avatarUploadVisible"
      @ok="handleAvatarUpload"
      @cancel="cancelAvatarUpload"
      :confirmLoading="uploadingAvatar"
    >
      <div class="avatar-upload-content">
        <a-upload
          :show-upload-list="false"
          :before-upload="beforeAvatarUpload"
          accept="image/*"
        >
          <div class="upload-area">
            <div v-if="previewAvatar" class="preview-avatar">
              <img :src="previewAvatar" alt="预览头像" />
            </div>
            <div v-else class="upload-placeholder">
              <a-icon type="plus" />
              <div class="upload-text">点击选择头像</div>
            </div>
          </div>
        </a-upload>
        <div class="upload-tips">
          <p>支持 JPG、PNG 格式，文件大小不超过 2MB</p>
          <p>建议上传正方形图片，获得最佳显示效果</p>
        </div>
      </div>
    </a-modal>

    <!-- 底部 -->
    <footer class="blog-footer">
      <div class="footer-content glass-card">
        <div class="footer-left">
          <div class="footer-logo">
            <a-icon type="global" />
            <span>OnlyBlog</span>
          </div>
          <p class="footer-text">&copy; 2025 OnlyBlog. All rights reserved.</p>
        </div>

        <div class="footer-links">
          <a href="javascript:void(0)" class="footer-link" @click="goHome">返回首页</a>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
import { getCurrentUser, updateUserProfile, changePassword, uploadAvatar } from '@/api/blog_auth'

export default {
  name: 'UserProfile',
  data() {
    return {
      loading: true,
      userInfo: {},

      // 个人资料表单
      profileForm: {
        Username: '',
        Nickname: '',
        Email: ''
      },
      profileRules: {
        Nickname: [
          { max: 50, message: '昵称不能超过50个字符', trigger: 'blur' }
        ],
        Email: [
          { type: 'email', message: '请输入有效的邮箱地址', trigger: 'blur' },
          { max: 100, message: '邮箱地址不能超过100个字符', trigger: 'blur' }
        ]
      },
      updating: false,

      // 密码修改表单
      passwordForm: {
        currentPassword: '',
        newPassword: '',
        confirmPassword: ''
      },
      passwordRules: {
        currentPassword: [
          { required: true, message: '请输入当前密码', trigger: 'blur' }
        ],
        newPassword: [
          { required: true, message: '请输入新密码', trigger: 'blur' },
          { min: 6, message: '密码至少6个字符', trigger: 'blur' },
          { max: 50, message: '密码不能超过50个字符', trigger: 'blur' }
        ],
        confirmPassword: [
          { required: true, message: '请确认新密码', trigger: 'blur' },
          { validator: this.validateConfirmPassword, trigger: 'blur' }
        ]
      },
      changingPassword: false,

      // 头像上传
      avatarUploadVisible: false,
      uploadingAvatar: false,
      previewAvatar: '',
      avatarFile: null,

      // 粒子随机数
      Math: Math
    }
  },

  async mounted() {
    await this.loadUserInfo()
  },

  methods: {
    async loadUserInfo() {
      this.loading = true
      try {
        const response = await getCurrentUser()
        if (response.Success && response.Data) {
          this.userInfo = response.Data
          this.profileForm = {
            Username: this.userInfo.Username,
            Nickname: this.userInfo.Nickname || '',
            Email: this.userInfo.Email || ''
          }
        } else {
          this.$message.error(response.Message || '加载用户信息失败')
        }
      } catch (error) {
        console.error('加载用户信息失败:', error)
        this.$message.error('加载用户信息失败')
      } finally {
        this.loading = false
      }
    },

    async updateProfile() {
      this.$refs.profileForm.validate(async (valid) => {
        if (!valid) return

        this.updating = true
        try {
          const response = await updateUserProfile({
            nickname: this.profileForm.Nickname,
            email: this.profileForm.Email
          })

          if (response.Success) {
            this.$message.success('个人资料更新成功!')
            // 更新本地用户信息
            this.userInfo.Nickname = this.profileForm.Nickname
            this.userInfo.Email = this.profileForm.Email
          } else {
            this.$message.error(response.Message || '更新失败')
          }
        } catch (error) {
          console.error('更新个人资料失败:', error)
          this.$message.error('更新失败，请稍后重试')
        } finally {
          this.updating = false
        }
      })
    },

    validateConfirmPassword(rule, value, callback) {
      if (value !== this.passwordForm.newPassword) {
        callback(new Error('两次输入的密码不一致'))
      } else {
        callback()
      }
    },

    async changeUserPassword() {
      this.$refs.passwordForm.validate(async (valid) => {
        if (!valid) return

        this.changingPassword = true
        try {
          const response = await changePassword({
            currentPassword: this.passwordForm.currentPassword,
            newPassword: this.passwordForm.newPassword,
            confirmPassword: this.passwordForm.confirmPassword
          })

          if (response.Success) {
            this.$message.success('密码修改成功!')
            // 清空表单
            this.passwordForm = {
              currentPassword: '',
              newPassword: '',
              confirmPassword: ''
            }
            this.$refs.passwordForm.clearValidate()
          } else {
            this.$message.error(response.Message || '密码修改失败')
          }
        } catch (error) {
          console.error('密码修改失败:', error)
          this.$message.error('密码修改失败，请稍后重试')
        } finally {
          this.changingPassword = false
        }
      })
    },

    showAvatarUpload() {
      this.avatarUploadVisible = true
      this.previewAvatar = this.userInfo.Avatar
    },

    cancelAvatarUpload() {
      this.avatarUploadVisible = false
      this.previewAvatar = ''
      this.avatarFile = null
    },

    beforeAvatarUpload(file) {
      const isImage = file.type.startsWith('image/')
      if (!isImage) {
        this.$message.error('只能上传图片文件!')
        return false
      }

      const isLt2M = file.size / 1024 / 1024 < 2
      if (!isLt2M) {
        this.$message.error('图片大小不能超过 2MB!')
        return false
      }

      this.avatarFile = file
      // 预览图片
      const reader = new FileReader()
      reader.onload = (e) => {
        this.previewAvatar = e.target.result
      }
      reader.readAsDataURL(file)

      return false // 阻止自动上传
    },

    async handleAvatarUpload() {
      if (!this.avatarFile) {
        this.$message.warning('请先选择头像文件')
        return
      }

      this.uploadingAvatar = true
      try {
        const formData = new FormData()
        formData.append('avatar', this.avatarFile)

        const response = await uploadAvatar(formData)
        if (response.Success) {
          this.$message.success('头像上传成功!')
          // 更新用户头像
          this.userInfo.Avatar = response.Data.AvatarUrl
          this.cancelAvatarUpload()
        } else {
          this.$message.error(response.Message || '头像上传失败')
        }
      } catch (error) {
        console.error('头像上传失败:', error)
        this.$message.error('头像上传失败，请稍后重试')
      } finally {
        this.uploadingAvatar = false
      }
    },

    goHome() {
      this.$router.push('/blog-website')
    },

    getRoleText(role) {
      const roleMap = {
        'admin': '管理员',
        'user': '普通用户'
      }
      return roleMap[role] || role
    },

    getAvatarText(name) {
      if (!name) return 'U'
      return name.charAt(0).toUpperCase()
    },

    formatTime(timeStr) {
      if (!timeStr) return '-'

      try {
        const date = new Date(timeStr)
        return date.toLocaleDateString('zh-CN', {
          year: 'numeric',
          month: 'long',
          day: 'numeric'
        })
      } catch (error) {
        return timeStr.split(' ')[0]
      }
    },

    // 鼠标交互效果已移除，保留粒子自然动画
  }
}
</script>

<style lang="less" scoped>
// 全局样式变量
:root {
  --primary-gradient: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  --glass-bg: rgba(255, 255, 255, 0.6);
  --glass-border: rgba(102, 126, 234, 0.15);
  --glass-shadow: 0 8px 32px rgba(102, 126, 234, 0.1);
  --text-primary: rgba(51, 51, 51, 0.9);
  --text-secondary: rgba(102, 102, 102, 0.8);
  --text-muted: rgba(153, 153, 153, 0.7);
}

// 玻璃形态样式
.glass-card {
  background: var(--glass-bg);
  backdrop-filter: blur(20px);
  -webkit-backdrop-filter: blur(20px);
  border: 1px solid var(--glass-border);
  box-shadow: var(--glass-shadow);
  border-radius: 16px;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);

  &:hover {
    background: rgba(255, 255, 255, 0.15);
    border-color: rgba(255, 255, 255, 0.3);
    box-shadow: 0 12px 48px rgba(31, 38, 135, 0.25);
  }
}

// 主容器
.user-profile-container {
  min-height: 100vh;
  background: #ffffff;
  position: relative;
  overflow-x: hidden;
}

// 背景动画
.background-animation {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  pointer-events: none;
  z-index: 0;

  .background-image {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background-image: url('~@/assets/background.svg');
    background-size: cover;
    background-position: center;
    background-repeat: no-repeat;
    opacity: 0.05;
    z-index: 1;
  }

  .particles-container {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 2;

    .particle {
      position: absolute;
      width: 2px;
      height: 2px;
      background: radial-gradient(circle, rgba(102, 126, 234, 0.4) 0%, transparent 70%);
      border-radius: 50%;
      animation: float-particle linear infinite;
    }
  }
}

// 头部导航
.blog-header {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 100;
  padding: 20px 0;
  background: rgba(255, 255, 255, 0.9);
  backdrop-filter: blur(20px);
  border-bottom: 1px solid rgba(102, 126, 234, 0.1);

  .header-content {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    display: flex;
    align-items: center;
    justify-content: space-between;

    .logo {
      display: flex;
      align-items: center;
      gap: 12px;
      cursor: pointer;

      .logo-icon-wrapper {
        width: 48px;
        height: 48px;
        background: var(--primary-gradient);
        border-radius: 12px;
        display: flex;
        align-items: center;
        justify-content: center;

        .logo-icon {
          font-size: 24px;
          color: #1890ff;
        }
      }

      .logo-text {
        .site-name {
          font-size: 24px;
          font-weight: 700;
          color: var(--text-primary);
          display: block;
          line-height: 1.2;
        }

        .tagline {
          font-size: 12px;
          color: var(--text-secondary);
          margin: 0;
        }
      }
    }

    .nav-links {
      ::v-deep .ant-btn-link {
        color: var(--text-secondary);
        padding: 8px 16px;

        &:hover {
          color: #667eea;
        }
      }
    }
  }
}

// 主内容
.main-content {
  position: relative;
  z-index: 10;
  padding: 120px 0 80px;
}

.profile-container {
  max-width: 1000px;
  margin: 0 auto;
  padding: 0 40px;
}

.profile-wrapper {
  display: flex;
  flex-direction: column;
  gap: 32px;
}

// 用户信息卡片
.user-info-card {
  padding: 40px;

  .avatar-section {
    display: flex;
    align-items: center;
    gap: 32px;

    .avatar-wrapper {
      position: relative;

      .user-avatar {
        background: var(--primary-gradient);
        color: white;
        font-size: 48px;
        font-weight: 600;
      }

      .avatar-upload-overlay {
        position: absolute;
        top: 0;
        left: 0;
        right: 0;
        bottom: 0;
        background: rgba(0, 0, 0, 0.6);
        border-radius: 50%;
        display: flex;
        flex-direction: column;
        align-items: center;
        justify-content: center;
        opacity: 0;
        transition: opacity 0.3s ease;
        cursor: pointer;
        color: white;
        font-size: 14px;

        &:hover {
          opacity: 1;
        }

        .anticon {
          font-size: 24px;
          margin-bottom: 4px;
        }
      }
    }

    .user-basic-info {
      .user-name {
        font-size: 32px;
        font-weight: 800;
        color: var(--text-primary);
        margin-bottom: 8px;
      }

      .user-role {
        font-size: 16px;
        color: var(--text-secondary);
        margin-bottom: 4px;
      }

      .join-date {
        font-size: 14px;
        color: var(--text-muted);
        margin: 0;
      }
    }
  }
}

// 卡片通用样式
.profile-edit-card,
.password-change-card {
  .card-header {
    padding: 24px 32px 0;
    border-bottom: 1px solid rgba(0, 0, 0, 0.06);
    margin-bottom: 32px;

    h3 {
      font-size: 20px;
      font-weight: 600;
      color: var(--text-primary);
      display: flex;
      align-items: center;
      gap: 8px;
      margin: 0;
      padding-bottom: 16px;
    }
  }

  .card-content {
    padding: 0 32px 32px;
  }
}

// 头像上传弹窗
.avatar-upload-content {
  text-align: center;

  .upload-area {
    width: 200px;
    height: 200px;
    margin: 0 auto 16px;
    border: 2px dashed #d9d9d9;
    border-radius: 8px;
    cursor: pointer;
    position: relative;
    overflow: hidden;
    transition: border-color 0.3s ease;

    &:hover {
      border-color: #667eea;
    }

    .preview-avatar {
      width: 100%;
      height: 100%;

      img {
        width: 100%;
        height: 100%;
        object-fit: cover;
      }
    }

    .upload-placeholder {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      height: 100%;
      color: #999;

      .anticon {
        font-size: 48px;
        margin-bottom: 12px;
      }

      .upload-text {
        font-size: 14px;
      }
    }
  }

  .upload-tips {
    color: var(--text-muted);
    font-size: 12px;

    p {
      margin: 4px 0;
    }
  }
}

// 状态页面
.loading-state {
  position: relative;
  z-index: 10;
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;

  .loading-content {
    text-align: center;
    padding: 60px 40px;

    p {
      margin-top: 20px;
      font-size: 16px;
      color: var(--text-secondary);
    }
  }
}

// 底部
.blog-footer {
  padding: 40px 0;
  margin-top: 80px;

  .footer-content {
    max-width: 1400px;
    margin: 0 auto;
    padding: 24px 40px;
    display: flex;
    justify-content: space-between;
    align-items: center;

    .footer-left {
      .footer-logo {
        display: flex;
        align-items: center;
        gap: 8px;
        font-size: 18px;
        font-weight: 600;
        color: var(--text-primary);
        margin-bottom: 8px;

        .anticon {
          font-size: 20px;
          color: #667eea;
        }
      }

      .footer-text {
        font-size: 14px;
        color: var(--text-secondary);
        margin: 0;
      }
    }

    .footer-links {
      display: flex;
      gap: 32px;

      .footer-link {
        font-size: 14px;
        color: var(--text-secondary);
        text-decoration: none;
        cursor: pointer;
        transition: color 0.3s ease;

        &:hover {
          color: var(--text-primary);
        }
      }
    }
  }
}

// 动画
@keyframes float-particle {
  0% {
    transform: translateY(100vh) translateX(0px);
    opacity: 0;
  }
  10% {
    opacity: 1;
  }
  90% {
    opacity: 1;
  }
  100% {
    transform: translateY(-100px) translateX(30px);
    opacity: 0;
  }
}

// 响应式设计
@media (max-width: 768px) {
  .blog-header .header-content {
    padding: 0 20px;
  }

  .profile-container {
    padding: 0 20px;
  }

  .user-info-card {
    padding: 24px;

    .avatar-section {
      flex-direction: column;
      text-align: center;
      gap: 24px;
    }
  }

  .profile-edit-card,
  .password-change-card {
    .card-header {
      padding: 20px 24px 0;
    }

    .card-content {
      padding: 0 24px 24px;
    }
  }

  .footer-content {
    padding: 24px 20px;
    flex-direction: column;
    gap: 16px;
    text-align: center;
  }
}
</style>