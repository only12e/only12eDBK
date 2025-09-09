<template>
  <div class="blog-articles">
    <div class="articles-header">
      <h1 class="page-title">技术文章</h1>
      <p class="page-subtitle">分享技术心得，记录学习历程</p>
    </div>

    <div class="articles-content">
      <div class="articles-list">
        <div v-for="article in articles" :key="article.id" class="article-item" @click="viewArticle(article.id)">
          <div class="article-cover">
            <img :src="article.coverImage || defaultCover" :alt="article.title" />
            <div class="article-category">{{ article.category }}</div>
          </div>
          <div class="article-info">
            <h2 class="article-title">{{ article.title }}</h2>
            <p class="article-summary">{{ article.summary }}</p>
            <div class="article-meta">
              <span class="article-date">{{ formatDate(article.createdAt) }}</span>
              <div class="article-stats">
                <span class="stat-item">
                  <a-icon type="eye" />
                  {{ article.viewCount }}
                </span>
                <span class="stat-item">
                  <a-icon type="like" />
                  {{ article.likeCount }}
                </span>
              </div>
            </div>
            <div class="article-tags">
              <a-tag v-for="tag in article.tags" :key="tag" color="blue">{{ tag }}</a-tag>
            </div>
          </div>
        </div>
      </div>

      <div class="loading-more" v-if="loading">
        <a-spin size="large" />
      </div>

      <div class="empty-state" v-if="!loading && articles.length === 0">
        <a-empty description="暂无文章" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogArticles',
  data() {
    return {
      articles: [],
      loading: false,
      defaultCover: '/assets/default-article-cover.jpg'
    }
  },
  methods: {
    async loadArticles() {
      this.loading = true
      try {
        // 模拟数据
        this.articles = [
          {
            id: 1,
            title: 'Vue.js 3.0 新特性详解',
            summary: '详细介绍Vue.js 3.0的新特性，包括Composition API、Teleport、Fragments等重要功能的使用方法和最佳实践。',
            category: '前端开发',
            coverImage: '/assets/vue-cover.jpg',
            createdAt: '2024-01-15',
            tags: ['Vue.js', 'JavaScript', '前端'],
            viewCount: 1250,
            likeCount: 89
          },
          {
            id: 2,
            title: '.NET Core微服务架构实战',
            summary: '从零开始构建基于.NET Core的微服务架构系统，包括服务注册与发现、负载均衡、API网关等核心组件的实现。',
            category: '后端开发',
            coverImage: '/assets/dotnet-cover.jpg',
            createdAt: '2024-01-12',
            tags: ['.NET Core', '微服务', '后端'],
            viewCount: 896,
            likeCount: 65
          },
          {
            id: 3,
            title: 'Docker容器化部署实践',
            summary: '使用Docker进行应用容器化部署的最佳实践，包括Dockerfile编写、镜像优化、容器编排等内容。',
            category: 'DevOps',
            coverImage: '/assets/docker-cover.jpg',
            createdAt: '2024-01-10',
            tags: ['Docker', '容器化', 'DevOps'],
            viewCount: 743,
            likeCount: 52
          }
        ]
      } catch (error) {
        console.error('加载文章失败:', error)
        this.$message.error('加载文章失败')
      } finally {
        this.loading = false
      }
    },

    formatDate(dateStr) {
      const date = new Date(dateStr)
      return date.toLocaleDateString('zh-CN')
    },

    viewArticle(id) {
      this.$router.push(`/blog/articles/${id}`)
    }
  },

  mounted() {
    this.loadArticles()
  }
}
</script>

<style lang="less" scoped>
.blog-articles {
  .articles-header {
    text-align: center;
    margin-bottom: 50px;

    .page-title {
      font-size: 36px;
      font-weight: 700;
      color: #2c3e50;
      margin-bottom: 15px;
    }

    .page-subtitle {
      font-size: 16px;
      color: #7f8c8d;
      margin: 0;
    }
  }

  .articles-content {
    .articles-list {
      display: flex;
      flex-direction: column;
      gap: 30px;

      .article-item {
        background: white;
        border-radius: 15px;
        overflow: hidden;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
        transition: all 0.3s ease;
        cursor: pointer;
        display: flex;

        &:hover {
          transform: translateY(-5px);
          box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
        }

        .article-cover {
          position: relative;
          width: 300px;
          height: 200px;
          flex-shrink: 0;

          img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            transition: transform 0.3s ease;
          }

          .article-category {
            position: absolute;
            top: 15px;
            left: 15px;
            background: linear-gradient(45deg, #667eea, #764ba2);
            color: white;
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 500;
          }
        }

        .article-info {
          flex: 1;
          padding: 25px;
          display: flex;
          flex-direction: column;

          .article-title {
            font-size: 22px;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 15px;
            line-height: 1.4;
          }

          .article-summary {
            color: #7f8c8d;
            line-height: 1.6;
            margin-bottom: 20px;
            flex: 1;
            display: -webkit-box;
            -webkit-line-clamp: 3;
            -webkit-box-orient: vertical;
            overflow: hidden;
          }

          .article-meta {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin-bottom: 15px;

            .article-date {
              color: #95a5a6;
              font-size: 14px;
            }

            .article-stats {
              display: flex;
              gap: 20px;

              .stat-item {
                display: flex;
                align-items: center;
                gap: 5px;
                color: #95a5a6;
                font-size: 14px;

                .anticon {
                  font-size: 14px;
                }
              }
            }
          }

          .article-tags {
            .ant-tag {
              margin-right: 8px;
              margin-bottom: 5px;
            }
          }
        }

        &:hover .article-cover img {
          transform: scale(1.05);
        }
      }
    }

    .loading-more {
      text-align: center;
      padding: 40px 0;
    }

    .empty-state {
      text-align: center;
      padding: 60px 0;
    }
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-articles {
    .articles-header {
      .page-title {
        font-size: 28px;
      }
    }

    .articles-content {
      .articles-list {
        .article-item {
          flex-direction: column;

          .article-cover {
            width: 100%;
            height: 200px;
          }

          .article-info {
            padding: 20px;

            .article-title {
              font-size: 18px;
            }

            .article-meta {
              flex-direction: column;
              align-items: flex-start;
              gap: 10px;
            }
          }
        }
      }
    }
  }
}
</style>