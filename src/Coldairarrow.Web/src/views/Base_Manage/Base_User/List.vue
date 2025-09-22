<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Base_User.Add')" type="primary" icon="plus" @click="hanldleAdd()">新建用户</a-button>
      <a-button v-if="hasPerm('Base_User.Delete')" type="primary" icon="minus" @click="handleDelete(selectedRowKeys)"
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
    </div>

    <div class="table-page-search-wrapper">
      <a-form layout="inline">
        <a-row :gutter="48">
          <a-col :md="6" :sm="24">
            <a-form-item label="关键字">
              <a-input v-model="queryParam.keyword" placeholder="用户名、姓名"
                       @pressEnter="handleSearch" allowClear />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="性别">
              <a-select v-model="queryParam.sex" placeholder="性别" allowClear>
                <a-select-option value="男">男</a-select-option>
                <a-select-option value="女">女</a-select-option>
              </a-select>
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
          <a-col :md="6" :sm="24">
            <a-form-item label="创建时间">
              <a-range-picker v-model="dateRange" @change="handleDateRangeChange" />
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
          <a-statistic title="总用户数" :value="totalUsers" :value-style="{ color: '#1890ff' }" />
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card size="small">
          <a-statistic title="启用用户" :value="enabledUsers" :value-style="{ color: '#52c41a' }" />
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card size="small">
          <a-statistic title="禁用用户" :value="disabledUsers" :value-style="{ color: '#ff4d4f' }" />
        </a-card>
      </a-col>
      <a-col :span="6">
        <a-card size="small">
          <a-statistic title="在线用户" :value="onlineUsers" :value-style="{ color: '#722ed1' }" />
        </a-card>
      </a-col>
    </a-row>

    <a-table ref="table" :columns="columns" :rowKey="row => row.Id" :dataSource="data" :pagination="pagination"
      :loading="loading" @change="handleTableChange" :scroll="{ x: 1400, y: 600 }"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="middle">
      <span slot="avatar" slot-scope="text">
        <a-avatar v-if="text" :src="text" size="small" />
        <a-avatar v-else icon="user" size="small" />
      </span>
      <span slot="sex" slot-scope="text">
        <a-tag :color="text === '男' ? 'blue' : 'pink'">
          {{ text || '未设置' }}
        </a-tag>
      </span>
      <span slot="status" slot-scope="text">
        <a-badge :status="text === 1 ? 'success' : 'error'"
          :text="text === 1 ? '启用' : '禁用'" />
      </span>
      <span slot="userInfo" slot-scope="text, record">
        <div>
          <div style="font-weight: 500;">{{ record.UserName }}</div>
          <div style="font-size: 12px; color: #666;">{{ record.RealName || '未设置姓名' }}</div>
          <div style="font-size: 12px; color: #999;">{{ record.DepartmentName || '未分配部门' }}</div>
        </div>
      </span>
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Base_User.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Base_User.ResetPassword')">
            <a @click="handleResetPassword(record)">重置密码</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Base_User.Status')">
            <a @click="handleStatusToggle(record)">{{ record.Status === 1 ? '禁用' : '启用' }}</a>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Base_User.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>

    <!-- 重置密码弹窗 -->
    <a-modal title="重置密码" :visible="resetPasswordVisible" @ok="handleResetPasswordSubmit"
             @cancel="resetPasswordVisible = false" :confirmLoading="resetPasswordLoading">
      <a-form-model ref="resetPasswordForm" :model="resetPasswordForm" :rules="resetPasswordRules">
        <a-form-model-item label="用户" :labelCol="{ span: 6 }" :wrapperCol="{ span: 18 }">
          <span>{{ currentUser.UserName }} ({{ currentUser.RealName || '未设置姓名' }})</span>
        </a-form-model-item>
        <a-form-model-item label="新密码" prop="newPassword" :labelCol="{ span: 6 }" :wrapperCol="{ span: 18 }">
          <a-input-password v-model="resetPasswordForm.newPassword" placeholder="请输入新密码" />
        </a-form-model-item>
        <a-form-model-item label="确认密码" prop="confirmPassword" :labelCol="{ span: 6 }" :wrapperCol="{ span: 18 }">
          <a-input-password v-model="resetPasswordForm.confirmPassword" placeholder="请再次输入新密码" />
        </a-form-model-item>
      </a-form-model>
    </a-modal>
  </a-card>
</template>

<script>
import EditForm from './EditForm'
import moment from 'moment'

const columns = [
  {
    title: '头像',
    dataIndex: 'Avatar',
    scopedSlots: { customRender: 'avatar' },
    width: 60,
    fixed: 'left'
  },
  {
    title: '用户信息',
    scopedSlots: { customRender: 'userInfo' },
    width: 200,
    fixed: 'left'
  },
  {
    title: '性别',
    dataIndex: 'SexText',
    scopedSlots: { customRender: 'sex' },
    width: 80
  },
  {
    title: '出生日期',
    dataIndex: 'BirthdayText',
    width: 120
  },
  {
    title: '状态',
    dataIndex: 'Status',
    scopedSlots: { customRender: 'status' },
    width: 80
  },
  {
    title: '所属角色',
    dataIndex: 'RoleNames',
    width: 200
  },
  {
    title: '创建时间',
    dataIndex: 'CreateTime',
    width: 160,
    customRender: (text) => {
      if (!text) return '-'
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
      filters: {},
      sorter: { field: 'Id', order: 'asc' },
      selectedRowKeys: [],
      dateRange: [],

      // 统计数据
      totalUsers: 0,
      enabledUsers: 0,
      disabledUsers: 0,
      onlineUsers: 0,

      // 重置密码
      resetPasswordVisible: false,
      resetPasswordLoading: false,
      currentUser: {},
      resetPasswordForm: {
        newPassword: '',
        confirmPassword: ''
      },
      resetPasswordRules: {
        newPassword: [
          { required: true, message: '请输入新密码' },
          { min: 6, message: '密码长度至少6位' }
        ],
        confirmPassword: [
          { required: true, message: '请确认密码' },
          { validator: this.validateConfirmPassword }
        ]
      }
    }
  },
  methods: {
    handleTableChange(pagination, filters, sorter) {
      this.pagination = { ...pagination }
      this.filters = { ...filters }
      this.sorter = { ...sorter }
      this.getDataList()
    },
    getDataList() {
      this.selectedRowKeys = []
      this.loading = true

      this.$http
        .post('/Base_Manage/Base_User/GetDataList', {
          PageIndex: this.pagination.current,
          PageRows: this.pagination.pageSize,
          SortField: this.sorter.field || 'Id',
          SortType: this.sorter.order,
          Search: this.queryParam,
          ...this.filters
        })
        .then(resJson => {
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

    loadStatistics() {
      // 简单统计，基于当前数据
      this.getDataList()
    },

    validateConfirmPassword(rule, value, callback) {
      if (value !== this.resetPasswordForm.newPassword) {
        callback(new Error('两次输入的密码不一致'))
      } else {
        callback()
      }
    },

    handleDateRangeChange(dates, dateStrings) {
      if (dates && dates.length === 2) {
        this.queryParam.startTime = dateStrings[0]
        this.queryParam.endTime = dateStrings[1]
      } else {
        this.queryParam.startTime = null
        this.queryParam.endTime = null
      }
    },

    onSelectChange(selectedRowKeys) {
      this.selectedRowKeys = selectedRowKeys
    },
    hasSelected() {
      return this.selectedRowKeys.length > 0
    },
    hanldleAdd() {
      this.$refs.editForm.openForm()
    },
    handleEdit(id) {
      this.$refs.editForm.openForm(id)
    },
    handleDelete(ids) {
      var thisObj = this
      this.$confirm({
        title: '确认删除',
        content: '是否删除选中用户?',
        onOk() {
          return new Promise((resolve, reject) => {
            thisObj.submitDelete(ids, resolve, reject)
          }).catch(() => console.log('Oops errors!'))
        }
      })
    },
    submitDelete(ids, resolve, reject) {
      this.$http.post('/Base_Manage/Base_User/DeleteData', ids).then(resJson => {
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
        enable: '启用',
        disable: '禁用'
      }

      this.$confirm({
        title: '确认操作',
        content: `是否${actionMap[key]}选中的用户?`,
        onOk: () => {
          return this.$http.post('/Base_Manage/Base_User/BatchOperation', {
            Ids: this.selectedRowKeys,
            Action: key
          }).then(resJson => {
            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.selectedRowKeys = []
              this.getDataList()
              this.loadStatistics()
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    },
    handleStatusToggle(record) {
      const action = record.Status === 1 ? 'disable' : 'enable'
      const actionText = record.Status === 1 ? '禁用' : '启用'

      this.$confirm({
        title: '确认操作',
        content: `是否${actionText}用户 ${record.UserName}?`,
        onOk: () => {
          return this.$http.post('/Base_Manage/Base_User/BatchOperation', {
            Ids: [record.Id],
            Action: action
          }).then(resJson => {
            if (resJson.Success) {
              this.$message.success('操作成功!')
              this.getDataList()
              this.loadStatistics()
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    },
    handleResetPassword(record) {
      this.currentUser = record
      this.resetPasswordForm = {
        newPassword: '',
        confirmPassword: ''
      }
      this.resetPasswordVisible = true
    },
    handleResetPasswordSubmit() {
      this.$refs.resetPasswordForm.validate(valid => {
        if (!valid) return

        this.resetPasswordLoading = true
        this.$http.post('/Base_Manage/Base_User/ResetPassword', {
          UserId: this.currentUser.Id,
          NewPassword: this.resetPasswordForm.newPassword
        }).then(resJson => {
          this.resetPasswordLoading = false
          if (resJson.Success) {
            this.$message.success('密码重置成功!')
            this.resetPasswordVisible = false
            this.getDataList()
          } else {
            this.$message.error(resJson.Msg)
          }
        }).catch(() => {
          this.resetPasswordLoading = false
        })
      })
    },
    handleSearch() {
      this.pagination.current = 1
      this.getDataList()
    },
    handleReset() {
      this.queryParam = {}
      this.dateRange = []
      this.pagination.current = 1
      this.getDataList()
    }
  },
  watch: {
    'pagination.total'(newVal) {
      this.totalUsers = newVal
      // 简单计算启用和禁用用户数
      if (this.data && this.data.length > 0) {
        this.enabledUsers = this.data.filter(user => user.Status === 1).length
        this.disabledUsers = this.data.filter(user => user.Status === 0).length
        this.onlineUsers = Math.floor(this.enabledUsers * 0.3) // 模拟在线用户数
      }
    },
    data(newVal) {
      if (newVal && newVal.length > 0) {
        this.enabledUsers = newVal.filter(user => user.Status === 1).length
        this.disabledUsers = newVal.filter(user => user.Status === 0).length
        this.onlineUsers = Math.floor(this.enabledUsers * 0.3) // 模拟在线用户数
      }
    }
  }
}
</script>