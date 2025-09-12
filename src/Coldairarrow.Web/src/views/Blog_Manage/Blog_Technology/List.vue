<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Blog_Technology.Add')" type="primary" icon="plus" @click="handleAdd()">新建技术栈</a-button>
      <a-button v-if="hasPerm('Blog_Technology.Delete')" type="primary" icon="minus"
        @click="handleDelete(selectedRowKeys)" :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-dropdown v-if="hasSelected()">
        <a-menu slot="overlay" @click="handleBatchAction">
          <a-menu-item key="featured">批量设为精选</a-menu-item>
          <a-menu-item key="unfeatured">取消精选</a-menu-item>
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
              <a-input v-model="queryParam.keyword" placeholder="技术名称、描述" @pressEnter="handleSearch" allowClear />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="分类">
              <a-select v-model="queryParam.category" placeholder="分类" allowClear>
                <a-select-option v-for="item in categories" :key="item" :value="item">
                  {{ getCategoryName(item) }}
                </a-select-option>
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
            <a-form-item label="最低熟练度">
              <a-input-number v-model="queryParam.minProficiencyLevel" placeholder="0-100" :min="0" :max="100"
                style="width: 100%" />
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
      :loading="loading" @change="handleTableChange" :scroll="{ x: 1400, y: 600 }"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="middle">
      <span slot="category" slot-scope="text">
        <a-tag :color="getCategoryColor(text)">{{ getCategoryName(text) }}</a-tag>
      </span>
      <span slot="proficiencyLevel" slot-scope="text">
        <a-progress :percent="text" :size="'small'" :stroke-color="getProficiencyColor(text)" />
        <span style="margin-left: 8px;">{{ text }}%</span>
      </span>
      <span slot="recommendationLevel" slot-scope="text">
        <a-progress :percent="text" :size="'small'" :stroke-color="getRecommendationColor(text)" />
        <span style="margin-left: 8px;">{{ text }}%</span>
      </span>
      <span slot="featured" slot-scope="text">
        <a-tag v-if="text" color="orange">精选</a-tag>
        <span v-else>-</span>
      </span>
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Blog_Technology.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Blog_Technology.Featured')">
            <a @click="handleFeaturedToggle(record)">{{ record.IsFeatured ? '取消精选' : '设为精选' }}</a>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Blog_Technology.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <!-- 技术栈编辑表单组件 -->
    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
  </a-card>
</template>

<script>
import { GetDataList, DeleteData, GetCategories } from '@/api/blog_technology'
import EditForm from './EditForm'

const columns = [
  {
    title: '技术名称',
    dataIndex: 'Name',
    width: 150,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: '分类',
    dataIndex: 'Category',
    scopedSlots: { customRender: 'category' },
    width: 120
  },
  {
    title: '描述',
    dataIndex: 'Description',
    width: 200,
    ellipsis: true
  },
  {
    title: '熟练度',
    dataIndex: 'ProficiencyLevel',
    scopedSlots: { customRender: 'proficiencyLevel' },
    width: 150
  },
  {
    title: '推荐度',
    dataIndex: 'RecommendationLevel',
    scopedSlots: { customRender: 'recommendationLevel' },
    width: 150
  },
  {
    title: '精选',
    dataIndex: 'IsFeatured',
    scopedSlots: { customRender: 'featured' },
    width: 80
  },
  {
    title: '排序权重',
    dataIndex: 'SortOrder',
    width: 100
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
    width: 180,
    fixed: 'right'
  }
]

export default {
  components: {
    EditForm
  },
  mounted() {
    this.getDataList()
    this.getCategories()
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
      categories: []
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
    getCategories() {
      GetCategories().then(resJson => {
        this.categories = resJson.Data || []
      })
    },
    getCategoryName(category) {
      const categoryMap = {
        'frontend': '前端',
        'backend': '后端',
        'database': '数据库',
        'devtools': '开发工具',
        'other': '其他'
      }
      return categoryMap[category] || category
    },
    getCategoryColor(category) {
      const colorMap = {
        'frontend': 'blue',
        'backend': 'green',
        'database': 'orange',
        'devtools': 'purple',
        'other': 'gray'
      }
      return colorMap[category] || 'default'
    },
    getProficiencyColor(percent) {
      if (percent >= 80) return '#52c41a'
      if (percent >= 60) return '#faad14'
      if (percent >= 40) return '#ff7875'
      return '#d9d9d9'
    },
    getRecommendationColor(percent) {
      if (percent >= 80) return '#722ed1'
      if (percent >= 60) return '#1890ff'
      if (percent >= 40) return '#faad14'
      return '#d9d9d9'
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
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    },
    handleBatchAction({ key }) {
      const actionMap = {
        featured: '设为精选',
        unfeatured: '取消精选'
      }

      this.$confirm({
        title: '确认操作',
        content: `是否${actionMap[key]}选中的技术栈?`,
        onOk: () => {
          // 这里需要实现批量精选功能
          this.$message.success('操作成功!')
          this.selectedRowKeys = []
          this.getDataList()
        }
      })
    },
    handleFeaturedToggle(record) {
      const action = record.IsFeatured ? '取消精选' : '设为精选'
      this.$confirm({
        title: '确认操作',
        content: `是否${action}该技术栈?`,
        onOk: () => {
          // 这里需要实现单个精选切换功能
          this.$message.success('操作成功!')
          this.getDataList()
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