<template>
  <div class="blog-home">
    <!-- 欢迎横幅 -->
    <section class="hero-section">
      <div class="hero-content">
        <h1 class="hero-title">欢迎来到我的技术博客</h1>
        <p class="hero-subtitle">分享技术心得，记录成长足迹，与你一起探索技术的无限可能</p>
        <div class="hero-actions">
          <router-link to="/blog/articles" class="hero-btn primary">浏览文章</router-link>
          <router-link to="/blog/projects" class="hero-btn secondary">查看项目</router-link>
        </div>
      </div>
      <div class="hero-decoration">
        <div class="floating-card card1">
          <a-icon type="code" />
          <span>前端开发</span>
        </div>
        <div class="floating-card card2">
          <a-icon type="database" />
          <span>后端架构</span>
        </div>
        <div class="floating-card card3">
          <a-icon type="mobile" />
          <span>移动开发</span>
        </div>
      </div>
    </section>

    <!-- 最新文章 -->
    <section class="latest-articles">
      <div class="section-header">
        <h2 class="section-title">最新文章</h2>
        <router-link to="/blog/articles" class="view-more">查看更多 <a-icon type="arrow-right" /></router-link>
      </div>
      <div class="articles-grid">
        <div v-for="article in latestArticles" :key="article.id" class="article-card" @click="viewArticle(article.id)">
          <div class="article-cover">
            <img :src="article.coverImage || defaultCover" :alt="article.title" />
            <div class="article-meta">
              <span class="article-date">{{ formatDate(article.createdAt) }}</span>
            </div>
          </div>
          <div class="article-content">
            <h3 class="article-title">{{ article.title }}</h3>
            <p class="article-summary">{{ article.summary || '暂无摘要...' }}</p>
            <div class="article-tags">
              <a-tag v-for="tag in article.tags" :key="tag" color="blue">{{ tag }}</a-tag>
            </div>
            <div class="article-stats">
              <span class="stat-item">
                <a-icon type="eye" />
                {{ article.viewCount || 0 }}
              </span>
              <span class="stat-item">
                <a-icon type="like" />
                {{ article.likeCount || 0 }}
              </span>
              <span class="stat-item">
                <a-icon type="message" />
                {{ article.commentCount || 0 }}
              </span>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- 热门项目 -->
    <section class="featured-projects">
      <div class="section-header">
        <h2 class="section-title">热门项目</h2>
        <router-link to="/blog/projects" class="view-more">查看更多 <a-icon type="arrow-right" /></router-link>
      </div>
      <div class="projects-grid">
        <div v-for="project in featuredProjects" :key="project.id" class="project-card"
          @click="viewProject(project.id)">
          <div class="project-cover">
            <img :src="project.coverImage || defaultProjectCover" :alt="project.name" />
            <div class="project-overlay">
              <div class="project-links">
                <a :href="project.demoUrl" target="_blank" class="project-link demo" v-if="project.demoUrl">
                  <a-icon type="eye" />
                  演示
                </a>
                <a :href="project.githubUrl" target="_blank" class="project-link github" v-if="project.githubUrl">
                  <a-icon type="github" />
                  代码
                </a>
              </div>
            </div>
          </div>
          <div class="project-content">
            <h3 class="project-name">{{ project.name }}</h3>
            <p class="project-description">{{ project.description }}</p>
            <div class="project-tech">
              <a-tag v-for="tech in project.technologies" :key="tech" color="green">{{ tech }}</a-tag>
            </div>
          </div>
        </div>
      </div>
    </section>

    <!-- 推荐工具 -->
    <section class="recommended-tools">
      <div class="section-header">
        <h2 class="section-title">推荐工具</h2>
        <router-link to="/blog/tools" class="view-more">查看更多 <a-icon type="arrow-right" /></router-link>
      </div>
      <div class="tools-list">
        <div v-for="tool in recommendedTools" :key="tool.id" class="tool-item" @click="viewTool(tool.id)">
          <div class="tool-icon">
            <img :src="tool.iconUrl || defaultToolIcon" :alt="tool.name" />
          </div>
          <div class="tool-info">
            <h4 class="tool-name">{{ tool.name }}</h4>
            <p class="tool-description">{{ tool.description }}</p>
          </div>
          <div class="tool-rating">
            <a-rate :value="tool.rating" disabled />
          </div>
        </div>
      </div>
    </section>
  </div>
</template>

<script>
export default {
  name: 'BlogHome',
  data() {
    return {
      latestArticles: [],
      featuredProjects: [],
      recommendedTools: [],
      defaultCover: '/assets/default-article-cover.jpg',
      defaultProjectCover: '/assets/default-project-cover.jpg',
      defaultToolIcon: '/assets/default-tool-icon.png',
      loading: false
    }
  },
  methods: {
    async loadData() {
      this.loading = true
      try {
        // 加载最新文章
        await this.loadLatestArticles()
        // 加载热门项目
        await this.loadFeaturedProjects()
        // 加载推荐工具
        await this.loadRecommendedTools()
      } catch (error) {
        console.error('加载数据失败:', error)
        this.$message.error('加载数据失败')
      } finally {
        this.loading = false
      }
    },

    async loadLatestArticles() {
      // 这里调用API获取最新文章
      // const response = await this.$http.get('/BlogApi/GetLatestArticles')
      // this.latestArticles = response.data

      // 模拟数据
      this.latestArticles = [
        {
          id: 1,
          title: 'Vue.js 3.0 新特性详解',
          summary: '详细介绍Vue.js 3.0的新特性，包括Composition API、Teleport等',
          coverImage: '/assets/vue-cover.jpg',
          createdAt: '2024-01-15',
          tags: ['Vue.js', 'JavaScript', '前端'],
          viewCount: 1250,
          likeCount: 89,
          commentCount: 23
        },
        {
          id: 2,
          title: '.NET Core微服务架构实战',
          summary: '从零开始构建基于.NET Core的微服务架构系统',
          coverImage: '/assets/dotnet-cover.jpg',
          createdAt: '2024-01-12',
          tags: ['.NET Core', '微服务', '后端'],
          viewCount: 896,
          likeCount: 65,
          commentCount: 18
        },
        {
          id: 3,
          title: 'Docker容器化部署实践',
          summary: '使用Docker进行应用容器化部署的最佳实践',
          coverImage: '/assets/docker-cover.jpg',
          createdAt: '2024-01-10',
          tags: ['Docker', '容器化', 'DevOps'],
          viewCount: 743,
          likeCount: 52,
          commentCount: 15
        }
      ]
    },

    async loadFeaturedProjects() {
      // 模拟数据
      this.featuredProjects = [
        {
          id: 1,
          name: '博客管理系统',
          description: '基于Vue.js和.NET Core的现代化博客管理系统',
          coverImage: '/assets/blog-project.jpg',
          demoUrl: '#',
          githubUrl: 'https://github.com',
          technologies: ['Vue.js', '.NET Core', 'MySQL']
        },
        {
          id: 2,
          name: '在线代码编辑器',
          description: '支持多语言的在线代码编辑和运行平台',
          coverImage: '/assets/editor-project.jpg',
          demoUrl: '#',
          githubUrl: 'https://github.com',
          technologies: ['React', 'Node.js', 'Monaco Editor']
        }
      ]
    },

    async loadRecommendedTools() {
      // 模拟数据
      this.recommendedTools = [
        {
          id: 1,
          name: 'VS Code',
          description: '最受欢迎的代码编辑器，功能强大，扩展丰富',
          iconUrl: '/assets/vscode-icon.png',
          rating: 5
        },
        {
          id: 2,
          name: 'Postman',
          description: 'API开发和测试的必备工具',
          iconUrl: '/assets/postman-icon.png',
          rating: 4.5
        },
        {
          id: 3,
          name: 'Git',
          description: '分布式版本控制系统，团队协作必备',
          iconUrl: '/assets/git-icon.png',
          rating: 5
        }
      ]
    },

    formatDate(dateStr) {
      const date = new Date(dateStr)
      return date.toLocaleDateString('zh-CN')
    },

    viewArticle(id) {
      this.$router.push(`/blog/articles/${id}`)
    },

    viewProject(id) {
      this.$router.push(`/blog/projects/${id}`)
    },

    viewTool(id) {
      this.$router.push(`/blog/tools/${id}`)
    }
  },

  mounted() {
    this.loadData()
  }
}
</script>

<style lang="less" scoped>
.blog-home {
  .hero-section {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    color: white;
    padding: 80px 0;
    margin: -40px -20px 60px;
    position: relative;
    overflow: hidden;

    .hero-content {
      max-width: 600px;
      text-align: center;
      margin: 0 auto;
      position: relative;
      z-index: 2;

      .hero-title {
        font-size: 48px;
        font-weight: 700;
        margin-bottom: 20px;
        text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
      }

      .hero-subtitle {
        font-size: 18px;
        line-height: 1.6;
        margin-bottom: 40px;
        opacity: 0.9;
      }

      .hero-actions {
        display: flex;
        gap: 20px;
        justify-content: center;

        .hero-btn {
          padding: 15px 30px;
          border-radius: 25px;
          text-decoration: none;
          font-weight: 600;
          font-size: 16px;
          transition: all 0.3s ease;

          &.primary {
            background: #ff6b6b;
            color: white;

            &:hover {
              background: #ff5252;
              transform: translateY(-2px);
              color: white;
            }
          }

          &.secondary {
            background: transparent;
            color: white;
            border: 2px solid rgba(255, 255, 255, 0.3);

            &:hover {
              background: rgba(255, 255, 255, 0.1);
              color: white;
            }
          }
        }
      }
    }

    .hero-decoration {
      position: absolute;
      top: 0;
      left: 0;
      right: 0;
      bottom: 0;
      pointer-events: none;

      .floating-card {
        position: absolute;
        background: rgba(255, 255, 255, 0.1);
        border-radius: 10px;
        padding: 15px;
        backdrop-filter: blur(10px);
        border: 1px solid rgba(255, 255, 255, 0.2);
        display: flex;
        align-items: center;
        gap: 10px;
        color: white;
        font-size: 14px;
        font-weight: 500;
        animation: float 6s ease-in-out infinite;

        .anticon {
          font-size: 18px;
        }

        &.card1 {
          top: 20%;
          left: 10%;
          animation-delay: 0s;
        }

        &.card2 {
          top: 30%;
          right: 15%;
          animation-delay: 2s;
        }

        &.card3 {
          bottom: 25%;
          left: 15%;
          animation-delay: 4s;
        }
      }
    }
  }

  .section-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    margin-bottom: 30px;

    .section-title {
      font-size: 28px;
      font-weight: 700;
      color: #2c3e50;
      margin: 0;
    }

    .view-more {
      color: #3498db;
      text-decoration: none;
      font-weight: 500;
      transition: all 0.3s ease;

      &:hover {
        color: #2980b9;
        transform: translateX(3px);
      }
    }
  }

  .latest-articles {
    margin-bottom: 60px;

    .articles-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
      gap: 30px;

      .article-card {
        background: white;
        border-radius: 15px;
        overflow: hidden;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
        transition: all 0.3s ease;
        cursor: pointer;

        &:hover {
          transform: translateY(-5px);
          box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
        }

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

          .article-meta {
            position: absolute;
            top: 15px;
            right: 15px;

            .article-date {
              background: rgba(0, 0, 0, 0.7);
              color: white;
              padding: 5px 12px;
              border-radius: 15px;
              font-size: 12px;
            }
          }
        }

        .article-content {
          padding: 25px;

          .article-title {
            font-size: 18px;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 12px;
            line-height: 1.4;
          }

          .article-summary {
            color: #7f8c8d;
            line-height: 1.6;
            margin-bottom: 15px;
            display: -webkit-box;
            -webkit-line-clamp: 2;
            -webkit-box-orient: vertical;
            overflow: hidden;
          }

          .article-tags {
            margin-bottom: 15px;

            .ant-tag {
              margin-right: 8px;
              margin-bottom: 5px;
            }
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

        &:hover .article-cover img {
          transform: scale(1.05);
        }
      }
    }
  }

  .featured-projects {
    margin-bottom: 60px;

    .projects-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
      gap: 30px;

      .project-card {
        background: white;
        border-radius: 15px;
        overflow: hidden;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
        transition: all 0.3s ease;
        cursor: pointer;

        &:hover {
          transform: translateY(-5px);
          box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
        }

        .project-cover {
          position: relative;
          height: 250px;
          overflow: hidden;

          img {
            width: 100%;
            height: 100%;
            object-fit: cover;
            transition: transform 0.3s ease;
          }

          .project-overlay {
            position: absolute;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background: rgba(0, 0, 0, 0.7);
            display: flex;
            align-items: center;
            justify-content: center;
            opacity: 0;
            transition: opacity 0.3s ease;

            .project-links {
              display: flex;
              gap: 15px;

              .project-link {
                background: rgba(255, 255, 255, 0.2);
                color: white;
                padding: 10px 20px;
                border-radius: 20px;
                text-decoration: none;
                font-weight: 500;
                transition: all 0.3s ease;
                backdrop-filter: blur(10px);

                &:hover {
                  background: rgba(255, 255, 255, 0.3);
                  color: white;
                  transform: translateY(-2px);
                }

                .anticon {
                  margin-right: 5px;
                }
              }
            }
          }

          &:hover .project-overlay {
            opacity: 1;
          }

          &:hover img {
            transform: scale(1.05);
          }
        }

        .project-content {
          padding: 25px;

          .project-name {
            font-size: 20px;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 12px;
          }

          .project-description {
            color: #7f8c8d;
            line-height: 1.6;
            margin-bottom: 15px;
          }

          .project-tech {
            .ant-tag {
              margin-right: 8px;
              margin-bottom: 5px;
            }
          }
        }
      }
    }
  }

  .recommended-tools {
    .tools-list {
      display: flex;
      flex-direction: column;
      gap: 15px;

      .tool-item {
        background: white;
        padding: 20px;
        border-radius: 15px;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
        display: flex;
        align-items: center;
        gap: 20px;
        transition: all 0.3s ease;
        cursor: pointer;

        &:hover {
          transform: translateY(-2px);
          box-shadow: 0 6px 25px rgba(0, 0, 0, 0.12);
        }

        .tool-icon {
          width: 60px;
          height: 60px;
          flex-shrink: 0;

          img {
            width: 100%;
            height: 100%;
            object-fit: contain;
            border-radius: 10px;
          }
        }

        .tool-info {
          flex: 1;

          .tool-name {
            font-size: 18px;
            font-weight: 600;
            color: #2c3e50;
            margin: 0 0 8px 0;
          }

          .tool-description {
            color: #7f8c8d;
            margin: 0;
            line-height: 1.5;
          }
        }

        .tool-rating {
          flex-shrink: 0;
        }
      }
    }
  }
}

@keyframes float {

  0%,
  100% {
    transform: translateY(0) rotate(0deg);
  }

  50% {
    transform: translateY(-10px) rotate(5deg);
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-home {
    .hero-section {
      padding: 60px 20px;

      .hero-content {
        .hero-title {
          font-size: 32px;
        }

        .hero-subtitle {
          font-size: 16px;
        }

        .hero-actions {
          flex-direction: column;
          align-items: center;

          .hero-btn {
            width: 200px;
            text-align: center;
          }
        }
      }

      .hero-decoration .floating-card {
        display: none;
      }
    }

    .articles-grid {
      grid-template-columns: 1fr !important;
    }

    .projects-grid {
      grid-template-columns: 1fr !important;
    }
  }
}
</style>