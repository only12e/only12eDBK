<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Blog_SystemConfig.Add')" type="primary" icon="plus" @click="handleAdd()">新建配置</a-button>
      <a-button v-if="hasPerm('Blog_SystemConfig.Delete')" type="primary" icon="minus" @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-button v-if="hasPerm('Blog_SystemConfig.BatchUpdate')" type="default" icon="sync" @click="handleBatchUpdate"
        :disabled="!hasSelected()">批量更新</a-button>
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="6" :sm="24">
            <a-form-item label="关键字">
              <a-input v-model="queryParam.keyword" placeholder="配置键名、配置说明" 
                       @pressEnter="handleSearch" allowClear />
            </a-form-item>
          </a-col>
          <a-col :md="5" :sm="24">
            <a-form-item label="配置键名">
              <a-input v-model="queryParam.configKey" placeholder="精确匹配配置键名" 
                       @pressEnter="handleSearch" allowClear />
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
      :loading="loading" @change="handleTableChange" :scroll="{ x: 1000, y: 600 }"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="middle">
      
      <span slot="configValue" slot-scope="text">
        <div style="max-width: 200px; word-break: break-all; white-space: pre-wrap;">{{ text || '-' }}</div>
      </span>
      
      <span slot="description" slot-scope="text">
        <div style="max-width: 250px; word-break: break-word;">{{ text || '-' }}</div>
      </span>
      
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Blog_SystemConfig.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Blog_SystemConfig.SetValue')">
            <a @click="handleSetValue(record)">设值</a>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Blog_SystemConfig.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <!-- 配置编辑表单组件 -->
    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>

    <!-- 设置配置值的模态框 -->
    <a-modal
      title="设置配置值"
      :visible="setValueVisible"
      @ok="handleSetValueSubmit"
      @cancel="handleSetValueCancel"
      :confirmLoading="setValueLoading">
      <a-form-model
        :model="setValueForm"
        :rules="setValueRules"
        ref="setValueForm"
        :labelCol="{ span: 6 }"
        :wrapperCol="{ span: 17 }">
        
        <a-form-model-item label="配置键名">
          <a-input v-model="setValueForm.ConfigKey" disabled />
        </a-form-model-item>

        <a-form-model-item label="配置值" prop="ConfigValue">
          <a-textarea
            v-model="setValueForm.ConfigValue"
            placeholder="请输入配置值"
            :rows="4"
            allowClear />
        </a-form-model-item>

        <a-form-model-item label="配置说明" prop="Description">
          <a-textarea
            v-model="setValueForm.Description"
            placeholder="请输入配置说明（可选）"
            :rows="3"
            allowClear />
        </a-form-model-item>

      </a-form-model>
    </a-modal>

    <!-- 批量更新的模态框 -->
    <a-modal
      title="批量更新配置"
      :visible="batchUpdateVisible"
      @ok="handleBatchUpdateSubmit"
      @cancel="handleBatchUpdateCancel"
      :confirmLoading="batchUpdateLoading"
      :width="800">
      
      <div style="margin-bottom: 16px;">
        <span>选中了 {{ selectedRowKeys.length }} 项配置，请输入要更新的配置值：</span>
      </div>

      <a-table
        :columns="batchUpdateColumns"
        :dataSource="batchUpdateData"
        :pagination="false"
        :rowKey="record => record.ConfigKey"
        size="small">
        
        <span slot="configValue" slot-scope="text, record, index">
          <a-textarea
            v-model="batchUpdateData[index].ConfigValue"
            placeholder="请输入新的配置值"
            :rows="2"
            allowClear />
        </span>

      </a-table>
    </a-modal>
  </a-card>
</template>

<script>
import { GetDataList, DeleteData, SetConfigValue, BatchUpdate } from '@/api/blog_systemconfig'
import EditForm from './EditForm'

const columns = [
  {
    title: '配置键名',
    dataIndex: 'ConfigKey',
    width: 200,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: '配置值',
    dataIndex: 'ConfigValue',
    scopedSlots: { customRender: 'configValue' },
    width: 220,
    ellipsis: true
  },
  {
    title: '配置说明',
    dataIndex: 'Description',
    scopedSlots: { customRender: 'description' },
    width: 270,
    ellipsis: true
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
    title: '更新时间',
    dataIndex: 'UpdatedAt',
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

const batchUpdateColumns = [
  {
    title: '配置键名',
    dataIndex: 'ConfigKey',
    width: 200
  },
  {
    title: '当前值',
    dataIndex: 'CurrentValue',
    width: 200,
    ellipsis: true
  },
  {
    title: '新配置值',
    scopedSlots: { customRender: 'configValue' },
    width: 250
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
      batchUpdateColumns,
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
      // 设置配置值相关
      setValueVisible: false,
      setValueLoading: false,
      setValueForm: {},
      setValueRules: {
        ConfigValue: [
          { required: true, message: '配置值不能为空', trigger: 'blur' }
        ]
      },
      // 批量更新相关
      batchUpdateVisible: false,
      batchUpdateLoading: false,
      batchUpdateData: []
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
        content: '是否删除选中配置?',
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
    handleSetValue(record) {
      this.setValueForm = {
        ConfigKey: record.ConfigKey,
        ConfigValue: record.ConfigValue,
        Description: record.Description
      }
      this.setValueVisible = true
    },
    handleSetValueSubmit() {
      this.$refs.setValueForm.validate(valid => {
        if (!valid) return

        this.setValueLoading = true
        SetConfigValue(
          this.setValueForm.ConfigKey,
          this.setValueForm.ConfigValue,
          this.setValueForm.Description
        ).then(resJson => {
          this.setValueLoading = false
          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.setValueVisible = false
            this.getDataList()
          } else {
            this.$message.error(resJson.Msg || '操作失败')
          }
        })
      })
    },
    handleSetValueCancel() {
      this.setValueVisible = false
      this.setValueForm = {}
    },
    handleBatchUpdate() {
      // 构建批量更新的数据
      this.batchUpdateData = this.selectedRowKeys.map(id => {
        const record = this.data.find(item => item.Id === id)
        return {
          ConfigKey: record.ConfigKey,
          CurrentValue: record.ConfigValue,
          ConfigValue: record.ConfigValue // 默认为当前值
        }
      })
      this.batchUpdateVisible = true
    },
    handleBatchUpdateSubmit() {
      const configs = this.batchUpdateData.map(item => ({
        ConfigKey: item.ConfigKey,
        ConfigValue: item.ConfigValue
      }))

      this.batchUpdateLoading = true
      BatchUpdate({ Configs: configs }).then(resJson => {
        this.batchUpdateLoading = false
        if (resJson.Success) {
          this.$message.success('批量更新成功!')
          this.batchUpdateVisible = false
          this.selectedRowKeys = []
          this.getDataList()
        } else {
          this.$message.error(resJson.Msg || '批量更新失败')
        }
      })
    },
    handleBatchUpdateCancel() {
      this.batchUpdateVisible = false
      this.batchUpdateData = []
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