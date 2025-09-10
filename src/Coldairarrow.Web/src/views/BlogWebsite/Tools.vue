<template>
  <div class="tools-container">
    <!-- 背景动态效果 -->
    <div class="background-animation">
      <div class="background-image"></div>
      <div class="particles-container">
        <div class="particle" v-for="n in 30" :key="n" 
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
              <p class="tagline">工具推荐</p>
            </div>
          </div>
        </div>
        
        <div class="nav-links">
          <a-button type="link" @click="goHome">
            <a-icon type="home" />
            返回首页
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
    <main class="main-content">
      <!-- 页面头部 -->
      <section class="page-header">
        <div class="header-content-wrapper">
          <div class="page-title">
            <h1>工具推荐</h1>
            <p class="subtitle">发现优秀的开发工具，提升工作效率</p>
          </div>
          
          <!-- 统计信息 -->
          <div class="stats-section">
            <div class="stat-card glass-card">
              <div class="stat-number">{{ totalTools }}</div>
              <div class="stat-label">推荐工具</div>
            </div>
            <div class="stat-card glass-card">
              <div class="stat-number">{{ totalCategories }}</div>
              <div class="stat-label">工具分类</div>
            </div>
            <div class="stat-card glass-card">
              <div class="stat-number">{{ recommendedCount }}</div>
              <div class="stat-label">精选推荐</div>
            </div>
          </div>
        </div>
      </section>

      <!-- 搜索和筛选区域 -->
      <section class="search-filter-section">
        <div class="search-filter-wrapper">
          <!-- 搜索框 -->
          <div class="search-box glass-card">
            <a-input-search
              v-model="searchKeyword"
              placeholder="搜索工具名称、描述..."
              size="large"
              @search="handleSearch"
              @change="handleSearchChange"
              class="search-input"
            >
              <a-icon slot="suffix" type="search" />
            </a-input-search>
            <a-button v-if="searchKeyword" @click="clearSearch" class="clear-btn" type="link">
              <a-icon type="close-circle" />
            </a-button>
          </div>

          <!-- 筛选选项 -->
          <div class="filter-options">
            <!-- 分类筛选 -->
            <div class="filter-group glass-card">
              <div class="filter-label">分类：</div>
              <div class="filter-buttons">
                <a-button 
                  :type="selectedCategory === null ? 'primary' : 'default'"
                  @click="handleCategoryFilter(null)"
                  size="small"
                >
                  全部
                </a-button>
                <a-button 
                  v-for="category in categories" 
                  :key="category"
                  :type="selectedCategory === category ? 'primary' : 'default'"
                  @click="handleCategoryFilter(category)"
                  size="small"
                >
                  {{ category }}
                </a-button>
              </div>
            </div>

            <!-- 价格类型筛选 -->
            <div class="filter-group glass-card">
              <div class="filter-label">价格：</div>
              <div class="filter-buttons">
                <a-button 
                  :type="selectedPriceType === null ? 'primary' : 'default'"
                  @click="handlePriceTypeFilter(null)"
                  size="small"
                >
                  全部
                </a-button>
                <a-button 
                  :type="selectedPriceType === 'free' ? 'primary' : 'default'"
                  @click="handlePriceTypeFilter('free')"
                  size="small"
                >
                  免费
                </a-button>
                <a-button 
                  :type="selectedPriceType === 'paid' ? 'primary' : 'default'"
                  @click="handlePriceTypeFilter('paid')"
                  size="small"
                >
                  付费
                </a-button>
                <a-button 
                  :type="selectedPriceType === 'freemium' ? 'primary' : 'default'"
                  @click="handlePriceTypeFilter('freemium')"
                  size="small"
                >
                  免费增值
                </a-button>
              </div>
            </div>

            <!-- 推荐筛选 -->
            <div class="filter-group glass-card">
              <a-button 
                :type="showOnlyRecommended ? 'primary' : 'default'"
                @click="handleRecommendedFilter"
                size="small"
              >
                <a-icon type="star" />
                仅看推荐
              </a-button>
            </div>
          </div>
        </div>
      </section>

      <!-- 工具展示区域 -->
      <section class="tools-section">
        <div class="tools-wrapper">
          <!-- 工具网格 -->
          <div v-if="!loading && tools.length > 0" class="tools-grid">
            <div 
              v-for="tool in tools" 
              :key="tool.Id"
              class="tool-card glass-card"
              @click="goToTool(tool.Id)"
            >
              <!-- 工具图标 -->
              <div class="tool-icon">
                <img v-if="tool.IconUrl" :src="tool.IconUrl" :alt="tool.Name" />
                <div v-else class="default-icon">
                  <a-icon type="tool" />
                </div>
              </div>

              <!-- 工具信息 -->
              <div class="tool-info">
                <div class="tool-header">
                  <h3 class="tool-name">{{ tool.Name }}</h3>
                  <div class="tool-badges">
                    <span v-if="tool.IsRecommended" class="badge recommended">
                      <a-icon type="star" />
                      推荐
                    </span>
                    <span class="badge price-type" :class="tool.PriceType">
                      {{ getPriceTypeText(tool.PriceType) }}
                    </span>
                  </div>
                </div>
                
                <p class="tool-description">{{ tool.Description }}</p>
                
                <div class="tool-meta">
                  <div class="tool-category">
                    <a-icon type="tag" />
                    {{ tool.Category || '未分类' }}
                  </div>
                  <div v-if="tool.Rating > 0" class="tool-rating">
                    <a-rate :value="tool.Rating" disabled allow-half :style="{ fontSize: '14px' }" />
                    <span class="rating-text">{{ tool.Rating }}</span>
                  </div>
                </div>

                <div class="tool-actions">
                  <a v-if="tool.OfficialUrl" 
                     :href="tool.OfficialUrl" 
                     target="_blank" 
                     class="action-btn official"
                     @click.stop
                  >
                    <a-icon type="link" />
                    官网
                  </a>
                  <a v-if="tool.DownloadUrl" 
                     :href="tool.DownloadUrl" 
                     target="_blank" 
                     class="action-btn download"
                     @click.stop
                  >
                    <a-icon type="download" />
                    下载
                  </a>
                </div>
              </div>
            </div>
          </div>

          <!-- 加载状态 -->
          <div v-else-if="loading" class="loading-section">
            <a-spin size="large" />
            <p>正在加载工具...</p>
          </div>

          <!-- 空状态 -->
          <div v-else class="empty-section glass-card">
            <div class="empty-content">
              <a-icon type="inbox" class="empty-icon" />
              <h3>暂无工具</h3>
              <p>当前筛选条件下没有找到相关工具</p>
              <a-button @click="clearAllFilters" type="primary">
                清除筛选条件
              </a-button>
            </div>
          </div>

          <!-- 分页 -->
          <div v-if="tools.length > 0" class="pagination-wrapper">
            <a-pagination 
              v-model="pagination.current"
              :total="pagination.total"
              :pageSize="pagination.pageSize"
              :showSizeChanger="true"
              :showQuickJumper="true"
              :showTotal="(total, range) => `共 ${total} 个工具，显示第 ${range[0]}-${range[1]} 条`"
              :pageSizeOptions="['12', '24', '36', '48']"
              @change="handlePageChange"
              @showSizeChange="handlePageSizeChange"
              class="custom-pagination"
            />
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
          <a href="javascript:void(0)" class="footer-link" @click="goHome">返回首页</a>
          <a href="javascript:void(0)" class="footer-link" @click="goToAdmin">管理后台</a>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
import { 
  GetToolListForPublic, 
  GetToolCategoriesForPublic, 
  GetRecommendedToolsForPublic 
} from '@/api/blog_tool_public'

export default {
  name: 'BlogTools',
  data() {
    return {
      loading: true,
      tools: [],
      categories: [],
      
      // 搜索和筛选
      searchKeyword: '',
      selectedCategory: null,
      selectedPriceType: null,
      showOnlyRecommended: false,
      
      // 统计信息
      totalTools: 0,
      totalCategories: 0,
      recommendedCount: 0,
      
      pagination: {
        current: 1,
        pageSize: 12,
        total: 0
      },
      
      // 粒子随机数
      Math: Math
    }
  },
  
  async mounted() {
    this.initMouseEffect()
    await this.loadData()
  },
  
  methods: {
    async loadData() {
      this.loading = true
      
      try {
        // 并行加载工具和分类数据
        await Promise.allSettled([
          this.loadTools(),
          this.loadCategories(),
          this.loadStatistics()
        ])
      } catch (error) {
        console.error('数据加载失败:', error)
      } finally {
        this.loading = false
      }
    },
    
    async loadTools() {
      try {
        const params = {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          keyword: this.searchKeyword || undefined,
          category: this.selectedCategory || undefined,
          priceType: this.selectedPriceType || undefined,
          isRecommended: this.showOnlyRecommended ? 1 : undefined
        }
        
        const response = await GetToolListForPublic(params)
        
        if (response.Success) {
          this.tools = response.Data || []
          this.pagination.total = response.Total || 0
          this.totalTools = response.Total || 0
        } else {
          throw new Error(response.Message || '获取工具列表失败')
        }
      } catch (error) {
        console.error('加载工具失败:', error)
        this.tools = []
        this.pagination.total = 0
      }
    },
    
    async loadCategories() {
      try {
        const response = await GetToolCategoriesForPublic()
        
        if (response.Success) {
          this.categories = response.Data || []
          this.totalCategories = this.categories.length
        } else {
          throw new Error(response.Message || '获取分类列表失败')
        }
      } catch (error) {
        console.error('加载分类失败:', error)
        this.categories = []
      }
    },
    
    async loadStatistics() {
      try {
        const response = await GetRecommendedToolsForPublic(100)
        
        if (response.Success) {
          this.recommendedCount = response.Data ? response.Data.length : 0
        }
      } catch (error) {
        console.error('加载统计数据失败:', error)
        this.recommendedCount = 0
      }
    },
    
    // 搜索处理
    handleSearch(value) {
      this.searchKeyword = value
      this.pagination.current = 1
      this.loadTools()
    },
    
    handleSearchChange(e) {
      if (!e.target.value) {
        this.searchKeyword = ''
        this.pagination.current = 1
        this.loadTools()
      }
    },
    
    clearSearch() {
      this.searchKeyword = ''
      this.pagination.current = 1
      this.loadTools()
    },
    
    // 分类筛选
    handleCategoryFilter(category) {
      this.selectedCategory = category
      this.pagination.current = 1
      this.loadTools()
    },
    
    // 价格类型筛选
    handlePriceTypeFilter(priceType) {
      this.selectedPriceType = priceType
      this.pagination.current = 1
      this.loadTools()
    },
    
    // 推荐筛选
    handleRecommendedFilter() {
      this.showOnlyRecommended = !this.showOnlyRecommended
      this.pagination.current = 1
      this.loadTools()
    },
    
    // 清除所有筛选
    clearAllFilters() {
      this.searchKeyword = ''
      this.selectedCategory = null
      this.selectedPriceType = null
      this.showOnlyRecommended = false
      this.pagination.current = 1
      this.loadTools()
    },
    
    // 分页处理
    handlePageChange(page) {
      this.pagination.current = page
      this.loadTools()
      
      // 滚动到顶部
      window.scrollTo({ top: 0, behavior: 'smooth' })
    },
    
    // 处理每页显示数量改变
    handlePageSizeChange(current, size) {
      this.pagination.current = 1  // 重置到第一页
      this.pagination.pageSize = size
      this.loadTools()
      
      // 滚动到顶部
      window.scrollTo({ top: 0, behavior: 'smooth' })
    },
    
    // 导航
    goHome() {
      this.$router.push('/blog-website')
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
    
    // 鼠标交互效果
    initMouseEffect() {
      document.addEventListener('mousemove', (e) => {
        const particles = document.querySelectorAll('.particle')
        const x = e.clientX / window.innerWidth
        const y = e.clientY / window.innerHeight
        
        particles.forEach((particle, index) => {
          const speed = (index + 1) * 0.2
          const xOffset = (x - 0.5) * speed * 10
          const yOffset = (y - 0.5) * speed * 10
          
          particle.style.transform = `translate(${xOffset}px, ${yOffset}px)`
        })
      })
    }
  }
}
</script>

<style lang="less" scoped>
// 复用技术文章和项目展示页面的样式结构
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
.tools-container {
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

// 页面头部
.page-header {
  padding: 40px 0 60px;
  
  .header-content-wrapper {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    
    .page-title {
      text-align: center;
      margin-bottom: 40px;
      
      h1 {
        font-size: 42px;
        font-weight: 800;
        color: var(--text-primary);
        margin-bottom: 16px;
        background: var(--primary-gradient);
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
      }
      
      .subtitle {
        font-size: 18px;
        color: var(--text-secondary);
        margin: 0;
      }
    }
    
    .stats-section {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
      gap: 24px;
      margin-bottom: 40px;
      
      .stat-card {
        padding: 24px;
        text-align: center;
        
        .stat-number {
          font-size: 32px;
          font-weight: 700;
          color: #667eea;
          margin-bottom: 8px;
        }
        
        .stat-label {
          font-size: 14px;
          color: var(--text-secondary);
        }
      }
    }
  }
}

// 搜索筛选区域
.search-filter-section {
  padding: 0 0 40px;
  
  .search-filter-wrapper {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    
    .search-box {
      display: flex;
      align-items: center;
      padding: 20px;
      margin-bottom: 24px;
      
      .search-input {
        flex: 1;
        
        ::v-deep .ant-input {
          background: rgba(255, 255, 255, 0.8);
          border: none;
          border-radius: 12px;
          padding: 12px 20px;
          font-size: 16px;
          
          &:focus {
            box-shadow: 0 0 0 2px rgba(102, 126, 234, 0.2);
          }
        }
        
        ::v-deep .ant-input-search-button {
          background: #667eea;
          border: none;
          border-radius: 0 12px 12px 0;
          
          &:hover {
            background: #5a6fd8;
          }
        }
      }
      
      .clear-btn {
        margin-left: 12px;
        color: var(--text-muted);
      }
    }
    
    .filter-options {
      display: flex;
      flex-wrap: wrap;
      gap: 16px;
      
      .filter-group {
        display: flex;
        align-items: center;
        padding: 12px 20px;
        gap: 12px;
        
        .filter-label {
          font-size: 14px;
          color: var(--text-secondary);
          white-space: nowrap;
        }
        
        .filter-buttons {
          display: flex;
          gap: 8px;
          flex-wrap: wrap;
          
          .ant-btn {
            border-radius: 16px;
            padding: 4px 12px;
            height: auto;
            font-size: 12px;
          }
        }
      }
    }
  }
}

// 工具展示区域
.tools-section {
  padding: 0 0 80px;
  
  .tools-wrapper {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
    
    .tools-grid {
      display: grid;
      grid-template-columns: repeat(auto-fill, minmax(320px, 1fr));
      gap: 24px;
      margin-bottom: 60px;
      
      .tool-card {
        padding: 24px;
        cursor: pointer;
        transition: all 0.3s ease;
        
        &:hover {
          transform: translateY(-4px);
          box-shadow: 0 16px 64px rgba(102, 126, 234, 0.15);
        }
        
        .tool-icon {
          width: 64px;
          height: 64px;
          margin-bottom: 16px;
          
          img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            border-radius: 12px;
          }
          
          .default-icon {
            width: 100%;
            height: 100%;
            background: var(--primary-gradient);
            border-radius: 12px;
            display: flex;
            align-items: center;
            justify-content: center;
            
            .anticon {
              font-size: 32px;
              color: white;
            }
          }
        }
        
        .tool-info {
          .tool-header {
            display: flex;
            justify-content: space-between;
            align-items: flex-start;
            margin-bottom: 12px;
            
            .tool-name {
              font-size: 18px;
              font-weight: 600;
              color: var(--text-primary);
              margin: 0;
              flex: 1;
            }
            
            .tool-badges {
              display: flex;
              gap: 6px;
              flex-wrap: wrap;
              
              .badge {
                padding: 4px 8px;
                border-radius: 12px;
                font-size: 12px;
                font-weight: 500;
                
                &.recommended {
                  background: rgba(255, 165, 0, 0.1);
                  color: #ff8c00;
                  
                  .anticon {
                    margin-right: 4px;
                  }
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
          }
          
          .tool-description {
            font-size: 14px;
            color: var(--text-secondary);
            line-height: 1.6;
            margin-bottom: 16px;
            display: -webkit-box;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
            overflow: hidden;
          }
          
          .tool-meta {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 16px;
            
            .tool-category {
              display: flex;
              align-items: center;
              gap: 6px;
              font-size: 12px;
              color: var(--text-muted);
              
              .anticon {
                font-size: 12px;
              }
            }
            
            .tool-rating {
              display: flex;
              align-items: center;
              gap: 6px;
              
              .rating-text {
                font-size: 12px;
                color: var(--text-secondary);
              }
            }
          }
          
          .tool-actions {
            display: flex;
            gap: 8px;
            
            .action-btn {
              display: flex;
              align-items: center;
              gap: 4px;
              padding: 6px 12px;
              border-radius: 16px;
              font-size: 12px;
              text-decoration: none;
              transition: all 0.3s ease;
              
              &.official {
                background: rgba(102, 126, 234, 0.1);
                color: #667eea;
                
                &:hover {
                  background: rgba(102, 126, 234, 0.2);
                  color: #667eea;
                }
              }
              
              &.download {
                background: rgba(52, 199, 89, 0.1);
                color: #34c759;
                
                &:hover {
                  background: rgba(52, 199, 89, 0.2);
                  color: #34c759;
                }
              }
            }
          }
        }
      }
    }
    
    .loading-section {
      text-align: center;
      padding: 80px 20px;
      
      p {
        margin-top: 20px;
        font-size: 16px;
        color: var(--text-secondary);
      }
    }
    
    .empty-section {
      padding: 80px 40px;
      text-align: center;
      
      .empty-content {
        .empty-icon {
          font-size: 64px;
          color: var(--text-muted);
          margin-bottom: 24px;
        }
        
        h3 {
          font-size: 24px;
          color: var(--text-primary);
          margin-bottom: 12px;
        }
        
        p {
          font-size: 16px;
          color: var(--text-secondary);
          margin-bottom: 24px;
        }
      }
    }
  }
  
  // 分页
  .pagination-wrapper {
    padding: 60px 0 0;
    text-align: center;
    
    .custom-pagination {
      ::v-deep .ant-pagination-item {
        background: var(--glass-bg);
        backdrop-filter: blur(10px);
        border: 1px solid var(--glass-border);
        border-radius: 8px;
        margin: 0 4px;
        
        a {
          color: var(--text-primary);
        }
        
        &:hover,
        &.ant-pagination-item-active {
          background: rgba(102, 126, 234, 0.1);
          border-color: #667eea;
          
          a {
            color: #667eea;
          }
        }
      }
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
@media (max-width: 768px) {
  .blog-header .header-content {
    padding: 0 20px;
  }
  
  .page-header .header-content-wrapper {
    padding: 0 20px;
    
    .page-title h1 {
      font-size: 32px;
    }
    
    .stats-section {
      grid-template-columns: 1fr;
      gap: 16px;
    }
  }
  
  .search-filter-section .search-filter-wrapper {
    padding: 0 20px;
    
    .filter-options {
      flex-direction: column;
      gap: 12px;
      
      .filter-group {
        justify-content: center;
      }
    }
  }
  
  .tools-section .tools-wrapper {
    padding: 0 20px;
    
    .tools-grid {
      grid-template-columns: 1fr;
      gap: 16px;
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