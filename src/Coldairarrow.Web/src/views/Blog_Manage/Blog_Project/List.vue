<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Blog_Project.Add')" type="primary" icon="plus" @click="handleAdd()">新建项目</a-button>
      <a-button v-if="hasPerm('Blog_Project.Delete')" type="primary" icon="minus" @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-dropdown v-if="hasSelected()">
        <a-menu slot="overlay" @click="handleBatchAction">
          <a-menu-item key="active">批量设为进行中</a-menu-item>
          <a-menu-item key="completed">批量设为已完成</a-menu-item>
          <a-menu-item key="archived">批量归档</a-menu-item>
        </a-menu>
        <a-button style="margin-left: 8px">
          批量操作 <a-icon type="down" />
        </a-button>
      </a-dropdown>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="8" :sm="24">
            <a-form-item label="关键字">
              <a-input v-model="queryParam.keyword" placeholder="项目名称、描述、内容" 
                       @pressEnter="handleSearch" allowClear />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="状态">
              <a-select v-model="queryParam.status" placeholder="状态" allowClear>
                <a-select-option value="active">进行中</a-select-option>
                <a-select-option value="completed">已完成</a-select-option>
                <a-select-option value="archived">已归档</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="精选">
              <a-select v-model="queryParam.isFeatured" placeholder="精选" allowClear>
                <a-select-option :value="1">是</a-select-option>
                <a-select-option :value="0">否</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="难度">
              <a-select v-model="queryParam.difficultyLevel" placeholder="难度" allowClear>
                <a-select-option value="beginner">初级</a-select-option>
                <a-select-option value="intermediate">中级</a-select-option>
                <a-select-option value="advanced">高级</a-select-option>
                <a-select-option value="expert">专家</a-select-option>
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

    <a-table ref="table" :columns="columns" :rowKey="row => row.Id" :dataSource="data" :pagination="pagination"
      :loading="loading" @change="handleTableChange" :scroll="{ x: 1200, y: 600 }"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="middle">
      <span slot="status" slot-scope="text">
        <a-badge :status="text === 'active' ? 'processing' : text === 'completed' ? 'success' : 'default'"
          :text="text === 'active' ? '进行中' : text === 'completed' ? '已完成' : '已归档'" />
      </span>
      <span slot="featured" slot-scope="text">
        <a-tag v-if="text" color="orange">精选</a-tag>
        <span v-else>-</span>
      </span>
      <span slot="difficulty" slot-scope="text">
        <a-tag :color="getDifficultyColor(text)">{{ getDifficultyText(text) }}</a-tag>
      </span>
      <span slot="stats" slot-scope="text, record">
        <div style="display: flex; flex-direction: column; gap: 2px;">
          <span style="font-size: 12px;"><a-icon type="eye" /> {{ formatNumber(record.ViewCount || 0) }}</span>
          <span style="font-size: 12px;"><a-icon type="heart" /> {{ formatNumber(record.LikeCount || 0) }}</span>
          <span style="font-size: 12px;"><a-icon type="message" /> {{ formatNumber(record.CommentCount || 0) }}</span>
        </div>
      </span>
      <span slot="links" slot-scope="text, record">
        <div style="line-height: 1.2;">
          <div v-if="record.DemoUrl">
            <a :href="record.DemoUrl" target="_blank" style="font-size: 12px;">演示</a>
          </div>
          <div v-if="record.GithubUrl">
            <a :href="record.GithubUrl" target="_blank" style="font-size: 12px;">GitHub</a>
          </div>
          <div v-if="record.WebsiteUrl">
            <a :href="record.WebsiteUrl" target="_blank" style="font-size: 12px;">网站</a>
          </div>
        </div>
      </span>
      <span slot="cover" slot-scope="text">
        <img v-if="text" :src="text" style="width: 40px; height: 30px; object-fit: cover; border-radius: 4px;" />
        <span v-else>-</span>
      </span>
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Blog_Project.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Blog_Project.Status')">
            <a-dropdown>
              <a-menu slot="overlay" @click="({ key }) => handleStatusChange(record, key)">
                <a-menu-item key="active">进行中</a-menu-item>
                <a-menu-item key="completed">已完成</a-menu-item>
                <a-menu-item key="archived">已归档</a-menu-item>
              </a-menu>
              <a>
                状态 <a-icon type="down" />
              </a>
            </a-dropdown>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Blog_Project.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <!-- 项目编辑表单组件 -->
    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
  </a-card>
</template>

<script>
import { GetDataList, DeleteData } from '@/api/blog_project'
import EditForm from './EditForm'

const columns = [
  {
    title: '封面',
    dataIndex: 'CoverImage',
    scopedSlots: { customRender: 'cover' },
    width: 80,
    fixed: 'left'
  },
  {
    title: '项目名称',
    dataIndex: 'Name',
    width: 200,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: '描述',
    dataIndex: 'Description',
    width: 200,
    ellipsis: true
  },
  {
    title: '状态',
    dataIndex: 'Status',
    scopedSlots: { customRender: 'status' },
    width: 100
  },
  {
    title: '精选',
    dataIndex: 'IsFeatured',
    scopedSlots: { customRender: 'featured' },
    width: 70
  },
  {
    title: '难度',
    dataIndex: 'DifficultyLevel',
    scopedSlots: { customRender: 'difficulty' },
    width: 80
  },
  {
    title: '统计',
    scopedSlots: { customRender: 'stats' },
    width: 90
  },
  {
    title: '链接',
    scopedSlots: { customRender: 'links' },
    width: 80
  },
  {
    title: '排序',
    dataIndex: 'SortOrder',
    width: 70
  },
  {
    title: '创建时间',
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
    width: 150,
    fixed: 'right'
  }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.getDataList()
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
      selectedRowKeys: []
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
        } else {
          this.$message.error(resJson.Msg || '获取数据失败')
        }
      })
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
        content: '是否删除选中项目?',
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
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    },
    handleBatchAction({ key }) {
      const statusMap = {
        active: '设为进行中',
        completed: '设为已完成',
        archived: '归档'
      }

      this.$confirm({
        title: '确认操作',
        content: `是否${statusMap[key]}选中的 ${this.selectedRowKeys.length} 个项目?`,
        onOk: () => {
          this.loading = true
          const promises = this.selectedRowKeys.map(id => {
            return this.handleStatusChange({ Id: id }, key, false)
          })
          Promise.all(promises).then(() => {
            this.loading = false
            this.$message.success(`已成功${statusMap[key]} ${this.selectedRowKeys.length} 个项目!`)
            this.selectedRowKeys = []
            this.getDataList()
          }).catch(() => {
            this.loading = false
            this.$message.error('批量操作失败，请重试')
          })
        }
      })
    },
    handleStatusChange(record, status, showMessage = true) {
      // 通过编辑接口更新状态
      return this.$refs.editForm.updateStatus(record.Id, status).then(() => {
        if (showMessage) {
          this.$message.success('操作成功!')
          this.getDataList()
        }
      })
    },
    getDifficultyColor(level) {
      const colorMap = {
        'beginner': 'green',
        'intermediate': 'blue',
        'advanced': 'orange',
        'expert': 'red'
      }
      return colorMap[level] || 'default'
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
    handleSearch() {
      this.pagination.current = 1
      this.getDataList()
    },
    handleReset() {
      this.queryParam = {}
      this.pagination.current = 1
      this.getDataList()
    },
    formatNumber(num) {
      if (num >= 10000) {
        return (num / 10000).toFixed(1) + 'w'
      } else if (num >= 1000) {
        return (num / 1000).toFixed(1) + 'k'
      }
      return num.toString()
    }
  }
}
</script>