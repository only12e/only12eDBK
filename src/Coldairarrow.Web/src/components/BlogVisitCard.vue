<template>
  <a-card class="blog-visit-card" :hoverable="false">
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
            <span class="stat-value">{{ currentTodayVisits }}</span>
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
              <span class="stat-value">{{ currentTodayVisits }}</span>
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
            @click="visitBlogDirectly">
            <a-icon type="global" />
            外部访问
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
      localTodayVisits: 0, // 本地今日访问计数器
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
  computed: {
    // 计算当前今日访问次数（优先使用本地计数，如果为0则使用传入的prop）
    currentTodayVisits() {
      return Math.max(this.localTodayVisits, this.todayVisits || 0)
    }
  },
  watch: {
    // 监听传入的今日访问次数，与本地计数同步
    todayVisits: {
      handler(newVal) {
        if (newVal && newVal > this.localTodayVisits) {
          this.localTodayVisits = newVal
          // 同步到localStorage
          const today = new Date().toDateString()
          const visitKey = 'blog_visit_count_' + today
          localStorage.setItem(visitKey, newVal.toString())
        }
      },
      immediate: true
    }
  },
  methods: {
    visitBlog() {
      this.visiting = true
      try {
        // 立即访问：在当前窗口导航到本地博客页面
        this.$router.push('/blog-website')
        this.$message.success('欢迎来到博客网站！')
        
        // 增加今日访问计数
        this.incrementTodayVisit()
        
        // 模拟访问统计更新
        this.$emit('visit-tracked', {
          type: 'blog_home_visit',
          timestamp: new Date(),
          url: '/blog-website',
          from: 'admin_dashboard',
          visitType: 'internal'
        })
      } catch (error) {
        this.$message.error('跳转博客首页失败')
        console.error('访问博客失败:', error)
      } finally {
        setTimeout(() => {
          this.visiting = false
        }, 1000)
      }
    },
    
    visitBlogDirectly() {
      try {
        // 预览模式：在新窗口打开外部博客网站
        const blogUrl = this.blogUrl || 'http://192.168.124.23:5001/blog-website'
        window.open(blogUrl, '_blank', 'width=1200,height=800,scrollbars=yes,toolbar=yes,location=yes,status=yes,menubar=yes,resizable=yes')
        // this.$message.success('已在新窗口打开外部博客网站')
        
        // 增加今日访问计数
        this.incrementTodayVisit()
        
        this.$emit('visit-tracked', {
          type: 'blog_external_visit',
          url: blogUrl,
          timestamp: new Date(),
          mode: 'preview',
          visitType: 'external'
        })
      } catch (error) {
        this.$message.error('无法打开外部博客网站')
        console.error('打开外部链接失败:', error)
      }
    },
    
    
    quickVisit(link) {
      // 快速访问：直接跳转到本地博客页面
      this.$router.push('/blog-website')
      this.$message.success(`欢迎访问博客${link.label}！`)
      
      // 增加今日访问计数
      this.incrementTodayVisit()
      
      this.$emit('quick-visit', {
        type: 'quick',
        link: link.key,
        url: '/blog-website',
        targetPath: link.path,
        timestamp: new Date(),
        visitType: 'internal'
      })
    },
    
    // 增加今日访问计数
    incrementTodayVisit() {
      const today = new Date().toDateString()
      const visitKey = 'blog_visit_count_' + today
      
      try {
        // 从localStorage获取今日访问次数
        let count = parseInt(localStorage.getItem(visitKey) || '0')
        count++
        
        // 更新本地计数
        this.localTodayVisits = count
        localStorage.setItem(visitKey, count.toString())
        
        // 通知父组件更新访问统计
        this.$emit('visit-count-updated', {
          todayVisits: count,
          date: today,
          timestamp: new Date()
        })
        
        // console.log(`今日访问次数已更新: ${count}`)
      } catch (error) {
        console.error('更新访问统计失败:', error)
      }
    },
    
    // 获取今日访问次数
    getTodayVisitCount() {
      const today = new Date().toDateString()
      const visitKey = 'blog_visit_count_' + today
      
      try {
        const count = parseInt(localStorage.getItem(visitKey) || '0')
        this.localTodayVisits = count
        return count
      } catch (error) {
        console.error('获取访问统计失败:', error)
        return 0
      }
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
    },
    
    // 清理旧的访问记录（保留最近7天）
    cleanOldVisitRecords() {
      try {
        const now = new Date()
        const keys = []
        
        // 获取所有localStorage中的访问记录key
        for (let i = 0; i < localStorage.length; i++) {
          const key = localStorage.key(i)
          if (key && key.startsWith('blog_visit_count_')) {
            keys.push(key)
          }
        }
        
        // 清理7天前的记录
        keys.forEach(key => {
          const dateStr = key.replace('blog_visit_count_', '')
          const recordDate = new Date(dateStr)
          const daysDiff = Math.floor((now - recordDate) / (1000 * 60 * 60 * 24))
          
          if (daysDiff > 7) {
            localStorage.removeItem(key)
            console.log(`已清理过期访问记录: ${key}`)
          }
        })
      } catch (error) {
        console.error('清理访问记录失败:', error)
      }
    }
  },
  
  mounted() {
    // 初始化今日访问统计
    this.getTodayVisitCount()
    
    // 每30秒更新一次统计数据
    this.statsInterval = setInterval(() => {
      this.fetchOnlineStats()
    }, 30000)
    
    // 每天清理旧的访问记录
    this.cleanOldVisitRecords()
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
  background: rgba(255, 255, 255, 0.95);
  background-image: url('~@/assets/background.svg');
  background-size: cover;
  background-position: center;
  background-blend-mode: overlay;
  border: 1px solid rgba(255, 255, 255, 0.2);
  backdrop-filter: blur(10px);
  
  :deep(.ant-card-body) {
    padding: 0;
    background: transparent;
  }
  
  .visit-content {
    padding: 24px;
    color: #262626;
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
                color: #262626;
                font-family: 'Monaco', 'Menlo', monospace;
                line-height: 1.2;
              }
              
              .stat-label {
                font-size: 12px;
                color: #666;
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
        color: #1890ff;
        filter: none;
        animation: rotate 10s linear infinite;
      }
    }
    
    .header-text {
      flex: 1;
      
      .visit-title {
        margin: 0 0 4px 0;
        font-size: 20px;
        font-weight: 700;
        color: #262626;
        text-shadow: none;
        letter-spacing: 0.5px;
      }
      
      .visit-subtitle {
        margin: 0;
        font-size: 14px;
        color: #666;
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
      background: rgba(24, 144, 255, 0.1);
      border-radius: 12px;
      border: 1px solid rgba(24, 144, 255, 0.2);
      backdrop-filter: blur(10px);
      flex: 1;
      
      .anticon {
        font-size: 18px;
        color: #1890ff;
        margin-bottom: 8px;
      }
      
      .stat-label {
        font-size: 12px;
        color: #666;
        margin-bottom: 4px;
        font-weight: 500;
      }
      
      .stat-value {
        font-size: 18px;
        font-weight: 700;
        color: #262626;
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
      border: 2px solid rgba(24, 144, 255, 0.9);
      border-radius: 12px;
      background: rgba(24, 144, 255, 0.9);
      color: white;
      font-weight: 600;
      font-size: 16px;
      box-shadow: 0 4px 15px rgba(24, 144, 255, 0.4);
      transition: all 0.3s ease;
      
      &:hover, &:focus {
        background: rgba(64, 169, 255, 0.9);
        box-shadow: 0 6px 20px rgba(24, 144, 255, 0.6);
        transform: translateY(-2px);
        border: 2px solid rgba(64, 169, 255, 0.9);
        color: white;
      }
      
      &:active {
        transform: translateY(0);
      }
    }
    
    .preview-btn {
      flex: 1;
      height: 44px;
      border: 2px solid rgba(255, 255, 255, 0.6);
      border-radius: 12px;
      background: rgba(255, 255, 255, 0.8);
      color: #262626;
      font-weight: 500;
      backdrop-filter: blur(10px);
      transition: all 0.3s ease;
      
      &:hover, &:focus {
        background: rgba(255, 255, 255, 0.9);
        border-color: rgba(64, 169, 255, 0.6);
        color: #262626;
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
      backdrop-filter: blur(10px);
      
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