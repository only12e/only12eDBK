<template>
  <div class="statistics-dashboard">
    <!-- 总览卡片 -->
    <a-row :gutter="[16, 16]">
      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card class="stat-card article-card">
          <template v-if="overviewLoading">
            <a-skeleton active>
              <a-skeleton-input style="width: 120px; height: 40px;" />
              <div style="margin-top: 16px;">
                <a-skeleton-input style="width: 80px; height: 20px;" />
                <a-skeleton-input style="width: 60px; height: 20px; margin-left: 16px;" />
              </div>
              <div style="margin-top: 12px;">
                <a-skeleton-input style="width: 100px; height: 20px;" />
              </div>
            </a-skeleton>
          </template>
          <template v-else>
          <a-statistic
            title="文章总数"
            :value="safeOverview.Articles.TotalCount || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="file-text" style="color: #1890ff;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">已发布: {{ safeOverview.Articles.PublishedCount || 0 }}</span>
            <span class="stat-item">草稿: {{ safeOverview.Articles.DraftCount || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ safeOverview.Articles.TodayCount || 0 }}
            </span>
          </div>
          </template>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card project-card">
          <a-statistic
            title="项目总数"
            :value="safeOverview.Projects.TotalCount || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="project" style="color: #52c41a;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">进行中: {{ safeOverview.Projects.ActiveCount || 0 }}</span>
            <span class="stat-item">已完成: {{ safeOverview.Projects.CompletedCount || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ safeOverview.Projects.TodayCount || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card tool-card">
          <a-statistic
            title="工具总数"
            :value="safeOverview.Tools.TotalCount || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="tool" style="color: #fa8c16;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">推荐: {{ safeOverview.Tools.RecommendedCount || 0 }}</span>
            <span class="stat-item">免费: {{ safeOverview.Tools.FreeCount || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ safeOverview.Tools.TodayCount || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card tech-card">
          <a-statistic
            title="技术栈总数"
            :value="safeOverview.Technologies.TotalCount || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="code" style="color: #eb2f96;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">精选: {{ safeOverview.Technologies.FeaturedCount || 0 }}</span>
            <span class="stat-item">平均熟练度: {{ safeOverview.Technologies.AverageProficiency || 0 }}%</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ safeOverview.Technologies.TodayCount || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>
    </a-row>

    <a-row :gutter="[16, 16]" style="margin-top: 16px;">
      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card comment-card">
          <a-statistic
            title="评论总数"
            :value="safeOverview.Comments.TotalCount || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="message" style="color: #722ed1;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">已通过: {{ safeOverview.Comments.ApprovedCount || 0 }}</span>
            <span class="stat-item">待审核: {{ safeOverview.Comments.PendingCount || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ safeOverview.Comments.TodayCount || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card user-card">
          <a-statistic
            title="用户总数"
            :value="safeOverview.Users.TotalCount || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="user" style="color: #13c2c2;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">活跃: {{ safeOverview.Users.ActiveCount || 0 }}</span>
            <span class="stat-item">管理员: {{ safeOverview.Users.AdminCount || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ safeOverview.Users.TodayCount || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card access-card">
          <a-statistic
            title="总访问量"
            :value="safeOverview.Access.TotalViews || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="eye" style="color: #f5222d;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">总点赞: {{ safeOverview.Access.TotalLikes || 0 }}</span>
            <span class="stat-item">今日访问: {{ safeOverview.Access.TodayViews || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="heart" />
              今日点赞: {{ safeOverview.Access.TodayLikes || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card class="stat-card action-card">
          <div class="quick-actions">
            <h4>快速操作</h4>
            <a-button-group style="width: 100%; margin-bottom: 12px;">
              <a-button type="primary" @click="refreshData" :loading="overviewLoading">
                <a-icon type="reload" />
                刷新数据
              </a-button>
            </a-button-group>
            <a-button-group style="width: 100%;">
              <a-button type="default" @click="exportData">
                <a-icon type="download" />
                导出数据
              </a-button>
            </a-button-group>
            <div style="margin-top: 12px;">
              <a-button type="link" @click="showTrendModal">
                <a-icon type="line-chart" />
                趋势分析
              </a-button>
              <a-button type="link" @click="showPopularModal">
                <a-icon type="fire" />
                热门内容
              </a-button>
            </div>
          </div>
        </a-card>
      </a-col>
    </a-row>

    <!-- 趋势图表区域 -->
    <a-row :gutter="[16, 16]" style="margin-top: 24px;">
      <a-col :span="24">
        <a-card title="趋势统计" :loading="trendLoading">
          <div slot="extra">
            <a-select v-model="trendQuery.type" style="width: 120px; margin-right: 8px;" @change="getTrendData">
              <a-select-option value="article">文章</a-select-option>
              <a-select-option value="project">项目</a-select-option>
              <a-select-option value="tool">工具</a-select-option>
              <a-select-option value="technology">技术栈</a-select-option>
              <a-select-option value="comment">评论</a-select-option>
              <a-select-option value="user">用户</a-select-option>
            </a-select>
            <a-select v-model="trendQuery.period" style="width: 100px;" @change="getTrendData">
              <a-select-option value="day">日</a-select-option>
              <a-select-option value="week">周</a-select-option>
              <a-select-option value="month">月</a-select-option>
            </a-select>
          </div>
          
          <div v-if="trendData && trendData.DataPoints && trendData.DataPoints.length > 0">
            <div id="trendChart" style="width: 100%; height: 400px;"></div>
          </div>
          <a-empty v-else description="暂无趋势数据">
            <a-button slot="description" type="primary" @click="getTrendData">刷新数据</a-button>
          </a-empty>
        </a-card>
      </a-col>
    </a-row>

    <!-- 热门内容 -->
    <a-row :gutter="[16, 16]" style="margin-top: 24px;" v-if="popularContent && ((popularContent.PopularArticles && popularContent.PopularArticles.length > 0) || (popularContent.PopularProjects && popularContent.PopularProjects.length > 0))">
      <a-col :xl="12" :lg="24" :md="24" :sm="24">
        <a-card class="popular-articles-card" :loading="popularLoading">
          <div slot="title" class="card-title-with-icon">
            <a-icon type="fire" class="title-icon" />
            <span class="title-text">热门文章</span>
            <div class="title-badge">
              <span class="badge-text">TOP</span>
              <span class="badge-count">5</span>
            </div>
          </div>
          
          <div v-if="popularContent.PopularArticles && popularContent.PopularArticles.length > 0" class="popular-content">
            <div 
              v-for="(item, index) in popularContent.PopularArticles" 
              :key="index" 
              class="popular-item popular-article-item"
              @click="handleArticleClick(item)">
              <div class="item-header">
                <div class="rank-badge">
                  <a-icon v-if="index === 0" type="crown" class="crown-icon" />
                  <span v-else class="rank-number">#{{ index + 1 }}</span>
                </div>
                <div class="item-content">
                  <div class="item-title">{{ item.Title }}</div>
                  <div class="item-meta">
                    <div class="meta-stats">
                      <span class="stat-item view-stat">
                        <a-icon type="eye" />
                        {{ item.ViewCount || 0 }}
                      </span>
                      <span class="stat-item like-stat">
                        <a-icon type="heart" />
                        {{ item.LikeCount || 0 }}
                      </span>
                      <span class="stat-item comment-stat">
                        <a-icon type="message" />
                        {{ item.CommentCount || 0 }}
                      </span>
                    </div>
                    <div class="meta-date">
                      {{ formatDate(item.CreatedAt) }}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div v-else class="empty-state">
            <a-icon type="file-text" class="empty-icon" />
            <p class="empty-text">暂无热门文章</p>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="12" :lg="24" :md="24" :sm="24">
        <a-card class="popular-projects-card" :loading="popularLoading">
          <div slot="title" class="card-title-with-icon">
            <a-icon type="trophy" class="title-icon" />
            <span class="title-text">热门项目</span>
            <div class="title-badge">
              <span class="badge-text">TOP</span>
              <span class="badge-count">5</span>
            </div>
          </div>
          
          <div v-if="popularContent.PopularProjects && popularContent.PopularProjects.length > 0" class="popular-content">
            <div 
              v-for="(item, index) in popularContent.PopularProjects" 
              :key="index" 
              class="popular-item popular-project-item"
              @click="handleProjectClick(item)">
              <div class="item-header">
                <div class="rank-badge">
                  <a-icon v-if="index === 0" type="crown" class="crown-icon" />
                  <span v-else class="rank-number">#{{ index + 1 }}</span>
                </div>
                <div class="item-content">
                  <div class="item-title">{{ item.Title }}</div>
                  <div class="item-meta">
                    <div class="meta-stats">
                      <span class="stat-item view-stat">
                        <a-icon type="eye" />
                        {{ item.ViewCount || 0 }}
                      </span>
                      <span class="stat-item like-stat">
                        <a-icon type="heart" />
                        {{ item.LikeCount || 0 }}
                      </span>
                      <span class="stat-item comment-stat">
                        <a-icon type="message" />
                        {{ item.CommentCount || 0 }}
                      </span>
                    </div>
                    <div class="meta-date">
                      {{ formatDate(item.CreatedAt) }}
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
          <div v-else class="empty-state">
            <a-icon type="project" class="empty-icon" />
            <p class="empty-text">暂无热门项目</p>
          </div>
        </a-card>
      </a-col>
    </a-row>
    
    <!-- 无数据时的提示 -->
    <a-row :gutter="[16, 16]" style="margin-top: 24px;" v-else-if="!popularLoading">
      <a-col :span="24">
        <a-card>
          <a-empty description="暂无热门内容数据">
            <a-button slot="description" type="primary" @click="getPopularContent">刷新数据</a-button>
          </a-empty>
        </a-card>
      </a-col>
    </a-row>

    <!-- 趋势详情模态框 -->
    <a-modal
      title="趋势分析详情"
      :visible="trendModalVisible"
      @cancel="trendModalVisible = false"
      :footer="null"
      :width="1000">
      <div>
        <a-row :gutter="16" style="margin-bottom: 16px;">
          <a-col :span="8">
            <a-select v-model="trendModalQuery.type" style="width: 100%;" @change="getTrendModalData">
              <a-select-option value="article">文章</a-select-option>
              <a-select-option value="project">项目</a-select-option>
              <a-select-option value="tool">工具</a-select-option>
              <a-select-option value="technology">技术栈</a-select-option>
              <a-select-option value="comment">评论</a-select-option>
              <a-select-option value="user">用户</a-select-option>
            </a-select>
          </a-col>
          <a-col :span="8">
            <a-select v-model="trendModalQuery.period" style="width: 100%;" @change="getTrendModalData">
              <a-select-option value="day">日</a-select-option>
              <a-select-option value="week">周</a-select-option>
              <a-select-option value="month">月</a-select-option>
            </a-select>
          </a-col>
          <a-col :span="8">
            <a-input-number v-model="trendModalQuery.limit" :min="10" :max="365" placeholder="数据点数量" style="width: 100%;" @change="getTrendModalData" />
          </a-col>
        </a-row>
        <a-spin :spinning="trendModalLoading">
          <div v-if="trendModalData">
            <div id="trendModalChart" style="width: 100%; height: 400px;"></div>
            <a-row :gutter="16" style="margin-top: 16px;">
              <a-col :span="6">
                <a-statistic title="总计" :value="trendModalData.Total || 0" />
              </a-col>
              <a-col :span="6">
                <a-statistic title="平均" :value="trendModalData.Average || 0" :precision="2" />
              </a-col>
              <a-col :span="6">
                <a-statistic title="最高" :value="trendModalData.Maximum || 0" />
              </a-col>
              <a-col :span="6">
                <a-statistic title="最低" :value="trendModalData.Minimum || 0" />
              </a-col>
            </a-row>
          </div>
          <a-empty v-else description="暂无数据" />
        </a-spin>
      </div>
    </a-modal>

    <!-- 热门内容详情模态框 -->
    <a-modal
      title="热门内容详情"
      :visible="popularModalVisible"
      @cancel="popularModalVisible = false"
      :footer="null"
      :width="1000">
      <a-spin :spinning="popularModalLoading">
        <a-tabs v-if="popularModalData" defaultActiveKey="articles">
          <a-tab-pane key="articles" tab="热门文章">
            <a-list
              item-layout="horizontal"
              :data-source="popularModalData.PopularArticles || []">
              <a-list-item slot="renderItem" slot-scope="item">
                <a-list-item-meta>
                  <div slot="title" class="popular-item-title">{{ item.Title }}</div>
                  <div slot="description">
                    <span class="stat-tag">浏览: {{ item.ViewCount }}</span>
                    <span class="stat-tag">点赞: {{ item.LikeCount }}</span>
                    <span class="stat-tag">评论: {{ item.CommentCount }}</span>
                    <span class="stat-tag">{{ formatDate(item.CreatedAt) }}</span>
                  </div>
                </a-list-item-meta>
              </a-list-item>
            </a-list>
          </a-tab-pane>
          <a-tab-pane key="projects" tab="热门项目">
            <a-list
              item-layout="horizontal"
              :data-source="popularModalData.PopularProjects || []">
              <a-list-item slot="renderItem" slot-scope="item">
                <a-list-item-meta>
                  <div slot="title" class="popular-item-title">{{ item.Title }}</div>
                  <div slot="description">
                    <span class="stat-tag">浏览: {{ item.ViewCount }}</span>
                    <span class="stat-tag">点赞: {{ item.LikeCount }}</span>
                    <span class="stat-tag">评论: {{ item.CommentCount }}</span>
                    <span class="stat-tag">{{ formatDate(item.CreatedAt) }}</span>
                  </div>
                </a-list-item-meta>
              </a-list-item>
            </a-list>
          </a-tab-pane>
          <a-tab-pane key="tools" tab="热门工具">
            <a-list
              item-layout="horizontal"
              :data-source="popularModalData.PopularTools || []">
              <a-list-item slot="renderItem" slot-scope="item">
                <a-list-item-meta>
                  <div slot="title" class="popular-item-title">{{ item.Title }}</div>
                  <div slot="description">
                    <span class="stat-tag">浏览: {{ item.ViewCount }}</span>
                    <span class="stat-tag">点赞: {{ item.LikeCount }}</span>
                    <span class="stat-tag">评论: {{ item.CommentCount }}</span>
                    <span class="stat-tag">{{ formatDate(item.CreatedAt) }}</span>
                  </div>
                </a-list-item-meta>
              </a-list-item>
            </a-list>
          </a-tab-pane>
          <a-tab-pane key="technologies" tab="热门技术">
            <a-list
              item-layout="horizontal"
              :data-source="popularModalData.PopularTechnologies || []">
              <a-list-item slot="renderItem" slot-scope="item">
                <a-list-item-meta>
                  <div slot="title" class="popular-item-title">{{ item.Title }}</div>
                  <div slot="description">
                    <span class="stat-tag">浏览: {{ item.ViewCount }}</span>
                    <span class="stat-tag">点赞: {{ item.LikeCount }}</span>
                    <span class="stat-tag">评论: {{ item.CommentCount }}</span>
                    <span class="stat-tag">{{ formatDate(item.CreatedAt) }}</span>
                  </div>
                </a-list-item-meta>
              </a-list-item>
            </a-list>
          </a-tab-pane>
        </a-tabs>
        <a-empty v-else description="暂无数据" />
      </a-spin>
    </a-modal>
  </div>
</template>

<script>
import { GetOverview, GetTrendStatistics, GetPopularContent } from '@/api/blog_statistics'
import * as echarts from 'echarts'

export default {
  name: 'BlogStatistics',
  data() {
    return {
      overview: {},
      overviewLoading: true,
      
      trendData: null,
      trendLoading: false,
      trendQuery: {
        type: 'article',
        period: 'day',
        limit: 30
      },
      
      popularContent: null,
      popularLoading: false,
      
      trendModalVisible: false,
      trendModalData: null,
      trendModalLoading: false,
      trendModalQuery: {
        type: 'article',
        period: 'day',
        limit: 30
      },
      
      popularModalVisible: false,
      popularModalData: null,
      popularModalLoading: false,
      
      chart: null,
      modalChart: null,
      
      refreshInterval: null,
      autoRefresh: false,
      skeletonLoading: true
    }
  },
  
  computed: {
    safeOverview() {
      return {
        Articles: this.overview.Articles || {},
        Projects: this.overview.Projects || {},
        Tools: this.overview.Tools || {},
        Technologies: this.overview.Technologies || {},
        Comments: this.overview.Comments || {},
        Users: this.overview.Users || {},
        Access: this.overview.Access || {}
      }
    },
    
    chartOptions() {
      return {
        responsive: true,
        maintainAspectRatio: false,
        animation: {
          duration: 1000,
          easing: 'easeInOutQuart'
        }
      }
    }
  },
  mounted() {
    this.loadData()
    window.addEventListener('resize', this.handleResize)
  },
  beforeDestroy() {
    this.cleanupResources()
  },
  
  methods: {
    cleanupResources() {
      if (this.chart) {
        this.chart.dispose()
        this.chart = null
      }
      if (this.modalChart) {
        this.modalChart.dispose()
        this.modalChart = null
      }
      if (this.refreshInterval) {
        clearInterval(this.refreshInterval)
        this.refreshInterval = null
      }
      
      window.removeEventListener('resize', this.handleResize)
    },
    
    handleResize() {
      if (this.chart) {
        this.chart.resize()
      }
      if (this.modalChart) {
        this.modalChart.resize()
      }
    },
    async loadData() {
      await Promise.all([
        this.getOverview(),
        this.getTrendData(),
        this.getPopularContent()
      ])
    },
    
    async getOverview() {
      try {
        this.overviewLoading = true
        const res = await GetOverview()
        if (res.Success) {
          this.overview = res.Data || {}
        }
      } catch (error) {
        this.$message.error('获取统计概览失败')
      } finally {
        this.overviewLoading = false
      }
    },
    
    async getTrendData() {
      try {
        this.trendLoading = true
        const res = await GetTrendStatistics(this.trendQuery)
        if (res.Success) {
          this.trendData = res.Data
          this.$nextTick(() => {
            this.renderChart()
          })
        }
      } catch (error) {
        this.$message.error('获取趋势数据失败')
      } finally {
        this.trendLoading = false
      }
    },
    
    async getPopularContent() {
      try {
        this.popularLoading = true
        const res = await GetPopularContent(5)
        if (res.Success) {
          this.popularContent = res.Data || {}
        }
      } catch (error) {
        this.$message.error('获取热门内容失败')
      } finally {
        this.popularLoading = false
      }
    },
    
    renderChart() {
      if (!this.trendData || !this.trendData.DataPoints || this.trendData.DataPoints.length === 0) {
        return
      }
      
      const chartDom = document.getElementById('trendChart')
      if (!chartDom) return
      
      try {
        if (this.chart) {
          this.chart.dispose()
          this.chart = null
        }
        
        this.chart = echarts.init(chartDom)
        
        const dataPoints = this.trendData.DataPoints
        const dates = dataPoints.map(item => {
          const date = new Date(item.Date || item.Time)
          return date.toLocaleDateString('zh-CN')
        })
        const values = dataPoints.map(item => item.Value)
        
        const option = {
          title: {
            text: `${this.getTypeText(this.trendQuery.type)}${this.getPeriodText(this.trendQuery.period)}趋势统计`,
            left: 'center',
            textStyle: {
              fontSize: 16,
              fontWeight: 'normal',
              color: '#262626'
            }
          },
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'cross'
            },
            backgroundColor: 'rgba(255, 255, 255, 0.95)',
            borderColor: '#e6f7ff',
            borderWidth: 1,
            textStyle: {
              color: '#262626'
            }
          },
          grid: {
            left: '3%',
            right: '4%',
            bottom: '3%',
            containLabel: true
          },
          xAxis: {
            type: 'category',
            boundaryGap: false,
            data: dates,
            axisLabel: {
              rotate: 45,
              color: '#666'
            },
            axisLine: {
              lineStyle: {
                color: '#e8e8e8'
              }
            }
          },
          yAxis: {
            type: 'value',
            minInterval: 1,
            axisLabel: {
              color: '#666'
            },
            axisLine: {
              lineStyle: {
                color: '#e8e8e8'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#f0f0f0'
              }
            }
          },
          series: [{
            name: '数量',
            type: 'line',
            smooth: true,
            data: values,
            areaStyle: {
              opacity: 0.3,
              color: new echarts.graphic.LinearGradient(0, 0, 0, 1, [
                { offset: 0, color: 'rgba(24, 144, 255, 0.3)' },
                { offset: 1, color: 'rgba(24, 144, 255, 0.05)' }
              ])
            },
            itemStyle: {
              color: '#1890ff'
            },
            lineStyle: {
              color: '#1890ff',
              width: 2
            },
            symbol: 'circle',
            symbolSize: 6
          }],
          animation: true,
          animationDuration: 1000,
          animationEasing: 'cubicOut'
        }
        
        this.chart.setOption(option)
        
        // 添加图表点击事件
        this.chart.on('click', (params) => {
          this.$message.info(`${params.name}: ${params.value}`)
        })
        
      } catch (error) {
        console.error('图表渲染失败:', error)
        this.$message.error('图表渲染失败')
      }
    },
    
    refreshData() {
      this.loadData()
    },
    
    exportData() {
      try {
        const exportData = {
          统计概览: this.safeOverview,
          趋势数据: this.trendData,
          热门内容: this.popularContent,
          导出时间: new Date().toLocaleString('zh-CN')
        }
        
        const dataStr = JSON.stringify(exportData, null, 2)
        const dataBlob = new Blob([dataStr], { type: 'application/json' })
        const url = URL.createObjectURL(dataBlob)
        
        const link = document.createElement('a')
        link.href = url
        link.download = `统计数据_${new Date().toLocaleDateString('zh-CN').replace(/\//g, '-')}.json`
        document.body.appendChild(link)
        link.click()
        document.body.removeChild(link)
        URL.revokeObjectURL(url)
        
        this.$message.success('数据导出成功')
      } catch (error) {
        console.error('导出失败:', error)
        this.$message.error('数据导出失败')
      }
    },
    
    showTrendModal() {
      this.trendModalVisible = true
      this.getTrendModalData()
    },
    
    showPopularModal() {
      this.popularModalVisible = true
      this.getPopularModalData()
    },
    
    async getTrendModalData() {
      try {
        this.trendModalLoading = true
        const res = await GetTrendStatistics(this.trendModalQuery)
        if (res.Success) {
          this.trendModalData = res.Data
          this.$nextTick(() => {
            this.renderModalChart()
          })
        }
      } catch (error) {
        this.$message.error('获取趋势数据失败')
      } finally {
        this.trendModalLoading = false
      }
    },
    
    async getPopularModalData() {
      try {
        this.popularModalLoading = true
        const res = await GetPopularContent(20)
        if (res.Success) {
          this.popularModalData = res.Data
        }
      } catch (error) {
        this.$message.error('获取热门内容失败')
      } finally {
        this.popularModalLoading = false
      }
    },
    
    renderModalChart() {
      if (!this.trendModalData || !this.trendModalData.DataPoints) return
      
      const chartDom = document.getElementById('trendModalChart')
      if (!chartDom) return
      
      if (this.modalChart) {
        this.modalChart.dispose()
      }
      
      this.modalChart = echarts.init(chartDom)
      
      const dataPoints = this.trendModalData.DataPoints
      const dates = dataPoints.map(item => {
        const date = new Date(item.Date)
        return date.toLocaleDateString('zh-CN')
      })
      const values = dataPoints.map(item => item.Value)
      
      const option = {
        title: {
          text: `${this.getTypeText(this.trendModalQuery.type)}${this.getPeriodText(this.trendModalQuery.period)}趋势统计`,
          left: 'center'
        },
        tooltip: {
          trigger: 'axis',
          formatter: function(params) {
            return `${params[0].name}: ${params[0].value}`
          }
        },
        xAxis: {
          type: 'category',
          data: dates,
          axisLabel: {
            rotate: 45
          }
        },
        yAxis: {
          type: 'value',
          minInterval: 1
        },
        series: [{
          data: values,
          type: 'line',
          smooth: true,
          areaStyle: {
            opacity: 0.3
          },
          itemStyle: {
            color: '#1890ff'
          },
          lineStyle: {
            color: '#1890ff'
          }
        }]
      }
      
      this.modalChart.setOption(option)
      
      window.addEventListener('resize', () => {
        if (this.modalChart) {
          this.modalChart.resize()
        }
      })
    },
    
    formatDate(dateString) {
      if (!dateString) return '-'
      const date = new Date(dateString)
      return date.toLocaleDateString('zh-CN')
    },
    
    getTypeText(type) {
      const typeMap = {
        'article': '文章',
        'project': '项目',
        'tool': '工具',
        'technology': '技术栈',
        'comment': '评论',
        'user': '用户'
      }
      return typeMap[type] || type
    },
    
    getPeriodText(period) {
      const periodMap = {
        'day': '日',
        'week': '周',
        'month': '月'
      }
      return periodMap[period] || period
    },
    
    handleArticleClick(article) {
      // this.$message.info(`点击了文章：${article.Title}`)
    },
    
    handleProjectClick(project) {
      // this.$message.info(`点击了项目：${project.Title}`)
    }
  }
}
</script>

<style lang="less" scoped>
.statistics-dashboard {
  .stat-card {
    height: 180px;
    border-radius: 12px;
    box-shadow: 0 2px 12px rgba(0, 0, 0, 0.08);
    transition: all 0.3s ease;
    
    &:hover {
      box-shadow: 0 4px 20px rgba(0, 0, 0, 0.12);
      transform: translateY(-2px);
    }
    
    .ant-card-body {
      padding: 24px;
    }
    
    .ant-statistic-title {
      font-size: 15px;
      color: #666;
      font-weight: 500;
      margin-bottom: 8px;
    }
    
    .ant-statistic-content {
      font-size: 32px;
      font-weight: bold;
      line-height: 1.2;
      font-family: 'Monaco', 'Menlo', 'Ubuntu Mono', monospace;
    }
    
    .stat-details {
      margin-top: 16px;
      font-size: 13px;
      line-height: 1.5;
      
      .stat-item {
        display: inline-block;
        margin-right: 16px;
        color: #999;
        padding: 4px 8px;
        background: #f5f5f5;
        border-radius: 4px;
        font-weight: 500;
      }
    }
    
    .stat-growth {
      margin-top: 12px;
      font-size: 13px;
      
      .growth-item {
        color: #52c41a;
        font-weight: 500;
        display: flex;
        align-items: center;
        
        .anticon {
          margin-right: 6px;
          font-size: 14px;
        }
      }
    }
    
    // 不同卡片的主题色彩和边框
    &.article-card {
      border-top: 4px solid #1890ff;
      
      .ant-statistic-content {
        color: #1890ff;
      }
    }
    
    &.project-card {
      border-top: 4px solid #52c41a;
      
      .ant-statistic-content {
        color: #52c41a;
      }
    }
    
    &.tool-card {
      border-top: 4px solid #fa8c16;
      
      .ant-statistic-content {
        color: #fa8c16;
      }
    }
    
    &.tech-card {
      border-top: 4px solid #eb2f96;
      
      .ant-statistic-content {
        color: #eb2f96;
      }
    }
    
    &.comment-card {
      border-top: 4px solid #722ed1;
      
      .ant-statistic-content {
        color: #722ed1;
      }
    }
    
    &.user-card {
      border-top: 4px solid #13c2c2;
      
      .ant-statistic-content {
        color: #13c2c2;
      }
    }
    
    &.access-card {
      border-top: 4px solid #f5222d;
      
      .ant-statistic-content {
        color: #f5222d;
      }
    }
    
    &.action-card {
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      color: white;
      border: none;
      
      .ant-card-body {
        padding: 24px;
      }
      
      .quick-actions {
        h4 {
          color: white;
          font-size: 16px;
          font-weight: 600;
          margin-bottom: 16px;
          text-align: center;
        }
        
        .ant-btn {
          border-radius: 6px;
          font-weight: 500;
          transition: all 0.3s ease;
          
          &:not(.ant-btn-link) {
            border: 2px solid rgba(255, 255, 255, 0.3);
            background: rgba(255, 255, 255, 0.1);
            color: white;
            
            &:hover {
              background: rgba(255, 255, 255, 0.2);
              border-color: rgba(255, 255, 255, 0.5);
              transform: translateY(-1px);
            }
          }
          
          &.ant-btn-link {
            color: rgba(255, 255, 255, 0.9);
            
            &:hover {
              color: white;
              background: rgba(255, 255, 255, 0.1);
              border-radius: 4px;
            }
          }
        }
      }
    }
  }
  
  // 卡片通用样式
  .ant-card {
    border-radius: 12px;
    box-shadow: 0 2px 12px rgba(0, 0, 0, 0.08);
    transition: all 0.3s ease;
    
    &:hover {
      box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
    }
    
    .ant-card-head {
      border-bottom: 1px solid #f0f0f0;
      border-radius: 12px 12px 0 0;
      
      .ant-card-head-title {
        font-size: 18px;
        font-weight: 600;
        color: #262626;
      }
      
      .ant-card-extra {
        .ant-select, .ant-input-number {
          border-radius: 6px;
          
          &:hover {
            border-color: #40a9ff;
          }
        }
      }
    }
    
    .ant-card-body {
      border-radius: 0 0 12px 12px;
    }
  }
  
  // 热门内容卡片样式
  .popular-articles-card, .popular-projects-card {
    border-radius: 16px;
    overflow: hidden;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.06);
    transition: all 0.3s ease;
    
    &:hover {
      box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
      transform: translateY(-4px);
    }
    
    .ant-card-head {
      background: #ffffff;
      border-bottom: 3px solid #f0f0f0;
      padding: 16px 24px;
      
      .card-title-with-icon {
        display: flex;
        align-items: center;
        justify-content: space-between;
        color: #262626;
        gap: 12px;
        
        .title-icon {
          font-size: 20px;
          color: #1890ff;
          filter: drop-shadow(0 2px 4px rgba(24, 144, 255, 0.3));
          animation: pulse 2s infinite;
        }
        
        .title-text {
          font-size: 18px;
          font-weight: 700;
          flex: 1;
          letter-spacing: 0.5px;
          color: #1a1a1a;
        }
        
        .title-badge {
          display: flex;
          flex-direction: column;
          align-items: center;
          gap: 4px;
          
          .badge-text {
            background: linear-gradient(45deg, #ff4757, #ff3838);
            color: white;
            font-size: 11px;
            font-weight: 800;
            padding: 3px 10px;
            border-radius: 12px;
            border: 2px solid rgba(255, 71, 87, 0.2);
            box-shadow: 0 3px 8px rgba(255, 71, 87, 0.4);
            letter-spacing: 0.8px;
            line-height: 1.2;
            text-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);
          }
          
          .badge-count {
            background: linear-gradient(45deg, #1890ff, #40a9ff);
            color: white;
            font-size: 16px;
            font-weight: 800;
            padding: 6px 12px;
            border-radius: 50%;
            border: 3px solid rgba(24, 144, 255, 0.2);
            min-width: 32px;
            min-height: 32px;
            display: flex;
            align-items: center;
            justify-content: center;
            font-family: 'Monaco', 'Menlo', monospace;
            box-shadow: 0 4px 12px rgba(24, 144, 255, 0.4);
            text-shadow: 0 1px 2px rgba(0, 0, 0, 0.2);
          }
        }
      }
      
      .ant-card-extra {
        .ant-btn-link {
          color: #666;
          border: 1px solid #d9d9d9;
          border-radius: 6px;
          padding: 4px 12px;
          
          &:hover {
            color: #1890ff;
            background: #f0f8ff;
            border-color: #40a9ff;
          }
        }
      }
    }
  }
  
  .popular-articles-card .ant-card-head {
    border-bottom: 3px solid #1890ff;
    
    .title-icon {
      color: #ff4757;
    }
    
    .title-badge {
      .badge-count {
        background: linear-gradient(45deg, #1890ff, #40a9ff);
      }
    }
  }
  
  .popular-projects-card .ant-card-head {
    border-bottom: 3px solid #52c41a;
    
    .title-icon {
      color: #fa8c16;
    }
    
    .title-badge {
      .badge-text {
        background: linear-gradient(45deg, #fa8c16, #ff9500);
        border-color: rgba(250, 140, 22, 0.2);
        box-shadow: 0 3px 8px rgba(250, 140, 22, 0.4);
      }
      
      .badge-count {
        background: linear-gradient(45deg, #52c41a, #73d13d);
        border-color: rgba(82, 196, 26, 0.2);
        box-shadow: 0 4px 12px rgba(82, 196, 26, 0.4);
      }
    }
  }
  
  // 热门内容列表
  .popular-content {
    max-height: 400px;
    overflow-y: auto;
    padding: 8px 0;
  }
  
  .popular-item {
    display: flex;
    align-items: center;
    padding: 16px 20px;
    margin-bottom: 12px;
    background: linear-gradient(45deg, #fafbff, #f0f8ff);
    border-radius: 12px;
    border: 1px solid #e6f4ff;
    cursor: pointer;
    transition: all 0.3s cubic-bezier(0.4, 0, 0.2, 1);
    position: relative;
    overflow: hidden;
    
    &::before {
      content: '';
      position: absolute;
      top: 0;
      left: 0;
      width: 4px;
      height: 100%;
      background: linear-gradient(to bottom, #1890ff, #40a9ff);
      transform: scaleY(0);
      transition: transform 0.3s ease;
    }
    
    &:hover {
      background: linear-gradient(45deg, #e6f7ff, #bae7ff);
      border-color: #40a9ff;
      box-shadow: 0 4px 16px rgba(24, 144, 255, 0.15);
      transform: translateX(8px) scale(1.02);
      
      &::before {
        transform: scaleY(1);
      }
      
      .item-action .anticon {
        transform: translateX(4px);
        color: #1890ff;
      }
    }
    
    .item-header {
      display: flex;
      align-items: flex-start;
      flex: 1;
      gap: 16px;
    }
    
    .rank-badge {
      display: flex;
      align-items: center;
      justify-content: center;
      width: 32px;
      height: 32px;
      border-radius: 50%;
      background: linear-gradient(45deg, #ffd53e, #ff7c00);
      color: white;
      font-weight: 700;
      font-size: 14px;
      box-shadow: 0 2px 8px rgba(255, 124, 0, 0.3);
      flex-shrink: 0;
      
      .crown-icon {
        font-size: 16px;
        color: #fff700;
        filter: drop-shadow(0 1px 2px rgba(0, 0, 0, 0.3));
      }
      
      .rank-number {
        font-family: 'Monaco', 'Menlo', monospace;
      }
    }
    
    .item-content {
      flex: 1;
      min-width: 0;
    }
    
    .item-title {
      font-size: 15px;
      font-weight: 600;
      color: #262626;
      margin-bottom: 8px;
      line-height: 1.4;
      display: -webkit-box;
      -webkit-line-clamp: 2;
      -webkit-box-orient: vertical;
      overflow: hidden;
      transition: color 0.3s ease;
    }
    
    .item-meta {
      display: flex;
      align-items: center;
      justify-content: space-between;
      gap: 16px;
    }
    
    .meta-stats {
      display: flex;
      gap: 16px;
      flex-wrap: wrap;
    }
    
    .stat-item {
      display: flex;
      align-items: center;
      gap: 4px;
      font-size: 12px;
      color: #666;
      font-weight: 500;
      padding: 4px 8px;
      background: rgba(255, 255, 255, 0.8);
      border-radius: 12px;
      border: 1px solid #e6f0ff;
      transition: all 0.3s ease;
      
      .anticon {
        font-size: 12px;
      }
      
      &.view-stat {
        .anticon {
          color: #1890ff;
        }
        
        &:hover {
          background: #e6f7ff;
          border-color: #1890ff;
        }
      }
      
      &.like-stat {
        .anticon {
          color: #f5222d;
        }
        
        &:hover {
          background: #fff1f0;
          border-color: #f5222d;
        }
      }
      
      &.comment-stat {
        .anticon {
          color: #52c41a;
        }
        
        &:hover {
          background: #f6ffed;
          border-color: #52c41a;
        }
      }
    }
    
    .meta-date {
      font-size: 12px;
      color: #999;
      white-space: nowrap;
      padding: 4px 8px;
      background: rgba(0, 0, 0, 0.04);
      border-radius: 8px;
    }
    
    .item-action {
      display: flex;
      align-items: center;
      margin-left: 16px;
      flex-shrink: 0;
      
      .anticon {
        font-size: 16px;
        color: #bfbfbf;
        transition: all 0.3s ease;
      }
    }
  }
  
  .popular-project-item {
    &::before {
      background: linear-gradient(to bottom, #52c41a, #73d13d);
    }
    
    &:hover {
      background: linear-gradient(45deg, #f6ffed, #d9f7be);
      border-color: #52c41a;
      box-shadow: 0 4px 16px rgba(82, 196, 26, 0.15);
      
      .item-action .anticon {
        color: #52c41a;
      }
    }
  }
  
  // 空状态样式
  .empty-state {
    display: flex;
    flex-direction: column;
    align-items: center;
    justify-content: center;
    padding: 40px 20px;
    color: #bfbfbf;
    
    .empty-icon {
      font-size: 48px;
      margin-bottom: 16px;
      opacity: 0.6;
    }
    
    .empty-text {
      font-size: 14px;
      margin: 0;
      opacity: 0.8;
    }
  }

  // 热门内容样式优化
  .popular-item-title {
    color: #262626;
    font-weight: 500;
    font-size: 15px;
    line-height: 1.4;
    margin-bottom: 6px;
    transition: color 0.3s ease;
    
    &:hover {
      color: #1890ff;
      cursor: pointer;
    }
  }
  
  .stat-tag {
    display: inline-block;
    margin-right: 12px;
    padding: 4px 8px;
    background: linear-gradient(45deg, #f0f2f5, #e6f7ff);
    border-radius: 4px;
    font-size: 12px;
    color: #666;
    font-weight: 500;
    border: 1px solid #d9d9d9;
    transition: all 0.3s ease;
    
    &:hover {
      background: linear-gradient(45deg, #e6f7ff, #bae7ff);
      color: #1890ff;
      border-color: #40a9ff;
    }
    
    &:last-child {
      margin-right: 0;
    }
  }
  
  // 列表项样式
  .ant-list-item {
    padding: 16px 20px;
    border-radius: 8px;
    margin-bottom: 8px;
    background: #fafafa;
    transition: all 0.3s ease;
    
    &:hover {
      background: #f0f8ff;
      box-shadow: 0 2px 8px rgba(24, 144, 255, 0.1);
      transform: translateX(4px);
    }
  }
  
  // 空状态样式
  .ant-empty {
    margin: 60px 0;
    
    .ant-empty-image {
      height: 80px;
      margin-bottom: 16px;
    }
    
    .ant-empty-description {
      color: #999;
      font-size: 14px;
      margin-bottom: 16px;
    }
  }
  
  // 弹窗样式优化
  .ant-modal {
    .ant-modal-header {
      border-radius: 8px 8px 0 0;
      background: linear-gradient(45deg, #f0f8ff, #e6f7ff);
      
      .ant-modal-title {
        font-size: 18px;
        font-weight: 600;
        color: #1890ff;
      }
    }
    
    .ant-modal-body {
      padding: 24px;
    }
  }
  
  // 标签页样式
  .ant-tabs {
    .ant-tabs-bar {
      border-bottom: 2px solid #f0f0f0;
      
      .ant-tabs-tab {
        padding: 12px 20px;
        font-weight: 500;
        border-radius: 6px 6px 0 0;
        
        &:hover {
          color: #40a9ff;
        }
        
        &.ant-tabs-tab-active {
          background: linear-gradient(45deg, #e6f7ff, #bae7ff);
          color: #1890ff;
          font-weight: 600;
        }
      }
    }
  }
  
  // 加载动画优化
  .ant-spin-container {
    transition: opacity 0.3s ease;
  }
  
  // 按钮组样式
  .ant-btn-group {
    .ant-btn {
      border-radius: 0;
      
      &:first-child {
        border-radius: 6px 0 0 6px;
      }
      
      &:last-child {
        border-radius: 0 6px 6px 0;
      }
    }
  }
  
  // 响应式优化
  @media (max-width: 1200px) {
    .statistics-dashboard .ant-col {
      margin-bottom: 16px;
    }
    
    .popular-content {
      max-height: 300px;
    }
  }
  
  @media (max-width: 768px) {
    .statistics-dashboard {
      padding: 0 8px;
    }
    
    .stat-card {
      height: auto;
      min-height: 140px;
      
      .ant-card-body {
        padding: 16px;
      }
      
      .ant-statistic-content {
        font-size: 24px;
      }
      
      .stat-details .stat-item {
        margin-right: 8px;
        margin-bottom: 8px;
        display: inline-block;
        width: auto;
        font-size: 12px;
      }
      
      .stat-growth .growth-item {
        font-size: 12px;
      }
    }
    
    .popular-item {
      padding: 12px 16px;
      margin-bottom: 8px;
      
      .item-header {
        gap: 12px;
      }
      
      .rank-badge {
        width: 28px;
        height: 28px;
        font-size: 12px;
      }
      
      .item-title {
        font-size: 14px;
      }
      
      .meta-stats {
        gap: 8px;
      }
      
      .stat-item {
        padding: 2px 6px;
        font-size: 11px;
      }
    }
    
    .quick-actions {
      .ant-btn-group {
        .ant-btn {
          font-size: 12px;
          padding: 4px 8px;
          height: auto;
        }
      }
    }
    
    #trendChart, #trendModalChart {
      height: 300px !important;
    }
  }
  
  @media (max-width: 576px) {
    .statistics-dashboard {
      padding: 0 4px;
    }
    
    .ant-card {
      margin-bottom: 12px;
      border-radius: 8px;
    }
    
    .stat-card {
      .ant-card-body {
        padding: 12px;
      }
      
      .ant-statistic-title {
        font-size: 13px;
      }
      
      .ant-statistic-content {
        font-size: 20px;
      }
    }
    
    .popular-item {
      padding: 8px 12px;
      
      .item-header {
        gap: 8px;
        flex-direction: column;
        align-items: flex-start;
      }
      
      .meta-stats {
        flex-wrap: wrap;
        gap: 4px;
      }
      
      .item-action {
        margin-left: 8px;
      }
    }
    
    .ant-modal {
      margin: 0;
      max-width: 100vw;
      
      .ant-modal-body {
        padding: 16px;
      }
    }
    
    #trendChart, #trendModalChart {
      height: 250px !important;
    }
  }
  
  // 小屏设备优化
  @media (max-width: 480px) {
    .popular-item .item-header {
      .rank-badge {
        width: 24px;
        height: 24px;
        font-size: 10px;
      }
      
      .item-title {
        font-size: 13px;
        -webkit-line-clamp: 1;
      }
    }
    
    .stat-details .stat-item {
      display: block;
      margin-bottom: 4px;
      width: fit-content;
    }
  }
}

// 全局动画效果
.statistics-dashboard {
  animation: fadeInUp 0.6s ease-out;
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes pulse {
  0%, 100% {
    transform: scale(1);
    opacity: 1;
  }
  50% {
    transform: scale(1.1);
    opacity: 0.8;
  }
}

// 图表动画
#trendChart, #trendModalChart {
  border-radius: 8px;
  transition: all 0.3s ease;
  
  &:hover {
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  }
}

// 滚动条样式优化
::-webkit-scrollbar {
  width: 6px;
  height: 6px;
}

::-webkit-scrollbar-track {
  background: #f1f1f1;
  border-radius: 3px;
}

::-webkit-scrollbar-thumb {
  background: #c1c1c1;
  border-radius: 3px;
  
  &:hover {
    background: #a8a8a8;
  }
}
</style>