<template>
  <div class="article-detail-container">
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
              <p class="tagline">技术文章</p>
            </div>
          </div>
        </div>
        
        <div class="nav-links">
          <a-button type="link" @click="goToArticles">
            <a-icon type="left" />
            返回列表
          </a-button>
        </div>
      </div>
    </header>

    <!-- 主要内容区域 -->
    <main class="main-content" v-if="!loading">
      <!-- 文章头部 -->
      <section class="article-header">
        <div class="header-content-wrapper">
          <!-- 面包屑导航 -->
          <div class="breadcrumb glass-card">
            <a @click="goHome" class="breadcrumb-link">首页</a>
            <a-icon type="right" />
            <a @click="goToArticles" class="breadcrumb-link">技术文章</a>
            <a-icon type="right" />
            <span class="current">{{ article.Title }}</span>
          </div>

          <!-- 文章元信息 -->
          <div class="article-meta">
            <div class="meta-tags">
              <div v-if="article.Category" class="category-tag">
                <a-icon type="folder" />
                {{ article.Category.Name }}
              </div>
              <div v-if="article.IsFeatured" class="featured-tag">
                <a-icon type="star" />
                精选文章
              </div>
            </div>
            
            <h1 class="article-title">{{ article.Title }}</h1>
            
            <div class="article-info">
              <div class="author-info" v-if="article.Author">
                <div class="author-details">
                  <span class="author-name">{{ article.Author.Nickname || article.Author.UserName }}</span>
                  <span class="publish-time">{{ formatTime(article.PublishedAt || article.CreatedAt) }}</span>
                </div>
              </div>
              
              <div class="article-stats">
                <div class="stat">
                  <a-icon type="eye" />
                  <span>{{ formatNumber(article.ViewCount || 0) }} 阅读</span>
                </div>
                <div class="stat">
                  <a-icon type="heart" />
                  <span>{{ formatNumber(article.LikeCount || 0) }} 点赞</span>
                </div>
                <div class="stat">
                  <a-icon type="message" />
                  <span>{{ formatNumber(article.CommentCount || 0) }} 评论</span>
                </div>
              </div>
            </div>
          </div>

          <!-- 封面图片 -->
          <div v-if="article.CoverImage" class="article-cover">
            <img :src="article.CoverImage" :alt="article.Title" />
          </div>
        </div>
      </section>

      <!-- 文章内容 -->
      <section class="article-content-section">
        <div class="content-wrapper">
          <div class="main-content-area">
            <!-- 文章摘要 -->
            <div v-if="article.Summary" class="article-summary glass-card">
              <h3>摘要</h3>
              <p>{{ article.Summary }}</p>
            </div>

            <!-- 文章正文 -->
            <div class="article-body glass-card">
              <div class="content-html" v-html="article.Content"></div>
            </div>

            <!-- 文章底部操作 -->
            <div class="article-actions glass-card">
              <div class="action-buttons">
                <like-button
                  target-type="article"
                  :target-id="article.Id"
                  :initial-liked="false"
                  :initial-count="article.LikeCount || 0"
                  size="large"
                  @liked="onArticleLiked"
                />
                <a-button @click="shareArticle">
                  <a-icon type="share-alt" />
                  分享文章
                </a-button>
              </div>
              
              <div class="article-tags" v-if="article.Tags && article.Tags.length">
                <span class="tags-label">标签：</span>
                <a-tag v-for="tag in article.Tags" :key="tag" color="blue">{{ tag }}</a-tag>
              </div>
            </div>
          </div>

          <!-- 侧边栏 -->
          <aside class="sidebar">
            <!-- 目录 -->
            <div class="toc-widget glass-card" v-if="tocItems.length > 0">
              <h4>文章目录</h4>
              <div class="toc-list">
                <div 
                  v-for="item in tocItems" 
                  :key="item.id"
                  :class="['toc-item', `level-${item.level}`, { active: activeAnchor === item.id }]"
                  @click="scrollToAnchor(item.id)"
                >
                  {{ item.text }}
                </div>
              </div>
            </div>

            <!-- 相关文章 -->
            <div class="related-articles glass-card" v-if="relatedArticles.length > 0">
              <h4>相关文章</h4>
              <div class="article-list">
                <div 
                  v-for="relatedArticle in relatedArticles" 
                  :key="relatedArticle.Id"
                  class="article-item"
                  @click="goToArticle(relatedArticle.Id)"
                >
                  <div class="article-title">{{ relatedArticle.Title }}</div>
                  <div class="article-meta">
                    <span>{{ formatTime(relatedArticle.PublishedAt || relatedArticle.CreatedAt) }}</span>
                    <span>{{ formatNumber(relatedArticle.ViewCount || 0) }} 阅读</span>
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
                    <like-button
                      target-type="comment"
                      :target-id="comment.Id"
                      :initial-liked="false"
                      :initial-count="comment.LikeCount || 0"
                      size="small"
                      @liked="onCommentLiked"
                    />
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
                      <like-button
                        target-type="comment"
                        :target-id="reply.Id"
                        :initial-liked="false"
                        :initial-count="reply.LikeCount || 0"
                        size="small"
                        @liked="onCommentLiked"
                      />
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
    <div v-else class="loading-state">
      <div class="loading-content">
        <a-spin size="large" />
        <p>正在加载文章...</p>
      </div>
    </div>

    <!-- 错误状态 -->
    <div v-if="error" class="error-state">
      <div class="error-content glass-card">
        <a-icon type="exclamation-circle" class="error-icon" />
        <h3>加载失败</h3>
        <p>{{ error }}</p>
        <a-button type="primary" @click="loadArticle">重新加载</a-button>
        <a-button @click="goToArticles" style="margin-left: 12px;">返回列表</a-button>
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
          <a href="javascript:void(0)" class="footer-link" @click="goToArticles">文章列表</a>
        </div>
      </div>
    </footer>
  </div>
</template>

<script>
import { GetTheData, GetDataList } from '@/api/blog_article'
import { LikeArticle, IncrementArticleViewCount } from '@/api/blog_article_public'
import { GetArticleComments, PostComment, LikeComment, GetRepliesByParentId } from '@/api/blog_comment'
import LikeButton from '@/components/LikeButton.vue'

export default {
  name: 'BlogArticleDetail',
  components: {
    LikeButton
  },
  data() {
    return {
      loading: true,
      error: null,
      article: {},
      relatedArticles: [],
      tocItems: [],
      activeAnchor: '',
      showBackTop: false,
      
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
    await this.loadArticle()
    await this.loadComments()
  },
  
  beforeDestroy() {
    window.removeEventListener('scroll', this.handleScroll)
  },
  
  methods: {
    // 文章点赞事件处理
    onArticleLiked(event) {
      // 更新文章的点赞数
      this.article.LikeCount = event.likeCount
    },

    // 评论点赞事件处理
    onCommentLiked(event) {
      // 可以在这里添加额外的逻辑，比如更新评论列表
      // console.log('评论点赞状态变化:', event)
    },
    async loadArticle() {
      const articleId = this.$route.params.id
      
      if (!articleId) {
        this.error = '文章ID不存在'
        this.loading = false
        return
      }
      
      this.loading = true
      this.error = null
      
      try {
        const response = await GetTheData({ id: articleId })
        
        if (response.Success && response.Data) {
          this.article = response.Data

          // 增加浏览量（这里可以调用专门的API）
          this.incrementViewCount()
          
          // 生成目录
          this.$nextTick(() => {
            this.generateTOC()
          })
          
          // 加载相关文章
          this.loadRelatedArticles()
          
        } else {
          throw new Error(response.Message || '文章不存在或已被删除')
        }
      } catch (error) {
        console.error('加载文章失败:', error)
        this.error = error.message || '加载文章失败'
      } finally {
        this.loading = false
      }
    },
    
    async loadRelatedArticles() {
      try {
        let relatedArticles = []
        
        // 优先加载同分类的文章
        if (this.article.CategoryId) {
          const response = await GetDataList({
            PageIndex: 1,
            PageRows: 10,
            categoryId: this.article.CategoryId,
            status: 'published'
          })
          
          if (response.Success && response.Data) {
            // 排除当前文章，并限制到4篇
            relatedArticles = response.Data
              .filter(article => article.Id !== this.article.Id)
              .slice(0, 4)
          }
        }
        
        // 如果同分类文章不足4篇，用最新文章补充
        if (relatedArticles.length < 4) {
          const response = await GetDataList({
            PageIndex: 1,
            PageRows: 8,
            status: 'published'
          })
          
          if (response.Success && response.Data) {
            const latestArticles = response.Data
              .filter(article => 
                article.Id !== this.article.Id && 
                !relatedArticles.some(related => related.Id === article.Id)
              )
              .slice(0, 4 - relatedArticles.length)
            
            relatedArticles = [...relatedArticles, ...latestArticles]
          }
        }
        
        this.relatedArticles = relatedArticles
        
      } catch (error) {
        console.error('加载相关文章失败:', error)
        this.relatedArticles = []
      }
    },
    
    generateTOC() {
      const contentEl = document.querySelector('.content-html')
      if (!contentEl) return
      
      const headings = contentEl.querySelectorAll('h1, h2, h3, h4, h5, h6')
      this.tocItems = []
      
      headings.forEach((heading, index) => {
        const id = `heading-${index}`
        heading.id = id
        
        const level = parseInt(heading.tagName.charAt(1))
        const text = heading.textContent.trim()
        
        if (text) {
          this.tocItems.push({
            id,
            level,
            text,
            element: heading
          })
        }
      })
      
      // 监听滚动，高亮当前标题
      this.updateActiveAnchor()
    },
    
    updateActiveAnchor() {
      if (this.tocItems.length === 0) return
      
      const scrollTop = window.pageYOffset || document.documentElement.scrollTop
      let activeIndex = 0
      
      for (let i = 0; i < this.tocItems.length; i++) {
        const element = this.tocItems[i].element
        if (element.offsetTop <= scrollTop + 100) {
          activeIndex = i
        }
      }
      
      this.activeAnchor = (this.tocItems[activeIndex] && this.tocItems[activeIndex].id) || ''
    },
    
    scrollToAnchor(anchorId) {
      const element = document.getElementById(anchorId)
      if (element) {
        const top = element.offsetTop - 100
        window.scrollTo({ top, behavior: 'smooth' })
      }
    },
    
    async incrementViewCount() {
      try {
        const response = await IncrementArticleViewCount(this.article.Id)
        if (response.Success) {
          // 更新本地数据
          if (this.article.ViewCount !== undefined) {
            this.article.ViewCount += 1
          }
        }
      } catch (error) {
        console.error('更新访问量失败:', error)
        // 即使API调用失败，也在本地更新显示，不影响用户体验
        if (this.article.ViewCount !== undefined) {
          this.article.ViewCount += 1
        }
      }
    },
    
    
    
    shareArticle() {
      const url = window.location.href
      const title = this.article.Title
      
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
        this.updateActiveAnchor()
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
    
    goToArticles() {
      this.$router.push('/blog-website/articles')
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
        return date.toLocaleDateString('zh-CN', {
          year: 'numeric',
          month: 'long',
          day: 'numeric'
        })
      } catch (error) {
        return timeStr.split(' ')[0]
      }
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
        const articleId = this.$route.params.id
        if (!articleId) return

        const response = await GetArticleComments(articleId, this.commentPage, this.commentPageSize)
        if (response.Success) {
          // 直接使用返回的评论数据
          this.comments = response.Data || []
          this.commentCount = this.comments.length
          this.hasMore = false // 暂时关闭分页

          // 为每个顶级评论加载回复
          for (let comment of this.comments) {
            // 只为顶级评论加载回复
            if (!comment.ParentId) {
              try {
                const repliesResponse = await GetRepliesByParentId(comment.Id)
                if (repliesResponse.Success) {
                  // 使用Vue.set确保响应式更新
                  this.$set(comment, 'replies', repliesResponse.Data || [])
                  this.$set(comment, 'ReplyCount', comment.replies.length)
                } else {
                  console.error('获取回复失败:', repliesResponse.Msg)
                }
              } catch (error) {
                console.error('加载回复失败:', error)
                this.$set(comment, 'replies', [])
                this.$set(comment, 'ReplyCount', 0)
              }
            }
          }

        } else {
          console.error('获取评论失败:', response.Msg)
        }
      } catch (error) {
        console.error('加载评论失败:', error)
      }
    },

    async loadMoreComments() {
      this.loadingMore = true
      this.commentPage++
      try {
        const articleId = this.$route.params.id
        const response = await GetArticleComments(articleId, this.commentPage, this.commentPageSize)
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
            TargetType: 'article',
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
          TargetType: 'article',
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
// 全局样式变量（复用）
:root {
  --primary-gradient: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  --glass-bg: rgba(255, 255, 255, 0.6);
  --glass-border: rgba(102, 126, 234, 0.15);
  --glass-shadow: 0 8px 32px rgba(102, 126, 234, 0.1);
  --text-primary: rgba(51, 51, 51, 0.9);
  --text-secondary: rgba(102, 102, 102, 0.8);
  --text-muted: rgba(153, 153, 153, 0.7);
}

// 玻璃形态样式（复用）
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
.article-detail-container {
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

// 文章头部
.article-header {
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
    
    .article-meta {
      margin-bottom: 40px;
      
      .meta-tags {
        display: flex;
        gap: 12px;
        margin-bottom: 24px;
        
        .category-tag,
        .featured-tag {
          display: flex;
          align-items: center;
          gap: 6px;
          padding: 6px 12px;
          background: rgba(102, 126, 234, 0.1);
          color: #667eea;
          border-radius: 16px;
          font-size: 14px;
          font-weight: 500;
        }
        
        .featured-tag {
          background: rgba(255, 165, 0, 0.1);
          color: #ff8c00;
        }
      }
      
      .article-title {
        font-size: 36px;
        font-weight: 800;
        color: var(--text-primary);
        line-height: 1.4;
        margin-bottom: 24px;
      }
      
      .article-info {
        display: flex;
        justify-content: space-between;
        align-items: center;
        
        .author-info {
          .author-details {
            display: flex;
            align-items: center;
            gap: 16px;
            
            .author-name {
              font-size: 16px;
              font-weight: 600;
              color: var(--text-primary);
            }
            
            .publish-time {
              font-size: 14px;
              color: var(--text-secondary);
            }
          }
        }
        
        .article-stats {
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
      }
    }
    
    .article-cover {
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

// 文章内容区域
.article-content-section {
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
      .article-summary {
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
      
      .article-body {
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
            
            h1 { font-size: 28px; }
            h2 { font-size: 24px; }
            h3 { font-size: 20px; }
            h4 { font-size: 18px; }
            h5 { font-size: 16px; }
            h6 { font-size: 14px; }
            
            p {
              margin: 16px 0;
              text-align: justify;
            }
            
            ul, ol {
              margin: 16px 0;
              padding-left: 24px;
              
              li {
                margin: 8px 0;
              }
            }
            
            blockquote {
              margin: 24px 0;
              padding: 16px 20px;
              background: rgba(102, 126, 234, 0.05);
              border-left: 4px solid #667eea;
              border-radius: 0 8px 8px 0;
              
              p {
                margin: 0;
                font-style: italic;
                color: var(--text-secondary);
              }
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
            
            img {
              max-width: 100%;
              height: auto;
              border-radius: 8px;
              margin: 20px 0;
              box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
            }
            
            table {
              width: 100%;
              border-collapse: collapse;
              margin: 24px 0;
              
              th, td {
                border: 1px solid #e8e8e8;
                padding: 12px 16px;
                text-align: left;
              }
              
              th {
                background: rgba(102, 126, 234, 0.05);
                font-weight: 600;
              }
            }
          }
        }
      }
      
      .article-actions {
        padding: 24px;
        
        .action-buttons {
          display: flex;
          gap: 16px;
          margin-bottom: 20px;
        }
        
        .article-tags {
          display: flex;
          align-items: center;
          flex-wrap: wrap;
          gap: 8px;
          
          .tags-label {
            color: var(--text-secondary);
            font-size: 14px;
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
      
      .toc-widget,
      .related-articles {
        padding: 20px;
        
        h4 {
          font-size: 16px;
          font-weight: 600;
          color: var(--text-primary);
          margin-bottom: 16px;
        }
        
        .toc-list {
          .toc-item {
            padding: 8px 12px;
            cursor: pointer;
            border-radius: 6px;
            transition: all 0.3s ease;
            margin-bottom: 4px;
            font-size: 14px;
            color: var(--text-secondary);
            
            &.level-1 { padding-left: 12px; font-weight: 600; }
            &.level-2 { padding-left: 24px; }
            &.level-3 { padding-left: 36px; }
            &.level-4 { padding-left: 48px; }
            
            &:hover,
            &.active {
              background: rgba(102, 126, 234, 0.1);
              color: #667eea;
            }
          }
        }
        
        .article-list {
          .article-item {
            padding: 12px;
            border-radius: 8px;
            cursor: pointer;
            transition: background 0.3s ease;
            margin-bottom: 8px;
            
            &:hover {
              background: rgba(102, 126, 234, 0.05);
            }
            
            .article-title {
              font-size: 14px;
              color: var(--text-primary);
              margin-bottom: 6px;
              display: -webkit-box;
              -webkit-line-clamp: 2;
              -webkit-box-orient: vertical;
              overflow: hidden;
            }
            
            .article-meta {
              display: flex;
              justify-content: space-between;
              font-size: 12px;
              color: var(--text-muted);
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
  .article-content-section .content-wrapper {
    grid-template-columns: 1fr;
    gap: 40px;
    
    .sidebar {
      position: static;
      order: -1;
      
      .toc-widget,
      .related-articles,
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
  
  .article-header .header-content-wrapper {
    padding: 0 20px;
    
    .article-title {
      font-size: 28px;
    }
    
    .article-info {
      flex-direction: column;
      align-items: flex-start;
      gap: 16px;
    }
    
    .article-cover img {
      height: 250px;
    }
  }
  
  .article-content-section .content-wrapper {
    padding: 0 20px;
    
    .main-content-area {
      .article-body {
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
              
              &:hover {
                color: var(--primary-color);
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