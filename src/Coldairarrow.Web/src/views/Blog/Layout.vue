<template>
  <div class="blog-layout">
    <!-- 博客头部导航 -->
    <div class="blog-header">
      <div class="header-container">
        <div class="logo-section">
          <h1 class="blog-title">我的技术博客</h1>
        </div>
        <nav class="nav-menu">
          <router-link to="/blog/home" class="nav-item">首页</router-link>
          <router-link to="/blog/articles" class="nav-item">技术文章</router-link>
          <router-link to="/blog/projects" class="nav-item">项目展示</router-link>
          <router-link to="/blog/tools" class="nav-item">工具推荐</router-link>
          <router-link to="/blog/about" class="nav-item">关于我</router-link>
          <router-link to="/blog/contact" class="nav-item">联系方式</router-link>
        </nav>
        <div class="user-section">
          <div v-if="!isLoggedIn" class="auth-buttons">
            <router-link to="/blog/login" class="auth-btn login-btn">登录</router-link>
            <router-link to="/blog/register" class="auth-btn register-btn">注册</router-link>
          </div>
          <div v-else class="user-info">
            <img :src="userInfo.avatar || defaultAvatar" :alt="userInfo.nickname" class="user-avatar" />
            <span class="user-name">{{ userInfo.nickname || userInfo.username }}</span>
            <a-dropdown>
              <a-menu slot="overlay" @click="handleMenuClick">
                <a-menu-item key="profile">个人资料</a-menu-item>
                <a-menu-divider />
                <a-menu-item key="logout">退出登录</a-menu-item>
              </a-menu>
              <a-icon type="down" class="dropdown-icon" />
            </a-dropdown>
          </div>
        </div>
      </div>
    </div>

    <!-- 主要内容区域 -->
    <main class="blog-main">
      <router-view />
    </main>

    <!-- 博客底部 -->
    <footer class="blog-footer">
      <div class="footer-container">
        <div class="footer-content">
          <div class="footer-section">
            <h3>关于博客</h3>
            <p>分享技术心得，记录成长足迹</p>
          </div>
          <div class="footer-section">
            <h3>联系方式</h3>
            <p>Email: contact@example.com</p>
          </div>
          <div class="footer-section">
            <h3>友情链接</h3>
            <div class="links">
              <a v-for="link in friendlyLinks" :key="link.id" :href="link.url" target="_blank">
                {{ link.name }}
              </a>
            </div>
          </div>
        </div>
        <div class="footer-bottom">
          <p>&copy; 2024 我的技术博客. All rights reserved.</p>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
export default {
  name: 'BlogLayout',
  data() {
    return {
      isLoggedIn: false,
      userInfo: {},
      friendlyLinks: [],
      defaultAvatar: '/assets/default-avatar.png'
    }
  },
  methods: {
    handleMenuClick({ key }) {
      if (key === 'logout') {
        this.logout()
      } else if (key === 'profile') {
        this.$router.push('/blog/profile')
      }
    },
    
    async logout() {
      try {
        // 清除本地存储的用户信息和token
        localStorage.removeItem('blog_token')
        localStorage.removeItem('blog_user')
        this.isLoggedIn = false
        this.userInfo = {}
        this.$message.success('退出登录成功')
        this.$router.push('/blog/home')
      } catch (error) {
        this.$message.error('退出登录失败')
      }
    },

    checkLoginStatus() {
      const token = localStorage.getItem('blog_token')
      const userStr = localStorage.getItem('blog_user')
      if (token && userStr) {
        try {
          this.userInfo = JSON.parse(userStr)
          this.isLoggedIn = true
        } catch (error) {
          localStorage.removeItem('blog_token')
          localStorage.removeItem('blog_user')
        }
      }
    },

    async loadFriendlyLinks() {
      try {
        // 这里调用API获取友情链接
        // const response = await this.$http.get('/api/blog/friendly-links')
        // this.friendlyLinks = response.data
        this.friendlyLinks = [
          { id: 1, name: 'Vue.js官网', url: 'https://vuejs.org' },
          { id: 2, name: 'GitHub', url: 'https://github.com' }
        ]
      } catch (error) {
        console.error('加载友情链接失败:', error)
      }
    }
  },

  mounted() {
    this.checkLoginStatus()
    this.loadFriendlyLinks()
  }
}
</script>

<style lang="less" scoped>
.blog-layout {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
  background-color: #f5f5f5;
}

.blog-header {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
  position: sticky;
  top: 0;
  z-index: 1000;
  
  .header-container {
    max-width: 1200px;
    margin: 0 auto;
    padding: 0 20px;
    height: 70px;
    display: flex;
    align-items: center;
    justify-content: space-between;
  }
  
  .logo-section {
    .blog-title {
      color: white;
      font-size: 24px;
      font-weight: 700;
      margin: 0;
      text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
    }
  }
  
  .nav-menu {
    display: flex;
    gap: 30px;
    
    .nav-item {
      color: rgba(255, 255, 255, 0.9);
      text-decoration: none;
      font-weight: 500;
      font-size: 16px;
      padding: 8px 16px;
      border-radius: 20px;
      transition: all 0.3s ease;
      
      &:hover, &.router-link-active {
        background: rgba(255, 255, 255, 0.2);
        color: white;
        transform: translateY(-1px);
      }
    }
  }
  
  .user-section {
    .auth-buttons {
      display: flex;
      gap: 12px;
      
      .auth-btn {
        padding: 8px 20px;
        border-radius: 20px;
        text-decoration: none;
        font-weight: 500;
        transition: all 0.3s ease;
        
        &.login-btn {
          color: white;
          border: 2px solid rgba(255, 255, 255, 0.3);
          
          &:hover {
            background: rgba(255, 255, 255, 0.1);
            color: white;
          }
        }
        
        &.register-btn {
          background: #ff6b6b;
          color: white;
          border: 2px solid transparent;
          
          &:hover {
            background: #ff5252;
            transform: translateY(-1px);
            color: white;
          }
        }
      }
    }
    
    .user-info {
      display: flex;
      align-items: center;
      gap: 12px;
      color: white;
      cursor: pointer;
      
      .user-avatar {
        width: 36px;
        height: 36px;
        border-radius: 50%;
        border: 2px solid rgba(255, 255, 255, 0.3);
      }
      
      .user-name {
        font-weight: 500;
      }
      
      .dropdown-icon {
        font-size: 12px;
      }
    }
  }
}

.blog-main {
  flex: 1;
  max-width: 1200px;
  margin: 0 auto;
  padding: 40px 20px;
  width: 100%;
}

.blog-footer {
  background: #2c3e50;
  color: white;
  
  .footer-container {
    max-width: 1200px;
    margin: 0 auto;
    padding: 40px 20px 20px;
  }
  
  .footer-content {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
    gap: 40px;
    margin-bottom: 30px;
    
    .footer-section {
      h3 {
        color: #3498db;
        margin-bottom: 15px;
        font-size: 18px;
      }
      
      p {
        color: #bdc3c7;
        line-height: 1.6;
      }
      
      .links {
        display: flex;
        flex-direction: column;
        gap: 8px;
        
        a {
          color: #bdc3c7;
          text-decoration: none;
          transition: color 0.3s ease;
          
          &:hover {
            color: #3498db;
          }
        }
      }
    }
  }
  
  .footer-bottom {
    border-top: 1px solid #34495e;
    padding-top: 20px;
    text-align: center;
    
    p {
      color: #95a5a6;
      margin: 0;
    }
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-header {
    .header-container {
      flex-direction: column;
      height: auto;
      padding: 15px 20px;
      gap: 15px;
    }
    
    .nav-menu {
      gap: 15px;
      flex-wrap: wrap;
      justify-content: center;
      
      .nav-item {
        font-size: 14px;
        padding: 6px 12px;
      }
    }
  }
  
  .blog-main {
    padding: 20px 15px;
  }
  
  .blog-footer {
    .footer-content {
      grid-template-columns: 1fr;
      gap: 20px;
      text-align: center;
    }
  }
}
</style>