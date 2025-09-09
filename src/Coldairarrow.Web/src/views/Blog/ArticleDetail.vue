<template>
  <div class="blog-article-detail">
    <div v-if="loading" class="loading-container">
      <a-spin size="large" />
    </div>

    <div v-else-if="article" class="article-container">
      <!-- 文章头部 -->
      <div class="article-header">
        <div class="breadcrumb">
          <router-link to="/blog/home">首页</router-link>
          <a-icon type="right" />
          <router-link to="/blog/articles">技术文章</router-link>
          <a-icon type="right" />
          <span>{{ article.title }}</span>
        </div>

        <h1 class="article-title">{{ article.title }}</h1>

        <div class="article-meta">
          <div class="meta-left">
            <span class="meta-item">
              <a-icon type="calendar" />
              {{ formatDate(article.createdAt) }}
            </span>
            <span class="meta-item">
              <a-icon type="user" />
              {{ article.author || '博主' }}
            </span>
            <span class="meta-item">
              <a-icon type="folder" />
              {{ article.category }}
            </span>
          </div>
          <div class="meta-right">
            <span class="meta-item">
              <a-icon type="eye" />
              {{ article.viewCount }}
            </span>
            <span class="meta-item">
              <a-icon type="like" theme="filled" :class="{ liked: isLiked }" @click="toggleLike" />
              {{ article.likeCount }}
            </span>
          </div>
        </div>

        <div class="article-tags">
          <a-tag v-for="tag in article.tags" :key="tag" color="blue">{{ tag }}</a-tag>
        </div>
      </div>

      <!-- 文章封面 -->
      <div v-if="article.coverImage" class="article-cover">
        <img :src="article.coverImage" :alt="article.title" />
      </div>

      <!-- 文章内容 -->
      <div class="article-content">
        <div class="content-body" v-html="article.content"></div>
      </div>

      <!-- 文章底部 -->
      <div class="article-footer">
        <div class="article-actions">
          <a-button :type="isLiked ? 'primary' : 'default'" @click="toggleLike" class="action-btn like-btn">
            <a-icon type="like" :theme="isLiked ? 'filled' : 'outlined'" />
            {{ isLiked ? '已点赞' : '点赞' }} ({{ article.likeCount }})
          </a-button>
          <a-button @click="showShareModal" class="action-btn share-btn">
            <a-icon type="share-alt" />
            分享
          </a-button>
          <a-button @click="scrollToComments" class="action-btn comment-btn">
            <a-icon type="message" />
            评论 ({{ article.commentCount }})
          </a-button>
        </div>
      </div>

      <!-- 相关文章 -->
      <div class="related-articles" v-if="relatedArticles.length > 0">
        <h3 class="related-title">相关文章</h3>
        <div class="related-list">
          <div v-for="related in relatedArticles" :key="related.id" class="related-item"
            @click="viewRelatedArticle(related.id)">
            <img :src="related.coverImage || defaultCover" :alt="related.title" class="related-cover" />
            <div class="related-info">
              <h4 class="related-article-title">{{ related.title }}</h4>
              <p class="related-summary">{{ related.summary }}</p>
              <span class="related-date">{{ formatDate(related.createdAt) }}</span>
            </div>
          </div>
        </div>
      </div>

      <!-- 评论区域 -->
      <div id="comments" class="comments-section">
        <h3 class="comments-title">评论 ({{ article.commentCount }})</h3>
        <!-- 这里可以集成评论组件 -->
        <div class="comments-placeholder">
          <a-empty description="暂无评论，快来发表第一个评论吧！" />
        </div>
      </div>
    </div>

    <div v-else class="not-found">
      <a-result status="404" title="文章未找到" sub-title="抱歉，您访问的文章不存在或已被删除。">
        <template #extra>
          <a-button type="primary" @click="$router.push('/blog/articles')">
            返回文章列表
          </a-button>
        </template>
      </a-result>
    </div>

    <!-- 分享弹窗 -->
    <a-modal v-model="shareModalVisible" title="分享文章" :footer="null" width="400px">
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
  </div>
</template>

<script>
export default {
  name: 'BlogArticleDetail',
  data() {
    return {
      article: null,
      relatedArticles: [],
      loading: true,
      isLiked: false,
      shareModalVisible: false,
      defaultCover: '/assets/default-article-cover.jpg'
    }
  },
  methods: {
    async loadArticle() {
      const articleId = this.$route.params.id
      this.loading = true

      try {
        // 模拟数据
        this.article = {
          id: parseInt(articleId),
          title: 'Vue.js 3.0 新特性详解',
          content: `
            <h2>Vue.js 3.0 简介</h2>
            <p>Vue.js 3.0 是 Vue.js 框架的最新主要版本，带来了许多激动人心的新特性和改进。</p>
            
            <h3>主要新特性</h3>
            <h4>1. Composition API</h4>
            <p>Composition API 是 Vue 3 最重要的新特性之一，它提供了一种更灵活的方式来组织组件逻辑。</p>
            <pre><code>
import { ref, reactive, computed, onMounted } from 'vue'

export default {
  setup() {
    const count = ref(0)
    const state = reactive({
      name: 'Vue 3'
    })
    
    const doubleCount = computed(() => count.value * 2)
    
    onMounted(() => {
      console.log('组件已挂载')
    })
    
    return {
      count,
      state,
      doubleCount
    }
  }
}
            </code></pre>
            
            <h4>2. Teleport</h4>
            <p>Teleport 组件允许我们将组件的一部分模板渲染到 DOM 中的其他位置。</p>
            
            <h4>3. 性能提升</h4>
            <p>Vue 3 在性能方面有了显著提升，包括：</p>
            <ul>
              <li>更小的包体积</li>
              <li>更快的渲染速度</li>
              <li>更好的 Tree-shaking 支持</li>
            </ul>
            
            <h3>总结</h3>
            <p>Vue.js 3.0 是一个重大的版本更新，为开发者提供了更强大、更灵活的工具来构建现代 Web 应用。</p>
          `,
          summary: '详细介绍Vue.js 3.0的新特性，包括Composition API、Teleport等',
          category: '前端开发',
          author: '博主',
          coverImage: '/assets/vue-cover.jpg',
          createdAt: '2024-01-15',
          tags: ['Vue.js', 'JavaScript', '前端'],
          viewCount: 1250,
          likeCount: 89,
          commentCount: 23
        }

        // 加载相关文章
        this.relatedArticles = [
          {
            id: 2,
            title: 'React vs Vue: 选择指南',
            summary: '详细对比 React 和 Vue 的特点，帮助你选择合适的框架',
            coverImage: '/assets/react-vue.jpg',
            createdAt: '2024-01-10'
          },
          {
            id: 3,
            title: 'TypeScript 入门指南',
            summary: 'TypeScript 基础知识和最佳实践',
            coverImage: '/assets/typescript.jpg',
            createdAt: '2024-01-08'
          }
        ]

      } catch (error) {
        console.error('加载文章失败:', error)
        this.$message.error('加载文章失败')
        this.article = null
      } finally {
        this.loading = false
      }
    },

    async toggleLike() {
      try {
        this.isLiked = !this.isLiked
        if (this.isLiked) {
          this.article.likeCount++
          this.$message.success('点赞成功！')
        } else {
          this.article.likeCount--
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
      const title = encodeURIComponent(this.article.title)
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

    scrollToComments() {
      document.getElementById('comments').scrollIntoView({ behavior: 'smooth' })
    },

    viewRelatedArticle(id) {
      this.$router.push(`/blog/articles/${id}`)
    },

    formatDate(dateStr) {
      const date = new Date(dateStr)
      return date.toLocaleDateString('zh-CN')
    }
  },

  mounted() {
    this.loadArticle()
  },

  watch: {
    '$route.params.id'() {
      this.loadArticle()
    }
  }
}
</script>

<style lang="less" scoped>
.blog-article-detail {
  max-width: 800px;
  margin: 0 auto;

  .loading-container {
    text-align: center;
    padding: 80px 0;
  }

  .article-container {
    background: white;
    border-radius: 15px;
    overflow: hidden;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);

    .article-header {
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

      .article-title {
        font-size: 32px;
        font-weight: 700;
        color: #2c3e50;
        margin-bottom: 20px;
        line-height: 1.4;
      }

      .article-meta {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 20px;

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

      .article-tags {
        .ant-tag {
          margin-right: 8px;
          margin-bottom: 5px;
        }
      }
    }

    .article-cover {
      img {
        width: 100%;
        height: 400px;
        object-fit: cover;
      }
    }

    .article-content {
      padding: 40px;

      .content-body {
        line-height: 1.8;
        color: #2c3e50;
        font-size: 16px;

        :deep(h2) {
          font-size: 24px;
          font-weight: 600;
          margin: 30px 0 20px 0;
          color: #2c3e50;
          border-bottom: 2px solid #3498db;
          padding-bottom: 10px;
        }

        :deep(h3) {
          font-size: 20px;
          font-weight: 600;
          margin: 25px 0 15px 0;
          color: #2c3e50;
        }

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

        :deep(ul),
        :deep(ol) {
          margin: 16px 0;
          padding-left: 20px;
        }

        :deep(li) {
          margin-bottom: 8px;
        }

        :deep(pre) {
          background: #f8f9fa;
          border-radius: 8px;
          padding: 20px;
          margin: 20px 0;
          overflow-x: auto;
          border-left: 4px solid #3498db;
        }

        :deep(code) {
          background: #f1f2f6;
          padding: 2px 6px;
          border-radius: 4px;
          font-family: 'Monaco', 'Menlo', monospace;
          font-size: 14px;
        }

        :deep(pre code) {
          background: none;
          padding: 0;
        }
      }
    }

    .article-footer {
      padding: 30px 40px;
      border-top: 1px solid #f1f2f6;

      .article-actions {
        display: flex;
        justify-content: center;
        gap: 15px;

        .action-btn {
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
    }

    .related-articles {
      padding: 40px;
      border-top: 1px solid #f1f2f6;

      .related-title {
        font-size: 24px;
        font-weight: 600;
        color: #2c3e50;
        margin-bottom: 25px;
      }

      .related-list {
        display: flex;
        flex-direction: column;
        gap: 20px;

        .related-item {
          display: flex;
          gap: 15px;
          padding: 15px;
          border-radius: 10px;
          transition: all 0.3s ease;
          cursor: pointer;

          &:hover {
            background: #f8f9fa;
            transform: translateX(5px);
          }

          .related-cover {
            width: 80px;
            height: 60px;
            object-fit: cover;
            border-radius: 8px;
            flex-shrink: 0;
          }

          .related-info {
            flex: 1;

            .related-article-title {
              font-size: 16px;
              font-weight: 500;
              color: #2c3e50;
              margin: 0 0 8px 0;
              line-height: 1.4;
            }

            .related-summary {
              color: #7f8c8d;
              font-size: 14px;
              margin: 0 0 5px 0;
              display: -webkit-box;
              -webkit-line-clamp: 2;
              -webkit-box-orient: vertical;
              overflow: hidden;
            }

            .related-date {
              color: #95a5a6;
              font-size: 12px;
            }
          }
        }
      }
    }

    .comments-section {
      padding: 40px;
      border-top: 1px solid #f1f2f6;

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
  .blog-article-detail {
    margin: 0 -20px;

    .article-container {
      border-radius: 0;

      .article-header {
        padding: 20px;

        .article-title {
          font-size: 24px;
        }

        .article-meta {
          flex-direction: column;
          align-items: flex-start;
          gap: 10px;
        }
      }

      .article-content {
        padding: 20px;

        .content-body {
          font-size: 15px;

          :deep(h2) {
            font-size: 20px;
          }

          :deep(h3) {
            font-size: 18px;
          }
        }
      }

      .article-footer {
        padding: 20px;

        .article-actions {
          flex-direction: column;
          gap: 10px;

          .action-btn {
            width: 100%;
          }
        }
      }

      .related-articles,
      .comments-section {
        padding: 20px;

        .related-list {
          .related-item {
            .related-cover {
              width: 60px;
              height: 45px;
            }
          }
        }
      }
    }

    .share-options {
      .share-item {
        padding: 15px;

        .anticon {
          font-size: 24px;
        }
      }
    }
  }
}
</style>