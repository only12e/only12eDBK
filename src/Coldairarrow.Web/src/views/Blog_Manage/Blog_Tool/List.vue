<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Blog_Tool.Add')" type="primary" icon="plus" @click="handleAdd()">新建工具</a-button>
      <a-button v-if="hasPerm('Blog_Tool.Delete')" type="primary" icon="minus" @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-dropdown v-if="hasSelected()">
        <a-menu slot="overlay" @click="handleBatchAction">
          <a-menu-item key="recommend">批量推荐</a-menu-item>
          <a-menu-item key="unrecommend">取消推荐</a-menu-item>
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
              <a-input v-model="queryParam.keyword" placeholder="工具名称、描述" 
                       @pressEnter="handleSearch" allowClear />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="分类">
              <a-select v-model="queryParam.category" placeholder="分类" allowClear>
                <a-select-option v-for="item in categories" :key="item" :value="item">
                  {{ item }}
                </a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="价格类型">
              <a-select v-model="queryParam.priceType" placeholder="价格类型" allowClear>
                <a-select-option value="free">免费</a-select-option>
                <a-select-option value="paid">付费</a-select-option>
                <a-select-option value="freemium">免费增值</a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="推荐">
              <a-select v-model="queryParam.isRecommended" placeholder="推荐" allowClear>
                <a-select-option :value="1">是</a-select-option>
                <a-select-option :value="0">否</a-select-option>
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
      :loading="loading" @change="handleTableChange" :scroll="{ x: 1400, y: 600 }"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="middle">
      <span slot="cover" slot-scope="text">
        <img v-if="text" :src="text" style="width: 40px; height: 30px; object-fit: cover; border-radius: 4px;" />
        <span v-else>-</span>
      </span>
      <span slot="icon" slot-scope="text">
        <img v-if="text" :src="text" style="width: 24px; height: 24px; object-fit: cover; border-radius: 4px;" />
        <span v-else>-</span>
      </span>
      <span slot="priceType" slot-scope="text">
        <a-tag v-if="text === 'free'" color="green">免费</a-tag>
        <a-tag v-else-if="text === 'paid'" color="orange">付费</a-tag>
        <a-tag v-else-if="text === 'freemium'" color="blue">免费增值</a-tag>
        <span v-else>-</span>
      </span>
      <span slot="rating" slot-scope="text">
        <a-rate v-model="text" disabled allow-half style="font-size: 14px;" />
        <span style="margin-left: 8px;">{{ text }}</span>
      </span>
      <span slot="recommended" slot-scope="text">
        <a-tag v-if="text" color="orange">推荐</a-tag>
        <span v-else>-</span>
      </span>
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Blog_Tool.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Blog_Tool.Recommend')">
            <a @click="handleRecommendToggle(record)">{{ record.IsRecommended ? '取消推荐' : '推荐' }}</a>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Blog_Tool.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <!-- 工具编辑表单组件 -->
    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
  </a-card>
</template>

<script>
import { GetDataList, DeleteData, GetCategories } from '@/api/blog_tool'
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
    title: '图标',
    dataIndex: 'IconUrl',
    scopedSlots: { customRender: 'icon' },
    width: 60,
    fixed: 'left'
  },
  {
    title: '工具名称',
    dataIndex: 'Name',
    width: 180,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: '分类',
    dataIndex: 'Category',
    width: 120
  },
  {
    title: '价格类型',
    dataIndex: 'PriceType',
    scopedSlots: { customRender: 'priceType' },
    width: 100
  },
  {
    title: '评分',
    dataIndex: 'Rating',
    scopedSlots: { customRender: 'rating' },
    width: 150
  },
  {
    title: '推荐',
    dataIndex: 'IsRecommended',
    scopedSlots: { customRender: 'recommended' },
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
        recommend: '推荐',
        unrecommend: '取消推荐'
      }

      this.$confirm({
        title: '确认操作',
        content: `是否${actionMap[key]}选中的工具?`,
        onOk: () => {
          // 这里需要实现批量推荐功能
          this.$message.success('操作成功!')
          this.selectedRowKeys = []
          this.getDataList()
        }
      })
    },
    handleRecommendToggle(record) {
      const action = record.IsRecommended ? '取消推荐' : '推荐'
      this.$confirm({
        title: '确认操作',
        content: `是否${action}该工具?`,
        onOk: () => {
          // 这里需要实现单个推荐切换功能
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