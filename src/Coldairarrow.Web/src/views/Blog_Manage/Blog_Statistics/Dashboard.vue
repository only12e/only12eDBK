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
            <div class="action-buttons">
              <a-button type="primary" @click="refreshData" :loading="overviewLoading" class="action-btn">
                <a-icon type="reload" />
                刷新数据
              </a-button>
              <a-button type="default" @click="exportData" class="action-btn">
                <a-icon type="download" />
                导出数据
              </a-button>
              <a-button type="default" @click="showTrendModal" class="action-btn">
                <a-icon type="line-chart" />
                趋势分析
              </a-button>
            </div>
          </div>
        </a-card>
      </a-col>
    </a-row>

    <a-row :gutter="[16, 16]" style="margin-top: 16px;">
      <a-col :xl="24" :lg="24" :md="24" :sm="24">
        <blog-visit-card 
          :blog-url="blogUrl"
          :today-visits="safeOverview.Access.TodayViews || 0"
          :online-users="safeOverview.Users.ActiveCount || 0"
          :is-horizontal="true"
          @visit-tracked="handleVisitTracked"
          @preview-opened="handlePreviewOpened"
          @quick-visit="handleQuickVisit"
          class="horizontal-blog-card"
        />
      </a-col>
    </a-row>

    <!-- 趋势图表区域 -->
    <a-row :gutter="[16, 16]" style="margin-top: 24px;">
      <a-col :xl="16" :lg="24" :md="24" :sm="24">
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
            <div id="trendChart" style="width: 100%; height: 450px;"></div>
          </div>
          <a-empty v-else description="暂无趋势数据">
            <a-button slot="description" type="primary" @click="getTrendData">刷新数据</a-button>
          </a-empty>
        </a-card>
      </a-col>
      
      <a-col :xl="8" :lg="24" :md="24" :sm="24">
        <a-card title="数据汇总" class="summary-card">
          <div class="summary-stats">
            <div class="summary-item">
              <div class="summary-icon">
                <a-icon type="file-text" style="color: #1890ff;" />
              </div>
              <div class="summary-content">
                <div class="summary-title">内容总数</div>
                <div class="summary-value">
                  {{ (safeOverview.Articles.TotalCount || 0) + (safeOverview.Projects.TotalCount || 0) }}
                </div>
                <div class="summary-desc">文章与项目</div>
              </div>
            </div>
            
            <div class="summary-item">
              <div class="summary-icon">
                <a-icon type="eye" style="color: #52c41a;" />
              </div>
              <div class="summary-content">
                <div class="summary-title">总浏览量</div>
                <div class="summary-value">{{ safeOverview.Access.TotalViews || 0 }}</div>
                <div class="summary-desc">累计访问次数</div>
              </div>
            </div>
            
            <div class="summary-item">
              <div class="summary-icon">
                <a-icon type="user" style="color: #fa8c16;" />
              </div>
              <div class="summary-content">
                <div class="summary-title">活跃用户</div>
                <div class="summary-value">{{ safeOverview.Users.ActiveCount || 0 }}</div>
                <div class="summary-desc">当前在线用户</div>
              </div>
            </div>
            
            <div class="summary-item">
              <div class="summary-icon">
                <a-icon type="message" style="color: #722ed1;" />
              </div>
              <div class="summary-content">
                <div class="summary-title">用户互动</div>
                <div class="summary-value">
                  {{ (safeOverview.Access.TotalLikes || 0) + (safeOverview.Comments.TotalCount || 0) }}
                </div>
                <div class="summary-desc">点赞与评论</div>
              </div>
            </div>
          </div>
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

  </div>
</template>

<script>
import { GetOverview, GetTrendStatistics } from '@/api/blog_statistics'
import BlogVisitCard from '@/components/BlogVisitCard.vue'
import * as echarts from 'echarts'

export default {
  name: 'BlogStatistics',
  components: {
    BlogVisitCard
  },
  data() {
    return {
      blogUrl: 'https://Only12eDBK.com', // 配置你的博客地址
      overview: {},
      overviewLoading: true,
      
      trendData: null,
      trendLoading: false,
      trendQuery: {
        type: 'article',
        period: 'day',
        limit: 30
      },
      
      trendModalVisible: false,
      trendModalData: null,
      trendModalLoading: false,
      trendModalQuery: {
        type: 'article',
        period: 'day',
        limit: 30
      },
      
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
        this.getTrendData()
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
    },
    
    // 博客访问相关事件处理
    handleVisitTracked(data) {
      this.$message.success('博客访问已记录')
      console.log('访问跟踪:', data)
      
      // 可以在这里调用API记录访问统计
      // trackVisit(data)
    },
    
    handlePreviewOpened(data) {
      this.$message.info('预览模式已开启')
      console.log('预览打开:', data)
    },
    
    handleQuickVisit(data) {
      console.log('快速访问:', data)
      
      // 可以在这里更新访问统计
      // updateQuickVisitStats(data)
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
    
    /* 不同卡片的主题色彩和边框 */
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
      height: 180px;
      
      .ant-card-body {
        padding: 24px;
        height: 100%;
        display: flex;
        flex-direction: column;
        justify-content: center;
      }
      
      .quick-actions {
        h4 {
          color: white;
          font-size: 14px;
          font-weight: 600;
          margin-bottom: 12px;
          text-align: center;
        }
        
        .action-buttons {
          display: flex;
          flex-direction: column;
          gap: 8px;
        }
        
        .action-btn {
          width: 100%;
          height: 32px;
          border-radius: 6px;
          font-weight: 500;
          font-size: 11px;
          transition: all 0.3s ease;
          display: flex;
          align-items: center;
          justify-content: center;
          padding: 0 8px;
          
          .anticon {
            margin-right: 4px;
            font-size: 12px;
          }
          
          &:not(.ant-btn-link) {
            border: 2px solid rgba(255, 255, 255, 0.3);
            background: rgba(255, 255, 255, 0.1);
            color: white;
            
            &:hover {
              background: rgba(255, 255, 255, 0.2);
              border-color: rgba(255, 255, 255, 0.5);
              transform: translateY(-1px);
              box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            }
            
            &:active {
              transform: translateY(0);
            }
          }
          
          &.ant-btn-primary {
            background: rgba(255, 255, 255, 0.2);
            border-color: rgba(255, 255, 255, 0.4);
            
            &:hover {
              background: rgba(255, 255, 255, 0.3);
              border-color: rgba(255, 255, 255, 0.6);
            }
          }
        }
      }
    }
  }
  
  /* 卡片通用样式 */
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
  
  /* 数据汇总卡片样式 */
  .summary-card {
    .summary-stats {
      display: flex;
      flex-direction: column;
      gap: 20px;
    }
    
    .summary-item {
      display: flex;
      align-items: center;
      padding: 16px;
      background: linear-gradient(45deg, #fafbff, #f0f8ff);
      border-radius: 12px;
      border: 1px solid #e6f4ff;
      transition: all 0.3s ease;
      
      &:hover {
        background: linear-gradient(45deg, #e6f7ff, #bae7ff);
        border-color: #40a9ff;
        box-shadow: 0 4px 16px rgba(24, 144, 255, 0.1);
        transform: translateY(-2px);
      }
      
      .summary-icon {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 48px;
        height: 48px;
        border-radius: 12px;
        background: rgba(255, 255, 255, 0.8);
        margin-right: 16px;
        flex-shrink: 0;
        
        .anticon {
          font-size: 24px;
        }
      }
      
      .summary-content {
        flex: 1;
        
        .summary-title {
          font-size: 14px;
          color: #666;
          margin-bottom: 4px;
          font-weight: 500;
        }
        
        .summary-value {
          font-size: 24px;
          font-weight: 700;
          color: #262626;
          margin-bottom: 4px;
          font-family: 'Monaco', 'Menlo', monospace;
        }
        
        .summary-desc {
          font-size: 12px;
          color: #999;
          font-weight: 400;
        }
      }
    }
  }
  
  /* 列表项样式 */
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
  
  /* 空状态样式 */
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
  
  /* 弹窗样式优化 */
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
  
  /* 标签页样式 */
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
  
  /* 加载动画优化 */
  .ant-spin-container {
    transition: opacity 0.3s ease;
  }
  
  /* 按钮组样式 */
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
  
  /* 响应式优化 */
  @media (max-width: 1200px) {
    .statistics-dashboard .ant-col {
      margin-bottom: 16px;
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
      
      &.action-card {
        height: auto;
        
        .ant-card-body {
          padding: 16px;
        }
        
        .action-buttons {
          gap: 8px;
        }
        
        .action-btn {
          height: 32px;
          font-size: 11px;
          padding: 0 6px;
          
          .anticon {
            font-size: 12px;
            margin-right: 4px;
          }
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
}

/* 全局动画效果 */
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

/* 图表动画 */
#trendChart, #trendModalChart {
  border-radius: 8px;
  transition: all 0.3s ease;
  
  &:hover {
    box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  }
}

/* 滚动条样式优化 */
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