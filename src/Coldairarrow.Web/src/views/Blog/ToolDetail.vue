<template>
  <div class="blog-tool-detail">
    <div v-if="loading" class="loading-container">
      <a-spin size="large" />
    </div>
    
    <div v-else-if="tool" class="tool-container">
      <!-- 工具头部 -->
      <div class="tool-header">
        <div class="breadcrumb">
          <router-link to="/blog/home">首页</router-link>
          <a-icon type="right" />
          <router-link to="/blog/tools">工具推荐</router-link>
          <a-icon type="right" />
          <span>{{ tool.name }}</span>
        </div>
        
        <div class="tool-title-section">
          <div class="tool-icon">
            <img :src="tool.iconUrl || defaultIcon" :alt="tool.name" />
          </div>
          <div class="title-info">
            <h1 class="tool-title">{{ tool.name }}</h1>
            <p class="tool-category">{{ tool.category }}</p>
          </div>
          <div class="tool-price">
            <a-tag :color="getPriceColor(tool.priceType)" size="large">
              {{ getPriceText(tool.priceType) }}
            </a-tag>
          </div>
        </div>
        
        <div class="tool-meta">
          <div class="meta-left">
            <div class="tool-rating">
              <a-rate :value="tool.rating" disabled />
              <span class="rating-text">{{ tool.rating }}/5</span>
            </div>
            <span class="meta-item" v-if="tool.isRecommended">
              <a-icon type="star" theme="filled" style="color: #f39c12;" />
              推荐工具
            </span>
          </div>
          <div class="meta-right">
            <span class="meta-item">
              <a-icon type="eye" />
              {{ tool.viewCount || 0 }}
            </span>
            <span class="meta-item">
              <a-icon type="like" theme="filled" :class="{ liked: isLiked }" @click="toggleLike" />
              {{ tool.likeCount || 0 }}
            </span>
          </div>
        </div>
        
        <!-- 工具链接 -->
        <div class="tool-links">
          <a v-if="tool.officialUrl" :href="tool.officialUrl" target="_blank" class="tool-link official">
            <a-icon type="global" />
            官方网站
          </a>
          <a v-if="tool.downloadUrl" :href="tool.downloadUrl" target="_blank" class="tool-link download">
            <a-icon type="download" />
            立即下载
          </a>
          <a v-if="tool.githubUrl" :href="tool.githubUrl" target="_blank" class="tool-link github">
            <a-icon type="github" />
            开源代码
          </a>
        </div>
      </div>
      
      <!-- 工具描述 -->
      <div class="tool-description">
        <h3 class="section-title">工具简介</h3>
        <p class="description-text">{{ tool.description }}</p>
      </div>
      
      <!-- 工具标签 -->
      <div class="tool-tags-section">
        <h3 class="section-title">相关标签</h3>
        <div class="tags-list">
          <a-tag v-for="tag in tool.tags" :key="tag" color="blue" class="tool-tag">
            {{ tag }}
          </a-tag>
        </div>
      </div>
      
      <!-- 工具详情 -->
      <div class="tool-content">
        <h3 class="section-title">详细介绍</h3>
        <div class="content-body" v-html="tool.content"></div>
      </div>
      
      <!-- 工具特性 -->
      <div v-if="tool.features && tool.features.length > 0" class="tool-features">
        <h3 class="section-title">主要特性</h3>
        <div class="features-list">
          <div v-for="feature in tool.features" :key="feature.id" class="feature-item">
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
      
      <!-- 工具截图 -->
      <div v-if="tool.screenshots && tool.screenshots.length > 0" class="tool-screenshots">
        <h3 class="section-title">界面截图</h3>
        <div class="screenshots-grid">
          <div 
            v-for="screenshot in tool.screenshots" 
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
      
      <!-- 使用指南 -->
      <div v-if="tool.guide" class="tool-guide">
        <h3 class="section-title">使用指南</h3>
        <div class="guide-content">
          <div v-for="step in tool.guide" :key="step.id" class="guide-step">
            <div class="step-number">{{ step.step }}</div>
            <div class="step-content">
              <h4 class="step-title">{{ step.title }}</h4>
              <p class="step-description">{{ step.description }}</p>
            </div>
          </div>
        </div>
      </div>
      
      <!-- 工具操作 -->
      <div class="tool-actions">
        <a-button 
          :type="isLiked ? 'primary' : 'default'" 
          @click="toggleLike"
          class="action-btn like-btn">
          <a-icon type="like" :theme="isLiked ? 'filled' : 'outlined'" />
          {{ isLiked ? '已点赞' : '点赞' }}
        </a-button>
        <a-button @click="showShareModal" class="action-btn share-btn">
          <a-icon type="share-alt" />
          分享工具
        </a-button>
        <a-button @click="scrollToComments" class="action-btn comment-btn">
          <a-icon type="message" />
          评论
        </a-button>
      </div>
      
      <!-- 相关工具 -->
      <div class="related-tools" v-if="relatedTools.length > 0">
        <h3 class="section-title">相关工具</h3>
        <div class="related-list">
          <div 
            v-for="related in relatedTools" 
            :key="related.id"
            class="related-item"
            @click="viewRelatedTool(related.id)">
            <div class="related-icon">
              <img :src="related.iconUrl || defaultIcon" :alt="related.name" />
            </div>
            <div class="related-info">
              <h4 class="related-tool-name">{{ related.name }}</h4>
              <p class="related-description">{{ related.description }}</p>
              <div class="related-rating">
                <a-rate :value="related.rating" disabled size="small" />
                <a-tag :color="getPriceColor(related.priceType)" size="small">
                  {{ getPriceText(related.priceType) }}
                </a-tag>
              </div>
            </div>
          </div>
        </div>
      </div>
      
      <!-- 评论区域 -->
      <div id="comments" class="comments-section">
        <h3 class="comments-title">用户评价</h3>
        <div class="comments-placeholder">
          <a-empty description="暂无评价，快来发表看法吧！" />
        </div>
      </div>
    </div>
    
    <div v-else class="not-found">
      <a-result
        status="404"
        title="工具未找到"
        sub-title="抱歉，您访问的工具不存在或已被删除。">
        <template #extra>
          <a-button type="primary" @click="$router.push('/blog/tools')">
            返回工具列表
          </a-button>
        </template>
      </a-result>
    </div>
    
    <!-- 分享弹窗 -->
    <a-modal
      v-model="shareModalVisible"
      title="分享工具"
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
  name: 'BlogToolDetail',
  data() {
    return {
      tool: null,
      relatedTools: [],
      loading: true,
      isLiked: false,
      shareModalVisible: false,
      imagePreviewVisible: false,
      previewImageUrl: '',
      defaultIcon: '/assets/default-tool-icon.png'
    }
  },
  methods: {
    async loadTool() {
      const toolId = this.$route.params.id
      this.loading = true
      
      try {
        // 模拟数据
        this.tool = {
          id: parseInt(toolId),
          name: 'VS Code',
          description: '微软开发的免费代码编辑器，功能强大，扩展丰富，支持多种编程语言，是前端开发者和全栈开发者的首选工具。提供智能代码补全、语法高亮、调试功能等。',
          category: '开发工具',
          priceType: 'free',
          rating: 5,
          isRecommended: true,
          iconUrl: '/assets/vscode-icon.png',
          officialUrl: 'https://code.visualstudio.com',
          downloadUrl: 'https://code.visualstudio.com/download',
          githubUrl: 'https://github.com/Microsoft/vscode',
          tags: ['编辑器', '微软', '前端', '开发', '免费'],
          viewCount: 2456,
          likeCount: 189,
          content: `
            <h4>关于 Visual Studio Code</h4>
            <p>Visual Studio Code 是一个轻量级但功能强大的源代码编辑器，可在桌面上运行，适用于 Windows、macOS 和 Linux。它内置了对 JavaScript、TypeScript 和 Node.js 的支持，并拥有丰富的其他语言扩展生态系统。</p>
            
            <h4>主要优势</h4>
            <ul>
              <li><strong>免费开源</strong>：完全免费，代码开源，社区活跃</li>
              <li><strong>跨平台</strong>：支持 Windows、macOS、Linux 多个平台</li>
              <li><strong>轻量快速</strong>：启动速度快，占用资源少</li>
              <li><strong>丰富扩展</strong>：拥有数万个扩展，可满足各种开发需求</li>
              <li><strong>智能感知</strong>：提供智能代码补全和语法检查</li>
            </ul>
            
            <h4>适用场景</h4>
            <p>VS Code 适用于各种编程场景，包括但不限于：</p>
            <ul>
              <li>前端开发（HTML、CSS、JavaScript、Vue、React等）</li>
              <li>后端开发（Node.js、Python、Java、C#等）</li>
              <li>移动应用开发（React Native、Flutter等）</li>
              <li>数据科学（Python、R、Jupyter Notebooks）</li>
            </ul>
          `,
          features: [
            {
              id: 1,
              icon: 'code',
              title: '智能代码编辑',
              description: '提供语法高亮、智能补全、错误检测等功能，提高编码效率'
            },
            {
              id: 2,
              icon: 'bug',
              title: '集成调试器',
              description: '内置强大的调试功能，支持断点、变量监视、调用堆栈等'
            },
            {
              id: 3,
              icon: 'git',
              title: 'Git 集成',
              description: '内置 Git 支持，可直接进行版本控制操作'
            },
            {
              id: 4,
              icon: 'appstore',
              title: '扩展市场',
              description: '丰富的扩展生态系统，可安装各种插件扩展功能'
            }
          ],
          screenshots: [
            {
              id: 1,
              url: '/assets/vscode-screenshot1.jpg',
              title: '主界面'
            },
            {
              id: 2,
              url: '/assets/vscode-screenshot2.jpg',
              title: '调试界面'
            },
            {
              id: 3,
              url: '/assets/vscode-screenshot3.jpg',
              title: '扩展市场'
            }
          ],
          guide: [
            {
              id: 1,
              step: 1,
              title: '下载安装',
              description: '访问官方网站下载适合您操作系统的版本，然后按照安装向导完成安装'
            },
            {
              id: 2,
              step: 2,
              title: '选择主题',
              description: '打开 VS Code，按 Ctrl+Shift+P 打开命令面板，搜索 "theme" 选择喜欢的主题'
            },
            {
              id: 3,
              step: 3,
              title: '安装扩展',
              description: '点击左侧扩展图标，搜索并安装常用扩展如 Chinese、Prettier、ESLint 等'
            },
            {
              id: 4,
              step: 4,
              title: '配置设置',
              description: '按 Ctrl+, 打开设置，根据个人喜好调整编辑器配置，如字体大小、缩进等'
            }
          ]
        }
        
        // 加载相关工具
        this.relatedTools = [
          {
            id: 2,
            name: 'Sublime Text',
            description: '轻量级文本编辑器，启动速度极快',
            iconUrl: '/assets/sublime-icon.png',
            rating: 4.5,
            priceType: 'paid'
          },
          {
            id: 3,
            name: 'WebStorm',
            description: 'JetBrains 出品的强大 JavaScript IDE',
            iconUrl: '/assets/webstorm-icon.png',
            rating: 4.8,
            priceType: 'paid'
          }
        ]
        
      } catch (error) {
        console.error('加载工具失败:', error)
        this.$message.error('加载工具失败')
        this.tool = null
      } finally {
        this.loading = false
      }
    },
    
    getPriceColor(priceType) {
      const colorMap = {
        free: 'green',
        paid: 'orange',
        freemium: 'blue'
      }
      return colorMap[priceType] || 'default'
    },

    getPriceText(priceType) {
      const textMap = {
        free: '免费',
        paid: '付费',
        freemium: '免费增值'
      }
      return textMap[priceType] || '未知'
    },
    
    async toggleLike() {
      try {
        this.isLiked = !this.isLiked
        if (this.isLiked) {
          this.tool.likeCount++
          this.$message.success('点赞成功！')
        } else {
          this.tool.likeCount--
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
      const title = encodeURIComponent(this.tool.name)
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
    
    viewRelatedTool(id) {
      this.$router.push(`/blog/tools/${id}`)
    }
  },
  
  mounted() {
    this.loadTool()
  },
  
  watch: {
    '$route.params.id'() {
      this.loadTool()
    }
  }
}
</script>

<style lang="less" scoped>
.blog-tool-detail {
  max-width: 900px;
  margin: 0 auto;
  
  .loading-container {
    text-align: center;
    padding: 80px 0;
  }
  
  .tool-container {
    background: white;
    border-radius: 15px;
    overflow: hidden;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
    
    .tool-header {
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
      
      .tool-title-section {
        display: flex;
        align-items: center;
        gap: 20px;
        margin-bottom: 20px;
        
        .tool-icon {
          width: 80px;
          height: 80px;
          flex-shrink: 0;
          
          img {
            width: 100%;
            height: 100%;
            object-fit: contain;
            border-radius: 12px;
          }
        }
        
        .title-info {
          flex: 1;
          
          .tool-title {
            font-size: 32px;
            font-weight: 700;
            color: #2c3e50;
            margin: 0 0 8px 0;
            line-height: 1.4;
          }
          
          .tool-category {
            color: #7f8c8d;
            font-size: 16px;
            margin: 0;
          }
        }
        
        .tool-price {
          flex-shrink: 0;
        }
      }
      
      .tool-meta {
        display: flex;
        justify-content: space-between;
        align-items: center;
        margin-bottom: 25px;
        
        .meta-left {
          display: flex;
          align-items: center;
          gap: 20px;
          
          .tool-rating {
            display: flex;
            align-items: center;
            gap: 8px;
            
            .rating-text {
              color: #7f8c8d;
              font-size: 14px;
            }
          }
        }
        
        .meta-item {
          display: inline-flex;
          align-items: center;
          gap: 5px;
          color: #7f8c8d;
          font-size: 14px;
          
          .anticon {
            font-size: 14px;
            
            &.liked {
              color: #e74c3c;
            }
          }
        }
        
        .meta-right {
          display: flex;
          gap: 20px;
          
          .meta-item {
            cursor: pointer;
            
            &:hover .anticon {
              color: #3498db;
            }
          }
        }
      }
      
      .tool-links {
        display: flex;
        gap: 15px;
        flex-wrap: wrap;
        
        .tool-link {
          padding: 12px 24px;
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
          
          &.official {
            background: linear-gradient(45deg, #3498db, #2980b9);
            color: white;
            
            &:hover {
              background: linear-gradient(45deg, #2980b9, #21618c);
            }
          }
          
          &.download {
            background: linear-gradient(45deg, #27ae60, #229954);
            color: white;
            
            &:hover {
              background: linear-gradient(45deg, #229954, #1e8449);
            }
          }
          
          &.github {
            background: linear-gradient(45deg, #2c3e50, #34495e);
            color: white;
            
            &:hover {
              background: linear-gradient(45deg, #34495e, #2c3e50);
            }
          }
        }
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
    
    .tool-description {
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
    
    .tool-tags-section {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .tags-list {
        display: flex;
        flex-wrap: wrap;
        gap: 10px;
        
        .tool-tag {
          font-size: 14px;
          padding: 8px 16px;
          border-radius: 20px;
          font-weight: 500;
        }
      }
    }
    
    .tool-content {
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
    
    .tool-features {
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
    
    .tool-screenshots {
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
    
    .tool-guide {
      padding: 40px;
      border-bottom: 1px solid #f1f2f6;
      
      .guide-content {
        .guide-step {
          display: flex;
          gap: 20px;
          margin-bottom: 30px;
          
          &:last-child {
            margin-bottom: 0;
          }
          
          .step-number {
            width: 40px;
            height: 40px;
            border-radius: 50%;
            background: linear-gradient(45deg, #667eea, #764ba2);
            color: white;
            display: flex;
            align-items: center;
            justify-content: center;
            font-size: 16px;
            font-weight: 600;
            flex-shrink: 0;
          }
          
          .step-content {
            flex: 1;
            
            .step-title {
              font-size: 18px;
              font-weight: 600;
              color: #2c3e50;
              margin: 0 0 8px 0;
            }
            
            .step-description {
              color: #7f8c8d;
              margin: 0;
              line-height: 1.6;
            }
          }
        }
      }
    }
    
    .tool-actions {
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
    
    .related-tools {
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
          
          .related-icon {
            width: 60px;
            height: 60px;
            flex-shrink: 0;
            
            img {
              width: 100%;
              height: 100%;
              object-fit: contain;
              border-radius: 8px;
            }
          }
          
          .related-info {
            flex: 1;
            
            .related-tool-name {
              font-size: 18px;
              font-weight: 500;
              color: #2c3e50;
              margin: 0 0 8px 0;
              line-height: 1.4;
            }
            
            .related-description {
              color: #7f8c8d;
              margin: 0 0 10px 0;
              display: -webkit-box;
              -webkit-line-clamp: 2;
              -webkit-box-orient: vertical;
              overflow: hidden;
            }
            
            .related-rating {
              display: flex;
              align-items: center;
              gap: 10px;
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
  .blog-tool-detail {
    margin: 0 -20px;
    
    .tool-container {
      border-radius: 0;
      
      .tool-header {
        padding: 20px;
        
        .tool-title-section {
          flex-direction: column;
          align-items: flex-start;
          gap: 15px;
          
          .tool-icon {
            width: 60px;
            height: 60px;
          }
          
          .title-info {
            .tool-title {
              font-size: 24px;
            }
          }
        }
        
        .tool-meta {
          flex-direction: column;
          align-items: flex-start;
          gap: 15px;
        }
        
        .tool-links {
          .tool-link {
            font-size: 14px;
            padding: 10px 20px;
          }
        }
      }
      
      .tool-description,
      .tool-tags-section,
      .tool-content,
      .tool-features,
      .tool-screenshots,
      .tool-guide,
      .tool-actions,
      .related-tools,
      .comments-section {
        padding: 20px;
      }
      
      .tool-features {
        .features-list {
          grid-template-columns: 1fr;
          gap: 15px;
          
          .feature-item {
            padding: 15px;
          }
        }
      }
      
      .tool-screenshots {
        .screenshots-grid {
          grid-template-columns: 1fr;
        }
      }
      
      .tool-actions {
        .action-btn {
          display: block;
          margin: 5px 0;
          width: 100%;
        }
      }
      
      .related-tools {
        .related-list {
          .related-item {
            flex-direction: column;
            
            .related-icon {
              width: 50px;
              height: 50px;
              align-self: center;
            }
          }
        }
      }
    }
  }
}
</style>