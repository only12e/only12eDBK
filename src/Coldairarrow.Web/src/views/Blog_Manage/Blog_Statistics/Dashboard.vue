<template>
  <div class="statistics-dashboard">
    <!-- 总览卡片 -->
    <a-row :gutter="[16, 16]">
      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card article-card">
          <a-statistic
            title="文章总数"
            :value="(overview.articles && overview.articles.totalCount) || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="file-text" style="color: #1890ff;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">已发布: {{ (overview.articles && overview.articles.publishedCount) || 0 }}</span>
            <span class="stat-item">草稿: {{ (overview.articles && overview.articles.draftCount) || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ (overview.articles && overview.articles.todayCount) || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card project-card">
          <a-statistic
            title="项目总数"
            :value="(overview.projects && overview.projects.totalCount) || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="project" style="color: #52c41a;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">进行中: {{ (overview.projects && overview.projects.activeCount) || 0 }}</span>
            <span class="stat-item">已完成: {{ (overview.projects && overview.projects.completedCount) || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ (overview.projects && overview.projects.todayCount) || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card tool-card">
          <a-statistic
            title="工具总数"
            :value="(overview.tools && overview.tools.totalCount) || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="tool" style="color: #fa8c16;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">推荐: {{ (overview.tools && overview.tools.recommendedCount) || 0 }}</span>
            <span class="stat-item">免费: {{ (overview.tools && overview.tools.freeCount) || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ (overview.tools && overview.tools.todayCount) || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card tech-card">
          <a-statistic
            title="技术栈总数"
            :value="(overview.technologies && overview.technologies.totalCount) || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="code" style="color: #eb2f96;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">精选: {{ (overview.technologies && overview.technologies.featuredCount) || 0 }}</span>
            <span class="stat-item">平均熟练度: {{ (overview.technologies && overview.technologies.averageProficiency) || 0 }}%</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ (overview.technologies && overview.technologies.todayCount) || 0 }}
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
            :value="(overview.comments && overview.comments.totalCount) || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="message" style="color: #722ed1;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">已通过: {{ (overview.comments && overview.comments.approvedCount) || 0 }}</span>
            <span class="stat-item">待审核: {{ (overview.comments && overview.comments.pendingCount) || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ (overview.comments && overview.comments.todayCount) || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card user-card">
          <a-statistic
            title="用户总数"
            :value="(overview.users && overview.users.totalCount) || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="user" style="color: #13c2c2;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">活跃: {{ (overview.users && overview.users.activeCount) || 0 }}</span>
            <span class="stat-item">管理员: {{ (overview.users && overview.users.adminCount) || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="arrow-up" />
              今日新增: {{ (overview.users && overview.users.todayCount) || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card access-card">
          <a-statistic
            title="总访问量"
            :value="(overview.access && overview.access.totalViews) || 0"
            :precision="0">
            <template #suffix>
              <a-icon type="eye" style="color: #f5222d;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">总点赞: {{ (overview.access && overview.access.totalLikes) || 0 }}</span>
            <span class="stat-item">今日访问: {{ (overview.access && overview.access.todayViews) || 0 }}</span>
          </div>
          <div class="stat-growth">
            <span class="growth-item">
              <a-icon type="heart" />
              今日点赞: {{ (overview.access && overview.access.todayLikes) || 0 }}
            </span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card class="stat-card action-card">
          <div class="quick-actions">
            <h4>快速操作</h4>
            <a-button-group style="width: 100%;">
              <a-button type="primary" @click="refreshData" :loading="overviewLoading">
                <a-icon type="reload" />
                刷新数据
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
          
          <div id="trendChart" style="width: 100%; height: 400px;"></div>
        </a-card>
      </a-col>
    </a-row>

    <!-- 热门内容 -->
    <a-row :gutter="[16, 16]" style="margin-top: 24px;" v-if="popularContent">
      <a-col :xl="12" :lg="24" :md="24" :sm="24">
        <a-card title="热门文章" :loading="popularLoading">
          <a-list
            item-layout="horizontal"
            :data-source="popularContent.popularArticles || []">
            <a-list-item slot="renderItem" slot-scope="item">
              <a-list-item-meta>
                <div slot="title" class="popular-item-title">{{ item.title }}</div>
                <div slot="description">
                  <span class="stat-tag">浏览: {{ item.viewCount }}</span>
                  <span class="stat-tag">点赞: {{ item.likeCount }}</span>
                  <span class="stat-tag">评论: {{ item.commentCount }}</span>
                  <span class="stat-tag">{{ formatDate(item.createdAt) }}</span>
                </div>
              </a-list-item-meta>
            </a-list-item>
          </a-list>
        </a-card>
      </a-col>

      <a-col :xl="12" :lg="24" :md="24" :sm="24">
        <a-card title="热门项目" :loading="popularLoading">
          <a-list
            item-layout="horizontal"
            :data-source="popularContent.popularProjects || []">
            <a-list-item slot="renderItem" slot-scope="item">
              <a-list-item-meta>
                <div slot="title" class="popular-item-title">{{ item.title }}</div>
                <div slot="description">
                  <span class="stat-tag">浏览: {{ item.viewCount }}</span>
                  <span class="stat-tag">点赞: {{ item.likeCount }}</span>
                  <span class="stat-tag">评论: {{ item.commentCount }}</span>
                  <span class="stat-tag">{{ formatDate(item.createdAt) }}</span>
                </div>
              </a-list-item-meta>
            </a-list-item>
          </a-list>
        </a-card>
      </a-col>
    </a-row>

    <!-- 趋势详情模态框 -->
    <a-modal
      title="趋势分析详情"
      :visible="trendModalVisible"
      @cancel="trendModalVisible = false"
      :footer="null"
      :width="800">
      <div>详细的趋势分析图表...</div>
    </a-modal>

    <!-- 热门内容详情模态框 -->
    <a-modal
      title="热门内容详情"
      :visible="popularModalVisible"
      @cancel="popularModalVisible = false"
      :footer="null"
      :width="800">
      <div>详细的热门内容列表...</div>
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
      popularModalVisible: false,
      
      chart: null
    }
  },
  mounted() {
    this.loadData()
  },
  beforeDestroy() {
    if (this.chart) {
      this.chart.dispose()
    }
  },
  methods: {
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
      const chartDom = document.getElementById('trendChart')
      if (!chartDom || !this.trendData) return
      
      if (this.chart) {
        this.chart.dispose()
      }
      
      this.chart = echarts.init(chartDom)
      
      const option = {
        tooltip: {
          trigger: 'axis',
          axisPointer: {
            type: 'cross'
          }
        },
        legend: {
          data: ['数量']
        },
        xAxis: {
          type: 'category',
          boundaryGap: false,
          data: (this.trendData.dataPoints && this.trendData.dataPoints.map(item => item.label)) || []
        },
        yAxis: {
          type: 'value'
        },
        series: [{
          name: '数量',
          type: 'line',
          smooth: true,
          data: (this.trendData.dataPoints && this.trendData.dataPoints.map(item => item.value)) || [],
          areaStyle: {
            opacity: 0.3
          }
        }]
      }
      
      this.chart.setOption(option)
    },
    
    refreshData() {
      this.loadData()
    },
    
    showTrendModal() {
      this.trendModalVisible = true
    },
    
    showPopularModal() {
      this.popularModalVisible = true
    },
    
    formatDate(dateString) {
      if (!dateString) return '-'
      const date = new Date(dateString)
      return date.toLocaleDateString('zh-CN')
    }
  }
}
</script>

<style lang="less" scoped>
.statistics-dashboard {
  .stat-card {
    height: 160px;
    
    .ant-card-body {
      padding: 20px;
    }
    
    .ant-statistic-title {
      font-size: 14px;
      color: #666;
    }
    
    .ant-statistic-content {
      font-size: 24px;
      font-weight: bold;
    }
    
    .stat-details {
      margin-top: 12px;
      font-size: 12px;
      
      .stat-item {
        display: inline-block;
        margin-right: 12px;
        color: #999;
      }
    }
    
    .stat-growth {
      margin-top: 8px;
      font-size: 12px;
      
      .growth-item {
        color: #52c41a;
        
        .anticon {
          margin-right: 4px;
        }
      }
    }
    
    &.article-card {
      border-left: 4px solid #1890ff;
    }
    
    &.project-card {
      border-left: 4px solid #52c41a;
    }
    
    &.tool-card {
      border-left: 4px solid #fa8c16;
    }
    
    &.tech-card {
      border-left: 4px solid #eb2f96;
    }
    
    &.comment-card {
      border-left: 4px solid #722ed1;
    }
    
    &.user-card {
      border-left: 4px solid #13c2c2;
    }
    
    &.access-card {
      border-left: 4px solid #f5222d;
    }
    
    &.action-card {
      border-left: 4px solid #faad14;
      
      .quick-actions {
        h4 {
          margin-bottom: 16px;
          color: #666;
        }
      }
    }
  }
  
  .popular-item-title {
    font-size: 14px;
    font-weight: 500;
    color: #333;
    margin-bottom: 4px;
  }
  
  .stat-tag {
    display: inline-block;
    padding: 2px 6px;
    background: #f0f2f5;
    border-radius: 2px;
    font-size: 11px;
    color: #666;
    margin-right: 6px;
  }
}
</style>