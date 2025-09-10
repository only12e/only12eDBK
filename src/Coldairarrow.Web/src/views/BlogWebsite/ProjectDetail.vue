<template>
  <div class="project-detail-container">
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
              <p class="tagline">项目详情</p>
            </div>
          </div>
        </div>
        
        <div class="nav-links">
          <a-button type="link" @click="goToProjects">
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
    <main class="main-content" v-if="!loading && project">
      <!-- 项目头部 -->
      <section class="project-header">
        <div class="header-content-wrapper">
          <!-- 面包屑导航 -->
          <div class="breadcrumb glass-card">
            <a @click="goHome" class="breadcrumb-link">首页</a>
            <a-icon type="right" />
            <a @click="goToProjects" class="breadcrumb-link">项目展示</a>
            <a-icon type="right" />
            <span class="current">{{ project.Name }}</span>
          </div>

          <!-- 项目元信息 -->
          <div class="project-meta">
            <div class="meta-tags">
              <div v-if="project.IsFeatured" class="featured-tag">
                <a-icon type="star" />
                精选项目
              </div>
              <div class="status-tag" :class="project.Status">
                {{ getStatusText(project.Status) }}
              </div>
              <div v-if="project.DifficultyLevel" class="difficulty-tag" :class="project.DifficultyLevel">
                <a-icon type="trophy" />
                {{ getDifficultyText(project.DifficultyLevel) }}
              </div>
            </div>
            
            <h1 class="project-title">{{ project.Name }}</h1>
            
            <div class="project-info">
              <div class="project-stats">
                <div class="stat">
                  <a-icon type="eye" />
                  <span>{{ formatNumber(project.ViewCount || 0) }} 浏览</span>
                </div>
                <div class="stat">
                  <a-icon type="heart" />
                  <span>{{ formatNumber(project.LikeCount || 0) }} 点赞</span>
                </div>
                <div class="stat">
                  <a-icon type="message" />
                  <span>{{ formatNumber(project.CommentCount || 0) }} 评论</span>
                </div>
              </div>
              
              <div class="project-date">
                <span class="date-label">创建时间：</span>
                <span class="date-value">{{ formatTime(project.CreatedAt) }}</span>
              </div>
            </div>

            <!-- 项目链接 -->
            <div class="project-links">
              <a v-if="project.DemoUrl" 
                 :href="project.DemoUrl" 
                 target="_blank" 
                 class="link-button demo">
                <a-icon type="play-circle" />
                <span>在线演示</span>
              </a>
              <a v-if="project.GithubUrl" 
                 :href="project.GithubUrl" 
                 target="_blank" 
                 class="link-button github">
                <a-icon type="github" />
                <span>查看代码</span>
              </a>
              <a v-if="project.WebsiteUrl" 
                 :href="project.WebsiteUrl" 
                 target="_blank" 
                 class="link-button website">
                <a-icon type="link" />
                <span>项目网站</span>
              </a>
            </div>
          </div>

          <!-- 封面图片 -->
          <div v-if="project.CoverImage" class="project-cover">
            <img :src="project.CoverImage" :alt="project.Name" />
          </div>
        </div>
      </section>

      <!-- 项目内容 -->
      <section class="project-content-section">
        <div class="content-wrapper">
          <div class="main-content-area">
            <!-- 项目描述 -->
            <div v-if="project.Description" class="project-description glass-card">
              <h3>项目简介</h3>
              <p>{{ project.Description }}</p>
            </div>

            <!-- 技术栈 -->
            <div v-if="project.TechnologyStack" class="tech-stack glass-card">
              <h3>技术栈</h3>
              <div class="tech-tags">
                <span v-for="tech in getTechArray(project.TechnologyStack)" 
                      :key="tech" 
                      class="tech-tag">{{ tech }}</span>
              </div>
            </div>

            <!-- 项目详细内容 -->
            <div class="project-body glass-card">
              <div class="content-html" v-html="project.Content || '暂无详细内容'"></div>
            </div>

            <!-- 项目操作 -->
            <div class="project-actions glass-card">
              <div class="action-buttons">
                <a-button type="primary" ghost @click="handleLike" :loading="liking">
                  <a-icon type="heart" :theme="liked ? 'filled' : 'outlined'" />
                  {{ liked ? '已点赞' : '点赞' }} ({{ project.LikeCount || 0 }})
                </a-button>
                <a-button @click="shareProject">
                  <a-icon type="share-alt" />
                  分享项目
                </a-button>
              </div>
            </div>
          </div>

          <!-- 侧边栏 -->
          <aside class="sidebar">
            <!-- 项目信息卡 -->
            <div class="project-info-card glass-card">
              <h4>项目信息</h4>
              <div class="info-list">
                <div class="info-item" v-if="project.DifficultyLevel">
                  <span class="info-label">难度级别：</span>
                  <span class="info-value" :class="project.DifficultyLevel">
                    {{ getDifficultyText(project.DifficultyLevel) }}
                  </span>
                </div>
                <div class="info-item">
                  <span class="info-label">项目状态：</span>
                  <span class="info-value" :class="project.Status">
                    {{ getStatusText(project.Status) }}
                  </span>
                </div>
                <div class="info-item" v-if="project.SortOrder">
                  <span class="info-label">排序权重：</span>
                  <span class="info-value">{{ project.SortOrder }}</span>
                </div>
                <div class="info-item">
                  <span class="info-label">最后更新：</span>
                  <span class="info-value">{{ formatTime(project.UpdatedAt || project.CreatedAt) }}</span>
                </div>
              </div>
            </div>

            <!-- 相关项目 -->
            <div class="related-projects glass-card" v-if="relatedProjects.length > 0">
              <h4>相关项目</h4>
              <div class="project-list">
                <div 
                  v-for="relatedProject in relatedProjects" 
                  :key="relatedProject.Id"
                  class="project-item"
                  @click="goToProject(relatedProject.Id)"
                >
                  <div class="project-thumb" v-if="relatedProject.CoverImage">
                    <img :src="relatedProject.CoverImage" :alt="relatedProject.Name" />
                  </div>
                  <div class="project-info">
                    <div class="project-name">{{ relatedProject.Name }}</div>
                    <div class="project-meta">
                      <span>{{ formatTime(relatedProject.CreatedAt) }}</span>
                      <span>{{ formatNumber(relatedProject.ViewCount || 0) }} 浏览</span>
                    </div>
                  </div>
                </div>
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
        <p>正在加载项目...</p>
      </div>
    </div>

    <!-- 错误状态 -->
    <div v-else-if="error" class="error-state">
      <div class="error-content glass-card">
        <a-icon type="exclamation-circle" class="error-icon" />
        <h3>加载失败</h3>
        <p>{{ error }}</p>
        <a-button type="primary" @click="loadProject">重新加载</a-button>
        <a-button @click="goToProjects" style="margin-left: 12px;">返回列表</a-button>
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
          <a href="javascript:void(0)" class="footer-link" @click="goToProjects">项目列表</a>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
import { GetProjectDetailForPublic, GetProjectListForPublic } from '@/api/blog_project_public'

export default {
  name: 'BlogProjectDetail',
  data() {
    return {
      loading: true,
      error: null,
      project: null,
      relatedProjects: [],
      showBackTop: false,
      liked: false,
      liking: false,
      
      // 粒子随机数
      Math: Math
    }
  },
  
  async mounted() {
    this.initMouseEffect()
    this.initScrollListener()
    await this.loadProject()
  },
  
  beforeDestroy() {
    window.removeEventListener('scroll', this.handleScroll)
  },
  
  methods: {
    async loadProject() {
      const projectId = this.$route.params.id
      
      if (!projectId) {
        this.error = '项目ID不存在'
        this.loading = false
        return
      }
      
      this.loading = true
      this.error = null
      
      try {
        const response = await GetProjectDetailForPublic(projectId)
        
        if (response.Success && response.Data) {
          this.project = response.Data
          
          // 只显示状态为 active 的项目
          if (this.project.Status !== 'active') {
            throw new Error('项目不可访问或已被删除')
          }
          
          // 检查用户是否已点赞此项目
          this.liked = this.checkUserLike(this.project.Id)
          
          // 增加浏览量
          this.incrementViewCount()
          
          // 加载相关项目
          this.loadRelatedProjects()
          
        } else {
          throw new Error(response.Message || '项目不存在或已被删除')
        }
      } catch (error) {
        console.error('加载项目失败:', error)
        this.error = error.message || '加载项目失败'
      } finally {
        this.loading = false
      }
    },
    
    async loadRelatedProjects() {
      try {
        let relatedProjects = []
        
        // 优先加载同难度级别的项目
        if (this.project.DifficultyLevel) {
          const response = await GetProjectListForPublic({
            PageIndex: 1,
            PageRows: 10,
            difficultyLevel: this.project.DifficultyLevel
          })
          
          if (response.Success && response.Data) {
            // 排除当前项目，并限制到4个
            relatedProjects = response.Data
              .filter(project => project.Id !== this.project.Id)
              .slice(0, 4)
          }
        }
        
        // 如果相关项目不足4个，用最新项目补充
        if (relatedProjects.length < 4) {
          const response = await GetProjectListForPublic({
            PageIndex: 1,
            PageRows: 8
          })
          
          if (response.Success && response.Data) {
            const latestProjects = response.Data
              .filter(project => 
                project.Id !== this.project.Id && 
                !relatedProjects.some(related => related.Id === project.Id)
              )
              .slice(0, 4 - relatedProjects.length)
            
            relatedProjects = [...relatedProjects, ...latestProjects]
          }
        }
        
        this.relatedProjects = relatedProjects
        
      } catch (error) {
        console.error('加载相关项目失败:', error)
        this.relatedProjects = []
      }
    },
    
    incrementViewCount() {
      // 这里可以调用专门的浏览量统计API
      // 暂时只在本地更新
      if (this.project.ViewCount !== undefined) {
        this.project.ViewCount += 1
      }
    },
    
    async handleLike() {
      if (this.liking) return
      
      this.liking = true
      
      try {
        // TODO: 这里应该调用真实的点赞API
        // 模拟API调用
        await new Promise(resolve => setTimeout(resolve, 500))
        
        // 模拟成功响应
        const success = Math.random() > 0.1 // 90% 成功率
        
        if (success) {
          if (!this.liked) {
            this.liked = true
            this.project.LikeCount = (this.project.LikeCount || 0) + 1
            this.$message.success('点赞成功！')
            
            // 保存到本地存储
            this.saveUserLike(this.project.Id, true)
          } else {
            this.liked = false
            this.project.LikeCount = Math.max((this.project.LikeCount || 0) - 1, 0)
            this.$message.success('取消点赞！')
            
            // 从本地存储移除
            this.saveUserLike(this.project.Id, false)
          }
        } else {
          throw new Error('服务器响应失败')
        }
      } catch (error) {
        console.error('点赞失败:', error)
        this.$message.error('操作失败，请稍后重试')
      } finally {
        this.liking = false
      }
    },
    
    // 保存用户点赞状态到本地存储
    saveUserLike(projectId, liked) {
      try {
        const key = 'blog_user_project_likes'
        const likes = JSON.parse(localStorage.getItem(key) || '{}')
        
        if (liked) {
          likes[projectId] = true
        } else {
          delete likes[projectId]
        }
        
        localStorage.setItem(key, JSON.stringify(likes))
      } catch (error) {
        console.error('保存点赞状态失败:', error)
      }
    },
    
    // 检查用户是否已点赞
    checkUserLike(projectId) {
      try {
        const key = 'blog_user_project_likes'
        const likes = JSON.parse(localStorage.getItem(key) || '{}')
        return !!likes[projectId]
      } catch (error) {
        console.error('检查点赞状态失败:', error)
        return false
      }
    },
    
    shareProject() {
      const url = window.location.href
      const title = this.project.Name
      
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
    
    goToProjects() {
      this.$router.push('/blog-website/projects')
    },
    
    goToAdmin() {
      this.$router.push('/')
      this.$message.success('正在跳转到管理后台...')
    },
    
    goToProject(projectId) {
      this.$router.push(`/blog-website/projects/${projectId}`)
    },
    
    // 工具方法
    formatNumber(num) {
      if (num >= 10000) {
        return (num / 10000).toFixed(1) + 'w'
      } else if (num >= 1000) {
        return (num / 1000).toFixed(1) + 'k'
      }
      return num.toString()
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
    
    getStatusText(status) {
      const statusMap = {
        'active': '进行中',
        'completed': '已完成',
        'archived': '已归档'
      }
      return statusMap[status] || status
    },
    
    getDifficultyText(level) {
      const textMap = {
        'beginner': '初级',
        'intermediate': '中级',
        'advanced': '高级',
        'expert': '专家'
      }
      return textMap[level] || level
    },
    
    getTechArray(techStack) {
      if (!techStack) return []
      return techStack.split(',').map(tech => tech.trim()).filter(tech => tech)
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
// 复用文章详情页面的样式结构，并适配项目展示
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
.project-detail-container {
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

// 项目头部
.project-header {
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
    
    .project-meta {
      margin-bottom: 40px;
      
      .meta-tags {
        display: flex;
        gap: 12px;
        margin-bottom: 24px;
        
        .featured-tag,
        .status-tag,
        .difficulty-tag {
          display: flex;
          align-items: center;
          gap: 6px;
          padding: 6px 12px;
          border-radius: 16px;
          font-size: 14px;
          font-weight: 500;
        }
        
        .featured-tag {
          background: rgba(255, 165, 0, 0.1);
          color: #ff8c00;
        }
        
        .status-tag {
          &.active {
            background: rgba(52, 199, 89, 0.1);
            color: #34c759;
          }
          
          &.completed {
            background: rgba(0, 122, 255, 0.1);
            color: #007aff;
          }
          
          &.archived {
            background: rgba(255, 149, 0, 0.1);
            color: #ff9500;
          }
        }
        
        .difficulty-tag {
          background: rgba(102, 126, 234, 0.1);
          color: #667eea;
          
          &.beginner { background: rgba(52, 199, 89, 0.1); color: #34c759; }
          &.intermediate { background: rgba(0, 122, 255, 0.1); color: #007aff; }
          &.advanced { background: rgba(255, 149, 0, 0.1); color: #ff9500; }
          &.expert { background: rgba(255, 59, 48, 0.1); color: #ff3b30; }
        }
      }
      
      .project-title {
        font-size: 36px;
        font-weight: 800;
        color: var(--text-primary);
        line-height: 1.4;
        margin-bottom: 24px;
      }
      
      .project-info {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 24px;
        
        .project-stats {
          display: flex;
          gap: 24px;
          
          .stat {
            display: flex;
            align-items: center;
            gap: 6px;
            font-size: 14px;
            color: var(--text-secondary);
            
            .anticon {
              font-size: 16px;
            }
          }
        }
        
        .project-date {
          .date-label {
            font-size: 14px;
            color: var(--text-secondary);
          }
          
          .date-value {
            font-size: 14px;
            color: var(--text-primary);
            font-weight: 500;
          }
        }
      }
    }
    
    .project-links {
      display: flex;
      gap: 16px;
      margin-bottom: 40px;
      
      .link-button {
        display: flex;
        align-items: center;
        gap: 8px;
        padding: 12px 24px;
        border-radius: 20px;
        font-size: 14px;
        font-weight: 500;
        text-decoration: none;
        transition: all 0.3s ease;
        
        &.demo {
          background: rgba(52, 199, 89, 0.1);
          color: #34c759;
          
          &:hover {
            background: rgba(52, 199, 89, 0.2);
            color: #34c759;
            transform: translateY(-2px);
          }
        }
        
        &.github {
          background: rgba(0, 0, 0, 0.05);
          color: #333;
          
          &:hover {
            background: rgba(0, 0, 0, 0.1);
            color: #333;
            transform: translateY(-2px);
          }
        }
        
        &.website {
          background: rgba(0, 122, 255, 0.1);
          color: #007aff;
          
          &:hover {
            background: rgba(0, 122, 255, 0.2);
            color: #007aff;
            transform: translateY(-2px);
          }
        }
      }
    }
    
    .project-cover {
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

// 项目内容区域
.project-content-section {
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
      .project-description {
        padding: 24px;
        margin-bottom: 32px;
        
        h3 {
          font-size: 18px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 16px;
        }
        
        p {
          font-size: 16px;
          color: var(--text-secondary);
          line-height: 1.8;
          margin: 0;
        }
      }
      
      .tech-stack {
        padding: 24px;
        margin-bottom: 32px;
        
        h3 {
          font-size: 18px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 16px;
        }
        
        .tech-tags {
          display: flex;
          flex-wrap: wrap;
          gap: 8px;
          
          .tech-tag {
            background: rgba(102, 126, 234, 0.1);
            color: #667eea;
            padding: 6px 12px;
            border-radius: 16px;
            font-size: 14px;
            font-weight: 500;
          }
        }
      }
      
      .project-body {
        padding: 40px;
        margin-bottom: 32px;
        
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
      
      .project-actions {
        padding: 24px;
        
        .action-buttons {
          display: flex;
          gap: 16px;
        }
      }
    }
    
    .sidebar {
      position: sticky;
      top: 120px;
      display: flex;
      flex-direction: column;
      gap: 24px;
      
      .project-info-card {
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
              
              &.beginner { color: #34c759; }
              &.intermediate { color: #007aff; }
              &.advanced { color: #ff9500; }
              &.expert { color: #ff3b30; }
              
              &.active { color: #34c759; }
              &.completed { color: #007aff; }
              &.archived { color: #ff9500; }
            }
          }
        }
      }
      
      .related-projects {
        padding: 20px;
        
        h4 {
          font-size: 16px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 16px;
        }
        
        .project-list {
          .project-item {
            display: flex;
            gap: 12px;
            padding: 12px 0;
            cursor: pointer;
            transition: background 0.3s ease;
            border-radius: 8px;
            margin-bottom: 8px;
            
            &:hover {
              background: rgba(102, 126, 234, 0.05);
            }
            
            .project-thumb {
              width: 60px;
              height: 45px;
              border-radius: 6px;
              overflow: hidden;
              flex-shrink: 0;
              
              img {
                width: 100%;
                height: 100%;
                object-fit: cover;
              }
            }
            
            .project-info {
              flex: 1;
              
              .project-name {
                font-size: 14px;
                color: var(--text-primary);
                margin-bottom: 4px;
                display: -webkit-box;
                -webkit-line-clamp: 2;
                -webkit-box-orient: vertical;
                overflow: hidden;
              }
              
              .project-meta {
                display: flex;
                justify-content: space-between;
                font-size: 12px;
                color: var(--text-muted);
              }
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

// 状态页面
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
@media (max-width: 1200px) {
  .project-content-section .content-wrapper {
    grid-template-columns: 1fr;
    gap: 40px;
    
    .sidebar {
      position: static;
      order: -1;
      
      .related-projects,
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
  
  .project-header .header-content-wrapper {
    padding: 0 20px;
    
    .project-title {
      font-size: 28px;
    }
    
    .project-info {
      flex-direction: column;
      align-items: flex-start;
      gap: 16px;
    }
    
    .project-cover img {
      height: 250px;
    }
    
    .project-links {
      flex-direction: column;
      
      .link-button {
        justify-content: center;
      }
    }
  }
  
  .project-content-section .content-wrapper {
    padding: 0 20px;
    
    .main-content-area {
      .project-body {
        padding: 24px;
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