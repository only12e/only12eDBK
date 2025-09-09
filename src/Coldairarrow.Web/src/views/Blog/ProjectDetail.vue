<template>
  <div class="blog-project-detail">
    <div v-if="loading" class="loading-container">
      <a-spin size="large" />
    </div>
    
    <div v-else-if="project" class="project-container">
      <!-- 项目头部 -->
      <div class="project-header">
        <div class="breadcrumb">
          <router-link to="/blog/home">首页</router-link>
          <a-icon type="right" />
          <router-link to="/blog/projects">项目展示</router-link>
          <a-icon type="right" />
          <span>{{ project.name }}</span>
        </div>
        
        <div class="project-title-section">
          <h1 class="project-title">{{ project.name }}</h1>
          <div class="project-status" :class="project.status">
            {{ getStatusText(project.status) }}
          </div>
        </div>
        
        <div class="project-meta">
          <div class="meta-left">
            <span class="meta-item">
              <a-icon type="calendar" />
              {{ formatDate(project.createdAt) }}
            </span>
            <span class="meta-item">
              <a-icon type="star" />
              {{ getDifficultyText(project.difficultyLevel) }}
            </span>
          </div>
          <div class="meta-right">
            <span class="meta-item">
              <a-icon type="eye" />
              {{ project.viewCount || 0 }}
            </span>
            <span class="meta-item">
              <a-icon type="like" theme="filled" :class="{ liked: isLiked }" @click="toggleLike" />
              {{ project.likeCount || 0 }}
            </span>
          </div>
        </div>
        
        <!-- 项目链接 -->
        <div class="project-links">
          <a v-if="project.demoUrl" :href="project.demoUrl" target="_blank" class="project-link demo">
            <a-icon type="eye" />
            在线演示
          </a>
          <a v-if="project.githubUrl" :href="project.githubUrl" target="_blank" class="project-link github">
            <a-icon type="github" />
            源码地址
          </a>
          <a v-if="project.websiteUrl" :href="project.websiteUrl" target="_blank" class="project-link website">
            <a-icon type="global" />
            项目官网
          </a>
        </div>
      </div>
      
      <!-- 项目封面 -->
      <div v-if="project.coverImage" class="project-cover">
        <img :src="project.coverImage" :alt="project.name" />
      </div>
      
      <!-- 项目描述 -->
      <div class="project-description">
        <h3 class="section-title">项目简介</h3>
        <p class="description-text">{{ project.description }}</p>
      </div>
      
      <!-- 技术栈 -->
      <div class="project-tech-stack">
        <h3 class="section-title">技术栈</h3>
        <div class="tech-list">
          <a-tag v-for="tech in project.technologies" :key="tech" color="green" class="tech-tag">
            {{ tech }}
          </a-tag>
        </div>
      </div>
      
      <!-- 项目详情 -->
      <div class="project-content">
        <h3 class="section-title">项目详情</h3>
        <div class="content-body" v-html="project.content"></div>
      </div>
      
      <!-- 项目特性 -->
      <div v-if="project.features && project.features.length > 0" class="project-features">
        <h3 class="section-title">主要特性</h3>
        <div class="features-list">
          <div v-for="feature in project.features" :key="feature.id" class="feature-item">
            <div class="feature-icon">
              <a-icon :type="feature.icon" />
            </div>
            <div class="feature-content">
              <h4 class="feature-title">{{ feature.title }}</h4>
              <p class="feature-description">{{ feature.description }}</p>
            </div>
          </div>
        </div>
      </div>
      
      <!-- 项目截图 -->
      <div v-if="project.screenshots && project.screenshots.length > 0" class="project-screenshots">
        <h3 class="section-title">项目截图</h3>
        <div class="screenshots-grid">
          <div 
            v-for="screenshot in project.screenshots" 
            :key="screenshot.id"
            class="screenshot-item"
            @click="previewImage(screenshot.url)">
            <img :src="screenshot.url" :alt="screenshot.title" />
            <div class="screenshot-overlay">
              <a-icon type="eye" />
            </div>
          </div>
        </div>
      </div>
      
      <!-- 项目操作 -->
      <div class="project-actions">
        <a-button 
          :type="isLiked ? 'primary' : 'default'" 
          @click="toggleLike"
          class="action-btn like-btn">
          <a-icon type="like" :theme="isLiked ? 'filled' : 'outlined'" />
          {{ isLiked ? '已点赞' : '点赞' }}
        </a-button>
        <a-button @click="showShareModal" class="action-btn share-btn">
          <a-icon type="share-alt" />
          分享项目
        </a-button>
        <a-button @click="scrollToComments" class="action-btn comment-btn">
          <a-icon type="message" />
          评论
        </a-button>
      </div>
      
      <!-- 相关项目 -->
      <div class="related-projects" v-if="relatedProjects.length > 0">
        <h3 class="section-title">相关项目</h3>
        <div class="related-list">
          <div 
            v-for="related in relatedProjects" 
            :key="related.id"
            class="related-item"
            @click="viewRelatedProject(related.id)">
            <img :src="related.coverImage || defaultCover" :alt="related.name" class="related-cover" />
            <div class="related-info">
              <h4 class="related-project-title">{{ related.name }}</h4>
              <p class="related-description">{{ related.description }}</p>
              <div class="related-tech">
                <a-tag v-for="tech in related.technologies.slice(0, 3)" :key="tech" size="small">
                  {{ tech }}
                </a-tag>
              </div>
            </div>
          </div>
        </div>
      </div>
      
      <!-- 评论区域 -->
      <div id="comments" class="comments-section">
        <h3 class="comments-title">项目讨论</h3>
        <div class="comments-placeholder">
          <a-empty description="暂无评论，快来发表看法吧！" />
        </div>
      </div>
    </div>
    
    <div v-else class="not-found">
      <a-result
        status="404"
        title="项目未找到"
        sub-title="抱歉，您访问的项目不存在或已被删除。">
        <template #extra>
          <a-button type="primary" @click="$router.push('/blog/projects')">
            返回项目列表
          </a-button>
        </template>
      </a-result>
    </div>
    
    <!-- 分享弹窗 -->
    <a-modal
      v-model="shareModalVisible"
      title="分享项目"
      :footer="null"
      width="400px">
      <div class="share-options">
        <div class="share-item" @click="shareToWeChat">
          <a-icon type="wechat" style="color: #2ecc71;" />
          <span>微信</span>
        </div>
        <div class="share-item" @click="shareToWeibo">
          <a-icon type="weibo" style="color: #e74c3c;" />
          <span>微博</span>
        </div>
        <div class="share-item" @click="copyLink">
          <a-icon type="link" style="color: #3498db;" />
          <span>复制链接</span>
        </div>
      </div>
    </a-modal>
    
    <!-- 图片预览 -->
    <a-modal
      v-model="imagePreviewVisible"
      :footer="null"
      width="80%"
      centered>
      <img :src="previewImageUrl" style="width: 100%;" />
    </a-modal>
  </div>
</template>

<script>
export default {
  name: 'BlogProjectDetail',
  data() {
    return {
      project: null,
      relatedProjects: [],
      loading: true,
      isLiked: false,
      shareModalVisible: false,
      imagePreviewVisible: false,
      previewImageUrl: '',
      defaultCover: '/assets/default-project-cover.jpg'
    }
  },
  methods: {
    async loadProject() {
      const projectId = this.$route.params.id
      this.loading = true
      
      try {
        // 模拟数据
        this.project = {
          id: parseInt(projectId),
          name: '博客管理系统',
          description: '基于Vue.js和.NET Core开发的现代化博客管理系统，支持文章管理、用户管理、评论系统、友情链接等功能。采用前后端分离架构，具有良好的扩展性和维护性。',
          status: 'completed',
          difficultyLevel: 'intermediate',
          coverImage: '/assets/blog-project.jpg',
          demoUrl: '#',
          githubUrl: 'https://github.com',
          websiteUrl: '',
          technologies: ['Vue.js', '.NET Core', 'MySQL', 'Ant Design', 'Entity Framework'],
          createdAt: '2024-01-01',
          viewCount: 856,
          likeCount: 42,
          content: `
            <h4>项目背景</h4>
            <p>随着互联网的发展，个人博客成为了技术人员分享知识和经验的重要平台。本项目旨在开发一个功能完善、易于使用的博客管理系统。</p>
            
            <h4>核心功能</h4>
            <ul>
              <li><strong>文章管理</strong>：支持富文本编辑，文章分类、标签管理</li>
              <li><strong>用户系统</strong>：用户注册登录、权限管理</li>
              <li><strong>评论系统</strong>：支持多级回复、评论审核</li>
              <li><strong>友情链接</strong>：友情链接管理和展示</li>
              <li><strong>数据统计</strong>：访问统计、数据分析</li>
            </ul>
            
            <h4>技术亮点</h4>
            <p>本项目采用了多项现代Web开发技术：</p>
            <ul>
              <li>前端使用Vue.js 3.0 + Ant Design Vue构建响应式用户界面</li>
              <li>后端基于.NET Core 6.0，采用RESTful API设计</li>
              <li>数据持久化使用Entity Framework Core ORM</li>
              <li>支持Docker容器化部署</li>
            </ul>
          `,
          features: [
            {
              id: 1,
              icon: 'edit',
              title: '富文本编辑',
              description: '集成强大的富文本编辑器，支持图片上传、代码高亮等功能'
            },
            {
              id: 2,
              icon: 'user',
              title: '用户管理',
              description: '完整的用户注册、登录、权限管理系统'
            },
            {
              id: 3,
              icon: 'message',
              title: '评论系统',
              description: '支持多级评论、回复通知、评论审核等功能'
            },
            {
              id: 4,
              icon: 'bar-chart',
              title: '数据统计',
              description: '详细的访问统计和数据分析功能'
            }
          ],
          screenshots: [
            {
              id: 1,
              url: '/assets/screenshot1.jpg',
              title: '首页展示'
            },
            {
              id: 2,
              url: '/assets/screenshot2.jpg',
              title: '文章管理'
            },
            {
              id: 3,
              url: '/assets/screenshot3.jpg',
              title: '用户界面'
            }
          ]
        }
        
        // 加载相关项目
        this.relatedProjects = [
          {
            id: 2,
            name: '在线代码编辑器',
            description: '支持多语言的在线代码编辑和运行平台',
            coverImage: '/assets/editor-project.jpg',
            technologies: ['React', 'Node.js', 'Monaco Editor']
          }
        ]
        
      } catch (error) {
        console.error('加载项目失败:', error)
        this.$message.error('加载项目失败')
        this.project = null
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
    
    async toggleLike() {
      try {
        this.isLiked = !this.isLiked
        if (this.isLiked) {
          this.project.likeCount++
          this.$message.success('点赞成功！')
        } else {
          this.project.likeCount--
          this.$message.info('取消点赞')
        }
      } catch (error) {
        console.error('点赞失败:', error)
        this.$message.error('操作失败')
      }
    },
    
    showShareModal() {
      this.shareModalVisible = true
    },
    
    shareToWeChat() {
      this.$message.info('请使用微信扫码分享')
      this.shareModalVisible = false
    },
    
    shareToWeibo() {
      const url = encodeURIComponent(window.location.href)
      const title = encodeURIComponent(this.project.name)
      window.open(`https://service.weibo.com/share/share.php?url=${url}&title=${title}`)
      this.shareModalVisible = false
    },
    
    async copyLink() {
      try {
        await navigator.clipboard.writeText(window.location.href)
        this.$message.success('链接已复制到剪贴板')
      } catch (error) {
        this.$message.error('复制失败，请手动复制')
      }
      this.shareModalVisible = false
    },
    
    previewImage(url) {
      this.previewImageUrl = url
      this.imagePreviewVisible = true
    },
    
    scrollToComments() {
      document.getElementById('comments').scrollIntoView({ behavior: 'smooth' })
    },
    
    viewRelatedProject(id) {
      this.$router.push(`/blog/projects/${id}`)
    },
    
    formatDate(dateStr) {
      const date = new Date(dateStr)
      return date.toLocaleDateString('zh-CN')
    }
  },
  
  mounted() {
    this.loadProject()
  },
  
  watch: {
    '$route.params.id'() {
      this.loadProject()
    }
  }
}
</script>

<style lang="less" scoped>
.blog-project-detail {
  max-width: 900px;
  margin: 0 auto;
  
  .loading-container {
    text-align: center;
    padding: 80px 0;
  }
  
  .project-container {
    background: white;
    border-radius: 15px;
    overflow: hidden;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
    
    .project-header {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .breadcrumb {
        margin-bottom: 20px;
        color: #7f8c8d;
        font-size: 14px;
        
        a {
          color: #3498db;
          text-decoration: none;
          
          &:hover {
            color: #2980b9;
          }
        }
        
        .anticon {
          margin: 0 8px;
          font-size: 12px;
        }
      }
      
      .project-title-section {
        display: flex;
        align-items: center;
        gap: 20px;
        margin-bottom: 20px;
        
        .project-title {
          font-size: 32px;
          font-weight: 700;
          color: #2c3e50;
          margin: 0;
          line-height: 1.4;
          flex: 1;
        }
        
        .project-status {
          padding: 8px 16px;
          border-radius: 20px;
          font-size: 14px;
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
      }
      
      .project-meta {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 25px;
        
        .meta-item {
          display: inline-flex;
          align-items: center;
          gap: 5px;
          margin-right: 20px;
          color: #7f8c8d;
          font-size: 14px;
          
          .anticon {
            font-size: 14px;
            
            &.liked {
              color: #e74c3c;
            }
          }
          
          &:last-child {
            margin-right: 0;
          }
        }
        
        .meta-right {
          .meta-item {
            margin-right: 15px;
            cursor: pointer;
            
            &:hover .anticon {
              color: #3498db;
            }
          }
        }
      }
      
      .project-links {
        display: flex;
        gap: 15px;
        flex-wrap: wrap;
        
        .project-link {
          padding: 10px 20px;
          border-radius: 20px;
          text-decoration: none;
          font-weight: 500;
          transition: all 0.3s ease;
          display: inline-flex;
          align-items: center;
          gap: 8px;
          
          &:hover {
            transform: translateY(-2px);
            color: white;
          }
          
          .anticon {
            font-size: 16px;
          }
          
          &.demo {
            background: linear-gradient(45deg, #3498db, #2980b9);
            color: white;
            
            &:hover {
              background: linear-gradient(45deg, #2980b9, #21618c);
            }
          }
          
          &.github {
            background: linear-gradient(45deg, #2c3e50, #34495e);
            color: white;
            
            &:hover {
              background: linear-gradient(45deg, #34495e, #2c3e50);
            }
          }
          
          &.website {
            background: linear-gradient(45deg, #27ae60, #229954);
            color: white;
            
            &:hover {
              background: linear-gradient(45deg, #229954, #1e8449);
            }
          }
        }
      }
    }
    
    .project-cover {
      img {
        width: 100%;
        height: 400px;
        object-fit: cover;
      }
    }
    
    .section-title {
      font-size: 24px;
      font-weight: 600;
      color: #2c3e50;
      margin-bottom: 20px;
      padding-bottom: 10px;
      border-bottom: 2px solid #3498db;
    }
    
    .project-description {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .description-text {
        font-size: 16px;
        line-height: 1.8;
        color: #5a6c7d;
        text-align: justify;
        margin: 0;
      }
    }
    
    .project-tech-stack {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .tech-list {
        display: flex;
        flex-wrap: wrap;
        gap: 10px;
        
        .tech-tag {
          font-size: 14px;
          padding: 8px 16px;
          border-radius: 20px;
          font-weight: 500;
        }
      }
    }
    
    .project-content {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .content-body {
        line-height: 1.8;
        color: #2c3e50;
        font-size: 16px;
        
        :deep(h4) {
          font-size: 18px;
          font-weight: 600;
          margin: 20px 0 10px 0;
          color: #2c3e50;
        }
        
        :deep(p) {
          margin-bottom: 16px;
          text-align: justify;
        }
        
        :deep(ul) {
          margin: 16px 0;
          padding-left: 20px;
        }
        
        :deep(li) {
          margin-bottom: 8px;
        }
        
        :deep(strong) {
          color: #2c3e50;
          font-weight: 600;
        }
      }
    }
    
    .project-features {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .features-list {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
        gap: 25px;
        
        .feature-item {
          display: flex;
          gap: 15px;
          padding: 20px;
          border-radius: 10px;
          background: #f8f9fa;
          transition: all 0.3s ease;
          
          &:hover {
            background: #e9ecef;
            transform: translateY(-2px);
          }
          
          .feature-icon {
            width: 50px;
            height: 50px;
            border-radius: 25px;
            background: linear-gradient(45deg, #667eea, #764ba2);
            display: flex;
            align-items: center;
            justify-content: center;
            flex-shrink: 0;
            
            .anticon {
              font-size: 24px;
              color: white;
            }
          }
          
          .feature-content {
            .feature-title {
              font-size: 16px;
              font-weight: 600;
              color: #2c3e50;
              margin: 0 0 8px 0;
            }
            
            .feature-description {
              color: #7f8c8d;
              margin: 0;
              font-size: 14px;
              line-height: 1.6;
            }
          }
        }
      }
    }
    
    .project-screenshots {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .screenshots-grid {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 20px;
        
        .screenshot-item {
          position: relative;
          cursor: pointer;
          border-radius: 10px;
          overflow: hidden;
          
          img {
            width: 100%;
            height: 200px;
            object-fit: cover;
            transition: transform 0.3s ease;
          }
          
          .screenshot-overlay {
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
            
            .anticon {
              font-size: 32px;
              color: white;
            }
          }
          
          &:hover {
            .screenshot-overlay {
              opacity: 1;
            }
            
            img {
              transform: scale(1.05);
            }
          }
        }
      }
    }
    
    .project-actions {
      padding: 30px 40px;
      border-bottom: 1px solid #f1f2f6;
      text-align: center;
      
      .action-btn {
        margin: 0 10px;
        height: 40px;
        border-radius: 20px;
        font-weight: 500;
        
        &.like-btn {
          &.ant-btn-primary {
            background: linear-gradient(45deg, #e74c3c, #c0392b);
            border: none;
          }
        }
        
        &.share-btn {
          color: #3498db;
          border-color: #3498db;
          
          &:hover {
            background: #3498db;
            color: white;
          }
        }
        
        &.comment-btn {
          color: #2ecc71;
          border-color: #2ecc71;
          
          &:hover {
            background: #2ecc71;
            color: white;
          }
        }
      }
    }
    
    .related-projects {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .related-list {
        display: flex;
        flex-direction: column;
        gap: 20px;
        
        .related-item {
          display: flex;
          gap: 20px;
          padding: 20px;
          border-radius: 10px;
          transition: all 0.3s ease;
          cursor: pointer;
          
          &:hover {
            background: #f8f9fa;
            transform: translateX(5px);
          }
          
          .related-cover {
            width: 120px;
            height: 90px;
            object-fit: cover;
            border-radius: 8px;
            flex-shrink: 0;
          }
          
          .related-info {
            flex: 1;
            
            .related-project-title {
              font-size: 18px;
              font-weight: 500;
              color: #2c3e50;
              margin: 0 0 10px 0;
              line-height: 1.4;
            }
            
            .related-description {
              color: #7f8c8d;
              margin: 0 0 15px 0;
              display: -webkit-box;
              -webkit-line-clamp: 2;
              -webkit-box-orient: vertical;
              overflow: hidden;
            }
            
            .related-tech {
              .ant-tag {
                margin-right: 5px;
              }
            }
          }
        }
      }
    }
    
    .comments-section {
      padding: 40px;
      
      .comments-title {
        font-size: 24px;
        font-weight: 600;
        color: #2c3e50;
        margin-bottom: 25px;
      }
      
      .comments-placeholder {
        text-align: center;
        padding: 40px 0;
      }
    }
  }
  
  .not-found {
    text-align: center;
    padding: 60px 0;
  }
  
  .share-options {
    display: flex;
    justify-content: space-around;
    padding: 20px 0;
    
    .share-item {
      display: flex;
      flex-direction: column;
      align-items: center;
      gap: 10px;
      cursor: pointer;
      padding: 20px;
      border-radius: 10px;
      transition: all 0.3s ease;
      
      &:hover {
        background: #f8f9fa;
      }
      
      .anticon {
        font-size: 32px;
      }
      
      span {
        color: #2c3e50;
        font-weight: 500;
      }
    }
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-project-detail {
    margin: 0 -20px;
    
    .project-container {
      border-radius: 0;
      
      .project-header {
        padding: 20px;
        
        .project-title-section {
          flex-direction: column;
          align-items: flex-start;
          gap: 10px;
          
          .project-title {
            font-size: 24px;
          }
        }
        
        .project-meta {
          flex-direction: column;
          align-items: flex-start;
          gap: 10px;
        }
        
        .project-links {
          .project-link {
            font-size: 14px;
            padding: 8px 16px;
          }
        }
      }
      
      .project-description,
      .project-tech-stack,
      .project-content,
      .project-features,
      .project-screenshots,
      .project-actions,
      .related-projects,
      .comments-section {
        padding: 20px;
      }
      
      .project-features {
        .features-list {
          grid-template-columns: 1fr;
          gap: 15px;
          
          .feature-item {
            padding: 15px;
          }
        }
      }
      
      .project-screenshots {
        .screenshots-grid {
          grid-template-columns: 1fr;
        }
      }
      
      .project-actions {
        .action-btn {
          display: block;
          margin: 5px 0;
          width: 100%;
        }
      }
      
      .related-projects {
        .related-list {
          .related-item {
            flex-direction: column;
            
            .related-cover {
              width: 100%;
              height: 150px;
            }
          }
        }
      }
    }
  }
}
</style>