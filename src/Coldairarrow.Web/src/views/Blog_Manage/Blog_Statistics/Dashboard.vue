<template>
  <div class="statistics-dashboard">
    <!-- 总览卡片 -->
    <a-row :gutter="[20, 20]">
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
            <a-statistic title="文章总数" :value="safeOverview.Articles.TotalCount || 0" :precision="0">
              <template #suffix>
                <a-icon type="file-text" style="color: #1890ff;" />
              </template>
            </a-statistic>
            <div class="stat-details">
              <span class="stat-item">已发布: {{ safeOverview.Articles.PublishedCount || 0 }}</span>
              <span class="stat-item">草稿: {{ safeOverview.Articles.DraftCount || 0 }}</span>
            </div>
          </template>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card project-card">
          <a-statistic title="项目总数" :value="safeOverview.Projects.TotalCount || 0" :precision="0">
            <template #suffix>
              <a-icon type="project" style="color: #52c41a;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">进行中: {{ safeOverview.Projects.ActiveCount || 0 }}</span>
            <span class="stat-item">已完成: {{ safeOverview.Projects.CompletedCount || 0 }}</span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card tool-card">
          <a-statistic title="工具总数" :value="safeOverview.Tools.TotalCount || 0" :precision="0">
            <template #suffix>
              <a-icon type="tool" style="color: #fa8c16;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">推荐: {{ safeOverview.Tools.RecommendedCount || 0 }}</span>
            <span class="stat-item">免费: {{ safeOverview.Tools.FreeCount || 0 }}</span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card tech-card">
          <a-statistic title="技术栈总数" :value="safeOverview.Technologies.TotalCount || 0" :precision="0">
            <template #suffix>
              <a-icon type="code" style="color: #eb2f96;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">精选: {{ safeOverview.Technologies.FeaturedCount || 0 }}</span>
            <span class="stat-item">平均熟练度: {{ safeOverview.Technologies.AverageProficiency || 0 }}%</span>
          </div>
        </a-card>
      </a-col>
    </a-row>

    <a-row :gutter="[20, 20]" style="margin-top: 24px;">
      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card comment-card">
          <a-statistic title="评论总数" :value="safeOverview.Comments.TotalCount || 0" :precision="0">
            <template #suffix>
              <a-icon type="message" style="color: #722ed1;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">已通过: {{ safeOverview.Comments.ApprovedCount || 0 }}</span>
            <span class="stat-item">待审核: {{ safeOverview.Comments.PendingCount || 0 }}</span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card user-card">
          <a-statistic title="用户总数" :value="safeOverview.Users.TotalCount || 0" :precision="0">
            <template #suffix>
              <a-icon type="user" style="color: #13c2c2;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">活跃: {{ safeOverview.Users.ActiveCount || 0 }}</span>
            <span class="stat-item">管理员: {{ safeOverview.Users.AdminCount || 0 }}</span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card :loading="overviewLoading" class="stat-card access-card">
          <a-statistic title="总访问量" :value="safeOverview.Access.TotalViews || 0" :precision="0">
            <template #suffix>
              <a-icon type="eye" style="color: #f5222d;" />
            </template>
          </a-statistic>
          <div class="stat-details">
            <span class="stat-item">总点赞: {{ safeOverview.Access.TotalLikes || 0 }}</span>
            <span class="stat-item">今日访问: {{ safeOverview.Access.TodayViews || 0 }}</span>
          </div>
        </a-card>
      </a-col>

      <a-col :xl="6" :lg="8" :md="12" :sm="24">
        <a-card class="stat-card action-card">
          <div class="quick-actions">
            <h4>快速操作</h4>
            <div class="action-buttons">
              <a-button type="default" @click="refreshData" :loading="overviewLoading" class="action-btn">
                <a-icon type="reload" />
                刷新数据
              </a-button>
              <a-button type="default" @click="exportData" class="action-btn">
                <a-icon type="download" />
                导出数据
              </a-button>
              <a-button type="default" @click="autoRefresh ? stopAutoRefresh() : startAutoRefresh()" class="action-btn"
                :class="{ 'auto-refresh-active': autoRefresh }">
                <a-icon :type="autoRefresh ? 'pause' : 'play-circle'" />
                {{ autoRefresh ? '停止自动刷新' : '开启自动刷新' }}
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

    <a-row :gutter="[20, 20]" style="margin-top: 24px;">
      <a-col :xl="24" :lg="24" :md="24" :sm="24">
        <blog-visit-card :blog-url="blogUrl" :today-visits="safeOverview.Access.TodayViews || 0"
          :online-users="safeOverview.Users.ActiveCount || 0" :is-horizontal="true" @visit-tracked="handleVisitTracked"
          @visit-count-updated="handleVisitCountUpdated" @quick-visit="handleQuickVisit" class="horizontal-blog-card" />
      </a-col>
    </a-row>

    <!-- 趋势图表区域 -->
    <a-row :gutter="[20, 20]" style="margin-top: 32px;">
      <a-col :xl="16" :lg="24" :md="24" :sm="24">
        <a-card title="趋势统计" :loading="trendLoading" class="trend-chart-card">
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

          <div v-if="trendData && trendData.DataPoints && trendData.DataPoints.length > 0"
            class="trend-chart-container">
            <div id="trendChart" class="trend-chart"></div>
          </div>
          <a-empty v-else description="暂无趋势数据" class="trend-empty">
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
    <a-modal title="趋势分析详情" :visible="trendModalVisible" @cancel="trendModalVisible = false" :footer="null"
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
            <a-input-number v-model="trendModalQuery.limit" :min="10" :max="365" placeholder="数据点数量"
              style="width: 100%;" @change="getTrendModalData" />
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
      blogUrl: 'http://localhost:5001/blog-website', // 修改为可访问的博客网站地址
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
    this.startAutoRefresh()
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
      this.stopAutoRefresh()

      window.removeEventListener('resize', this.handleResize)
    },

    startAutoRefresh() {
      // 每10秒自动刷新统计数据，提高实时性
      this.refreshInterval = setInterval(() => {
        this.getOverview()
      }, 10000)
      this.autoRefresh = true
    },

    stopAutoRefresh() {
      if (this.refreshInterval) {
        clearInterval(this.refreshInterval)
        this.refreshInterval = null
      }
      this.autoRefresh = false
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
            top: '10px',
            textStyle: {
              fontSize: 16,
              fontWeight: 'normal',
              color: '#262626'
            }
          },
          legend: {
            data: ['数量'],
            left: 'center',
            top: '40px',
            textStyle: {
              color: '#666',
              fontSize: 12
            },
            itemWidth: 16,
            itemHeight: 10
          },
          tooltip: {
            trigger: 'axis',
            axisPointer: {
              type: 'cross'
            },
            backgroundColor: '#ffffff',
            borderColor: '#e2e8f0',
            borderWidth: 1,
            textStyle: {
              color: '#374151',
              fontSize: 13
            },
            boxShadow: '0 4px 12px rgba(0, 0, 0, 0.1)',
            borderRadius: 8
          },
          grid: {
            left: '5%',
            right: '5%',
            top: '90px',
            bottom: '70px',
            containLabel: true
          },
          xAxis: {
            type: 'category',
            boundaryGap: false,
            data: dates,
            axisLabel: {
              rotate: 45,
              color: '#64748b',
              fontSize: 12,
              fontWeight: 500
            },
            axisLine: {
              lineStyle: {
                color: '#e2e8f0'
              }
            }
          },
          yAxis: {
            type: 'value',
            minInterval: 1,
            axisLabel: {
              color: '#64748b',
              fontSize: 12,
              fontWeight: 500
            },
            axisLine: {
              lineStyle: {
                color: '#e2e8f0'
              }
            },
            splitLine: {
              lineStyle: {
                color: '#f1f5f9',
                type: 'dashed'
              }
            }
          },
          series: [{
            name: '数量',
            type: 'line',
            smooth: true,
            data: values,
            areaStyle: {
              opacity: 0.12,
              color: {
                type: 'linear',
                x: 0,
                y: 0,
                x2: 0,
                y2: 1,
                colorStops: [
                  { offset: 0, color: '#3b82f6' },
                  { offset: 1, color: '#dbeafe' }
                ]
              }
            },
            itemStyle: {
              color: '#3b82f6',
              borderColor: '#ffffff',
              borderWidth: 2
            },
            lineStyle: {
              color: '#3b82f6',
              width: 3
            },
            symbol: 'circle',
            symbolSize: 8,
            emphasis: {
              itemStyle: {
                color: '#1d4ed8',
                shadowBlur: 10,
                shadowColor: '#3b82f6'
              }
            }
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
      this.$message.success('数据已刷新')
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
          left: 'center',
          top: '10px',
          textStyle: {
            fontSize: 16,
            fontWeight: 'normal',
            color: '#262626'
          }
        },
        legend: {
          data: ['数量'],
          left: 'center',
          top: '40px',
          textStyle: {
            color: '#666',
            fontSize: 12
          },
          itemWidth: 16,
          itemHeight: 10
        },
        tooltip: {
          trigger: 'axis',
          axisPointer: {
            type: 'cross'
          },
          backgroundColor: '#ffffff',
          borderColor: '#f0f0f0',
          borderWidth: 1,
          formatter: function (params) {
            return `${params[0].name}: ${params[0].value}`
          }
        },
        grid: {
          left: '5%',
          right: '5%',
          top: '80px',
          bottom: '60px',
          containLabel: true
        },
        xAxis: {
          type: 'category',
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
          data: values,
          type: 'line',
          smooth: true,
          areaStyle: {
            opacity: 0.1,
            color: '#e6f7ff'
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
      // console.log('访问跟踪:', data)

      // 根据访问类型显示不同提示
      if (data.visitType === 'internal') {
        // this.$message.success('已跳转到本地博客页面')
      } else if (data.visitType === 'external') {
        // this.$message.success('外部博客网站已在新窗口打开')
      }

      // 可以在这里调用API记录访问统计
      // trackVisit(data)
    },

    handleVisitCountUpdated(data) {
      // console.log('访问计数更新:', data)

      // 更新本地访问统计显示
      if (this.overview && this.overview.Access) {
        this.overview.Access.TodayViews = data.todayVisits
      }

      // 可以调用API同步到服务器
      // updateTodayVisitCount(data)

      // this.$message.success(`今日访问量已更新: ${data.todayVisits}`, 2)
    },

    handleQuickVisit(data) {
      // console.log('快速访问:', data)

      // 更新快速访问统计
      if (data.visitType === 'internal') {
        // this.$message.success(`已通过快捷方式访问${data.link}页面`)
      }

      // 可以在这里更新快速访问统计数据
      // updateQuickVisitStats(data)
    }
  }
}
</script>

<style lang="less" scoped>
.statistics-dashboard {
  position: relative;
  min-height: 100vh;
  background: linear-gradient(135deg, #f8fafc 0%, #e2e8f0 100%);
  padding: 24px;

  .stat-card {
    height: 160px;
    border-radius: 20px;
    background: #ffffff;
    border: 1px solid #e1e5e9;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
    transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);

    &:hover {
      transform: translateY(-4px);
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.12);
      border-color: #1890ff;
    }

    .ant-card-body {
      padding: 28px;
      background: #ffffff;
      border-radius: 20px;
      height: 100%;
      display: flex;
      flex-direction: column;
      justify-content: center;
    }

    .ant-statistic-title {
      font-size: 16px;
      color: #475569;
      font-weight: 600;
      margin-bottom: 16px;
      letter-spacing: 0.025em;
    }

    .ant-statistic-content {
      font-size: 36px;
      font-weight: 700;
      line-height: 1.1;
      font-family: 'Inter', 'SF Pro Display', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
      color: #1e293b;
    }

    .stat-details {
      margin-top: 20px;
      font-size: 13px;
      line-height: 1.5;

      .stat-item {
        display: inline-block;
        margin-right: 16px;
        margin-bottom: 8px;
        color: #64748b;
        padding: 8px 16px;
        background: #f8fafc;
        border-radius: 12px;
        font-weight: 500;
        border: 1px solid #e2e8f0;
        box-shadow: 0 1px 3px rgba(0, 0, 0, 0.04);
        font-size: 13px;
        transition: all 0.2s ease;

        &:hover {
          background: #f1f5f9;
          border-color: #cbd5e1;
          transform: translateY(-1px);
        }
      }
    }

    .stat-growth {
      margin-top: 16px;
      font-size: 13px;
      display: flex;
      flex-wrap: wrap;

      .growth-item {
        color: #16a34a;
        font-weight: 600;
        display: flex;
        align-items: center;
        padding: 8px 12px;
        background: #f0fdf4;
        border-radius: 10px;
        border: 1px solid #bbf7d0;
        font-size: 12px;
        transition: all 0.2s ease;
        white-space: nowrap;
        overflow: hidden;
        text-overflow: ellipsis;
        max-width: 100%;
        box-sizing: border-box;

        &:hover {
          background: #dcfce7;
          transform: translateY(-1px);
        }

        .anticon {
          margin-right: 4px;
          font-size: 12px;
          flex-shrink: 0;
        }
      }
    }

    &.action-card {
      height: 200px;

      .ant-card-body {
        padding: 16px;
        height: 100%;
        display: flex;
        flex-direction: column;
      }

      .quick-actions {
        height: 100%;
        display: flex;
        flex-direction: column;

        h4 {
          color: rgba(0, 0, 0, 0.75);
          font-size: 15px;
          font-weight: 600;
          margin-bottom: 8px;
          text-align: left;
          flex-shrink: 0;
        }

        .action-buttons {
          display: flex;
          flex-direction: column;
          gap: 6px;
          flex: 1;
          justify-content: space-between;
        }

        .action-btn {
          width: 100%;
          height: 30px;
          border-radius: 8px;
          font-weight: 500;
          font-size: 11px;
          transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);
          display: flex;
          align-items: center;
          justify-content: center;
          white-space: nowrap;
          overflow: hidden;
          text-overflow: ellipsis;
          padding: 0 6px;
          box-sizing: border-box;
          flex-shrink: 0;

          .anticon {
            margin-right: 3px;
            font-size: 11px;
            flex-shrink: 0;
          }

          &.ant-btn-default {
            border: 1px solid #d9d9d9;
            background: #ffffff;
            color: rgba(0, 0, 0, 0.75);

            &:hover {
              background: #f0f8ff;
              border-color: #40a9ff;
              color: #40a9ff;
              transform: translateY(-2px);
              box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
            }
          }

          &.ant-btn-primary {
            background: #1890ff;
            border: 1px solid #1890ff;
            color: white;

            &:hover {
              background: #40a9ff;
              border-color: #40a9ff;
              color: white;
              transform: translateY(-2px);
              box-shadow: 0 4px 12px rgba(24, 144, 255, 0.4);
            }
          }

          &.auto-refresh-active {
            background: #52c41a;
            border: 1px solid #52c41a;
            color: white;

            &:hover {
              background: #73d13d;
              border-color: #73d13d;
              color: white;
              transform: translateY(-2px);
              box-shadow: 0 4px 12px rgba(82, 196, 26, 0.4);
            }
          }

          &:active {
            transform: translateY(0);
          }
        }
      }
    }
  }

  /* 水平博客卡片样式 */
  .horizontal-blog-card {
    border-radius: 20px;
    background: #ffffff;
    border: 1px solid #e1e5e9;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
    transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);

    &:hover {
      transform: translateY(-2px);
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.12);
    }
  }

  /* 卡片通用样式 */
  .ant-card {
    border-radius: 20px;
    background: #ffffff;
    border: 1px solid #e1e5e9;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
    transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);

    &:hover {
      transform: translateY(-2px);
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.12);
    }

    .ant-card-head {
      border-bottom: 1px solid #e2e8f0;
      border-radius: 20px 20px 0 0;
      background: #f8fafc;
      padding: 20px 24px;

      .ant-card-head-title {
        font-size: 18px;
        font-weight: 700;
        color: #1e293b;
        letter-spacing: 0.025em;
      }

      .ant-card-extra {

        .ant-select,
        .ant-input-number {
          border-radius: 8px;
          background: #ffffff;
          border: 1px solid #d9d9d9;

          &:hover {
            border-color: #40a9ff;
            background: #ffffff;
          }
        }
      }
    }

    .ant-card-body {
      border-radius: 0 0 20px 20px;
      background: #ffffff;
      padding: 24px;
    }
  }

  /* 趋势图表卡片样式 */
  .trend-chart-card {
    height: auto;
    min-height: 650px;
    border-radius: 20px;
    background: #ffffff;
    border: 1px solid #e1e5e9;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
    transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);

    &:hover {
      transform: translateY(-2px);
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.12);
    }

    .ant-card-body {
      padding: 24px;
      height: calc(100% - 64px);
      display: flex;
      flex-direction: column;
      background: #ffffff;
    }

    .trend-chart-container {
      flex: 1;
      display: flex;
      flex-direction: column;
      min-height: 0;
    }

    .trend-chart {
      width: 100%;
      flex: 1;
      min-height: 400px;
      max-height: 500px;
      padding: 10px;
    }

    .trend-empty {
      flex: 1;
      display: flex;
      flex-direction: column;
      justify-content: center;
      align-items: center;
      min-height: 400px;
    }

    /* 响应式高度调整 */
    @media (max-width: 1200px) {
      min-height: 550px;

      .trend-chart {
        min-height: 300px;
        max-height: 400px;
        padding: 8px;
      }

      .trend-empty {
        min-height: 300px;
      }
    }

    @media (max-width: 768px) {
      min-height: 450px;

      .trend-chart {
        min-height: 250px;
        max-height: 300px;
        padding: 5px;
      }

      .trend-empty {
        min-height: 250px;
      }
    }
  }

  /* 数据汇总卡片样式 */
  .summary-card {
    height: auto;
    min-height: 650px;
    border-radius: 20px;
    background: #ffffff;
    border: 1px solid #e1e5e9;
    box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
    transition: all 0.3s cubic-bezier(0.25, 0.46, 0.45, 0.94);

    &:hover {
      transform: translateY(-2px);
      box-shadow: 0 8px 32px rgba(0, 0, 0, 0.12);
    }

    .ant-card-body {
      padding: 24px;
      height: calc(100% - 64px);
      display: flex;
      flex-direction: column;
      background: #ffffff;
    }

    .summary-stats {
      display: flex;
      flex-direction: column;
      gap: 20px;
      flex: 1;
      justify-content: space-around;
    }

    /* 响应式高度调整 */
    @media (max-width: 1200px) {
      min-height: 550px;
    }

    @media (max-width: 768px) {
      min-height: 450px;
    }

    .summary-item {
      display: flex;
      align-items: center;
      padding: 24px;
      background: #f8fafc;
      border-radius: 16px;
      border: 1px solid #e2e8f0;
      box-shadow: 0 2px 8px rgba(0, 0, 0, 0.06);
      transition: all 0.3s ease;

      &:hover {
        background: #f1f5f9;
        border-color: #cbd5e1;
        transform: translateY(-2px);
        box-shadow: 0 4px 16px rgba(0, 0, 0, 0.1);
      }

      .summary-icon {
        display: flex;
        align-items: center;
        justify-content: center;
        width: 64px;
        height: 64px;
        border-radius: 18px;
        background: #ffffff;
        border: 1px solid #e2e8f0;
        margin-right: 24px;
        flex-shrink: 0;
        box-shadow: 0 4px 12px rgba(0, 0, 0, 0.08);
        transition: all 0.3s ease;

        .anticon {
          font-size: 32px;
          transition: transform 0.3s ease;
        }
      }

      .summary-content {
        flex: 1;

        .summary-title {
          font-size: 16px;
          color: #475569;
          margin-bottom: 8px;
          font-weight: 600;
          letter-spacing: 0.025em;
        }

        .summary-value {
          font-size: 32px;
          font-weight: 700;
          color: #1e293b;
          margin-bottom: 6px;
          font-family: 'Inter', 'SF Pro Display', -apple-system, BlinkMacSystemFont, 'Segoe UI', sans-serif;
        }

        .summary-desc {
          font-size: 14px;
          color: #64748b;
          font-weight: 500;
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
      background: #fafafa;

      .ant-modal-title {
        font-size: 18px;
        font-weight: 600;
        color: #1890ff;
      }
    }

    .ant-modal-body {
      padding: 24px;
      background: #ffffff;
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

        &.ant-tabs-tab-active {
          background: #e6f7ff;
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
    .statistics-dashboard {
      padding: 20px;

      .ant-col {
        margin-bottom: 20px;
      }
    }

    .trend-chart-card,
    .summary-card {
      min-height: 580px;
    }

    .stat-card {
      height: 180px;

      .ant-card-body {
        padding: 20px;
      }

      .ant-statistic-content {
        font-size: 28px;
      }

      .stat-growth .growth-item {
        font-size: 11px;
        padding: 6px 8px;
      }
    }
  }

  @media (max-width: 768px) {
    .statistics-dashboard {
      padding: 16px;
    }

    .trend-chart-card,
    .summary-card {
      min-height: 480px;
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
        padding: 6px 12px;
        font-size: 12px;
      }

      .stat-growth .growth-item {
        font-size: 11px;
        padding: 4px 6px;

        .anticon {
          margin-right: 2px;
          font-size: 10px;
        }
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
          padding: 0 8px;
        }
      }
    }

    #trendChart,
    #trendModalChart {
      height: 320px !important;
    }

    .trend-chart {
      min-height: 260px !important;
      max-height: 320px !important;
    }

    .summary-item {
      padding: 16px;

      .summary-icon {
        width: 48px;
        height: 48px;
        margin-right: 16px;

        .anticon {
          font-size: 24px;
        }
      }

      .summary-content {
        .summary-title {
          font-size: 14px;
        }

        .summary-value {
          font-size: 24px;
        }

        .summary-desc {
          font-size: 12px;
        }
      }
    }
  }

  @media (max-width: 576px) {
    .statistics-dashboard {
      padding: 12px;
    }

    .ant-card {
      margin-bottom: 16px;
      border-radius: 16px;
    }

    .trend-chart-card,
    .summary-card {
      min-height: 420px;
    }

    .stat-card {
      .ant-card-body {
        padding: 12px;
      }

      .ant-statistic-title {
        font-size: 14px;
      }

      .ant-statistic-content {
        font-size: 20px;
      }

      .stat-details .stat-item {
        padding: 4px 8px;
        font-size: 11px;
        margin-right: 6px;
      }

      .stat-growth .growth-item {
        font-size: 10px;
        padding: 3px 6px;

        .anticon {
          margin-right: 2px;
          font-size: 9px;
        }
      }
    }

    .ant-modal {
      margin: 0;
      max-width: 100vw;

      .ant-modal-body {
        padding: 16px;
      }
    }

    #trendChart,
    #trendModalChart {
      height: 280px !important;
    }

    .trend-chart {
      min-height: 220px !important;
      max-height: 280px !important;
    }

    .summary-item {
      padding: 12px;
      flex-direction: column;
      text-align: center;

      .summary-icon {
        margin-right: 0;
        margin-bottom: 12px;
        width: 40px;
        height: 40px;

        .anticon {
          font-size: 20px;
        }
      }

      .summary-content {
        .summary-title {
          font-size: 13px;
        }

        .summary-value {
          font-size: 20px;
        }

        .summary-desc {
          font-size: 11px;
        }
      }
    }
  }
}

/* 全局动画效果 */
.statistics-dashboard {
  animation: fadeInUp 0.6s cubic-bezier(0.25, 0.46, 0.45, 0.94);
}

.stat-card,
.ant-card {
  animation: slideInUp 0.8s cubic-bezier(0.25, 0.46, 0.45, 0.94);
  animation-fill-mode: both;
}

.stat-card:nth-child(1) {
  animation-delay: 0.1s;
}

.stat-card:nth-child(2) {
  animation-delay: 0.2s;
}

.stat-card:nth-child(3) {
  animation-delay: 0.3s;
}

.stat-card:nth-child(4) {
  animation-delay: 0.4s;
}

@keyframes slideInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(40px);
  }

  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes pulse {

  0%,
  100% {
    transform: scale(1);
    opacity: 1;
  }

  50% {
    transform: scale(1.05);
    opacity: 0.9;
  }
}

@keyframes shimmer {
  0% {
    background-position: -200px 0;
  }

  100% {
    background-position: calc(200px + 100%) 0;
  }
}

@keyframes float {

  0%,
  100% {
    transform: translateY(0) scale(1);
  }

  50% {
    transform: translateY(-10px) scale(1.02);
  }
}

/* 图表动画 */
#trendChart,
#trendModalChart {
  border-radius: 20px;
  transition: all 0.4s cubic-bezier(0.25, 0.46, 0.45, 0.94);
  background: #ffffff;
  border: 1px solid #e2e8f0;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.06);

  &:hover {
    box-shadow: 0 8px 24px rgba(0, 0, 0, 0.12);
    transform: scale(1.01);
  }
}

/* 滚动条样式优化 */
::-webkit-scrollbar {
  width: 6px;
  height: 6px;
}

::-webkit-scrollbar-track {
  background: #f8fafc;
  border-radius: 8px;
}

::-webkit-scrollbar-thumb {
  background: #cbd5e1;
  border-radius: 8px;
  border: 1px solid #f1f5f9;

  &:hover {
    background: #94a3b8;
  }
}
</style>