<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Blog_FriendlyLink.Add')" type="primary" icon="plus" @click="handleAdd()">新建链接</a-button>
      <a-button v-if="hasPerm('Blog_FriendlyLink.Delete')" type="primary" icon="minus" @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-dropdown v-if="hasSelected()">
        <a-menu slot="overlay" @click="handleBatchAction">
          <a-menu-item key="enable">批量启用</a-menu-item>
          <a-menu-item key="disable">批量禁用</a-menu-item>
        </a-menu>
        <a-button style="margin-left: 8px">
          批量操作 <a-icon type="down" />
        </a-button>
      </a-dropdown>
      <a-button v-if="hasPerm('Blog_FriendlyLink.Sort')" type="default" icon="sort-ascending" @click="handleSortMode">
        {{ sortMode ? '退出排序' : '排序模式' }}
      </a-button>
      <a-button v-if="sortMode" type="primary" @click="handleSaveSortOrder" :loading="sortLoading">
        <a-icon type="check" />
        保存排序
      </a-button>
    </div>

    <div class="table-page-search-wrapper" v-if="!sortMode">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="6" :sm="24">
            <a-form-item label="关键字">
              <a-input v-model="queryParam.keyword" placeholder="链接名称、描述" 
                       @pressEnter="handleSearch" allowClear />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="状态">
              <a-select v-model="queryParam.status" placeholder="状态" allowClear>
                <a-select-option :value="1">启用</a-select-option>
                <a-select-option :value="0">禁用</a-select-option>
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
      :loading="loading" @change="handleTableChange" :scroll="{ x: 800, y: 600 }"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="middle">
      
      <span slot="url" slot-scope="text">
        <a :href="text" target="_blank" style="color: #1890ff;">
          <span style="max-width: 200px; display: inline-block; overflow: hidden; text-overflow: ellipsis; white-space: nowrap;">{{ text }}</span>
          <a-icon type="export" style="margin-left: 4px;" />
        </a>
      </span>
      
      <span slot="description" slot-scope="text">
        <div style="max-width: 200px; word-break: break-word;">{{ text || '-' }}</div>
      </span>
      
      <span slot="status" slot-scope="text">
        <a-badge :status="text === 1 ? 'success' : 'default'" :text="text === 1 ? '启用' : '禁用'" />
      </span>

      <span slot="sortOrder" slot-scope="text, record" v-if="sortMode">
        <a-input-number 
          v-model="record.SortOrder" 
          :min="0" 
          :max="9999" 
          size="small" 
          style="width: 80px;" />
      </span>
      <span v-else slot="sortOrder" slot-scope="text">{{ text }}</span>
      
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Blog_FriendlyLink.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Blog_FriendlyLink.ToggleStatus')">
            <a @click="handleToggleStatus(record)">{{ record.Status === 1 ? '禁用' : '启用' }}</a>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Blog_FriendlyLink.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <!-- 友情链接编辑表单组件 -->
    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
  </a-card>
</template>

<script>
import { GetDataList, DeleteData, BatchUpdateStatus, UpdateSortOrder, ToggleStatus } from '@/api/blog_friendlylink'
import EditForm from './EditForm'

const columns = [
  {
    title: '链接名称',
    dataIndex: 'Name',
    width: 150,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: '链接地址',
    dataIndex: 'Url',
    scopedSlots: { customRender: 'url' },
    width: 220,
    ellipsis: true
  },
  {
    title: '链接描述',
    dataIndex: 'Description',
    scopedSlots: { customRender: 'description' },
    width: 220,
    ellipsis: true
  },
  {
    title: '状态',
    dataIndex: 'Status',
    scopedSlots: { customRender: 'status' },
    width: 80
  },
  {
    title: '排序权重',
    dataIndex: 'SortOrder',
    scopedSlots: { customRender: 'sortOrder' },
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
    width: 120,
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
      sortLoading: false,
      sortMode: false,
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
      if (this.sortMode) return // 排序模式下禁用分页
      
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
        content: '是否删除选中友情链接?',
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
          this.selectedRowKeys = []
        } else {
          this.$message.error(resJson.Msg)
        }
      })
    },
    handleBatchAction({ key }) {
      const statusMap = {
        enable: { status: 1, text: '启用' },
        disable: { status: 0, text: '禁用' }
      }
      
      const action = statusMap[key]
      if (!action) return

      this.$confirm({
        title: '确认操作',
        content: `是否${action.text}选中的 ${this.selectedRowKeys.length} 个友情链接?`,
        onOk: () => {
          BatchUpdateStatus({
            Ids: this.selectedRowKeys,
            Status: action.status
          }).then(resJson => {
            if (resJson.Success) {
              this.$message.success(`已成功${action.text} ${this.selectedRowKeys.length} 个友情链接!`)
              this.selectedRowKeys = []
              this.getDataList()
            } else {
              this.$message.error(resJson.Msg || `${action.text}失败`)
            }
          })
        }
      })
    },
    handleToggleStatus(record) {
      const action = record.Status === 1 ? '禁用' : '启用'
      this.$confirm({
        title: '确认操作',
        content: `是否${action}友情链接"${record.Name}"?`,
        onOk: () => {
          ToggleStatus(record.Id).then(resJson => {
            if (resJson.Success) {
              this.$message.success(`${action}成功!`)
              this.getDataList()
            } else {
              this.$message.error(resJson.Msg || `${action}失败`)
            }
          })
        }
      })
    },
    handleSortMode() {
      this.sortMode = !this.sortMode
      if (this.sortMode) {
        // 进入排序模式，禁用分页并获取所有数据
        this.pagination = false
        this.queryParam = {} // 清空筛选条件
        this.getDataList()
        this.$message.info('已进入排序模式，您可以调整排序权重，调整完成后点击"保存排序"')
      } else {
        // 退出排序模式，恢复分页
        this.pagination = {
          current: 1,
          pageSize: 10,
          showSizeChanger: true,
          showQuickJumper: true,
          pageSizeOptions: ['10', '20', '50', '100'],
          showTotal: (total, range) => `共 ${total} 条记录，显示第 ${range[0]}-${range[1]} 条`
        }
        this.getDataList()
      }
    },
    handleSaveSortOrder() {
      const items = this.data.map(item => ({
        Id: item.Id,
        SortOrder: item.SortOrder || 0
      }))

      this.sortLoading = true
      UpdateSortOrder({ Items: items }).then(resJson => {
        this.sortLoading = false
        if (resJson.Success) {
          this.$message.success('排序保存成功!')
          this.handleSortMode() // 退出排序模式
        } else {
          this.$message.error(resJson.Msg || '保存排序失败')
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

<style lang="less" scoped>
.table-operator {
  margin-bottom: 16px;
  
  .ant-btn {
    margin-right: 8px;
  }
}
</style>