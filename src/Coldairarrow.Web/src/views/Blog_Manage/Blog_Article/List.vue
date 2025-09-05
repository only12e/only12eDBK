<template>
  <a-card :bordered="false">
    <div class="table-operator">
      <a-button v-if="hasPerm('Blog_Article.Add')" type="primary" icon="plus" @click="handleAdd()">新建文章</a-button>
      <a-button v-if="hasPerm('Blog_Article.Delete')" type="primary" icon="minus" @click="handleDelete(selectedRowKeys)"
        :disabled="!hasSelected()" :loading="loading">删除</a-button>
      <a-dropdown v-if="hasSelected()">
        <a-menu slot="overlay" @click="handleBatchAction">
          <a-menu-item key="published">批量发布</a-menu-item>
          <a-menu-item key="draft">批量设为草稿</a-menu-item>
          <a-menu-item key="hidden">批量隐藏</a-menu-item>
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
              <a-input v-model="queryParam.keyword" placeholder="标题、摘要、内容" 
                       @pressEnter="handleSearch" allowClear />
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="分类">
              <a-select v-model="queryParam.categoryId" placeholder="分类" allowClear>
                <a-select-option v-for="item in categories" :key="item.Id" :value="item.Id">
                  {{ item.Name }}
                </a-select-option>
              </a-select>
            </a-form-item>
          </a-col>
          <a-col :md="4" :sm="24">
            <a-form-item label="状态">
              <a-select v-model="queryParam.status" placeholder="状态" allowClear>
                <a-select-option value="draft">草稿</a-select-option>
                <a-select-option value="published">已发布</a-select-option>
                <a-select-option value="hidden">隐藏</a-select-option>
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
            <a-button type="primary" @click="handleSearch">查询</a-button>
            <a-button style="margin-left: 8px" @click="handleReset">重置</a-button>
            <a-button style="margin-left: 8px" @click="handleCategoryManage">分类管理</a-button>
          </a-col>
        </a-row>
      </a-form>
    </div>

    <a-table ref="table" :columns="columns" :rowKey="row => row.Id" :dataSource="data" :pagination="pagination"
      :loading="loading" @change="handleTableChange" :scroll="{ x: 1200, y: 600 }"
      :rowSelection="{ selectedRowKeys: selectedRowKeys, onChange: onSelectChange }" :bordered="true" size="middle">
      <span slot="cover" slot-scope="text">
        <img v-if="text" :src="text" style="width: 40px; height: 30px; object-fit: cover; border-radius: 4px;" />
        <span v-else>-</span>
      </span>
      <span slot="status" slot-scope="text">
        <a-badge :status="text === 'published' ? 'success' : text === 'draft' ? 'default' : 'error'"
          :text="text === 'published' ? '已发布' : text === 'draft' ? '草稿' : '隐藏'" />
      </span>
      <span slot="featured" slot-scope="text">
        <a-tag v-if="text" color="orange">精选</a-tag>
        <span v-else>-</span>
      </span>
      <span slot="stats" slot-scope="text, record">
        <div style="display: flex; flex-direction: column; gap: 2px;">
          <span style="font-size: 12px;"><a-icon type="eye" /> {{ formatNumber(record.ViewCount || 0) }}</span>
          <span style="font-size: 12px;"><a-icon type="heart" /> {{ formatNumber(record.LikeCount || 0) }}</span>
          <span style="font-size: 12px;"><a-icon type="message" /> {{ formatNumber(record.CommentCount || 0) }}</span>
        </div>
      </span>
      <span slot="action" slot-scope="text, record">
        <template>
          <template v-if="hasPerm('Blog_Article.Edit')">
            <a @click="handleEdit(record.Id)">编辑</a>
            <a-divider type="vertical" />
          </template>
          <template v-if="hasPerm('Blog_Article.Publish')">
            <a-dropdown>
              <a-menu slot="overlay" @click="({ key }) => handleStatusChange(record, key)">
                <a-menu-item key="published">发布</a-menu-item>
                <a-menu-item key="draft">草稿</a-menu-item>
                <a-menu-item key="hidden">隐藏</a-menu-item>
              </a-menu>
              <a>
                状态 <a-icon type="down" />
              </a>
            </a-dropdown>
            <a-divider type="vertical" />
          </template>
          <a v-if="hasPerm('Blog_Article.Delete')" @click="handleDelete([record.Id])">删除</a>
        </template>
      </span>
    </a-table>

    <!-- 分类管理弹窗 -->
    <a-modal title="分类管理" :visible="categoryVisible" :width="800" @cancel="categoryVisible = false"
      @ok="handleCategorySave">
      <a-table :columns="categoryColumns" :dataSource="categories" :rowKey="row => row.Id" :pagination="false" size="small">
        <span slot="action" slot-scope="text, record">
          <a @click="handleCategoryEdit(record)">编辑</a>
          <a-divider type="vertical" />
          <a @click="handleCategoryDelete(record.Id)">删除</a>
        </span>
      </a-table>
      <div style="margin-top: 16px;">
        <a-button type="primary" @click="handleCategoryAdd">新增分类</a-button>
      </div>
    </a-modal>

    <!-- 分类编辑弹窗 -->
    <a-modal :title="categoryForm.Id ? '编辑分类' : '新增分类'" :visible="categoryFormVisible"
      @cancel="categoryFormVisible = false" @ok="handleCategoryFormSave">
      <a-form :form="categoryFormObj">
        <a-form-item label="分类名称" :labelCol="{ span: 4 }" :wrapperCol="{ span: 20 }">
          <a-input v-model="categoryForm.Name" placeholder="请输入分类名称" />
        </a-form-item>
        <a-form-item label="分类描述" :labelCol="{ span: 4 }" :wrapperCol="{ span: 20 }">
          <a-textarea v-model="categoryForm.Description" placeholder="请输入分类描述" :rows="3" />
        </a-form-item>
        <a-form-item label="排序权重" :labelCol="{ span: 4 }" :wrapperCol="{ span: 20 }">
          <a-input-number v-model="categoryForm.SortOrder" placeholder="数字越大越靠前" style="width: 100%" />
        </a-form-item>
      </a-form>
    </a-modal>
    <!-- 文章编辑表单组件 -->
    <edit-form ref="editForm" :afterSubmit="getDataList"></edit-form>
  </a-card>
</template>

<script>
import { GetDataList, DeleteData, PublishData, GetCategories, SaveCategory, DeleteCategory } from '@/api/blog_article'
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
    title: '标题',
    dataIndex: 'Title',
    width: 200,
    ellipsis: true,
    fixed: 'left'
  },
  {
    title: '分类',
    dataIndex: 'Category.Name',
    width: 120
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
    width: 80
  },
  {
    title: '统计',
    scopedSlots: { customRender: 'stats' },
    width: 100
  },
  {
    title: '作者',
    dataIndex: 'Author.Nickname',
    width: 120
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

const categoryColumns = [
  {
    title: '分类名称',
    dataIndex: 'Name'
  },
  {
    title: '分类描述',
    dataIndex: 'Description',
    ellipsis: true
  },
  {
    title: '排序权重',
    dataIndex: 'SortOrder'
  },
  {
    title: '操作',
    scopedSlots: { customRender: 'action' },
    width: 120
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
        pageSize: 20,
        showSizeChanger: true,
        showQuickJumper: true,
        pageSizeOptions: ['10', '20', '50', '100'],
        showTotal: (total, range) => `共 ${total} 条记录，显示第 ${range[0]}-${range[1]} 条`
      },
      selectedRowKeys: [],

      // 分类管理
      categories: [],
      categoryVisible: false,
      categoryColumns,
      categoryFormVisible: false,
      categoryForm: {},
      categoryFormObj: this.$form.createForm(this)
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
      const statusMap = {
        published: '发布',
        draft: '设为草稿',
        hidden: '隐藏'
      }

      this.$confirm({
        title: '确认操作',
        content: `是否${statusMap[key]}选中的文章?`,
        onOk: () => {
          return PublishData({
            Ids: this.selectedRowKeys,
            Status: key
          }).then(resJson => {
            this.$message.success('操作成功!')
            this.selectedRowKeys = []
            this.getDataList()
          })
        }
      })
    },
    handleStatusChange(record, status) {
      PublishData({
        Ids: [record.Id],
        Status: status
      }).then(resJson => {
        this.$message.success('操作成功!')
        this.getDataList()
      })
    },

    // 分类管理
    handleCategoryManage() {
      this.categoryVisible = true
    },
    handleCategoryAdd() {
      this.categoryForm = { SortOrder: 0 }
      this.categoryFormVisible = true
    },
    handleCategoryEdit(record) {
      this.categoryForm = { ...record }
      this.categoryFormVisible = true
    },
    handleCategoryDelete(id) {
      this.$confirm({
        title: '确认删除',
        content: '删除分类可能影响已有文章，确认删除?',
        onOk: () => {
          return DeleteCategory([id]).then(resJson => {
            this.$message.success('操作成功!')
            this.getCategories()
          })
        }
      })
    },
    handleCategoryFormSave() {
      SaveCategory(this.categoryForm).then(resJson => {
        this.$message.success('操作成功!')
        this.categoryFormVisible = false
        this.categoryForm = {}
        this.getCategories()
      })
    },
    handleCategorySave() {
      this.categoryVisible = false
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