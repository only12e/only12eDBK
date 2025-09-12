<template>
  <div class="blog-website-container">
    <!-- 背景动态效果 -->
    <div class="background-animation">
      <!-- 背景图片 -->
      <div class="background-image"></div>
      
      <!-- 粒子特效 -->
      <div class="particles-container">
        <div class="particle" v-for="n in 50" :key="n" 
             :style="{
               left: Math.random() * 100 + '%',
               top: Math.random() * 100 + '%',
               animationDelay: Math.random() * 20 + 's',
               animationDuration: (Math.random() * 10 + 10) + 's'
             }">
        </div>
      </div>
      
      <!-- 浮动形状 -->
      <div class="floating-shapes">
        <div class="shape shape-1"></div>
        <div class="shape shape-2"></div>
        <div class="shape shape-3"></div>
        <div class="shape shape-4"></div>
        <div class="shape shape-5"></div>
      </div>
    </div>

    <!-- 顶部导航 -->
    <header class="blog-header">
      <div class="header-content">
        <div class="logo-section">
          <div class="logo">
            <div class="logo-icon-wrapper">
              <a-icon type="global" class="logo-icon" />
            </div>
            <div class="logo-text">
              <span class="site-name">OnlyBlog</span>
              <p class="tagline">分享技术，记录生活</p>
            </div>
          </div>
        </div>
        
        <nav class="main-nav">
          <div class="nav-wrapper">
            <div class="nav-item" v-for="item in navItems" :key="item.key" 
                 :class="{ active: activeNav === item.key }" 
                 @click="handleNavClick(item)">
              <a-icon :type="item.icon" />
              <span>{{ item.label }}</span>
            </div>
          </div>
        </nav>
        
        <div class="header-actions">
          <a-button type="primary" @click="goToAdmin" class="admin-btn glass-btn">
            <a-icon type="setting" />
            <span>管理后台</span>
          </a-button>
        </div>
      </div>
    </header>

    <!-- 主要内容区域 -->
    <main class="main-content">
      <!-- Hero 区域 -->
      <section class="hero-section">
        <div class="hero-content">
          <div class="hero-text">
            <h1 class="hero-title">
              <span class="title-line">欢迎来到</span>
              <span class="title-line accent" title="OnlyBlog">OnlyBlog</span>
            </h1>
            <p class="hero-subtitle">
              探索技术的边界，记录成长的足迹
            </p>
            
            <div class="hero-stats">
              <div class="stat-card glass-card" v-for="(stat, index) in stats" :key="index">
                <div class="stat-icon">
                  <a-icon :type="stat.icon" />
                </div>
                <div class="stat-content">
                  <div class="stat-value">
                    <div v-if="statsLoading" class="loading-placeholder">
                      <div class="loading-bar"></div>
                    </div>
                    <span v-else>{{ stat.value }}</span>
                  </div>
                  <div class="stat-label">{{ stat.label }}</div>
                </div>
              </div>
            </div>
            
            <div class="hero-actions">
              <button class="primary-btn" @click="scrollToFeaturedContent">
                <a-icon type="compass" />
                <span>开始探索</span>
                <div class="btn-bg"></div>
              </button>
              <button class="secondary-btn" @click="scrollToAboutSection">
                <a-icon type="message" />
                <span>与我交流</span>
                <div class="btn-bg"></div>
              </button>
            </div>
          </div>
          
          <div class="hero-visual">
            <div class="visual-container">
              <div class="central-orb"></div>
              <div class="orbit-ring ring-1">
                <div class="orbit-item item-1"><a-icon type="code" /></div>
                <div class="orbit-item item-2"><a-icon type="bulb" /></div>
                <div class="orbit-item item-3"><a-icon type="rocket" /></div>
              </div>
              <div class="orbit-ring ring-2">
                <div class="orbit-item item-4"><a-icon type="heart" /></div>
                <div class="orbit-item item-5"><a-icon type="coffee" /></div>
                <div class="orbit-item item-6"><a-icon type="star" /></div>
              </div>
            </div>
          </div>
        </div>
      </section>

      <!-- 特色内容展示 -->
      <section class="features-section" id="featured-content">
        <div class="section-header">
          <h2 class="section-title">精选内容</h2>
          <p class="section-subtitle">发现更多精彩内容</p>
          
          <!-- 数据刷新按钮 -->
          <div class="section-actions">
            <a-button 
              type="primary" 
              ghost 
              :loading="loading" 
              @click="refreshData"
              class="refresh-btn glass-btn"
            >
              <a-icon type="reload" />
              <!-- <span>刷新数据</span> -->
            </a-button>
          </div>
        </div>
        
        <div class="features-grid">
          <div class="feature-card glass-card" v-for="(feature, index) in features" :key="index">
            <div class="feature-header">
              <div class="feature-icon">
                <a-icon :type="feature.icon" />
              </div>
              <h3 class="feature-title">{{ feature.title }}</h3>
            </div>
            
            <p class="feature-description">{{ feature.description }}</p>
            
            <div class="feature-stats">
              <span class="feature-stat">
                <div v-if="featuresLoading" class="loading-placeholder">
                  <div class="loading-bar small"></div>
                </div>
                <span v-else>{{ feature.count }}</span>
              </span>
              <span class="feature-meta">{{ feature.meta }}</span>
            </div>
            
            <button class="feature-btn glass-btn" :disabled="featuresLoading" @click="handleFeatureClick(index)">
              <span>探索更多</span>
              <a-icon type="arrow-right" />
              <div class="btn-bg"></div>
            </button>
          </div>
        </div>
      </section>

      <!-- 关于区域 -->
      <section class="about-section" id="about-section">
        <div class="about-content">
          <div class="about-text">
            <div class="about-badge glass-card">
              <a-icon type="user" />
              <span>关于我</span>
            </div>
            
            <h2 class="about-title">
              热爱技术的<br>
              <span class="accent">全栈工程师</span>
            </h2>
            
            <p class="about-description">
              通过这个博客，
              我希望能够分享自己的技术经验，记录学习成长的点点滴滴，
              同时也希望能够帮助到更多有需要的开发者。
            </p>
            
            <div class="tech-stack">
              <div class="tech-category">
                <h4>前端技术</h4>
                <div class="tech-tags">
                  <span class="tech-tag glass-card">Vue.js</span>
                  <span class="tech-tag glass-card">JavaScript</span>
                  <span class="tech-tag glass-card">React</span>
                </div>
              </div>
              
              <div class="tech-category">
                <h4>后端技术</h4>
                <div class="tech-tags">
                  <span class="tech-tag glass-card">.NET</span>
                  <span class="tech-tag glass-card">Java/SpringBoot</span>
                  <span class="tech-tag glass-card">Mysql</span>
                </div>
              </div>
            </div>
          </div>
          
          <div class="contact-card glass-card">
            <h3>联系方式</h3>
            <div class="contact-list">
              <div class="contact-item" v-for="contact in contacts" :key="contact.type">
                <div class="contact-icon">
                  <a-icon :type="contact.icon" />
                </div>
                <div class="contact-info">
                  <span class="contact-label">{{ contact.label }}</span>
                  <span class="contact-desc">{{ contact.desc }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </main>

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
          <a href="#" class="footer-link">隐私政策</a>
          <a href="#" class="footer-link">使用条款</a>
          <a href="#" class="footer-link">网站地图</a>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
import { GetOverview, GetPopularContent } from '@/api/blog_statistics'
import { GetFeaturedArticles, GetLatestArticles } from '@/api/blog_article'
import { GetFeaturedProjects, GetLatestProjects } from '@/api/blog_project'

export default {
  name: 'BlogWebsiteIndex',
  data() {
    return {
      activeNav: 'home',
      loading: true,
      statsLoading: true,
      featuresLoading: true,
      error: null,
      dataLoadTime: null,
      
      navItems: [
        { key: 'home', label: '首页', icon: 'home' },
        { key: 'articles', label: '技术文章', icon: 'file-text', route: '/blog-website/articles' },
        { key: 'projects', label: '项目展示', icon: 'project', route: '/blog-website/projects' },
        { key: 'tools', label: '工具推荐', icon: 'tool', route: '/blog-website/tools' },
      ],
      
      // 统计数据
      stats: [
        { icon: 'file-text', value: '0', label: '技术文章' },
        { icon: 'project', value: '0', label: '项目展示' },
        { icon: 'eye', value: '0', label: '总访问' },
        { icon: 'heart', value: '0', label: '获得点赞' }
      ],
      
      // 特色内容
      features: [
        {
          icon: 'file-text',
          title: '技术文章',
          description: '分享前端开发、后端技术、数据库优化等方面的技术文章和实战经验',
          count: '加载中...',
          meta: '持续更新',
          items: []
        },
        {
          icon: 'project',
          title: '项目展示',
          description: '展示个人开发的各种项目，包括开源项目、实战项目和技术Demo',
          count: '加载中...',
          meta: '源码开放',
          items: []
        },
        {
          icon: 'tool',
          title: '工具推荐',
          description: '推荐各种实用的开发工具、在线服务和资源网站',
          count: '精选工具',
          meta: '定期更新',
          items: []
        }
      ],
      
      contacts: [
        { type: 'email', icon: 'mail', label: '邮箱联系：zhansfai@Outlook.com', desc: '技术交流' },
        { type: 'github', icon: 'github', label: 'GitHub：zhansfai@Outlook.com', desc: '开源项目' },
        // { type: 'wechat', icon: 'wechat', label: '微信交流', desc: '即时沟通' },
        { type: 'qq', icon: 'qq', label: 'QQ交流：3458719294', desc: '在线咨询' }
      ],
      
      // 粒子随机数生成器
      Math: Math,
      
      // 实际数据
      overview: {},
      featuredArticles: [],
      latestArticles: [],
      featuredProjects: [],
      latestProjects: [],
      popularContent: []
    }
  },
  
  computed: {
    // 安全的统计数据访问
    safeOverview() {
      return {
        Articles: this.overview.Articles || { TotalCount: 0, PublishedCount: 0, DraftCount: 0, TodayCount: 0 },
        Projects: this.overview.Projects || { TotalCount: 0, ActiveCount: 0, CompletedCount: 0, TodayCount: 0 },
        Tools: this.overview.Tools || { TotalCount: 0, RecommendedCount: 0, FreeCount: 0, TodayCount: 0 },
        Technologies: this.overview.Technologies || { TotalCount: 0, FeaturedCount: 0, AverageProficiency: 0, TodayCount: 0 },
        Comments: this.overview.Comments || { TotalCount: 0, ApprovedCount: 0, PendingCount: 0, TodayCount: 0 },
        Users: this.overview.Users || { TotalCount: 0, ActiveCount: 0, AdminCount: 0, TodayCount: 0 },
        Access: this.overview.Access || { TotalViews: 0, TotalLikes: 0, TodayViews: 0, TodayLikes: 0 }
      }
    }
  },
  
  async mounted() {
    this.initMouseEffect()
    await this.loadAllData()
  },
  
  methods: {
    goToAdmin() {
      this.$router.push('/')
      this.$message.success('正在跳转到管理后台...')
    },
    
    // 滚动到精选内容区域
    scrollToFeaturedContent() {
      const featuredContentEl = document.getElementById('featured-content')
      if (featuredContentEl) {
        featuredContentEl.scrollIntoView({ 
          behavior: 'smooth', 
          block: 'start' 
        })
      }
    },
    
    // 滚动到关于我区域
    scrollToAboutSection() {
      const aboutSectionEl = document.getElementById('about-section')
      if (aboutSectionEl) {
        aboutSectionEl.scrollIntoView({ 
          behavior: 'smooth', 
          block: 'start' 
        })
      }
    },
    
    // 处理导航点击
    handleNavClick(item) {
      this.activeNav = item.key
      
      if (item.route) {
        this.$router.push(item.route)
      } else if (item.key === 'home') {
        // 刷新当前页面或滚动到顶部
        window.scrollTo({ top: 0, behavior: 'smooth' })
      } else {
        // 其他导航项暂时显示提示
        this.$message.info(`${item.label}功能即将上线，敬请期待！`)
      }
    },
    
    // 处理特色内容点击
    handleFeatureClick(index) {
      if (index === 0) {
        // 技术文章
        this.$router.push('/blog-website/articles')
        this.$message.success('正在跳转到技术文章页面...')
      } else if (index === 1) {
        // 项目展示
        this.$router.push('/blog-website/projects')
        this.$message.success('正在跳转到项目展示页面...')
      } else if (index === 2) {
        // 工具推荐
        this.$router.push('/blog-website/tools')
        this.$message.success('正在跳转到工具推荐页面...')
      }
    },
    
    initMouseEffect() {
      document.addEventListener('mousemove', (e) => {
        const shapes = document.querySelectorAll('.floating-shapes .shape')
        const x = e.clientX / window.innerWidth
        const y = e.clientY / window.innerHeight
        
        shapes.forEach((shape, index) => {
          const speed = (index + 1) * 0.5
          const xOffset = (x - 0.5) * speed * 20
          const yOffset = (y - 0.5) * speed * 20
          
          shape.style.transform = `translate(${xOffset}px, ${yOffset}px)`
        })
      })
    },
    
    // 加载所有数据
    async loadAllData() {
      const startTime = Date.now()
      this.loading = true
      this.error = null
      
      try {
        // 并行加载多个数据源
        const results = await Promise.allSettled([
          this.loadOverview(),
          this.loadFeaturedContent(),
          this.loadPopularContent()
        ])
        
        // 检查是否有失败的请求
        const failedRequests = results.filter(result => result.status === 'rejected')
        if (failedRequests.length > 0) {
          console.warn('部分数据加载失败:', failedRequests)
          this.$message.warning(`${failedRequests.length} 个数据源加载失败，部分内容可能不完整`)
        }
        
        this.dataLoadTime = Date.now() - startTime
        console.log(`数据加载完成，耗时 ${this.dataLoadTime}ms`)
        
      } catch (error) {
        console.error('数据加载失败:', error)
        this.error = error.message || '数据加载失败'
        this.$message.error('部分数据加载失败，请刷新页面重试')
      } finally {
        this.loading = false
      }
    },
    
    // 加载统计概览
    async loadOverview() {
      try {
        this.statsLoading = true
        const response = await GetOverview()
        
        if (response.Success && response.Data) {
          this.overview = response.Data
          this.updateStats()
        } else {
          throw new Error(response.Message || '获取统计数据失败')
        }
      } catch (error) {
        this.handleDataError(error.message, '统计概览')
        // 设置默认值，确保界面不会崩溃
        this.stats = this.stats.map(stat => ({ ...stat, value: '0' }))
      } finally {
        this.statsLoading = false
      }
    },
    
    // 加载特色内容
    async loadFeaturedContent() {
      try {
        this.featuresLoading = true
        
        // 并行加载文章和项目数据
        const [articlesRes, projectsRes] = await Promise.allSettled([
          GetFeaturedArticles(6),
          GetFeaturedProjects(6)
        ])
        
        // 处理文章数据
        if (articlesRes.status === 'fulfilled' && articlesRes.value.Success) {
          this.featuredArticles = articlesRes.value.Data || []
          this.features[0].items = this.featuredArticles
          this.features[0].count = `${this.featuredArticles.length}篇精选`
        } else {
          this.handleDataError('获取精选文章失败', '特色内容')
          this.features[0].count = '0篇精选'
        }
        
        // 处理项目数据
        if (projectsRes.status === 'fulfilled' && projectsRes.value.Success) {
          this.featuredProjects = projectsRes.value.Data || []
          this.features[1].items = this.featuredProjects
          this.features[1].count = `${this.featuredProjects.length}个精选`
        } else {
          this.handleDataError('获取精选项目失败', '特色内容')
          this.features[1].count = '0个精选'
        }
        
      } catch (error) {
        this.handleDataError(error.message, '特色内容')
      } finally {
        this.featuresLoading = false
      }
    },
    
    // 加载热门内容
    async loadPopularContent() {
      try {
        const response = await GetPopularContent(10)
        
        if (response.Success && response.Data) {
          this.popularContent = response.Data
        } else {
          throw new Error(response.Message || '获取热门内容失败')
        }
      } catch (error) {
        this.handleDataError(error.message, '热门内容')
        this.popularContent = []
      }
    },
    
    // 更新统计数据显示
    updateStats() {
      const overview = this.safeOverview
      
      this.stats = [
        { 
          icon: 'file-text', 
          value: this.formatNumber(overview.Articles.TotalCount), 
          label: '技术文章' 
        },
        { 
          icon: 'project', 
          value: this.formatNumber(overview.Projects.TotalCount), 
          label: '项目展示' 
        },
        { 
          icon: 'eye', 
          value: this.formatNumber(overview.Access.TotalViews), 
          label: '总访问' 
        },
        { 
          icon: 'heart', 
          value: this.formatNumber(overview.Access.TotalLikes), 
          label: '获得点赞' 
        }
      ]
    },
    
    // 格式化数字显示
    formatNumber(num) {
      if (num >= 10000) {
        return (num / 1000).toFixed(1) + 'K'
      } else if (num >= 1000) {
        return (num / 1000).toFixed(1) + 'K'
      }
      return num.toString()
    },
    
    // 刷新数据
    async refreshData() {
      this.$message.loading('正在刷新数据...', 0.5)
      await this.loadAllData()
      
      if (!this.error) {
        this.$message.success(`数据已刷新 (${this.dataLoadTime}ms)`)
      }
    },
    
    // 处理数据加载错误
    handleDataError(errorMessage, context = '') {
      console.error(`${context} 数据加载错误:`, errorMessage)
      
      // 可以在这里添加错误上报逻辑
      if (process.env.NODE_ENV === 'development') {
        this.$message.error(`${context}: ${errorMessage}`)
      }
    }
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
}

// 玻璃形态基础样式
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
    transform: translateY(-2px);
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
  position: relative;
  overflow: hidden;
  cursor: pointer;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
  
  .btn-bg {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    background: rgba(255, 255, 255, 0.1);
    opacity: 0;
    transition: opacity 0.3s ease;
  }
  
  &:hover {
    transform: translateY(-2px);
    box-shadow: 0 8px 32px rgba(102, 126, 234, 0.2);
    
    .btn-bg {
      opacity: 1;
    }
  }
  
  &:active {
    transform: translateY(0);
  }
}

.blog-website-container {
  min-height: 100vh;
  background: #ffffff;
  position: relative;
  overflow-x: hidden;
}

// 背景动画效果
.background-animation {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  pointer-events: none;
  z-index: 0;
  
  // 背景图片层
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
    opacity: 0.1;
    z-index: 1;
  }
  
  // 粒子特效层
  .particles-container {
    position: absolute;
    top: 0;
    left: 0;
    right: 0;
    bottom: 0;
    z-index: 2;
    
    .particle {
      position: absolute;
      width: 4px;
      height: 4px;
      background: radial-gradient(circle, rgba(102, 126, 234, 0.6) 0%, rgba(118, 75, 162, 0.4) 50%, transparent 100%);
      border-radius: 50%;
      animation: float-particle linear infinite;
      
      &:nth-child(2n) {
        width: 3px;
        height: 3px;
        background: radial-gradient(circle, rgba(240, 147, 251, 0.5) 0%, rgba(102, 126, 234, 0.3) 50%, transparent 100%);
      }
      
      &:nth-child(3n) {
        width: 2px;
        height: 2px;
        background: radial-gradient(circle, rgba(118, 75, 162, 0.4) 0%, rgba(195, 207, 226, 0.2) 50%, transparent 100%);
      }
      
      &:nth-child(4n) {
        width: 5px;
        height: 5px;
        background: radial-gradient(circle, rgba(102, 126, 234, 0.3) 0%, transparent 70%);
      }
    }
  }
  
  // 浮动形状层
  .floating-shapes {
    position: relative;
    width: 100%;
    height: 100%;
    z-index: 3;
    
    .shape {
      position: absolute;
      background: radial-gradient(circle, rgba(102, 126, 234, 0.15) 0%, transparent 70%);
      border-radius: 50%;
      transition: transform 0.3s ease;
      
      &.shape-1 {
        width: 300px;
        height: 300px;
        top: 10%;
        left: 10%;
        animation: float 15s ease-in-out infinite;
      }
      
      &.shape-2 {
        width: 200px;
        height: 200px;
        top: 60%;
        right: 15%;
        animation: float 20s ease-in-out infinite reverse;
      }
      
      &.shape-3 {
        width: 150px;
        height: 150px;
        bottom: 20%;
        left: 20%;
        animation: float 18s ease-in-out infinite;
      }
      
      &.shape-4 {
        width: 100px;
        height: 100px;
        top: 30%;
        right: 30%;
        animation: float 12s ease-in-out infinite reverse;
      }
      
      &.shape-5 {
        width: 80px;
        height: 80px;
        bottom: 40%;
        right: 10%;
        animation: float 25s ease-in-out infinite;
      }
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
  background: rgba(255, 255, 255, 0.8);
  backdrop-filter: blur(20px);
  -webkit-backdrop-filter: blur(20px);
  border-bottom: 1px solid rgba(102, 126, 234, 0.1);
  box-shadow: 0 2px 20px rgba(102, 126, 234, 0.05);
  
  .header-content {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    display: flex;
    align-items: center;
    justify-content: space-between;
    
    .logo-section {
      .logo {
        display: flex;
        align-items: center;
        gap: 12px;
        
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
            line-height: 1;
          }
        }
      }
    }
    
    .main-nav {
      .nav-wrapper {
        display: flex;
        gap: 8px;
        background: var(--glass-bg);
        backdrop-filter: blur(20px);
        border: 1px solid var(--glass-border);
        border-radius: 16px;
        padding: 8px;
        
        .nav-item {
          display: flex;
          align-items: center;
          gap: 8px;
          padding: 12px 20px;
          border-radius: 12px;
          color: var(--text-secondary);
          cursor: pointer;
          transition: all 0.3s ease;
          position: relative;
          
          &:hover, &.active {
            color: var(--text-primary);
            background: rgba(255, 255, 255, 0.1);
          }
          
          .anticon {
            font-size: 16px;
          }
          
          span {
            font-size: 14px;
            font-weight: 500;
          }
        }
      }
    }
    
    .header-actions {
      .admin-btn {
        display: flex;
        align-items: center;
        gap: 8px;
        font-weight: 500;
        
        .anticon {
          font-size: 16px;
        }
      }
    }
  }
}

// 主内容区域
.main-content {
  position: relative;
  z-index: 10;
  padding-top: 100px;
}

// Hero 区域
.hero-section {
  padding: 80px 0 120px;
  
  .hero-content {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    display: flex;
    align-items: center;
    gap: 80px;
    
    .hero-text {
      flex: 1;
      
      .hero-title {
        font-size: 64px;
        font-weight: 800;
        line-height: 1.3;
        margin-bottom: 24px;
        color: var(--text-primary);
        overflow: visible;
        
        .title-line {
          display: block;
          
          &.accent {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            background-clip: text;
            font-weight: 800;
            
            // 备用样式，防止渐变文字不显示
            @supports not (-webkit-background-clip: text) {
              color: #667eea !important;
              background: none;
            }
          }
        }
      }
      
      .hero-subtitle {
        font-size: 20px;
        color: var(--text-secondary);
        margin-bottom: 48px;
        line-height: 1.6;
      }
      
      .hero-stats {
        display: flex;
        gap: 24px;
        margin-bottom: 48px;
        
        .stat-card {
          display: flex;
          align-items: center;
          gap: 16px;
          padding: 20px 24px;
          min-width: 140px;
          
          .stat-icon {
            width: 48px;
            height: 48px;
            border-radius: 12px;
            background: var(--primary-gradient);
            display: flex;
            align-items: center;
            justify-content: center;
            
            .anticon {
              font-size: 20px;
              color: white;
            }
          }
          
          .stat-content {
            .stat-value {
              font-size: 24px;
              font-weight: 700;
              color: var(--text-primary);
              line-height: 1;
              margin-bottom: 4px;
            }
            
            .stat-label {
              font-size: 12px;
              color: var(--text-secondary);
              line-height: 1;
            }
          }
        }
      }
      
      .hero-actions {
        display: flex;
        gap: 16px;
        
        .primary-btn, .secondary-btn {
          display: flex;
          align-items: center;
          gap: 8px;
          padding: 16px 32px;
          font-size: 16px;
          font-weight: 600;
          border: none;
          cursor: pointer;
          border-radius: 12px;
          transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
          position: relative;
          overflow: hidden;
        }
        
        .primary-btn {
          background: rgba(255, 255, 255, 0.2);
          color: #333;
          border: none;
          backdrop-filter: blur(20px);
          
          &:hover {
            background: rgba(255, 255, 255, 0.3);
            transform: translateY(-2px);
            box-shadow: 0 8px 32px rgba(0, 0, 0, 0.1);
          }
          
          span, .anticon {
            color: #333;
            z-index: 2;
            position: relative;
          }
        }
        
        .secondary-btn {
          background: rgba(255, 255, 255, 0.2);
          border: none;
          color: #667eea;
          backdrop-filter: blur(20px);
          
          &:hover {
            background: rgba(255, 255, 255, 0.3);
            transform: translateY(-2px);
            box-shadow: 0 8px 32px rgba(102, 126, 234, 0.15);
          }
          
          span, .anticon {
            color: #667eea;
            z-index: 2;
            position: relative;
          }
        }
      }
    }
    
    .hero-visual {
      flex: 0 0 400px;
      
      .visual-container {
        position: relative;
        width: 400px;
        height: 400px;
        
        .central-orb {
          position: absolute;
          top: 50%;
          left: 50%;
          width: 120px;
          height: 120px;
          background: var(--primary-gradient);
          border-radius: 50%;
          transform: translate(-50%, -50%);
          box-shadow: 0 0 60px rgba(102, 126, 234, 0.5);
          animation: pulse 4s ease-in-out infinite;
        }
        
        .orbit-ring {
          position: absolute;
          top: 50%;
          left: 50%;
          border: 1px solid rgba(255, 255, 255, 0.1);
          border-radius: 50%;
          transform: translate(-50%, -50%);
          
          &.ring-1 {
            width: 200px;
            height: 200px;
            animation: rotate 20s linear infinite;
          }
          
          &.ring-2 {
            width: 300px;
            height: 300px;
            animation: rotate 30s linear infinite reverse;
          }
          
          .orbit-item {
            position: absolute;
            width: 40px;
            height: 40px;
            background: var(--glass-bg);
            backdrop-filter: blur(10px);
            border: 1px solid var(--glass-border);
            border-radius: 50%;
            display: flex;
            align-items: center;
            justify-content: center;
            color: rgba(102, 126, 234, 0.9);
            
            &.item-1 { top: -20px; left: 50%; transform: translateX(-50%); }
            &.item-2 { top: 50%; right: -20px; transform: translateY(-50%); }
            &.item-3 { bottom: -20px; left: 50%; transform: translateX(-50%); }
            &.item-4 { top: -20px; left: 50%; transform: translateX(-50%); }
            &.item-5 { top: 50%; right: -20px; transform: translateY(-50%); }
            &.item-6 { bottom: -20px; left: 50%; transform: translateX(-50%); }
          }
        }
      }
    }
  }
}

// 特色内容区域
.features-section {
  padding: 80px 0;
  
  .section-header {
    text-align: center;
    margin-bottom: 80px;
    position: relative;
    
    .section-title {
      font-size: 48px;
      font-weight: 700;
      color: var(--text-primary);
      margin-bottom: 16px;
    }
    
    .section-subtitle {
      font-size: 18px;
      color: var(--text-secondary);
      margin: 0 0 32px 0;
    }
    
    .section-actions {
      display: flex;
      justify-content: center;
      
      .refresh-btn {
        display: flex;
        align-items: center;
        gap: 8px;
        font-weight: 500;
        
        .anticon {
          font-size: 14px;
        }
      }
    }
  }
  
  .features-grid {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
    gap: 32px;
    
    .feature-card {
      padding: 40px 32px;
      
      .feature-header {
        display: flex;
        align-items: center;
        gap: 16px;
        margin-bottom: 20px;
        
        .feature-icon {
          width: 56px;
          height: 56px;
          border-radius: 16px;
          background: var(--primary-gradient);
          display: flex;
          align-items: center;
          justify-content: center;
          
          .anticon {
            font-size: 24px;
            color: white;
          }
        }
        
        .feature-title {
          font-size: 24px;
          font-weight: 600;
          color: var(--text-primary);
          margin: 0;
        }
      }
      
      .feature-description {
        color: var(--text-secondary);
        line-height: 1.6;
        margin-bottom: 24px;
      }
      
      .feature-stats {
        display: flex;
        gap: 16px;
        margin-bottom: 32px;
        
        .feature-stat {
          font-size: 14px;
          font-weight: 600;
          color: var(--text-primary);
          background: rgba(102, 126, 234, 0.2);
          padding: 6px 12px;
          border-radius: 20px;
        }
        
        .feature-meta {
          font-size: 12px;
          color: var(--text-secondary);
          padding: 6px 12px;
          background: rgba(255, 255, 255, 0.05);
          border-radius: 20px;
        }
      }
      
      .feature-btn {
        display: flex;
        align-items: center;
        justify-content: center;
        gap: 8px;
        width: 100%;
        font-weight: 500;
        
        .anticon {
          font-size: 14px;
        }
      }
    }
  }
}

// 关于区域
.about-section {
  padding: 80px 0;
  
  .about-content {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    display: grid;
    grid-template-columns: 2fr 1fr;
    gap: 80px;
    align-items: start;
    
    .about-text {
      .about-badge {
        display: inline-flex;
        align-items: center;
        gap: 8px;
        padding: 8px 16px;
        margin-bottom: 24px;
        
        .anticon {
          font-size: 16px;
        }
        
        span {
          font-size: 14px;
          font-weight: 500;
          color: var(--text-primary);
        }
      }
      
      .about-title {
        font-size: 40px;
        font-weight: 700;
        color: var(--text-primary);
        line-height: 1.4;
        margin-bottom: 24px;
        overflow: visible;
        
        .accent {
          background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
          -webkit-background-clip: text;
          -webkit-text-fill-color: transparent;
          background-clip: text;
          font-weight: 800;
          
          // 备用样式，防止渐变文字不显示
          @supports not (-webkit-background-clip: text) {
            color: #667eea !important;
            background: none;
          }
        }
      }
      
      .about-description {
        font-size: 16px;
        color: var(--text-secondary);
        line-height: 1.7;
        margin-bottom: 40px;
      }
      
      .tech-stack {
        display: flex;
        flex-direction: column;
        gap: 24px;
        
        .tech-category {
          h4 {
            font-size: 16px;
            font-weight: 600;
            color: var(--text-primary);
            margin-bottom: 12px;
          }
          
          .tech-tags {
            display: flex;
            flex-wrap: wrap;
            gap: 12px;
            
            .tech-tag {
              padding: 8px 16px;
              font-size: 14px;
              font-weight: 500;
              color: var(--text-primary);
            }
          }
        }
      }
    }
    
    .contact-card {
      padding: 32px;
      
      h3 {
        font-size: 24px;
        font-weight: 600;
        color: var(--text-primary);
        margin-bottom: 24px;
      }
      
      .contact-list {
        display: flex;
        flex-direction: column;
        gap: 20px;
        
        .contact-item {
          display: flex;
          align-items: center;
          gap: 16px;
          
          .contact-icon {
            width: 40px;
            height: 40px;
            border-radius: 10px;
            background: var(--primary-gradient);
            display: flex;
            align-items: center;
            justify-content: center;
            
            .anticon {
              font-size: 16px;
              color: white;
            }
          }
          
          .contact-info {
            flex: 1;
            
            .contact-label {
              display: block;
              font-size: 14px;
              font-weight: 500;
              color: var(--text-primary);
              line-height: 1;
              margin-bottom: 4px;
            }
            
            .contact-desc {
              font-size: 12px;
              color: var(--text-secondary);
              line-height: 1;
            }
          }
        }
      }
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
        transition: color 0.3s ease;
        
        &:hover {
          color: var(--text-primary);
        }
      }
    }
  }
}

// 动画效果
@keyframes float {
  0%, 100% {
    transform: translateY(0px) rotate(0deg);
  }
  33% {
    transform: translateY(-20px) rotate(2deg);
  }
  66% {
    transform: translateY(-10px) rotate(-2deg);
  }
}

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
    transform: translateY(-100px) translateX(100px);
    opacity: 0;
  }
}

@keyframes pulse {
  0%, 100% {
    transform: translate(-50%, -50%) scale(1);
    box-shadow: 0 0 60px rgba(102, 126, 234, 0.5);
  }
  50% {
    transform: translate(-50%, -50%) scale(1.1);
    box-shadow: 0 0 80px rgba(102, 126, 234, 0.7);
  }
}

@keyframes rotate {
  from {
    transform: translate(-50%, -50%) rotate(0deg);
  }
  to {
    transform: translate(-50%, -50%) rotate(360deg);
  }
}

@keyframes loading-shimmer {
  0% {
    background-position: -468px 0;
  }
  100% {
    background-position: 468px 0;
  }
}

// 自定义加载占位符样式
.loading-placeholder {
  display: inline-block;
  
  .loading-bar {
    height: 20px;
    width: 60px;
    background: linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%);
    background-size: 400% 100%;
    border-radius: 4px;
    animation: loading-shimmer 1.4s ease infinite;
    
    &.small {
      width: 40px;
      height: 14px;
    }
  }
}

// 响应式设计
@media (max-width: 1200px) {
  .hero-content {
    flex-direction: column;
    text-align: center;
    gap: 60px;
    
    .hero-visual {
      flex: none;
    }
  }
  
  .about-content {
    grid-template-columns: 1fr;
    gap: 60px;
  }
}

@media (max-width: 768px) {
  .blog-header .header-content {
    padding: 0 20px;
    
    .main-nav {
      display: none;
    }
  }
  
  .hero-section .hero-content {
    padding: 0 20px;
    
    .hero-title {
      font-size: 40px;
    }
    
    .hero-stats {
      grid-template-columns: repeat(2, 1fr);
    }
    
    .hero-actions {
      flex-direction: column;
      
      .primary-btn, .secondary-btn {
        width: 100%;
        justify-content: center;
      }
    }
    
    .hero-visual .visual-container {
      width: 300px;
      height: 300px;
    }
  }
  
  .features-grid {
    padding: 0 20px;
    grid-template-columns: 1fr;
  }
  
  .about-content {
    padding: 0 20px;
  }
  
  .footer-content {
    padding: 24px 20px;
    flex-direction: column;
    gap: 20px;
    text-align: center;
  }
}
</style>