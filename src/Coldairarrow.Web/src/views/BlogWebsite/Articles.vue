<template>
  <div class="articles-container">
    <!-- 背景动态效果 -->
    <div class="background-animation">
      <div class="background-image"></div>
      <div class="particles-container">
        <div class="particle" v-for="n in 30" :key="n" :style="{
          left: Math.random() * 100 + '%',
          top: Math.random() * 100 + '%',
          animationDelay: Math.random() * 20 + 's',
          animationDuration: (Math.random() * 10 + 10) + 's'
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
              <p class="tagline">技术文章</p>
            </div>
          </div>
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
      <!-- 面包屑导航 -->
      <section class="breadcrumb-section">
        <div class="breadcrumb-container glass-card">
          <a-breadcrumb class="custom-breadcrumb">
            <a-breadcrumb-item>
              <a-icon type="home" />
              <a @click="goHome" class="breadcrumb-link">首页</a>
            </a-breadcrumb-item>
            <a-breadcrumb-item>
              <a-icon type="book" />
              <span class="breadcrumb-current">技术文章</span>
            </a-breadcrumb-item>
          </a-breadcrumb>
        </div>
      </section>

      <!-- 页面头部 -->
      <section class="page-header">
        <div class="header-content-wrapper">
          <h1 class="page-title">技术文章</h1>
          <p class="page-subtitle">分享技术经验，记录成长足迹</p>

          <!-- 统计信息 -->
          <div class="stats-bar glass-card">
            <div class="stat-item">
              <a-icon type="file-text" />
              <span class="stat-value">
                <div v-if="loading" class="loading-placeholder">
                  <div class="loading-bar small"></div>
                </div>
                <span v-else>{{ totalArticles }}</span>
              </span>
              <span class="stat-label">篇文章</span>
            </div>
            <div class="stat-item">
              <a-icon type="folder" />
              <span class="stat-value">{{ categories.length }}</span>
              <span class="stat-label">个分类</span>
            </div>
            <div class="stat-item">
              <a-icon type="eye" />
              <span class="stat-value">
                <div v-if="loading" class="loading-placeholder">
                  <div class="loading-bar small"></div>
                </div>
                <span v-else>{{ formatNumber(totalViews) }}</span>
              </span>
              <span class="stat-label">总浏览</span>
            </div>
          </div>
        </div>
      </section>

      <!-- 搜索和筛选 -->
      <section class="filter-section">
        <div class="filter-content">
          <div class="search-box glass-card">
            <a-input-search v-model="searchKeyword" placeholder="搜索文章标题或内容..." @search="handleSearch"
              @change="handleSearchChange" class="search-input" size="large">
              <a-icon slot="prefix" type="search" />
            </a-input-search>
          </div>

          <div class="filter-tabs">
            <div class="category-filters">
              <div class="filter-chip" :class="{ active: !selectedCategory }" @click="handleCategoryFilter(null)">
                <span>全部</span>
                <span class="count">{{ totalArticles }}</span>
              </div>
              <div class="filter-chip" v-for="category in categories" :key="category.Id"
                :class="{ active: selectedCategory === category.Id }" @click="handleCategoryFilter(category.Id)">
                <span>{{ category.Name }}</span>
                <span class="count">{{ category.ArticleCount || 0 }}</span>
              </div>
            </div>
          </div>
        </div>
      </section>

      <!-- 文章列表 -->
      <section class="articles-section">
        <div class="articles-content">
          <!-- 加载状态 -->
          <div v-if="loading" class="loading-container">
            <div class="article-skeleton" v-for="n in 6" :key="n">
              <div class="skeleton-card glass-card">
                <div class="skeleton-content">
                  <div class="skeleton-avatar"></div>
                  <div class="skeleton-text">
                    <div class="skeleton-line long"></div>
                    <div class="skeleton-line medium"></div>
                    <div class="skeleton-line short"></div>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- 文章网格 -->
          <div v-else-if="articles.length > 0" class="articles-grid">
            <div class="article-card glass-card" v-for="article in articles" :key="article.Id"
              @click="goToArticle(article.Id)">
              <!-- 封面图片 -->
              <div class="article-cover">
                <img v-if="article.CoverImage" :src="article.CoverImage" :alt="article.Title" />
                <div v-else class="default-cover">
                  <a-icon type="file-text" />
                </div>

                <!-- 精选标记 -->
                <div v-if="article.IsFeatured" class="featured-badge">
                  <a-icon type="star" />
                  <span>精选</span>
                </div>

                <!-- 分类标签 -->
                <div v-if="article.Category" class="category-tag">
                  {{ article.Category.Name }}
                </div>
              </div>

              <!-- 文章内容 -->
              <div class="article-content">
                <h3 class="article-title">{{ article.Title }}</h3>
                <p class="article-summary">{{ article.Summary || '暂无摘要' }}</p>

                <!-- 文章统计 -->
                <div class="article-stats">
                  <div class="stat-group">
                    <span class="stat"><a-icon type="eye" /> {{ formatNumber(article.ViewCount || 0) }}</span>
                    <span class="stat"><a-icon type="heart" /> {{ formatNumber(article.LikeCount || 0) }}</span>
                    <span class="stat"><a-icon type="message" /> {{ formatNumber(article.CommentCount || 0) }}</span>
                  </div>
                  <div class="publish-info">
                    <span class="publish-time">{{ formatTime(article.PublishedAt || article.CreatedAt) }}</span>
                  </div>
                </div>

                <!-- 文章标签和作者信息 -->
                <div class="article-footer">
                  <div class="article-author" v-if="article.Author">
                    <span class="author-name">{{ article.Author.Nickname || article.Author.UserName || '匿名用户' }}</span>
                  </div>

                  <!-- 文章状态 -->
                  <div class="article-badges">
                    <span v-if="article.IsFeatured" class="badge featured">
                      <a-icon type="star" />
                      精选
                    </span>
                    <span v-if="isNewArticle(article)" class="badge new">
                      <a-icon type="clock-circle" />
                      新发布
                    </span>
                  </div>
                </div>
              </div>
            </div>
          </div>

          <!-- 空状态 -->
          <div v-else class="empty-state">
            <div class="empty-content glass-card">
              <a-icon type="file-text" class="empty-icon" />
              <h3>暂无文章</h3>
              <p>{{ searchKeyword ? '没有找到相关文章' : '还没有发布任何文章' }}</p>
              <a-button v-if="searchKeyword" type="primary" @click="clearSearch">
                清除搜索
              </a-button>
            </div>
          </div>

          <!-- 分页 -->
          <div v-if="articles.length > 0" class="pagination-wrapper">
            <a-pagination v-model="pagination.current" :total="pagination.total" :pageSize="pagination.pageSize"
              :showSizeChanger="true" :showQuickJumper="true"
              :showTotal="(total, range) => `共 ${total} 篇文章，显示第 ${range[0]}-${range[1]} 条`"
              :pageSizeOptions="['12', '24', '36', '48']" @change="handlePageChange"
              @showSizeChange="handlePageSizeChange" class="custom-pagination" />
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
import { GetDataList, GetCategories } from '@/api/blog_article'

export default {
  name: 'BlogArticles',
  data() {
    return {
      loading: true,
      articles: [],
      categories: [],
      searchKeyword: '',
      selectedCategory: null,
      totalArticles: 0,
      totalViews: 0,

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
        // 并行加载文章和分类数据
        await Promise.allSettled([
          this.loadArticles(),
          this.loadCategories()
        ])
      } catch (error) {
        console.error('数据加载失败:', error)
        this.$message.error('数据加载失败')
      } finally {
        this.loading = false
      }
    },

    async loadArticles() {
      try {
        const params = {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          keyword: this.searchKeyword || undefined,
          categoryId: this.selectedCategory || undefined,
          status: 'published' // 只显示已发布的文章
        }

        const response = await GetDataList(params)

        if (response.Success) {
          this.articles = response.Data || []
          this.pagination.total = response.Total || 0
          this.totalArticles = response.Total || 0

          // 计算总浏览量
          this.totalViews = this.articles.reduce((total, article) => {
            return total + (article.ViewCount || 0)
          }, 0)
        } else {
          throw new Error(response.Message || '获取文章列表失败')
        }
      } catch (error) {
        console.error('加载文章失败:', error)
        this.articles = []
        this.pagination.total = 0
      }
    },

    async loadCategories() {
      try {
        const response = await GetCategories()

        if (response.Success) {
          this.categories = response.Data || []

          // 为每个分类添加文章数量统计
          await this.loadCategoryStats()
        } else {
          throw new Error(response.Message || '获取分类失败')
        }
      } catch (error) {
        console.error('加载分类失败:', error)
        this.categories = []
      }
    },

    // 加载分类文章数量统计
    async loadCategoryStats() {
      try {
        for (const category of this.categories) {
          const response = await GetDataList({
            PageIndex: 1,
            PageRows: 1, // 只需要总数
            categoryId: category.Id,
            status: 'published'
          })

          if (response.Success) {
            category.ArticleCount = response.Total || 0
          }
        }

        // 强制更新视图
        this.$forceUpdate()
      } catch (error) {
        console.error('加载分类统计失败:', error)
      }
    },

    // 搜索处理
    handleSearch(value) {
      this.searchKeyword = value
      this.pagination.current = 1
      this.loadArticles()
    },

    handleSearchChange(e) {
      if (!e.target.value) {
        this.searchKeyword = ''
        this.pagination.current = 1
        this.loadArticles()
      }
    },

    clearSearch() {
      this.searchKeyword = ''
      this.pagination.current = 1
      this.loadArticles()
    },

    // 分类筛选
    handleCategoryFilter(categoryId) {
      this.selectedCategory = categoryId
      this.pagination.current = 1
      this.loadArticles()
    },

    // 分页处理
    handlePageChange(page) {
      this.pagination.current = page
      this.loadArticles()

      // 滚动到顶部
      window.scrollTo({ top: 0, behavior: 'smooth' })
    },

    // 处理每页显示数量改变
    handlePageSizeChange(current, size) {
      this.pagination.current = 1  // 重置到第一页
      this.pagination.pageSize = size
      this.loadArticles()

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

    goToArticle(articleId) {
      this.$router.push(`/blog-website/articles/${articleId}`)
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
        const now = new Date()
        const diffTime = now - date
        const diffDays = Math.floor(diffTime / (1000 * 60 * 60 * 24))

        if (diffDays === 0) {
          return '今天'
        } else if (diffDays === 1) {
          return '昨天'
        } else if (diffDays < 7) {
          return `${diffDays}天前`
        } else {
          return date.toLocaleDateString()
        }
      } catch (error) {
        return timeStr.split(' ')[0] // 返回日期部分
      }
    },

    // 判断是否为新发布的文章（7天内）
    isNewArticle(article) {
      if (!article.PublishedAt && !article.CreatedAt) return false

      const publishTime = new Date(article.PublishedAt || article.CreatedAt)
      const now = new Date()
      const diffTime = now - publishTime
      const diffDays = Math.floor(diffTime / (1000 * 60 * 60 * 24))

      return diffDays <= 7
    },

    // 鼠标交互效果
    initMouseEffect() {
      document.addEventListener('mousemove', (e) => {
        const particles = document.querySelectorAll('.particle')
        const x = e.clientX / window.innerWidth
        const y = e.clientY / window.innerHeight

        particles.forEach((particle, index) => {
          const speed = (index + 1) * 0.3
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
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);

  &:hover {
    transform: translateY(-2px);
    box-shadow: 0 8px 32px rgba(102, 126, 234, 0.2);
  }
}

// 主容器
.articles-container {
  min-height: 100vh;
  background: #ffffff;
  position: relative;
  overflow-x: hidden;
}

// 背景动画 (重用首页样式)
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
    opacity: 0.1;
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
      width: 3px;
      height: 3px;
      background: radial-gradient(circle, rgba(102, 126, 234, 0.5) 0%, transparent 70%);
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
  background: rgba(255, 255, 255, 0.8);
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

// 面包屑导航
.breadcrumb-section {
  padding: 0 40px;
  margin-bottom: 20px;

  .breadcrumb-container {
    max-width: 1400px;
    margin: 0 auto;
    padding: 16px 24px;
    border-radius: 12px;
  }

  .custom-breadcrumb {
    margin: 0;

    ::v-deep .ant-breadcrumb-separator {
      color: rgba(102, 126, 234, 0.6);
      margin: 0 8px;
    }

    ::v-deep .ant-breadcrumb-link {
      display: inline-flex;
      align-items: center;
      gap: 6px;
      color: var(--text-primary);
      font-weight: 500;

      .anticon {
        font-size: 14px;
        color: var(--primary-color);
      }

      &:hover {
        color: var(--primary-color);
        text-decoration: none;
      }
    }

    .breadcrumb-link {
      display: inline-flex;
      align-items: center;
      gap: 6px;
      color: var(--text-primary);
      text-decoration: none;
      font-weight: 500;
      cursor: pointer;
      transition: color 0.3s ease;

      .anticon {
        font-size: 14px;
        color: var(--primary-color);
      }

      &:hover {
        color: var(--primary-color);
      }
    }

    .breadcrumb-current {
      display: inline-flex;
      align-items: center;
      gap: 6px;
      color: var(--primary-color);
      font-weight: 600;

      .anticon {
        font-size: 14px;
      }
    }
  }
}

// 页面头部
.page-header {
  padding: 60px 0;
  text-align: center;

  .header-content-wrapper {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
  }

  .page-title {
    font-size: 48px;
    font-weight: 800;
    color: var(--text-primary);
    margin-bottom: 16px;
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
  }

  .page-subtitle {
    font-size: 18px;
    color: var(--text-secondary);
    margin-bottom: 40px;
  }

  .stats-bar {
    display: inline-flex;
    gap: 40px;
    padding: 24px 40px;

    .stat-item {
      display: flex;
      align-items: center;
      gap: 8px;

      .anticon {
        font-size: 20px;
        color: #667eea;
      }

      .stat-value {
        font-size: 24px;
        font-weight: 700;
        color: var(--text-primary);
      }

      .stat-label {
        font-size: 14px;
        color: var(--text-secondary);
      }
    }
  }
}

// 筛选区域
.filter-section {
  padding: 40px 0;

  .filter-content {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;

    .search-box {
      margin-bottom: 32px;
      padding: 24px;

      .search-input {
        max-width: 600px;

        ::v-deep .ant-input {
          font-size: 16px;
          border-radius: 12px;
          border: none;
          background: rgba(255, 255, 255, 0.8);

          &:focus {
            box-shadow: 0 0 0 2px rgba(102, 126, 234, 0.2);
          }
        }
      }
    }

    .category-filters {
      display: flex;
      flex-wrap: wrap;
      gap: 12px;

      .filter-chip {
        display: flex;
        align-items: center;
        gap: 8px;
        padding: 8px 16px;
        background: rgba(255, 255, 255, 0.4);
        backdrop-filter: blur(10px);
        border: 1px solid rgba(102, 126, 234, 0.1);
        border-radius: 20px;
        cursor: pointer;
        transition: all 0.3s ease;

        span {
          font-size: 14px;
          color: var(--text-secondary);
        }

        .count {
          background: rgba(102, 126, 234, 0.1);
          color: #667eea;
          padding: 2px 6px;
          border-radius: 10px;
          font-size: 12px;
        }

        &.active,
        &:hover {
          background: rgba(102, 126, 234, 0.1);
          border-color: #667eea;

          span {
            color: #667eea;
          }
        }
      }
    }
  }
}

// 文章区域
.articles-section {
  padding: 0 0 80px;

  .articles-content {
    max-width: 1400px;
    margin: 0 auto;
    padding: 0 40px;
  }

  // 加载状态
  .loading-container {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(380px, 1fr));
    gap: 32px;

    .skeleton-card {
      padding: 24px;

      .skeleton-content {
        display: flex;
        gap: 16px;

        .skeleton-avatar {
          width: 60px;
          height: 60px;
          border-radius: 50%;
          background: linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%);
          background-size: 400% 100%;
          animation: loading-shimmer 1.4s ease infinite;
          flex-shrink: 0;
        }

        .skeleton-text {
          flex: 1;
          display: flex;
          flex-direction: column;
          gap: 12px;

          .skeleton-line {
            height: 16px;
            background: linear-gradient(90deg, #f0f0f0 25%, #e0e0e0 50%, #f0f0f0 75%);
            background-size: 400% 100%;
            border-radius: 4px;
            animation: loading-shimmer 1.4s ease infinite;

            &.long {
              width: 100%;
            }

            &.medium {
              width: 80%;
            }

            &.short {
              width: 60%;
            }
          }
        }
      }
    }
  }

  // 文章网格
  .articles-grid {
    display: grid;
    grid-template-columns: repeat(auto-fill, minmax(400px, 1fr));
    gap: 32px;

    .article-card {
      padding: 0;
      overflow: hidden;
      cursor: pointer;
      min-height: 380px;
      display: flex;
      flex-direction: column;

      .article-cover {
        position: relative;
        height: 200px;
        overflow: hidden;

        img {
          width: 100%;
          height: 100%;
          object-fit: cover;
          transition: transform 0.3s ease;
        }

        .default-cover {
          width: 100%;
          height: 100%;
          background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
          display: flex;
          align-items: center;
          justify-content: center;

          .anticon {
            font-size: 48px;
            color: white;
            opacity: 0.8;
          }
        }

        .featured-badge {
          position: absolute;
          top: 12px;
          left: 12px;
          background: rgba(255, 165, 0, 0.9);
          color: white;
          padding: 4px 8px;
          border-radius: 12px;
          font-size: 12px;
          display: flex;
          align-items: center;
          gap: 4px;
        }

        .category-tag {
          position: absolute;
          top: 12px;
          right: 12px;
          background: rgba(255, 255, 255, 0.9);
          color: #667eea;
          padding: 4px 8px;
          border-radius: 12px;
          font-size: 12px;
          font-weight: 500;
        }
      }

      .article-content {
        padding: 24px;
        flex: 1;
        display: flex;
        flex-direction: column;

        .article-title {
          font-size: 20px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 12px;
          line-height: 1.4;
          display: -webkit-box;
          -webkit-line-clamp: 2;
          -webkit-box-orient: vertical;
          overflow: hidden;
        }

        .article-summary {
          font-size: 14px;
          color: var(--text-secondary);
          line-height: 1.6;
          margin-bottom: 16px;
          display: -webkit-box;
          -webkit-line-clamp: 3;
          -webkit-box-orient: vertical;
          overflow: hidden;
          flex: 1;
        }

        .article-stats {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin-bottom: 16px;

          .stat-group {
            display: flex;
            gap: 16px;

            .stat {
              font-size: 12px;
              color: var(--text-muted);
              display: flex;
              align-items: center;
              gap: 4px;
            }
          }

          .publish-info {
            .publish-time {
              font-size: 12px;
              color: var(--text-muted);
            }
          }
        }

        .article-footer {
          display: flex;
          justify-content: space-between;
          align-items: center;

          .article-author {
            .author-name {
              font-size: 14px;
              font-weight: 500;
              color: var(--text-secondary);
            }
          }

          .article-badges {
            display: flex;
            gap: 8px;

            .badge {
              display: flex;
              align-items: center;
              gap: 4px;
              padding: 2px 8px;
              border-radius: 12px;
              font-size: 12px;
              font-weight: 500;

              &.featured {
                background: rgba(255, 165, 0, 0.1);
                color: #ff8c00;
              }

              &.new {
                background: rgba(82, 196, 26, 0.1);
                color: #52c41a;
              }
            }
          }
        }
      }

      &:hover {
        .article-cover img {
          transform: scale(1.05);
        }
      }
    }
  }

  // 空状态
  .empty-state {
    padding: 80px 0;
    text-align: center;

    .empty-content {
      padding: 60px 40px;
      max-width: 400px;
      margin: 0 auto;

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
        color: var(--text-secondary);
        margin-bottom: 24px;
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

      ::v-deep .ant-pagination-prev,
      ::v-deep .ant-pagination-next {
        background: var(--glass-bg);
        backdrop-filter: blur(10px);
        border: 1px solid var(--glass-border);
        border-radius: 8px;
        margin: 0 4px;

        a {
          color: var(--text-primary);
        }

        &:hover {
          background: rgba(102, 126, 234, 0.1);
          border-color: #667eea;

          a {
            color: #667eea;
          }
        }
      }

      ::v-deep .ant-pagination-jump-prev,
      ::v-deep .ant-pagination-jump-next {
        background: var(--glass-bg);
        backdrop-filter: blur(10px);
        border: 1px solid var(--glass-border);
        border-radius: 8px;
        margin: 0 4px;

        a {
          color: var(--text-secondary);
        }

        &:hover a {
          color: #667eea;
        }
      }

      ::v-deep .ant-pagination-options {
        margin-left: 16px;

        .ant-select {
          .ant-select-selection {
            background: var(--glass-bg);
            backdrop-filter: blur(10px);
            border: 1px solid var(--glass-border);
            border-radius: 6px;
            color: var(--text-primary);
          }

          &:hover .ant-select-selection {
            border-color: #667eea;
          }
        }

        .ant-pagination-options-quick-jumper {
          input {
            background: var(--glass-bg);
            backdrop-filter: blur(10px);
            border: 1px solid var(--glass-border);
            border-radius: 6px;
            color: var(--text-primary);

            &:hover,
            &:focus {
              border-color: #667eea;
              box-shadow: 0 0 0 2px rgba(102, 126, 234, 0.2);
            }
          }
        }
      }

      ::v-deep .ant-pagination-total-text {
        color: var(--text-secondary);
        font-size: 14px;
        margin-right: 16px;
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
    transform: translateY(-100px) translateX(50px);
    opacity: 0;
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
    height: 16px;
    width: 50px;
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
  .articles-grid {
    grid-template-columns: repeat(auto-fill, minmax(320px, 1fr));
  }
}

@media (max-width: 768px) {
  .blog-header .header-content {
    padding: 0 20px;
  }

  .page-header {
    .header-content-wrapper {
      padding: 0 20px;
    }

    .page-title {
      font-size: 32px;
    }

    .stats-bar {
      flex-direction: column;
      gap: 16px;
      padding: 20px;
    }
  }

  .filter-content,
  .articles-content {
    padding: 0 20px;
  }

  .articles-grid {
    grid-template-columns: 1fr;
    gap: 20px;
  }

  .footer-content {
    padding: 24px 20px;
    flex-direction: column;
    gap: 16px;
    text-align: center;
  }
}
</style>