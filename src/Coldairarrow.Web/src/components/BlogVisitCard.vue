<template>
  <a-card class="blog-visit-card" :hoverable="true">
    <div class="visit-content" :class="{ 'horizontal-layout': isHorizontal }">
      <div class="visit-left-section">
        <div class="visit-header">
          <div class="header-icon">
            <a-icon type="global" class="globe-icon" />
          </div>
          <div class="header-text">
            <h3 class="visit-title">访问博客网站</h3>
            <p class="visit-subtitle">查看线上博客内容</p>
          </div>
        </div>
        
        <div class="visit-stats" v-if="!isHorizontal">
          <div class="stat-item">
            <a-icon type="eye" />
            <span class="stat-label">今日访问</span>
            <span class="stat-value">{{ todayVisits || 0 }}</span>
          </div>
          <div class="stat-item">
            <a-icon type="user" />
            <span class="stat-label">在线用户</span>
            <span class="stat-value">{{ onlineUsers || 0 }}</span>
          </div>
        </div>
      </div>
      
      <div class="visit-middle-section" v-if="isHorizontal">
        <div class="horizontal-stats">
          <div class="horizontal-stat-item">
            <a-icon type="eye" />
            <div class="stat-info">
              <span class="stat-value">{{ todayVisits || 0 }}</span>
              <span class="stat-label">今日访问</span>
            </div>
          </div>
          <div class="horizontal-stat-item">
            <a-icon type="user" />
            <div class="stat-info">
              <span class="stat-value">{{ onlineUsers || 0 }}</span>
              <span class="stat-label">在线用户</span>
            </div>
          </div>
        </div>
      </div>
      
      <div class="visit-right-section">
        <div class="visit-actions" :class="{ 'horizontal-actions': isHorizontal }">
          <a-button 
            type="primary" 
            :size="isHorizontal ? 'default' : 'large'"
            class="visit-btn"
            @click="visitBlog"
            :loading="visiting">
            <a-icon type="rocket" />
            立即访问
          </a-button>
          
          <a-button 
            type="default" 
            :size="isHorizontal ? 'default' : 'large'"
            class="preview-btn"
            @click="previewBlog">
            <a-icon type="desktop" />
            预览模式
          </a-button>
        </div>
        
        <div class="quick-links" v-if="!isHorizontal">
          <a-tag 
            v-for="link in quickLinks" 
            :key="link.key"
            :color="link.color"
            class="quick-link-tag"
            @click="quickVisit(link)">
            <a-icon :type="link.icon" />
            {{ link.label }}
          </a-tag>
        </div>
        
        <div class="horizontal-links" v-if="isHorizontal">
          <a-tag 
            v-for="link in quickLinks.slice(0, 4)" 
            :key="link.key"
            :color="link.color"
            class="horizontal-link-tag"
            @click="quickVisit(link)">
            <a-icon :type="link.icon" />
            {{ link.label }}
          </a-tag>
        </div>
      </div>
    </div>
  </a-card>
</template>

<script>
export default {
  name: 'BlogVisitCard',
  props: {
    blogUrl: {
      type: String,
      default: 'https://example.com'
    },
    todayVisits: {
      type: Number,
      default: 0
    },
    onlineUsers: {
      type: Number,
      default: 0
    },
    isHorizontal: {
      type: Boolean,
      default: false
    }
  },
  data() {
    return {
      visiting: false,
      quickLinks: [
        { key: 'home', label: '首页', icon: 'home', color: 'blue', path: '' },
        { key: 'articles', label: '技术文章', icon: 'file-text', color: 'green', path: '' },
        { key: 'projects', label: '项目展示', icon: 'project', color: 'orange', path: '' },
        { key: 'tools', label: '工具推荐', icon: 'tool', color: 'purple', path: '' },
        { key: 'about', label: '关于我', icon: 'user', color: 'cyan', path: '' },
        { key: 'contact', label: '联系方式', icon: 'mail', color: 'red', path: '' }
      ]
    }
  },
  methods: {
    visitBlog() {
      this.visiting = true
      try {
        // 构建内部博客路由URL - 使用完整路径避免路由嵌套问题
        const currentOrigin = window.location.origin
        const currentPort = window.location.port
        const baseUrl = currentPort ? `${currentOrigin}` : currentOrigin
        const blogUrl = `${baseUrl}/#/blog-website`
        
        // 直接在当前窗口导航而不是打开新窗口，避免路由嵌套
        this.$router.push('/blog-website')
        this.$message.success('正在跳转到博客首页...')
        
        // 模拟访问统计更新
        this.$emit('visit-tracked', {
          type: 'blog_home_visit',
          timestamp: new Date(),
          url: blogUrl,
          from: 'admin_dashboard'
        })
      } catch (error) {
        this.$message.error('打开博客首页失败')
        console.error('访问博客失败:', error)
      } finally {
        setTimeout(() => {
          this.visiting = false
        }, 1000)
      }
    },
    
    previewBlog() {
      // 预览模式在新窗口打开博客首页，使用完整URL确保独立性
      const currentOrigin = window.location.origin
      const previewUrl = `${currentOrigin}/#/blog-website`
      window.open(previewUrl, '_blank', 'width=1200,height=800,scrollbars=yes')
      this.$message.info('已在预览模式下打开博客首页')
      
      this.$emit('preview-opened', {
        url: previewUrl,
        timestamp: new Date(),
        mode: 'preview'
      })
    },
    
    quickVisit(link) {
      // 快速访问直接跳转到博客页面
      this.$router.push('/blog-website')
      this.$message.success(`正在跳转到博客${link.label}...`)
      
      this.$emit('quick-visit', {
        type: 'quick',
        link: link.key,
        url: '/blog-website',
        targetPath: link.path,
        timestamp: new Date()
      })
    },
    
    // 获取在线统计数据
    async fetchOnlineStats() {
      try {
        // 这里可以调用API获取实时统计数据
        // const response = await getBlogStats()
        // this.$emit('stats-updated', response.data)
      } catch (error) {
        console.error('获取统计数据失败:', error)
      }
    }
  },
  
  mounted() {
    // 每30秒更新一次统计数据
    this.statsInterval = setInterval(() => {
      this.fetchOnlineStats()
    }, 30000)
  },
  
  beforeDestroy() {
    if (this.statsInterval) {
      clearInterval(this.statsInterval)
    }
  }
}
</script>

<style lang="less" scoped>
.blog-visit-card {
  border-radius: 16px;
  overflow: hidden;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
  border: none;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  
  &:hover {
    box-shadow: 0 8px 30px rgba(102, 126, 234, 0.3);
    transform: translateY(-4px) scale(1.02);
  }
  
  :deep(.ant-card-body) {
    padding: 0;
    background: transparent;
  }
  
  .visit-content {
    padding: 24px;
    color: white;
    position: relative;
    overflow: hidden;
    
    /* 水平布局样式 */
    &.horizontal-layout {
      display: flex;
      align-items: center;
      gap: 32px;
      padding: 20px 32px;
      
      .visit-left-section {
        flex: 0 0 auto;
        
        .visit-header {
          margin-bottom: 0;
          
          .header-text {
            .visit-title {
              font-size: 18px;
              margin-bottom: 2px;
            }
            
            .visit-subtitle {
              font-size: 13px;
              margin: 0;
            }
          }
        }
      }
      
      .visit-middle-section {
        flex: 1;
        
        .horizontal-stats {
          display: flex;
          gap: 24px;
          justify-content: center;
          
          .horizontal-stat-item {
            display: flex;
            align-items: center;
            gap: 12px;
            padding: 12px 20px;
            background: rgba(255, 255, 255, 0.15);
            border-radius: 12px;
            border: 1px solid rgba(255, 255, 255, 0.2);
            backdrop-filter: blur(10px);
            min-width: 120px;
            transition: all 0.3s ease;
            
            &:hover {
              background: rgba(255, 255, 255, 0.25);
              transform: translateY(-2px);
            }
            
            .anticon {
              font-size: 20px;
              color: #ffd700;
            }
            
            .stat-info {
              display: flex;
              flex-direction: column;
              
              .stat-value {
                font-size: 18px;
                font-weight: 700;
                color: white;
                font-family: 'Monaco', 'Menlo', monospace;
                line-height: 1.2;
              }
              
              .stat-label {
                font-size: 12px;
                color: rgba(255, 255, 255, 0.8);
                font-weight: 500;
              }
            }
          }
        }
      }
      
      .visit-right-section {
        flex: 0 0 auto;
        
        .horizontal-actions {
          display: flex;
          gap: 12px;
          margin-bottom: 12px;
          
          .visit-btn, .preview-btn {
            height: 36px;
            font-size: 14px;
            padding: 0 16px;
          }
        }
        
        .horizontal-links {
          display: flex;
          gap: 8px;
          flex-wrap: wrap;
          
          .horizontal-link-tag {
            font-size: 11px;
            padding: 2px 8px;
            margin: 0;
            
            .anticon {
              margin-right: 4px;
              font-size: 12px;
            }
          }
        }
      }
    }
    
    &::before {
      content: '';
      position: absolute;
      top: -50%;
      right: -20%;
      width: 200px;
      height: 200px;
      background: radial-gradient(circle, rgba(255, 255, 255, 0.1) 0%, transparent 70%);
      border-radius: 50%;
      animation: float 6s ease-in-out infinite;
    }
    
    &::after {
      content: '';
      position: absolute;
      bottom: -30%;
      left: -10%;
      width: 150px;
      height: 150px;
      background: radial-gradient(circle, rgba(255, 255, 255, 0.08) 0%, transparent 70%);
      border-radius: 50%;
      animation: float 8s ease-in-out infinite reverse;
    }
  }
  
  .visit-header {
    display: flex;
    align-items: center;
    margin-bottom: 20px;
    position: relative;
    z-index: 2;
    
    .header-icon {
      margin-right: 16px;
      
      .globe-icon {
        font-size: 32px;
        color: #ffd700;
        filter: drop-shadow(0 2px 8px rgba(255, 215, 0, 0.4));
        animation: rotate 10s linear infinite;
      }
    }
    
    .header-text {
      flex: 1;
      
      .visit-title {
        margin: 0 0 4px 0;
        font-size: 20px;
        font-weight: 700;
        color: white;
        text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
        letter-spacing: 0.5px;
      }
      
      .visit-subtitle {
        margin: 0;
        font-size: 14px;
        color: rgba(255, 255, 255, 0.8);
        font-weight: 400;
      }
    }
  }
  
  .visit-stats {
    display: flex;
    gap: 24px;
    margin-bottom: 24px;
    position: relative;
    z-index: 2;
    
    .stat-item {
      display: flex;
      flex-direction: column;
      align-items: center;
      padding: 12px 16px;
      background: rgba(255, 255, 255, 0.15);
      border-radius: 12px;
      border: 1px solid rgba(255, 255, 255, 0.2);
      backdrop-filter: blur(10px);
      flex: 1;
      transition: all 0.3s ease;
      
      &:hover {
        background: rgba(255, 255, 255, 0.25);
        transform: translateY(-2px);
      }
      
      .anticon {
        font-size: 18px;
        color: #ffd700;
        margin-bottom: 8px;
      }
      
      .stat-label {
        font-size: 12px;
        color: rgba(255, 255, 255, 0.7);
        margin-bottom: 4px;
        font-weight: 500;
      }
      
      .stat-value {
        font-size: 18px;
        font-weight: 700;
        color: white;
        font-family: 'Monaco', 'Menlo', monospace;
      }
    }
  }
  
  .visit-actions {
    display: flex;
    gap: 12px;
    margin-bottom: 20px;
    position: relative;
    z-index: 2;
    
    .visit-btn {
      flex: 2;
      height: 44px;
      border: none;
      border-radius: 12px;
      background: linear-gradient(45deg, #ff6b6b, #ff5252);
      color: white;
      font-weight: 600;
      font-size: 16px;
      box-shadow: 0 4px 15px rgba(255, 107, 107, 0.4);
      transition: all 0.3s ease;
      
      &:hover, &:focus {
        background: linear-gradient(45deg, #ff5252, #ff4444);
        box-shadow: 0 6px 20px rgba(255, 107, 107, 0.6);
        transform: translateY(-2px);
        border: none;
        color: white;
      }
      
      &:active {
        transform: translateY(0);
      }
    }
    
    .preview-btn {
      flex: 1;
      height: 44px;
      border: 2px solid rgba(255, 255, 255, 0.3);
      border-radius: 12px;
      background: rgba(255, 255, 255, 0.1);
      color: white;
      font-weight: 500;
      backdrop-filter: blur(10px);
      transition: all 0.3s ease;
      
      &:hover, &:focus {
        background: rgba(255, 255, 255, 0.2);
        border-color: rgba(255, 255, 255, 0.5);
        color: white;
        transform: translateY(-2px);
      }
    }
  }
  
  .quick-links {
    display: flex;
    flex-wrap: wrap;
    gap: 8px;
    position: relative;
    z-index: 2;
    
    .quick-link-tag {
      border: none;
      border-radius: 20px;
      padding: 6px 12px;
      font-size: 12px;
      font-weight: 500;
      cursor: pointer;
      transition: all 0.3s ease;
      backdrop-filter: blur(10px);
      
      &:hover {
        transform: translateY(-2px) scale(1.05);
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.2);
      }
      
      .anticon {
        margin-right: 4px;
      }
    }
  }
}

@keyframes float {
  0%, 100% {
    transform: translateY(0) scale(1);
  }
  50% {
    transform: translateY(-20px) scale(1.1);
  }
}

@keyframes rotate {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}

  .visit-left-section,
  .visit-middle-section,
  .visit-right-section {
    position: relative;
    z-index: 2;
  }
  
  /* 响应式设计 */
  @media (max-width: 1200px) {
    .blog-visit-card .visit-content.horizontal-layout {
      gap: 24px;
      padding: 16px 24px;
      
      .visit-middle-section .horizontal-stats {
        gap: 16px;
        
        .horizontal-stat-item {
          min-width: 100px;
          padding: 10px 16px;
        }
      }
    }
  }
  
  @media (max-width: 992px) {
    .blog-visit-card .visit-content.horizontal-layout {
      flex-direction: column;
      gap: 16px;
      
      .visit-left-section,
      .visit-middle-section,
      .visit-right-section {
        flex: none;
        width: 100%;
      }
      
      .visit-middle-section .horizontal-stats {
        justify-content: space-around;
      }
      
      .visit-right-section {
        .horizontal-actions {
          justify-content: center;
        }
        
        .horizontal-links {
          justify-content: center;
        }
      }
    }
  }
@media (max-width: 768px) {
  .blog-visit-card {
    .visit-content {
      padding: 16px;
    }
    
    .visit-header {
      .header-icon .globe-icon {
        font-size: 24px;
      }
      
      .header-text .visit-title {
        font-size: 16px;
      }
    }
    
    .visit-stats {
      gap: 12px;
      
      .stat-item {
        padding: 8px 12px;
        
        .stat-value {
          font-size: 14px;
        }
      }
    }
    
    .visit-actions {
      flex-direction: column;
      gap: 8px;
      
      .visit-btn, .preview-btn {
        height: 40px;
        font-size: 14px;
      }
    }
    
    .quick-links .quick-link-tag {
      font-size: 11px;
      padding: 4px 8px;
    }
  }
}
</style>