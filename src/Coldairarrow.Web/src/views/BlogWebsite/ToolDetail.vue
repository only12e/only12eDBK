<template>
  <div class="tool-detail-container">
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
              <p class="tagline">工具详情</p>
            </div>
          </div>
        </div>
        
        <div class="nav-links">
          <a-button type="link" @click="goToTools">
            <a-icon type="left" />
            返回列表
          </a-button>
        </div>
        
        <div class="header-actions">
          <a-button type="primary" @click="goToAdmin" class="admin-btn glass-btn">
            <a-icon type="setting" />
            <span>管理后台</span>
          </a-button>
        </div>
      </div>
    </header>

    <!-- 主要内容区域 -->
    <main class="main-content" v-if="!loading && tool">
      <!-- 工具头部 -->
      <section class="tool-header">
        <div class="header-content-wrapper">
          <!-- 面包屑导航 -->
          <div class="breadcrumb glass-card">
            <a @click="goHome" class="breadcrumb-link">首页</a>
            <a-icon type="right" />
            <a @click="goToTools" class="breadcrumb-link">工具推荐</a>
            <a-icon type="right" />
            <span class="current">{{ tool.Name }}</span>
          </div>

          <!-- 工具基本信息 -->
          <div class="tool-main-info">
            <div class="tool-icon-section">
              <div class="tool-icon">
                <img v-if="tool.IconUrl" :src="tool.IconUrl" :alt="tool.Name" />
                <div v-else class="default-icon">
                  <a-icon type="tool" />
                </div>
              </div>
            </div>

            <div class="tool-info-section">
              <div class="tool-tags">
                <span v-if="tool.IsRecommended" class="tag recommended">
                  <a-icon type="star" />
                  推荐工具
                </span>
                <span class="tag category">{{ tool.Category || '未分类' }}</span>
                <span class="tag price-type" :class="tool.PriceType">
                  {{ getPriceTypeText(tool.PriceType) }}
                </span>
              </div>
              
              <h1 class="tool-name">{{ tool.Name }}</h1>
              
              <p v-if="tool.Description" class="tool-description">{{ tool.Description }}</p>
              
              <div class="tool-rating" v-if="tool.Rating > 0">
                <a-rate :value="tool.Rating" disabled allow-half />
                <span class="rating-text">{{ tool.Rating }} 分</span>
              </div>

              <!-- 工具操作按钮 -->
              <div class="tool-actions">
                <a v-if="tool.OfficialUrl" 
                   :href="tool.OfficialUrl" 
                   target="_blank" 
                   class="action-button primary">
                  <a-icon type="link" />
                  <span>访问官网</span>
                </a>
                <a v-if="tool.DownloadUrl" 
                   :href="tool.DownloadUrl" 
                   target="_blank" 
                   class="action-button secondary">
                  <a-icon type="download" />
                  <span>立即下载</span>
                </a>
                <a-button @click="shareToolPage" class="action-button share">
                  <a-icon type="share-alt" />
                  <span>分享工具</span>
                </a-button>
              </div>
            </div>
          </div>

          <!-- 工具封面图片 -->
          <div v-if="tool.CoverImage" class="tool-cover">
            <img :src="tool.CoverImage" :alt="tool.Name" />
          </div>
        </div>
      </section>

      <!-- 工具详细内容 -->
      <section class="tool-content-section">
        <div class="content-wrapper">
          <div class="main-content-area">
            <!-- 工具详细内容 -->
            <div class="tool-content glass-card">
              <h3>工具介绍</h3>
              <div class="content-html" v-html="tool.Content || '暂无详细介绍'"></div>
            </div>

            <!-- 相关工具推荐 -->
            <div v-if="relatedTools.length > 0" class="related-tools glass-card">
              <h3>相关工具推荐</h3>
              <div class="tools-grid">
                <div 
                  v-for="relatedTool in relatedTools" 
                  :key="relatedTool.Id"
                  class="tool-item"
                  @click="goToTool(relatedTool.Id)"
                >
                  <div class="tool-icon">
                    <img v-if="relatedTool.IconUrl" :src="relatedTool.IconUrl" :alt="relatedTool.Name" />
                    <div v-else class="default-icon">
                      <a-icon type="tool" />
                    </div>
                  </div>
                  <div class="tool-info">
                    <div class="tool-name">{{ relatedTool.Name }}</div>
                    <div class="tool-description">{{ relatedTool.Description }}</div>
                    <div class="tool-meta">
                      <span class="category">{{ relatedTool.Category || '未分类' }}</span>
                      <span class="price-type" :class="relatedTool.PriceType">
                        {{ getPriceTypeText(relatedTool.PriceType) }}
                      </span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- 侧边栏 -->
          <aside class="sidebar">
            <!-- 工具信息卡 -->
            <div class="tool-info-card glass-card">
              <h4>工具信息</h4>
              <div class="info-list">
                <div class="info-item">
                  <span class="info-label">分类：</span>
                  <span class="info-value">{{ tool.Category || '未分类' }}</span>
                </div>
                <div class="info-item">
                  <span class="info-label">价格：</span>
                  <span class="info-value price-type" :class="tool.PriceType">
                    {{ getPriceTypeText(tool.PriceType) }}
                  </span>
                </div>
                <div class="info-item" v-if="tool.Rating > 0">
                  <span class="info-label">评分：</span>
                  <span class="info-value">{{ tool.Rating }} / 5</span>
                </div>
                <div class="info-item" v-if="tool.IsRecommended">
                  <span class="info-label">推荐状态：</span>
                  <span class="info-value recommended">
                    <a-icon type="star" />
                    推荐工具
                  </span>
                </div>
                <div class="info-item">
                  <span class="info-label">添加时间：</span>
                  <span class="info-value">{{ formatTime(tool.CreatedAt) }}</span>
                </div>
                <div class="info-item">
                  <span class="info-label">更新时间：</span>
                  <span class="info-value">{{ formatTime(tool.UpdatedAt || tool.CreatedAt) }}</span>
                </div>
              </div>
            </div>

            <!-- 快速链接 -->
            <div class="quick-links glass-card">
              <h4>快速链接</h4>
              <div class="link-list">
                <a v-if="tool.OfficialUrl" 
                   :href="tool.OfficialUrl" 
                   target="_blank" 
                   class="link-item official">
                  <a-icon type="link" />
                  <span>官方网站</span>
                  <a-icon type="external-link" />
                </a>
                <a v-if="tool.DownloadUrl" 
                   :href="tool.DownloadUrl" 
                   target="_blank" 
                   class="link-item download">
                  <a-icon type="download" />
                  <span>下载地址</span>
                  <a-icon type="external-link" />
                </a>
              </div>
            </div>

            <!-- 返回顶部 -->
            <div class="back-to-top glass-card" @click="backToTop" v-show="showBackTop">
              <a-icon type="up" />
            </div>
          </aside>
        </div>
      </section>
    </main>

    <!-- 加载状态 -->
    <div v-else-if="loading" class="loading-state">
      <div class="loading-content">
        <a-spin size="large" />
        <p>正在加载工具信息...</p>
      </div>
    </div>

    <!-- 错误状态 -->
    <div v-else-if="error" class="error-state">
      <div class="error-content glass-card">
        <a-icon type="exclamation-circle" class="error-icon" />
        <h3>加载失败</h3>
        <p>{{ error }}</p>
        <a-button type="primary" @click="loadTool">重新加载</a-button>
        <a-button @click="goToTools" style="margin-left: 12px;">返回列表</a-button>
      </div>
    </div>

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
          <a href="javascript:void(0)" class="footer-link" @click="goToTools">工具推荐</a>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
import { GetToolDetailForPublic, GetToolListForPublic } from '@/api/blog_tool_public'

export default {
  name: 'BlogToolDetail',
  data() {
    return {
      loading: true,
      error: null,
      tool: null,
      relatedTools: [],
      showBackTop: false,
      
      // 粒子随机数
      Math: Math
    }
  },
  
  async mounted() {
    this.initMouseEffect()
    this.initScrollListener()
    await this.loadTool()
  },
  
  beforeDestroy() {
    window.removeEventListener('scroll', this.handleScroll)
  },
  
  methods: {
    async loadTool() {
      const toolId = this.$route.params.id
      
      if (!toolId) {
        this.error = '工具ID不存在'
        this.loading = false
        return
      }
      
      this.loading = true
      this.error = null
      
      try {
        const response = await GetToolDetailForPublic(toolId)
        
        if (response.Success && response.Data) {
          this.tool = response.Data
          
          // 加载相关工具
          this.loadRelatedTools()
          
        } else {
          throw new Error(response.Message || '工具不存在或已被删除')
        }
      } catch (error) {
        console.error('加载工具失败:', error)
        this.error = error.message || '加载工具失败'
      } finally {
        this.loading = false
      }
    },
    
    async loadRelatedTools() {
      try {
        let relatedTools = []
        
        // 优先加载同分类的工具
        if (this.tool.Category) {
          const response = await GetToolListForPublic({
            PageIndex: 1,
            PageRows: 8,
            category: this.tool.Category
          })
          
          if (response.Success && response.Data) {
            // 排除当前工具，并限制到4个
            relatedTools = response.Data
              .filter(tool => tool.Id !== this.tool.Id)
              .slice(0, 4)
          }
        }
        
        // 如果相关工具不足4个，用推荐工具补充
        if (relatedTools.length < 4) {
          const response = await GetToolListForPublic({
            PageIndex: 1,
            PageRows: 8,
            isRecommended: 1
          })
          
          if (response.Success && response.Data) {
            const recommendedTools = response.Data
              .filter(tool => 
                tool.Id !== this.tool.Id && 
                !relatedTools.some(related => related.Id === tool.Id)
              )
              .slice(0, 4 - relatedTools.length)
            
            relatedTools = [...relatedTools, ...recommendedTools]
          }
        }
        
        this.relatedTools = relatedTools
        
      } catch (error) {
        console.error('加载相关工具失败:', error)
        this.relatedTools = []
      }
    },
    
    shareToolPage() {
      const url = window.location.href
      const title = this.tool.Name
      
      if (navigator.share) {
        navigator.share({ title, url })
      } else {
        // 复制链接到剪贴板
        navigator.clipboard.writeText(url).then(() => {
          this.$message.success('链接已复制到剪贴板')
        }).catch(() => {
          this.$message.error('分享失败')
        })
      }
    },
    
    initScrollListener() {
      const handleScroll = () => {
        this.showBackTop = window.pageYOffset > 300
      }
      
      this.handleScroll = handleScroll
      window.addEventListener('scroll', handleScroll, { passive: true })
    },
    
    backToTop() {
      window.scrollTo({ top: 0, behavior: 'smooth' })
    },
    
    // 导航方法
    goHome() {
      this.$router.push('/blog-website')
    },
    
    goToTools() {
      this.$router.push('/blog-website/tools')
    },
    
    goToAdmin() {
      this.$router.push('/')
      this.$message.success('正在跳转到管理后台...')
    },
    
    goToTool(toolId) {
      this.$router.push(`/blog-website/tools/${toolId}`)
    },
    
    // 工具方法
    getPriceTypeText(priceType) {
      const typeMap = {
        'free': '免费',
        'paid': '付费',
        'freemium': '免费增值'
      }
      return typeMap[priceType] || priceType
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
    
    // 鼠标交互效果
    initMouseEffect() {
      document.addEventListener('mousemove', (e) => {
        const particles = document.querySelectorAll('.particle')
        const x = e.clientX / window.innerWidth
        const y = e.clientY / window.innerHeight
        
        particles.forEach((particle, index) => {
          const speed = (index + 1) * 0.2
          const xOffset = (x - 0.5) * speed * 8
          const yOffset = (y - 0.5) * speed * 8
          
          particle.style.transform = `translate(${xOffset}px, ${yOffset}px)`
        })
      })
    }
  }
}
</script>

<style lang="less" scoped>
// 复用项目详情页面的样式结构，并适配工具展示
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

.glass-btn {
  background: var(--glass-bg);
  backdrop-filter: blur(20px);
  -webkit-backdrop-filter: blur(20px);
  border: 1px solid var(--glass-border);
  color: var(--text-primary);
  border-radius: 12px;
  padding: 12px 24px;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  
  &:hover {
    transform: translateY(-2px);
    box-shadow: 0 8px 32px rgba(102, 126, 234, 0.2);
  }
}

// 主容器
.tool-detail-container {
  min-height: 100vh;
  background: #ffffff;
  position: relative;
  overflow-x: hidden;
}

// 背景动画（复用）
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

// 头部导航（复用）
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
          color: white;
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
    
    .admin-btn {
      display: flex;
      align-items: center;
      gap: 8px;
      font-weight: 500;
    }
  }
}

// 主内容
.main-content {
  position: relative;
  z-index: 10;
  padding-top: 100px;
}

// 工具头部
.tool-header {
  padding: 40px 0 60px;
  
  .header-content-wrapper {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    
    .breadcrumb {
      display: inline-flex;
      align-items: center;
      gap: 8px;
      padding: 8px 16px;
      margin-bottom: 32px;
      font-size: 14px;
      
      .breadcrumb-link {
        color: var(--text-secondary);
        text-decoration: none;
        cursor: pointer;
        
        &:hover {
          color: #667eea;
        }
      }
      
      .current {
        color: var(--text-primary);
        font-weight: 500;
        max-width: 200px;
        overflow: hidden;
        text-overflow: ellipsis;
        white-space: nowrap;
      }
      
      .anticon {
        color: var(--text-muted);
        font-size: 12px;
      }
    }
    
    .tool-main-info {
      display: grid;
      grid-template-columns: auto 1fr;
      gap: 32px;
      align-items: start;
      margin-bottom: 40px;
      
      .tool-icon-section {
        .tool-icon {
          width: 120px;
          height: 120px;
          
          img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            border-radius: 20px;
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
          }
          
          .default-icon {
            width: 100%;
            height: 100%;
            background: var(--primary-gradient);
            border-radius: 20px;
            display: flex;
            align-items: center;
            justify-content: center;
            box-shadow: 0 8px 32px rgba(102, 126, 234, 0.2);
            
            .anticon {
              font-size: 48px;
              color: white;
            }
          }
        }
      }
      
      .tool-info-section {
        .tool-tags {
          display: flex;
          gap: 12px;
          margin-bottom: 16px;
          flex-wrap: wrap;
          
          .tag {
            display: flex;
            align-items: center;
            gap: 6px;
            padding: 6px 12px;
            border-radius: 16px;
            font-size: 14px;
            font-weight: 500;
            
            &.recommended {
              background: rgba(255, 165, 0, 0.1);
              color: #ff8c00;
            }
            
            &.category {
              background: rgba(102, 126, 234, 0.1);
              color: #667eea;
            }
            
            &.price-type {
              &.free {
                background: rgba(52, 199, 89, 0.1);
                color: #34c759;
              }
              
              &.paid {
                background: rgba(255, 59, 48, 0.1);
                color: #ff3b30;
              }
              
              &.freemium {
                background: rgba(0, 122, 255, 0.1);
                color: #007aff;
              }
            }
          }
        }
        
        .tool-name {
          font-size: 36px;
          font-weight: 800;
          color: var(--text-primary);
          line-height: 1.4;
          margin-bottom: 16px;
        }
        
        .tool-description {
          font-size: 18px;
          color: var(--text-secondary);
          line-height: 1.6;
          margin-bottom: 16px;
        }
        
        .tool-rating {
          display: flex;
          align-items: center;
          gap: 12px;
          margin-bottom: 24px;
          
          .rating-text {
            font-size: 16px;
            color: var(--text-secondary);
          }
        }
        
        .tool-actions {
          display: flex;
          gap: 16px;
          flex-wrap: wrap;
          
          .action-button {
            display: flex;
            align-items: center;
            gap: 8px;
            padding: 12px 24px;
            border-radius: 20px;
            font-size: 16px;
            font-weight: 500;
            text-decoration: none;
            transition: all 0.3s ease;
            border: none;
            cursor: pointer;
            
            &.primary {
              background: var(--primary-gradient);
              color: white;
              
              &:hover {
                transform: translateY(-2px);
                box-shadow: 0 8px 32px rgba(102, 126, 234, 0.3);
                color: white;
              }
            }
            
            &.secondary {
              background: rgba(52, 199, 89, 0.1);
              color: #34c759;
              
              &:hover {
                background: rgba(52, 199, 89, 0.2);
                transform: translateY(-2px);
                color: #34c759;
              }
            }
            
            &.share {
              background: rgba(0, 0, 0, 0.05);
              color: var(--text-primary);
              
              &:hover {
                background: rgba(0, 0, 0, 0.1);
                transform: translateY(-2px);
              }
            }
          }
        }
      }
    }
    
    .tool-cover {
      border-radius: 16px;
      overflow: hidden;
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
      
      img {
        width: 100%;
        height: 400px;
        object-fit: cover;
      }
    }
  }
}

// 工具内容区域
.tool-content-section {
  padding: 0 0 80px;
  
  .content-wrapper {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    display: grid;
    grid-template-columns: 1fr 300px;
    gap: 60px;
    align-items: start;
    
    .main-content-area {
      .tool-content {
        padding: 40px;
        margin-bottom: 40px;
        
        h3 {
          font-size: 24px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 24px;
        }
        
        .content-html {
          line-height: 1.8;
          font-size: 16px;
          color: var(--text-primary);
          
          ::v-deep {
            h1, h2, h3, h4, h5, h6 {
              color: var(--text-primary);
              font-weight: 600;
              margin: 32px 0 16px;
              line-height: 1.4;
              
              &:first-child {
                margin-top: 0;
              }
            }
            
            p {
              margin: 16px 0;
              text-align: justify;
            }
            
            code {
              background: rgba(102, 126, 234, 0.1);
              padding: 2px 6px;
              border-radius: 4px;
              font-family: 'Monaco', 'Menlo', monospace;
              font-size: 14px;
            }
            
            pre {
              background: #f8f9fa;
              padding: 20px;
              border-radius: 8px;
              overflow-x: auto;
              margin: 24px 0;
              
              code {
                background: none;
                padding: 0;
              }
            }
          }
        }
      }
      
      .related-tools {
        padding: 40px;
        
        h3 {
          font-size: 24px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 24px;
        }
        
        .tools-grid {
          display: grid;
          grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
          gap: 20px;
          
          .tool-item {
            padding: 20px;
            border-radius: 12px;
            cursor: pointer;
            transition: all 0.3s ease;
            border: 1px solid rgba(0, 0, 0, 0.05);
            
            &:hover {
              background: rgba(102, 126, 234, 0.05);
              transform: translateY(-2px);
            }
            
            .tool-icon {
              width: 48px;
              height: 48px;
              margin-bottom: 12px;
              
              img {
                width: 100%;
                height: 100%;
                object-fit: cover;
                border-radius: 8px;
              }
              
              .default-icon {
                width: 100%;
                height: 100%;
                background: var(--primary-gradient);
                border-radius: 8px;
                display: flex;
                align-items: center;
                justify-content: center;
                
                .anticon {
                  font-size: 24px;
                  color: white;
                }
              }
            }
            
            .tool-info {
              .tool-name {
                font-size: 16px;
                font-weight: 600;
                color: var(--text-primary);
                margin-bottom: 8px;
                display: -webkit-box;
                -webkit-line-clamp: 1;
                -webkit-box-orient: vertical;
                overflow: hidden;
              }
              
              .tool-description {
                font-size: 14px;
                color: var(--text-secondary);
                margin-bottom: 12px;
                display: -webkit-box;
                -webkit-line-clamp: 2;
                -webkit-box-orient: vertical;
                overflow: hidden;
              }
              
              .tool-meta {
                display: flex;
                gap: 12px;
                font-size: 12px;
                
                .category {
                  color: var(--text-muted);
                }
                
                .price-type {
                  padding: 2px 6px;
                  border-radius: 8px;
                  
                  &.free {
                    background: rgba(52, 199, 89, 0.1);
                    color: #34c759;
                  }
                  
                  &.paid {
                    background: rgba(255, 59, 48, 0.1);
                    color: #ff3b30;
                  }
                  
                  &.freemium {
                    background: rgba(0, 122, 255, 0.1);
                    color: #007aff;
                  }
                }
              }
            }
          }
        }
      }
    }
    
    .sidebar {
      position: sticky;
      top: 120px;
      display: flex;
      flex-direction: column;
      gap: 24px;
      
      .tool-info-card {
        padding: 20px;
        
        h4 {
          font-size: 16px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 16px;
        }
        
        .info-list {
          .info-item {
            display: flex;
            justify-content: space-between;
            align-items: center;
            padding: 8px 0;
            border-bottom: 1px solid rgba(0, 0, 0, 0.05);
            
            &:last-child {
              border-bottom: none;
            }
            
            .info-label {
              font-size: 14px;
              color: var(--text-secondary);
            }
            
            .info-value {
              font-size: 14px;
              color: var(--text-primary);
              font-weight: 500;
              
              &.price-type {
                &.free { color: #34c759; }
                &.paid { color: #ff3b30; }
                &.freemium { color: #007aff; }
              }
              
              &.recommended {
                color: #ff8c00;
                
                .anticon {
                  margin-right: 4px;
                }
              }
            }
          }
        }
      }
      
      .quick-links {
        padding: 20px;
        
        h4 {
          font-size: 16px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 16px;
        }
        
        .link-list {
          .link-item {
            display: flex;
            align-items: center;
            justify-content: space-between;
            padding: 12px;
            margin-bottom: 8px;
            border-radius: 8px;
            text-decoration: none;
            transition: all 0.3s ease;
            
            &:last-child {
              margin-bottom: 0;
            }
            
            &.official {
              background: rgba(102, 126, 234, 0.05);
              color: #667eea;
              
              &:hover {
                background: rgba(102, 126, 234, 0.1);
                color: #667eea;
              }
            }
            
            &.download {
              background: rgba(52, 199, 89, 0.05);
              color: #34c759;
              
              &:hover {
                background: rgba(52, 199, 89, 0.1);
                color: #34c759;
              }
            }
            
            span {
              flex: 1;
              margin: 0 8px;
            }
          }
        }
      }
      
      .back-to-top {
        width: 48px;
        height: 48px;
        display: flex;
        align-items: center;
        justify-content: center;
        cursor: pointer;
        align-self: flex-end;
        
        .anticon {
          font-size: 20px;
          color: #667eea;
        }
        
        &:hover {
          transform: translateY(-2px);
        }
      }
    }
  }
}

// 状态页面（复用）
.loading-state,
.error-state {
  position: relative;
  z-index: 10;
  min-height: 100vh;
  display: flex;
  align-items: center;
  justify-content: center;
  
  .loading-content,
  .error-content {
    text-align: center;
    padding: 60px 40px;
    
    p {
      margin-top: 20px;
      font-size: 16px;
      color: var(--text-secondary);
    }
    
    .error-icon {
      font-size: 64px;
      color: var(--text-muted);
      margin-bottom: 24px;
    }
    
    h3 {
      font-size: 24px;
      color: var(--text-primary);
      margin-bottom: 12px;
    }
  }
}

// 底部（复用）
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

// 动画（复用）
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
@media (max-width: 1200px) {
  .tool-content-section .content-wrapper {
    grid-template-columns: 1fr;
    gap: 40px;
    
    .sidebar {
      position: static;
      order: -1;
      
      .back-to-top {
        display: none;
      }
    }
  }
}

@media (max-width: 768px) {
  .blog-header .header-content {
    padding: 0 20px;
  }
  
  .tool-header .header-content-wrapper {
    padding: 0 20px;
    
    .tool-main-info {
      grid-template-columns: 1fr;
      gap: 20px;
      text-align: center;
      
      .tool-icon-section {
        justify-self: center;
      }
      
      .tool-info-section {
        .tool-name {
          font-size: 28px;
        }
        
        .tool-actions {
          justify-content: center;
        }
      }
    }
    
    .tool-cover img {
      height: 250px;
    }
  }
  
  .tool-content-section .content-wrapper {
    padding: 0 20px;
    
    .main-content-area {
      .tool-content {
        padding: 24px;
      }
      
      .related-tools {
        padding: 24px;
        
        .tools-grid {
          grid-template-columns: 1fr;
        }
      }
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