<template>
  <div class="blog-projects">
    <div class="projects-header">
      <h1 class="page-title">项目展示</h1>
      <p class="page-subtitle">记录开发历程，分享实战经验</p>
    </div>

    <div class="projects-content">
      <div class="projects-grid">
        <div 
          v-for="project in projects" 
          :key="project.id" 
          class="project-card"
          @click="viewProject(project.id)">
          <div class="project-cover">
            <img :src="project.coverImage || defaultCover" :alt="project.name" />
            <div class="project-overlay">
              <div class="project-links">
                <a v-if="project.demoUrl" :href="project.demoUrl" target="_blank" class="project-link demo" @click.stop>
                  <a-icon type="eye" />
                  演示
                </a>
                <a v-if="project.githubUrl" :href="project.githubUrl" target="_blank" class="project-link github" @click.stop>
                  <a-icon type="github" />
                  代码
                </a>
                <a v-if="project.websiteUrl" :href="project.websiteUrl" target="_blank" class="project-link website" @click.stop>
                  <a-icon type="global" />
                  官网
                </a>
              </div>
            </div>
            <div class="project-status" :class="project.status">
              {{ getStatusText(project.status) }}
            </div>
          </div>
          <div class="project-content">
            <h3 class="project-name">{{ project.name }}</h3>
            <p class="project-description">{{ project.description }}</p>
            <div class="project-tech">
              <a-tag v-for="tech in project.technologies" :key="tech" color="green">{{ tech }}</a-tag>
            </div>
            <div class="project-meta">
              <span class="difficulty" :class="project.difficultyLevel">
                <a-icon type="star" />
                {{ getDifficultyText(project.difficultyLevel) }}
              </span>
              <span class="project-date">{{ formatDate(project.createdAt) }}</span>
            </div>
          </div>
        </div>
      </div>

      <div class="loading-more" v-if="loading">
        <a-spin size="large" />
      </div>

      <div class="empty-state" v-if="!loading && projects.length === 0">
        <a-empty description="暂无项目" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogProjects',
  data() {
    return {
      projects: [],
      loading: false,
      defaultCover: '/assets/default-project-cover.jpg'
    }
  },
  methods: {
    async loadProjects() {
      this.loading = true
      try {
        // 模拟数据
        this.projects = [
          {
            id: 1,
            name: '博客管理系统',
            description: '基于Vue.js和.NET Core开发的现代化博客管理系统，支持文章管理、用户管理、评论系统等功能。',
            coverImage: '/assets/blog-project.jpg',
            status: 'completed',
            difficultyLevel: 'intermediate',
            demoUrl: '#',
            githubUrl: 'https://github.com',
            websiteUrl: '',
            technologies: ['Vue.js', '.NET Core', 'MySQL', 'Ant Design'],
            createdAt: '2024-01-01'
          },
          {
            id: 2,
            name: '在线代码编辑器',
            description: '支持多语言的在线代码编辑和运行平台，集成Monaco Editor，支持语法高亮和代码提示。',
            coverImage: '/assets/editor-project.jpg',
            status: 'active',
            difficultyLevel: 'advanced',
            demoUrl: '#',
            githubUrl: 'https://github.com',
            websiteUrl: '#',
            technologies: ['React', 'Node.js', 'Monaco Editor', 'WebSocket'],
            createdAt: '2024-02-15'
          },
          {
            id: 3,
            name: 'API文档生成工具',
            description: '自动生成RESTful API文档的工具，支持Swagger规范，提供美观的在线文档界面。',
            coverImage: '/assets/api-project.jpg',
            status: 'archived',
            difficultyLevel: 'beginner',
            demoUrl: '#',
            githubUrl: 'https://github.com',
            websiteUrl: '',
            technologies: ['JavaScript', 'Express.js', 'Swagger', 'Bootstrap'],
            createdAt: '2023-12-10'
          }
        ]
      } catch (error) {
        console.error('加载项目失败:', error)
        this.$message.error('加载项目失败')
      } finally {
        this.loading = false
      }
    },

    getStatusText(status) {
      const statusMap = {
        active: '进行中',
        completed: '已完成',
        archived: '已归档'
      }
      return statusMap[status] || '未知'
    },

    getDifficultyText(level) {
      const difficultyMap = {
        beginner: '入门',
        intermediate: '中级',
        advanced: '高级'
      }
      return difficultyMap[level] || '未知'
    },

    formatDate(dateStr) {
      const date = new Date(dateStr)
      return date.toLocaleDateString('zh-CN')
    },

    viewProject(id) {
      this.$router.push(`/blog/projects/${id}`)
    }
  },

  mounted() {
    this.loadProjects()
  }
}
</script>

<style lang="less" scoped>
.blog-projects {
  .projects-header {
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
  
  .projects-content {
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
                
                &.demo {
                  background: rgba(52, 152, 219, 0.8);
                }
                
                &.github {
                  background: rgba(52, 73, 94, 0.8);
                }
                
                &.website {
                  background: rgba(46, 204, 113, 0.8);
                }
              }
            }
          }
          
          .project-status {
            position: absolute;
            top: 15px;
            right: 15px;
            padding: 5px 12px;
            border-radius: 20px;
            font-size: 12px;
            font-weight: 500;
            color: white;
            
            &.active {
              background: linear-gradient(45deg, #3498db, #2980b9);
            }
            
            &.completed {
              background: linear-gradient(45deg, #27ae60, #229954);
            }
            
            &.archived {
              background: linear-gradient(45deg, #95a5a6, #7f8c8d);
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
            line-height: 1.4;
          }
          
          .project-description {
            color: #7f8c8d;
            line-height: 1.6;
            margin-bottom: 15px;
            display: -webkit-box;
            -webkit-line-clamp: 3;
            -webkit-box-orient: vertical;
            overflow: hidden;
          }
          
          .project-tech {
            margin-bottom: 15px;
            
            .ant-tag {
              margin-right: 8px;
              margin-bottom: 5px;
            }
          }
          
          .project-meta {
            display: flex;
            justify-content: space-between;
            align-items: center;
            
            .difficulty {
              display: flex;
              align-items: center;
              gap: 5px;
              font-size: 14px;
              font-weight: 500;
              
              &.beginner {
                color: #27ae60;
              }
              
              &.intermediate {
                color: #f39c12;
              }
              
              &.advanced {
                color: #e74c3c;
              }
              
              .anticon {
                font-size: 14px;
              }
            }
            
            .project-date {
              color: #95a5a6;
              font-size: 14px;
            }
          }
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
  .blog-projects {
    .projects-header {
      .page-title {
        font-size: 28px;
      }
    }
    
    .projects-content {
      .projects-grid {
        grid-template-columns: 1fr;
        
        .project-card {
          .project-content {
            padding: 20px;
            
            .project-name {
              font-size: 18px;
            }
            
            .project-meta {
              flex-direction: column;
              align-items: flex-start;
              gap: 8px;
            }
          }
        }
      }
    }
  }
}
</style>