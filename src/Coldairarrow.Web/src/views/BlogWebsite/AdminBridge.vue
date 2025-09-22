<template>
  <div class="admin-bridge-container">
    <div class="bridge-content">
      <div class="loading-wrapper">
        <a-spin size="large" />
        <p class="loading-text">正在为您自动登录管理系统...</p>
      </div>
    </div>
  </div>
</template>

<script>
import { getBlogAdminToken } from '@/api/blog_auth'
import TokenCache from '@/utils/cache/TokenCache'

export default {
  name: 'BlogAdminBridge',
  mounted() {
    this.bridgeToAdmin()
  },
  methods: {
    async bridgeToAdmin() {
      try {
        const blogToken = localStorage.getItem('blog_token')
        if (!blogToken) {
          this.$message.error('博客登录状态已失效')
          this.$router.replace('/blog-website')
          return
        }

        // 调用后端API获取管理系统token
        const response = await getBlogAdminToken(blogToken)
        
        if (response.Success && response.AdminToken) {
          // 设置管理系统token
          TokenCache.setToken(response.AdminToken)
          
          // 跳转到目标管理页面
          const redirect = this.$route.query.redirect || '/'
          this.$router.replace(redirect)
        } else {
          this.$message.error(response.Message || '权限不足，无法访问管理系统')
          this.$router.replace('/blog-website')
        }
      } catch (error) {
        console.error('自动登录管理系统失败:', error)
        this.$message.error('自动登录失败，请稍后重试')
        this.$router.replace('/blog-website')
      }
    }
  }
}
</script>

<style lang="less" scoped>
.admin-bridge-container {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: #f5f5f5;
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 9999;

  .bridge-content {
    text-align: center;
    padding: 40px;
    background: white;
    border-radius: 12px;
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);

    .loading-wrapper {
      .loading-text {
        margin-top: 20px;
        font-size: 16px;
        color: #666;
        margin-bottom: 0;
      }
    }
  }
}
</style>