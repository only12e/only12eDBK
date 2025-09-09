<template>
  <div class="blog-tools">
    <div class="tools-header">
      <h1 class="page-title">工具推荐</h1>
      <p class="page-subtitle">分享好用的开发工具和资源</p>
    </div>

    <div class="tools-content">
      <div class="tools-categories">
        <a-button 
          v-for="category in categories" 
          :key="category.key"
          :type="selectedCategory === category.key ? 'primary' : 'default'"
          @click="selectCategory(category.key)"
          class="category-btn">
          <a-icon :type="category.icon" />
          {{ category.name }}
        </a-button>
      </div>

      <div class="tools-grid">
        <div 
          v-for="tool in filteredTools" 
          :key="tool.id" 
          class="tool-card"
          @click="viewTool(tool.id)">
          <div class="tool-header">
            <div class="tool-icon">
              <img :src="tool.iconUrl || defaultIcon" :alt="tool.name" />
            </div>
            <div class="tool-info">
              <h3 class="tool-name">{{ tool.name }}</h3>
              <p class="tool-category">{{ tool.category }}</p>
            </div>
            <div class="tool-price">
              <a-tag :color="getPriceColor(tool.priceType)">
                {{ getPriceText(tool.priceType) }}
              </a-tag>
            </div>
          </div>
          
          <div class="tool-body">
            <p class="tool-description">{{ tool.description }}</p>
            
            <div class="tool-rating">
              <a-rate :value="tool.rating" disabled />
              <span class="rating-text">{{ tool.rating }}/5</span>
            </div>
            
            <div class="tool-actions">
              <a v-if="tool.officialUrl" :href="tool.officialUrl" target="_blank" class="tool-link official" @click.stop>
                <a-icon type="global" />
                官网
              </a>
              <a v-if="tool.downloadUrl" :href="tool.downloadUrl" target="_blank" class="tool-link download" @click.stop>
                <a-icon type="download" />
                下载
              </a>
            </div>
          </div>
          
          <div class="tool-footer">
            <div class="tool-tags">
              <a-tag v-for="tag in tool.tags" :key="tag" size="small">{{ tag }}</a-tag>
            </div>
            <div class="tool-recommend" v-if="tool.isRecommended">
              <a-icon type="star" theme="filled" />
              推荐
            </div>
          </div>
        </div>
      </div>

      <div class="loading-more" v-if="loading">
        <a-spin size="large" />
      </div>

      <div class="empty-state" v-if="!loading && filteredTools.length === 0">
        <a-empty description="暂无工具" />
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogTools',
  data() {
    return {
      tools: [],
      loading: false,
      selectedCategory: 'all',
      defaultIcon: '/assets/default-tool-icon.png',
      categories: [
        { key: 'all', name: '全部', icon: 'appstore' },
        { key: 'development', name: '开发工具', icon: 'code' },
        { key: 'design', name: '设计工具', icon: 'bg-colors' },
        { key: 'productivity', name: '效率工具', icon: 'dashboard' },
        { key: 'testing', name: '测试工具', icon: 'bug' },
        { key: 'deployment', name: '部署工具', icon: 'cloud-upload' }
      ]
    }
  },
  computed: {
    filteredTools() {
      if (this.selectedCategory === 'all') {
        return this.tools
      }
      return this.tools.filter(tool => tool.category === this.selectedCategory)
    }
  },
  methods: {
    async loadTools() {
      this.loading = true
      try {
        // 模拟数据
        this.tools = [
          {
            id: 1,
            name: 'VS Code',
            description: '微软开发的免费代码编辑器，功能强大，扩展丰富，是前端开发者的首选工具。',
            category: 'development',
            priceType: 'free',
            rating: 5,
            isRecommended: true,
            iconUrl: '/assets/vscode-icon.png',
            officialUrl: 'https://code.visualstudio.com',
            downloadUrl: 'https://code.visualstudio.com/download',
            tags: ['编辑器', '微软', '前端']
          },
          {
            id: 2,
            name: 'Postman',
            description: 'API开发和测试的必备工具，支持多种HTTP方法，提供友好的界面和强大的功能。',
            category: 'testing',
            priceType: 'freemium',
            rating: 4.8,
            isRecommended: true,
            iconUrl: '/assets/postman-icon.png',
            officialUrl: 'https://www.postman.com',
            downloadUrl: 'https://www.postman.com/downloads',
            tags: ['API', '测试', 'HTTP']
          },
          {
            id: 3,
            name: 'Figma',
            description: '基于云端的设计工具，支持团队协作，是UI/UX设计师的热门选择。',
            category: 'design',
            priceType: 'freemium',
            rating: 4.9,
            isRecommended: true,
            iconUrl: '/assets/figma-icon.png',
            officialUrl: 'https://www.figma.com',
            downloadUrl: 'https://www.figma.com/downloads',
            tags: ['设计', 'UI', 'UX', '协作']
          },
          {
            id: 4,
            name: 'Docker',
            description: '容器化平台，简化应用部署和管理，是现代DevOps的重要工具。',
            category: 'deployment',
            priceType: 'free',
            rating: 4.7,
            isRecommended: true,
            iconUrl: '/assets/docker-icon.png',
            officialUrl: 'https://www.docker.com',
            downloadUrl: 'https://www.docker.com/get-started',
            tags: ['容器', '部署', 'DevOps']
          },
          {
            id: 5,
            name: 'Notion',
            description: '集笔记、知识库、项目管理于一体的生产力工具，适合个人和团队使用。',
            category: 'productivity',
            priceType: 'freemium',
            rating: 4.5,
            isRecommended: false,
            iconUrl: '/assets/notion-icon.png',
            officialUrl: 'https://www.notion.so',
            downloadUrl: 'https://www.notion.so/desktop',
            tags: ['笔记', '项目管理', '协作']
          }
        ]
      } catch (error) {
        console.error('加载工具失败:', error)
        this.$message.error('加载工具失败')
      } finally {
        this.loading = false
      }
    },

    selectCategory(category) {
      this.selectedCategory = category
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

    viewTool(id) {
      this.$router.push(`/blog/tools/${id}`)
    }
  },

  mounted() {
    this.loadTools()
  }
}
</script>

<style lang="less" scoped>
.blog-tools {
  .tools-header {
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
  
  .tools-content {
    .tools-categories {
      display: flex;
      gap: 10px;
      margin-bottom: 40px;
      flex-wrap: wrap;
      justify-content: center;
      
      .category-btn {
        border-radius: 20px;
        height: 40px;
        padding: 0 20px;
        font-weight: 500;
        transition: all 0.3s ease;
        
        &:hover {
          transform: translateY(-2px);
        }
        
        .anticon {
          margin-right: 8px;
        }
      }
    }
    
    .tools-grid {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
      gap: 25px;
      
      .tool-card {
        background: white;
        border-radius: 15px;
        padding: 25px;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
        transition: all 0.3s ease;
        cursor: pointer;
        
        &:hover {
          transform: translateY(-5px);
          box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
        }
        
        .tool-header {
          display: flex;
          align-items: flex-start;
          margin-bottom: 15px;
          
          .tool-icon {
            width: 60px;
            height: 60px;
            margin-right: 15px;
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
              font-size: 20px;
              font-weight: 600;
              color: #2c3e50;
              margin: 0 0 5px 0;
            }
            
            .tool-category {
              color: #7f8c8d;
              margin: 0;
              font-size: 14px;
            }
          }
          
          .tool-price {
            flex-shrink: 0;
          }
        }
        
        .tool-body {
          margin-bottom: 20px;
          
          .tool-description {
            color: #7f8c8d;
            line-height: 1.6;
            margin-bottom: 15px;
            display: -webkit-box;
            -webkit-line-clamp: 3;
            -webkit-box-orient: vertical;
            overflow: hidden;
          }
          
          .tool-rating {
            display: flex;
            align-items: center;
            gap: 10px;
            margin-bottom: 15px;
            
            .rating-text {
              color: #95a5a6;
              font-size: 14px;
            }
          }
          
          .tool-actions {
            display: flex;
            gap: 10px;
            
            .tool-link {
              padding: 8px 16px;
              border-radius: 20px;
              text-decoration: none;
              font-size: 14px;
              font-weight: 500;
              transition: all 0.3s ease;
              
              .anticon {
                margin-right: 5px;
              }
              
              &.official {
                background: linear-gradient(45deg, #3498db, #2980b9);
                color: white;
                
                &:hover {
                  background: linear-gradient(45deg, #2980b9, #21618c);
                  color: white;
                  transform: translateY(-2px);
                }
              }
              
              &.download {
                background: linear-gradient(45deg, #27ae60, #229954);
                color: white;
                
                &:hover {
                  background: linear-gradient(45deg, #229954, #1e8449);
                  color: white;
                  transform: translateY(-2px);
                }
              }
            }
          }
        }
        
        .tool-footer {
          display: flex;
          justify-content: space-between;
          align-items: center;
          padding-top: 15px;
          border-top: 1px solid #f1f2f6;
          
          .tool-tags {
            flex: 1;
            
            .ant-tag {
              margin-right: 5px;
              margin-bottom: 5px;
            }
          }
          
          .tool-recommend {
            color: #f39c12;
            font-size: 14px;
            font-weight: 500;
            display: flex;
            align-items: center;
            gap: 5px;
            flex-shrink: 0;
            
            .anticon {
              color: #f39c12;
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
  .blog-tools {
    .tools-header {
      .page-title {
        font-size: 28px;
      }
    }
    
    .tools-content {
      .tools-categories {
        .category-btn {
          font-size: 14px;
          height: 36px;
          padding: 0 16px;
        }
      }
      
      .tools-grid {
        grid-template-columns: 1fr;
        
        .tool-card {
          padding: 20px;
          
          .tool-header {
            .tool-icon {
              width: 50px;
              height: 50px;
            }
            
            .tool-info {
              .tool-name {
                font-size: 18px;
              }
            }
          }
          
          .tool-body {
            .tool-actions {
              flex-direction: column;
              
              .tool-link {
                text-align: center;
              }
            }
          }
          
          .tool-footer {
            flex-direction: column;
            align-items: flex-start;
            gap: 10px;
          }
        }
      }
    }
  }
}
</style>