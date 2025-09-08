<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Blog_Comment.Add')" type="primary" icon="plus" @click="handleAdd()">新建评论</a-button>
      <a-button v-if="hasPerm('Blog_Comment.Delete')" type="primary" icon="minus" @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-dropdown v-if="hasSelected()">
        <a-menu slot="overlay" @click="handleBatchAction">
          <a-menu-item key="approve">批量通过</a-menu-item>
          <a-menu-item key="reject">批量拒绝</a-menu-item>
          <a-menu-item key="pending">设为待审核</a-menu-item>
        </a-menu>
        <a-button style="margin-left: 8px">
          批量操作 <a-icon type="down" />
        </a-button>
      </a-dropdown>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="6" :sm="24">
            <a-form-item label="关键字">
              <a-input v-model="queryParam.keyword" placeholder="评论内容" 
                       @pressEnter="handleSearch" allowClear />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="目标类型">
              <a-select v-model="queryParam.targetType" placeholder="目标类型" allowClear>
                <a-select-option value="article">文章</a-select-option>
                <a-select-option value="project">项目</a-select-option>
                <a-select-option value="tool">工具</a-select-option>
                <a-select-option value="technology">技术栈</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="审核状态">
              <a-select v-model="queryParam.status" placeholder="状态" allowClear>
                <a-select-option value="approved">已通过</a-select-option>
                <a-select-option value="pending">待审核</a-select-option>
                <a-select-option value="rejected">已拒绝</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="评论类型">
              <a-select v-model="queryParam.isTopLevel" placeholder="评论类型" allowClear>
                <a-select-option :value="true">顶级评论</a-select-option>
                <a-select-option :value="false">回复评论</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-button type="primary" @click="handleSearch">查询</a-button>
            <a-button style="margin-left: 8px" @click="handleReset">重置</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <!-- 统计卡片 -->
    <a-row :gutter="16" style="margin-bottom: 16px;">
      <a-col :span="6">
        <a-card size="small">
          <a-statistic title="待审核评论" :value="pendingCount" :value-style="{ color: '#faad14' }" />
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card size="small">
          <a-statistic title="已通过评论" :value="approvedCount" :value-style="{ color: '#52c41a' }" />
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card size="small">
          <a-statistic title="已拒绝评论" :value="rejectedCount" :value-style="{ color: '#ff4d4f' }" />
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card size="small">
          <a-statistic title="总评论数" :value="totalCount" :value-style="{ color: '#1890ff' }" />
        </a-card>
      </a-col>
    </a-row>

    <a-table ref="table" :columns="columns" :rowKey="row => row.Id" :dataSource="data" :pagination="pagination"
      :loading="loading" @change="handleTableChange" :scroll="{ x: 1600, y: 600 }"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="middle">
      <span slot="content" slot-scope="text">
        <div style="max-width: 200px; white-space: pre-wrap; word-break: break-word;">
          {{ text }}
        </div>
      </span>
      <span slot="targetType" slot-scope="text">
        <a-tag :color="getTargetTypeColor(text)">{{ getTargetTypeName(text) }}</a-tag>
      </span>
      <span slot="status" slot-scope="text">
        <a-badge :status="text === 'approved' ? 'success' : text === 'pending' ? 'processing' : 'error'"
          :text="text === 'approved' ? '已通过' : text === 'pending' ? '待审核' : '已拒绝'" />
      </span>
      <span slot="isReply" slot-scope="text, record">
        <a-tag v-if="record.ParentId" color="blue">回复</a-tag>
        <a-tag v-else color="green">顶级</a-tag>
      </span>
      <span slot="likeCount" slot-scope="text">
        <span style="color: #ff4757;">
          <a-icon type="heart" /> {{ text || 0 }}
        </span>
      </span>
      <span slot="user" slot-scope="text, record">
        <div v-if="record.User">
          <div>{{ record.User.Nickname || record.User.Username }}</div>
          <div style="font-size: 12px; color: #999;">{{ record.User.Email }}</div>
        </div>
        <span v-else>-</span>
      </span>
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Blog_Comment.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Blog_Comment.Approve')">
            <a-dropdown>
              <a-menu slot="overlay" @click="({ key }) => handleStatusChange(record, key)">
                <a-menu-item key="approved">通过</a-menu-item>
                <a-menu-item key="pending">待审核</a-menu-item>
                <a-menu-item key="rejected">拒绝</a-menu-item>
              </a-menu>
              <a>
                审核 <a-icon type="down" />
              </a>
            </a-dropdown>
            <a-divider type="vertical" />
          </template>
          <template v-if="record.ParentId == null">
            <a @click="handleViewReplies(record)">查看回复({{ record.ReplyCount || 0 }})</a>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Blog_Comment.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <!-- 评论编辑表单组件 -->
    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
    
    <!-- 回复查看弹窗 -->
    <a-modal title="查看回复" :visible="repliesVisible" :width="800" :footer="null" @cancel="repliesVisible = false">
      <div style="max-height: 500px; overflow-y: auto;">
        <a-list :dataSource="replies" :loading="repliesLoading">
          <a-list-item slot="renderItem" slot-scope="item">
            <a-list-item-meta>
              <div slot="title">
                <span>{{ item.User ? (item.User.Nickname || item.User.Username) : '未知用户' }}</span>
                <a-tag style="margin-left: 8px;" :color="item.Status === 'approved' ? 'green' : item.Status === 'pending' ? 'orange' : 'red'">
                  {{ item.Status === 'approved' ? '已通过' : item.Status === 'pending' ? '待审核' : '已拒绝' }}
                </a-tag>
              </div>
              <div slot="description">{{ item.CreatedAt }}</div>
            </a-list-item-meta>
            <div style="margin-bottom: 8px;">{{ item.Content }}</div>
            <div style="color: #999; font-size: 12px;">
              <a-icon type="heart" style="color: #ff4757;" /> {{ item.LikeCount || 0 }}
            </div>
          </a-list-item>
        </a-list>
      </div>
    </a-modal>
  </a-card>
</template>

<script>
import { GetDataList, DeleteData, BatchApproval, GetCommentCountByStatus, GetRepliesByParentId } from '@/api/blog_comment'
import EditForm from './EditForm'

const columns = [
  {
    title: '评论内容',
    dataIndex: 'Content',
    scopedSlots: { customRender: 'content' },
    width: 250,
    fixed: 'left'
  },
  {
    title: '评论者',
    scopedSlots: { customRender: 'user' },
    width: 150
  },
  {
    title: '目标类型',
    dataIndex: 'TargetType',
    scopedSlots: { customRender: 'targetType' },
    width: 100
  },
  {
    title: '目标ID',
    dataIndex: 'TargetId',
    width: 80
  },
  {
    title: '评论类型',
    scopedSlots: { customRender: 'isReply' },
    width: 100
  },
  {
    title: '状态',
    dataIndex: 'Status',
    scopedSlots: { customRender: 'status' },
    width: 100
  },
  {
    title: '点赞数',
    dataIndex: 'LikeCount',
    scopedSlots: { customRender: 'likeCount' },
    width: 80
  },
  {
    title: 'IP地址',
    dataIndex: 'IpAddress',
    width: 120
  },
  {
    title: '评论时间',
    dataIndex: 'CreatedAt',
    width: 160,
    customRender: (text) => {
      if (!text) return '-'
      // 截取到分钟，格式：2025-09-05 10:30
      const parts = text.split(' ')
      if (parts.length >= 2) {
        const timePart = parts[1].split(':')
        if (timePart.length >= 2) {
          return `${parts[0]} ${timePart[0]}:${timePart[1]}`
        }
      }
      return text
    }
  },
  {
    title: '操作',
    scopedSlots: { customRender: 'action' },
    width: 220,
    fixed: 'right'
  }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.getDataList()
    this.loadStatistics()
  },
  data() {
    return {
      data: [],
      loading: false,
      columns,
      queryParam: {},
      pagination: {
        current: 1,
        pageSize: 10,
        showSizeChanger: true,
        showQuickJumper: true,
        pageSizeOptions: ['10', '20', '50', '100'],
        showTotal: (total, range) => `共 ${total} 条记录，显示第 ${range[0]}-${range[1]} 条`
      },
      selectedRowKeys: [],
      
      // 统计数据
      pendingCount: 0,
      approvedCount: 0,
      rejectedCount: 0,
      totalCount: 0,
      
      // 回复弹窗
      repliesVisible: false,
      repliesLoading: false,
      replies: []
    }
  },
  methods: {
    getDataList() {
      this.loading = true

      GetDataList({
        ...this.queryParam,
        PageIndex: this.pagination.current,
        PageRows: this.pagination.pageSize
      }).then(resJson => {
        this.loading = false
        if (resJson.Success) {
          this.data = resJson.Data
          const pagination = { ...this.pagination }
          pagination.total = resJson.Total
          this.pagination = pagination
          this.totalCount = resJson.Total
        } else {
          this.$message.error(resJson.Msg || '获取数据失败')
        }
      })
    },
    
    loadStatistics() {
      // 加载各状态的评论数量
      GetCommentCountByStatus('pending').then(resJson => {
        if (resJson.Success) this.pendingCount = resJson.Data
      })
      GetCommentCountByStatus('approved').then(resJson => {
        if (resJson.Success) this.approvedCount = resJson.Data
      })
      GetCommentCountByStatus('rejected').then(resJson => {
        if (resJson.Success) this.rejectedCount = resJson.Data
      })
    },
    
    getTargetTypeName(type) {
      const typeMap = {
        'article': '文章',
        'project': '项目',
        'tool': '工具',
        'technology': '技术栈'
      }
      return typeMap[type] || type
    },
    
    getTargetTypeColor(type) {
      const colorMap = {
        'article': 'blue',
        'project': 'green',
        'tool': 'orange',
        'technology': 'purple'
      }
      return colorMap[type] || 'default'
    },
    
    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    handleTableChange(pagination, filters, sorter) {
      const pager = { ...this.pagination }
      pager.current = pagination.current
      pager.pageSize = pagination.pageSize
      this.pagination = pager
      this.getDataList()
    },
    handleAdd() {
      this.$refs.editForm.openForm()
    },
    handleEdit(Id) {
      this.$refs.editForm.openForm(Id)
    },
    handleDelete(ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除',
        content: '是否删除选中数据?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.submitDelete(ids, resolve, reject)
          }).catch(() => console.log('Oops errors!'))
        }
      })
    },
    submitDelete(ids, resolve, reject) {
      DeleteData(ids).then(resJson => {
        resolve()
        if (resJson.Success) {
          this.$message.success('操作成功!')
          this.getDataList()
          this.loadStatistics()
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    },
    handleBatchAction({ key }) {
      const actionMap = {
        approve: '通过',
        reject: '拒绝',
        pending: '设为待审核'
      }

      this.$confirm({
        title: '确认操作',
        content: `是否${actionMap[key]}选中的评论?`,
        onOk: () => {
          return BatchApproval({
            Ids: this.selectedRowKeys,
            Action: key
          }).then(resJson => {
            this.$message.success('操作成功!')
            this.selectedRowKeys = []
            this.getDataList()
            this.loadStatistics()
          })
        }
      })
    },
    handleStatusChange(record, status) {
      BatchApproval({
        Ids: [record.Id],
        Action: status
      }).then(resJson => {
        this.$message.success('操作成功!')
        this.getDataList()
        this.loadStatistics()
      })
    },
    handleViewReplies(record) {
      this.repliesVisible = true
      this.repliesLoading = true
      GetRepliesByParentId(record.Id).then(resJson => {
        this.repliesLoading = false
        if (resJson.Success) {
          this.replies = resJson.Data
        }
      })
    },
    handleSearch() {
      this.pagination.current = 1
      this.getDataList()
    },
    handleReset() {
      this.queryParam = {}
      this.pagination.current = 1
      this.getDataList()
    }
  }
}
</script>