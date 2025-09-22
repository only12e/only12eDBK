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

        <!-- 评论区 -->
        <section class="comments-section">
          <div class="comments-container glass-card">
            <div class="comments-header">
              <h3>
                <a-icon type="message" />
                评论 ({{ commentCount }})
              </h3>
            </div>

            <!-- 发表评论 -->
            <div class="comment-form-container">
              <div class="comment-form glass-card-inner">
                <a-form-model ref="commentForm" :model="commentForm" :rules="commentRules">
                  <a-form-model-item prop="Content">
                    <a-textarea
                      v-model="commentForm.Content"
                      placeholder="写下你的评论..."
                      :rows="4"
                      :disabled="submittingComment"
                    />
                  </a-form-model-item>
                  <div class="form-row">
                    <div class="user-inputs">
                      <a-input
                        v-model="commentForm.Username"
                        placeholder="昵称"
                        style="width: 120px; margin-right: 12px;"
                        :disabled="submittingComment"
                      />
                      <a-input
                        v-model="commentForm.Email"
                        placeholder="邮箱（可选）"
                        style="width: 160px;"
                        :disabled="submittingComment"
                      />
                    </div>
                    <a-button
                      type="primary"
                      :loading="submittingComment"
                      @click="submitComment"
                      class="submit-btn"
                    >
                      <a-icon type="send" />
                      发表评论
                    </a-button>
                  </div>
                </a-form-model>
              </div>
            </div>

            <!-- 评论列表 -->
            <div class="comments-list" v-if="comments.length > 0">
              <div
                v-for="comment in comments"
                :key="comment.Id"
                class="comment-item glass-card-inner"
              >
                <div class="comment-header">
                  <div class="comment-author">
                    <a-avatar :size="32" class="author-avatar">
                      {{ getAvatarText(comment.User ? (comment.User.Nickname || comment.User.Username) : '匿名') }}
                    </a-avatar>
                    <div class="author-info">
                      <span class="author-name">
                        {{ comment.User ? (comment.User.Nickname || comment.User.Username) : '匿名用户' }}
                      </span>
                      <span class="comment-time">{{ formatTime(comment.CreatedAt) }}</span>
                    </div>
                  </div>
                  <div class="comment-actions">
                    <a-button
                      type="link"
                      size="small"
                      @click="likeComment(comment)"
                      :disabled="comment.liking"
                      :class="{ 'liked-button': comment.liked }"
                    >
                      <a-icon
                        type="heart"
                        :theme="comment.liked ? 'filled' : 'outlined'"
                        :style="{
                          color: comment.liked ? '#ff4757' : '#999',
                          transition: 'all 0.3s ease'
                        }"
                      />
                      {{ comment.LikeCount || 0 }}
                    </a-button>
                    <a-button
                      type="link"
                      size="small"
                      @click="showReplyForm(comment.Id)"
                    >
                      <a-icon type="message" />
                      回复
                    </a-button>
                  </div>
                </div>

                <div class="comment-content">{{ comment.Content }}</div>

                <!-- 回复表单 -->
                <div v-if="replyingTo === comment.Id" class="reply-form">
                  <a-textarea
                    v-model="replyForm.Content"
                    placeholder="写下你的回复..."
                    :rows="3"
                    :disabled="submittingReply"
                  />
                  <div class="reply-actions">
                    <a-button
                      size="small"
                      @click="cancelReply"
                    >
                      取消
                    </a-button>
                    <a-button
                      type="primary"
                      size="small"
                      :loading="submittingReply"
                      @click="submitReply(comment.Id)"
                      style="margin-left: 8px;"
                    >
                      回复
                    </a-button>
                  </div>
                </div>

                <!-- 回复列表 -->
                <div v-if="comment.replies && comment.replies.length > 0" class="replies-list">
                  <div
                    v-for="reply in comment.replies"
                    :key="reply.Id"
                    class="reply-item"
                  >
                    <div class="reply-header">
                      <a-avatar :size="24" class="author-avatar">
                        {{ getAvatarText(reply.User ? (reply.User.Nickname || reply.User.Username) : '匿名') }}
                      </a-avatar>
                      <div class="reply-info">
                        <span class="reply-author">
                          {{ reply.User ? (reply.User.Nickname || reply.User.Username) : '匿名用户' }}
                        </span>
                        <span class="reply-time">{{ formatTime(reply.CreatedAt) }}</span>
                      </div>
                      <a-button
                        type="link"
                        size="small"
                        @click="likeComment(reply)"
                        :disabled="reply.liking"
                        :class="{ 'liked-button': reply.liked }"
                      >
                        <a-icon
                          type="heart"
                          :theme="reply.liked ? 'filled' : 'outlined'"
                          :style="{
                            color: reply.liked ? '#ff4757' : '#999',
                            transition: 'all 0.3s ease'
                          }"
                        />
                        {{ reply.LikeCount || 0 }}
                      </a-button>
                    </div>
                    <div class="reply-content">{{ reply.Content }}</div>
                  </div>
                </div>
              </div>
            </div>

            <!-- 空状态 -->
            <div v-else class="empty-comments">
              <a-empty description="暂无评论">
                <span slot="description">
                  <a-icon type="message" style="font-size: 48px; color: #ccc;" />
                  <p style="margin-top: 16px; color: #999;">暂无评论，快来发表第一条评论吧！</p>
                </span>
              </a-empty>
            </div>

            <!-- 加载更多 -->
            <div v-if="comments.length > 0 && hasMore" class="load-more">
              <a-button
                @click="loadMoreComments"
                :loading="loadingMore"
                block
                class="glass-btn"
              >
                加载更多评论
              </a-button>
            </div>
          </div>
        </section>
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
import { GetProjectDetailForPublic, GetProjectListForPublic, LikeProject, IncrementProjectViewCount } from '@/api/blog_project_public'
import { GetArticleComments, PostComment, LikeComment, GetRepliesByParentId } from '@/api/blog_comment'

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

      // 评论相关
      comments: [],
      commentCount: 0,
      commentPage: 1,
      commentPageSize: 10,
      hasMore: false,
      loadingMore: false,

      // 评论表单
      commentForm: {
        Content: '',
        Username: '',
        Email: ''
      },
      commentRules: {
        Content: [
          { required: true, message: '请输入评论内容', trigger: 'blur' },
          { min: 5, message: '评论内容至少5个字符', trigger: 'blur' }
        ]
      },
      submittingComment: false,

      // 回复功能
      replyingTo: null,
      replyForm: {
        Content: ''
      },
      submittingReply: false,

      // 粒子随机数
      Math: Math
    }
  },
  
  async mounted() {
    // this.initMouseEffect() // Commented out mouse-controlled particle effects
    this.initScrollListener()
    await this.loadProject()
    await this.loadComments()
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
    
    async incrementViewCount() {
      // 调用后端API增加访问量
      try {
        const response = await IncrementProjectViewCount(this.project.Id)
        if (response.Success) {
          // 成功更新数据库后，同步更新本地显示
          if (this.project.ViewCount !== undefined) {
            this.project.ViewCount += 1
          }
        }
      } catch (error) {
        console.error('更新访问量失败:', error)
        // 即使API失败，也可以显示本地增加的数字以提升用户体验
        if (this.project.ViewCount !== undefined) {
          this.project.ViewCount += 1
        }
      }
    },
    
    async handleLike() {
      if (this.liking) return

      // 根据需求，点赞逻辑只能点赞而不能取消点赞
      if (this.liked) {
        this.$message.warning('您已经点赞过了')
        return
      }

      this.liking = true

      // 乐观更新：先更新UI，提升用户体验
      const originalLikeCount = this.project.LikeCount || 0
      const originalLiked = this.liked

      this.liked = true
      this.project.LikeCount = originalLikeCount + 1

      try {
        // 调用真实的点赞API
        const response = await LikeProject(this.project.Id)

        if (response.Success) {
          this.$message.success('点赞成功！')

          // 保存到本地存储
          this.saveUserLike(this.project.Id, true)

          // 如果后端返回了新的点赞数，使用后端数据
          if (response.Data && response.Data.LikeCount !== undefined) {
            this.project.LikeCount = response.Data.LikeCount
          }
        } else {
          // 点赞失败，回滚UI状态
          this.liked = originalLiked
          this.project.LikeCount = originalLikeCount
          this.$message.error(response.Message || '点赞失败，请稍后重试')
        }
      } catch (error) {
        // 请求失败，回滚UI状态
        this.liked = originalLiked
        this.project.LikeCount = originalLikeCount
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
    
    // Mouse effect method commented out to remove mouse-controlled particle effects
    // initMouseEffect() {
    //   document.addEventListener('mousemove', (e) => {
    //     const particles = document.querySelectorAll('.particle')
    //     const x = e.clientX / window.innerWidth
    //     const y = e.clientY / window.innerHeight
    //
    //     particles.forEach((particle, index) => {
    //       const speed = (index + 1) * 0.2
    //       const xOffset = (x - 0.5) * speed * 8
    //       const yOffset = (y - 0.5) * speed * 8
    //
    //       particle.style.transform = `translate(${xOffset}px, ${yOffset}px)`
    //     })
    //   })
    // },

    // 评论相关方法
    async loadComments() {
      try {
        const projectId = this.$route.params.id
        if (!projectId) return

        const response = await GetArticleComments(projectId, this.commentPage, this.commentPageSize)
        if (response.Success) {
          // 根据是否为第一页决定是否替换还是追加数据
          if (this.commentPage === 1) {
            this.comments = response.Data || []
          } else {
            this.comments.push(...(response.Data || []))
          }

          this.commentCount = response.Total || 0
          this.hasMore = response.Total > this.commentPage * this.commentPageSize

          // 为每个顶级评论加载回复（只处理新加载的评论）
          const commentsToProcess = this.commentPage === 1 ? this.comments : (response.Data || [])
          for (let comment of commentsToProcess) {
            try {
              const repliesResponse = await GetRepliesByParentId(comment.Id)
              if (repliesResponse.Success) {
                comment.replies = repliesResponse.Data || []
                comment.ReplyCount = comment.replies.length
              }
            } catch (error) {
              console.error('加载回复失败:', error)
            }
          }

          // 初始化点赞状态
          this.initializeCommentLikeStates()
        }
      } catch (error) {
        console.error('加载评论失败:', error)
      }
    },

    async loadMoreComments() {
      this.loadingMore = true
      this.commentPage++
      try {
        const projectId = this.$route.params.id
        const response = await GetArticleComments(projectId, this.commentPage, this.commentPageSize)
        if (response.Success) {
          const newComments = response.Data || []

          // 为新加载的评论也加载回复
          for (let comment of newComments) {
            try {
              const repliesResponse = await GetRepliesByParentId(comment.Id)
              if (repliesResponse.Success) {
                comment.replies = repliesResponse.Data || []
                comment.ReplyCount = comment.replies.length
              }
            } catch (error) {
              console.error('加载回复失败:', error)
            }
          }

          this.comments.push(...newComments)
          this.hasMore = response.Total > this.commentPage * this.commentPageSize
        }
      } catch (error) {
        console.error('加载更多评论失败:', error)
      } finally {
        this.loadingMore = false
      }
    },

    async submitComment() {
      this.$refs.commentForm.validate(async (valid) => {
        if (!valid) return

        this.submittingComment = true
        try {
          const commentData = {
            Content: this.commentForm.Content,
            TargetType: 'project',
            TargetId: parseInt(this.$route.params.id),
            UserId: 1, // 临时用户ID，实际应该从登录状态获取
            Status: 'approved' // 直接设置为通过状态
          }

          // 如果有昵称和邮箱信息，可以创建临时用户或传递额外信息
          if (this.commentForm.Username) {
            commentData.Username = this.commentForm.Username
          }
          if (this.commentForm.Email) {
            commentData.Email = this.commentForm.Email
          }

          const response = await PostComment(commentData)
          if (response.Success) {
            this.$message.success('评论发表成功!')

            // 构造新评论对象并直接添加到列表开头
            const newComment = {
              Id: (response.Data && response.Data.Id) || Date.now(), // 使用返回的ID或临时ID
              Content: this.commentForm.Content,
              CreatedAt: new Date().toISOString(),
              LikeCount: 0,
              liked: false, // 新评论默认未点赞
              liking: false, // 默认未在点赞中
              replies: [],
              ReplyCount: 0,
              User: {
                Username: this.commentForm.Username || 'current_user',
                Nickname: this.commentForm.Username || 'current_user'
              }
            }

            // 添加到评论列表开头
            this.comments.unshift(newComment)

            // 更新评论总数
            this.commentCount += 1

            // 清空表单
            this.commentForm.Content = ''
            this.commentForm.Username = ''
            this.commentForm.Email = ''
            this.$refs.commentForm.clearValidate()
          } else {
            this.$message.error(response.Msg || '评论提交失败')
          }
        } catch (error) {
          console.error('评论提交失败:', error)
          this.$message.error('评论提交失败，请稍后重试')
        } finally {
          this.submittingComment = false
        }
      })
    },

    async likeComment(comment) {
      // 防止重复点击：正在点赞中直接返回
      if (comment.liking) {
        return
      }

      // 检查是否已经点赞过
      if (comment.liked) {
        this.$message.warning('您已经点赞过了')
        return
      }

      // 立即设置点赞状态，防止重复点击
      comment.liking = true

      // 乐观更新：先更新UI，再发送请求
      const originalLikeCount = comment.LikeCount || 0
      const originalLiked = comment.liked

      comment.LikeCount = originalLikeCount + 1
      comment.liked = true

      try {
        const response = await LikeComment(comment.Id)
        if (response.Success) {
          // 点赞成功，保存状态到本地存储
          this.saveCommentLikeState(comment.Id, true)
          this.$message.success('点赞成功!')
        } else {
          // 点赞失败，回滚UI状态
          comment.LikeCount = originalLikeCount
          comment.liked = originalLiked
          this.$message.error('点赞失败，请稍后重试')
        }
      } catch (error) {
        // 请求失败，回滚UI状态
        comment.LikeCount = originalLikeCount
        comment.liked = originalLiked
        console.error('点赞失败:', error)
        this.$message.error('点赞失败，请稍后重试')
      } finally {
        // 无论成功失败都要重置loading状态
        comment.liking = false
      }
    },

    // 保存评论点赞状态到本地存储
    saveCommentLikeState(commentId, liked) {
      try {
        const key = 'blog_comment_likes'
        const likes = JSON.parse(localStorage.getItem(key) || '{}')

        if (liked) {
          likes[commentId] = true
        } else {
          delete likes[commentId]
        }

        localStorage.setItem(key, JSON.stringify(likes))
      } catch (error) {
        console.error('保存点赞状态失败:', error)
      }
    },

    // 检查评论是否已点赞
    checkCommentLikeState(commentId) {
      try {
        const key = 'blog_comment_likes'
        const likes = JSON.parse(localStorage.getItem(key) || '{}')
        return !!likes[commentId]
      } catch (error) {
        console.error('检查点赞状态失败:', error)
        return false
      }
    },

    // 初始化评论的点赞状态
    initializeCommentLikeStates() {
      this.comments.forEach(comment => {
        comment.liked = this.checkCommentLikeState(comment.Id)

        // 也初始化回复的点赞状态
        if (comment.replies && comment.replies.length > 0) {
          comment.replies.forEach(reply => {
            reply.liked = this.checkCommentLikeState(reply.Id)
          })
        }
      })
    },

    showReplyForm(commentId) {
      this.replyingTo = commentId
      this.replyForm.Content = ''
    },

    cancelReply() {
      this.replyingTo = null
      this.replyForm.Content = ''
    },

    async submitReply(parentId) {
      if (!this.replyForm.Content.trim()) {
        this.$message.error('请输入回复内容')
        return
      }

      this.submittingReply = true
      try {
        const replyData = {
          Content: this.replyForm.Content,
          TargetType: 'project',
          TargetId: parseInt(this.$route.params.id),
          UserId: 1, // 临时用户ID
          ParentId: parentId,
          Status: 'approved' // 直接设置为通过状态
        }

        const response = await PostComment(replyData)
        if (response.Success) {
          this.$message.success('回复发表成功!')

          // 直接在前端添加新回复到对应的评论，避免重新加载所有数据
          const parentComment = this.comments.find(c => c.Id === parentId)
          if (parentComment) {
            if (!parentComment.replies) {
              parentComment.replies = []
            }

            // 构造新回复对象
            const newReply = {
              Id: (response.Data && response.Data.Id) || Date.now(), // 使用返回的ID或临时ID
              Content: this.replyForm.Content,
              CreatedAt: new Date().toISOString(),
              LikeCount: 0,
              liked: false, // 新回复默认未点赞
              liking: false, // 默认未在点赞中
              User: {
                Username: 'current_user', // 临时用户名
                Nickname: 'current_user'
              }
            }

            // 添加到回复列表开头
            parentComment.replies.unshift(newReply)
            parentComment.ReplyCount = parentComment.replies.length

            // 更新总评论数
            this.commentCount += 1
          }

          this.cancelReply()
        } else {
          this.$message.error(response.Msg || '回复提交失败')
        }
      } catch (error) {
        console.error('回复提交失败:', error)
        this.$message.error('回复提交失败，请稍后重试')
      } finally {
        this.submittingReply = false
      }
    },

    getAvatarText(name) {
      if (!name) return '匿'
      return name.charAt(0).toUpperCase()
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

// 评论区样式
.comments-section {
  margin-top: 40px;

  .comments-container {
    padding: 32px;

    .comments-header {
      margin-bottom: 24px;

      h3 {
        font-size: 20px;
        font-weight: 600;
        color: var(--text-primary);
        display: flex;
        align-items: center;
        gap: 8px;
        margin: 0;

        .anticon {
          color: var(--primary-color);
        }
      }
    }

    .comment-form-container {
      margin-bottom: 32px;

      .comment-form {
        padding: 20px;
        border-radius: 12px;

        .form-row {
          display: flex;
          justify-content: space-between;
          align-items: flex-end;
          margin-top: 12px;

          .user-inputs {
            display: flex;
            align-items: center;
          }

          .submit-btn {
            border-radius: 20px;
            padding: 0 20px;
            height: 36px;
            display: flex;
            align-items: center;
            gap: 6px;
          }
        }
      }
    }

    .comments-list {
      .comment-item {
        margin-bottom: 20px;
        padding: 20px;
        border-radius: 12px;

        .comment-header {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin-bottom: 12px;

          .comment-author {
            display: flex;
            align-items: center;
            gap: 12px;

            .author-avatar {
              background: var(--primary-gradient);
              color: white;
              font-weight: 600;
            }

            .author-info {
              .author-name {
                font-weight: 600;
                color: var(--text-primary);
                margin-right: 8px;
              }

              .comment-time {
                font-size: 13px;
                color: #999;
              }
            }
          }

          .comment-actions {
            display: flex;
            gap: 4px;

            .ant-btn-link {
              padding: 4px 8px;
              height: auto;
              font-size: 13px;
              color: #999;
              border-radius: 16px;
              transition: all 0.3s ease;

              &:hover {
                color: var(--primary-color);
                background: rgba(102, 126, 234, 0.1);
              }

              &.liked-button {
                color: #ff4757;
                background: rgba(255, 71, 87, 0.1);

                &:hover {
                  background: rgba(255, 71, 87, 0.2);
                }
              }
            }
          }
        }

        .comment-content {
          margin-left: 44px;
          line-height: 1.6;
          color: var(--text-primary);
          margin-bottom: 12px;
        }

        .reply-form {
          margin: 16px 0 0 44px;
          padding: 16px;
          background: rgba(248, 249, 250, 0.8);
          border-radius: 8px;

          .reply-actions {
            margin-top: 12px;
            text-align: right;
          }
        }

        .replies-list {
          margin: 16px 0 0 44px;
          padding-left: 20px;
          border-left: 2px solid rgba(102, 126, 234, 0.1);

          .reply-item {
            padding: 12px 0;
            border-bottom: 1px solid rgba(0, 0, 0, 0.05);

            &:last-child {
              border-bottom: none;
            }

            .reply-header {
              display: flex;
              align-items: center;
              gap: 8px;
              margin-bottom: 8px;

              .reply-info {
                flex: 1;

                .reply-author {
                  font-size: 13px;
                  font-weight: 600;
                  color: var(--text-primary);
                  margin-right: 8px;
                }

                .reply-time {
                  font-size: 12px;
                  color: #999;
                }
              }

              .ant-btn-link {
                padding: 2px 6px;
                height: auto;
                font-size: 12px;
                color: #999;
                border-radius: 12px;
                transition: all 0.3s ease;

                &:hover {
                  color: var(--primary-color);
                  background: rgba(102, 126, 234, 0.1);
                }

                &.liked-button {
                  color: #ff4757;
                  background: rgba(255, 71, 87, 0.1);

                  &:hover {
                    background: rgba(255, 71, 87, 0.2);
                  }
                }
              }
            }

            .reply-content {
              font-size: 14px;
              line-height: 1.5;
              color: var(--text-primary);
            }
          }
        }
      }
    }

    .empty-comments {
      text-align: center;
      padding: 40px 20px;
    }

    .load-more {
      margin-top: 24px;

      .glass-btn {
        border-radius: 20px;
        height: 40px;
        font-weight: 500;
      }
    }
  }
}

.glass-card-inner {
  background: rgba(255, 255, 255, 0.4);
  backdrop-filter: blur(10px);
  -webkit-backdrop-filter: blur(10px);
  border: 1px solid rgba(102, 126, 234, 0.1);
  box-shadow: 0 4px 16px rgba(102, 126, 234, 0.05);
  border-radius: 12px;
  transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);

  &:hover {
    background: rgba(255, 255, 255, 0.5);
    border-color: rgba(102, 126, 234, 0.2);
    box-shadow: 0 6px 24px rgba(102, 126, 234, 0.1);
  }
}

@media (max-width: 768px) {
  .comments-section {
    margin-top: 24px;

    .comments-container {
      padding: 20px 16px;

      .comment-form-container .comment-form .form-row {
        flex-direction: column;
        align-items: stretch;
        gap: 12px;

        .user-inputs {
          flex-direction: column;
          gap: 8px;

          input {
            width: 100% !important;
            margin-right: 0 !important;
          }
        }

        .submit-btn {
          align-self: flex-end;
          width: auto;
        }
      }

      .comments-list .comment-item {
        padding: 16px;

        .comment-content,
        .reply-form,
        .replies-list {
          margin-left: 0;
        }

        .replies-list {
          padding-left: 16px;
        }
      }
    }
  }
}
</style>