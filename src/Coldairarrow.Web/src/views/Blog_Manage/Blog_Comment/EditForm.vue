<template>
  <a-modal :title="entity.Id ? '编辑评论' : '新建评论'" :width="'80%'" :visible="visible" :confirmLoading="confirmLoading"
    @ok="handleSubmit" @cancel="() => { this.visible = false }" :okText="entity.Id ? '保存' : '创建评论'">
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-model-item label="评论内容" prop="Content">
              <a-textarea v-model="entity.Content" placeholder="请输入评论内容" :rows="6" />
            </a-form-model-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="8">
            <a-form-model-item label="目标类型" prop="TargetType">
              <a-select v-model="entity.TargetType" placeholder="请选择目标类型">
                <a-select-option value="article">文章</a-select-option>
                <a-select-option value="project">项目</a-select-option>
                <a-select-option value="tool">工具</a-select-option>
                <a-select-option value="technology">技术栈</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item label="目标ID" prop="TargetId">
              <a-input-number v-model="entity.TargetId" placeholder="请输入目标ID" style="width: 100%" :min="1" />
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item label="审核状态" prop="Status">
              <a-select v-model="entity.Status" placeholder="请选择状态">
                <a-select-option value="approved">已通过</a-select-option>
                <a-select-option value="pending">待审核</a-select-option>
                <a-select-option value="rejected">已拒绝</a-select-option>
              </a-select>
            </a-form-model-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="8">
            <a-form-model-item label="评论用户ID" prop="UserId">
              <a-input-number v-model="entity.UserId" placeholder="请输入用户ID" style="width: 100%" :min="1" />
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item label="父评论ID">
              <a-input-number v-model="entity.ParentId" placeholder="回复评论时填写" style="width: 100%" :min="1" />
              <div style="font-size: 12px; color: #999; margin-top: 4px;">
                留空表示顶级评论，填写则为回复评论
              </div>
            </a-form-model-item>
          </a-col>
          <a-col :span="8">
            <a-form-model-item label="IP地址">
              <a-input v-model="entity.IpAddress" placeholder="评论者IP地址" />
            </a-form-model-item>
          </a-col>
        </a-row>

        <a-row :gutter="24">
          <a-col :span="24">
            <a-form-model-item label="浏览器信息">
              <a-textarea v-model="entity.UserAgent" placeholder="User Agent信息" :rows="2" />
            </a-form-model-item>
          </a-col>
        </a-row>

        <!-- 预览区域 -->
        <a-row v-if="entity.Id" :gutter="24">
          <a-col :span="24">
            <a-card title="评论信息预览" size="small">
              <a-descriptions :column="2" size="small">
                <a-descriptions-item label="目标类型">
                  <a-tag :color="getTargetTypeColor(entity.TargetType)">
                    {{ getTargetTypeName(entity.TargetType) }}
                  </a-tag>
                </a-descriptions-item>
                <a-descriptions-item label="目标ID">{{ entity.TargetId }}</a-descriptions-item>
                <a-descriptions-item label="评论类型">
                  <a-tag v-if="entity.ParentId" color="blue">回复评论</a-tag>
                  <a-tag v-else color="green">顶级评论</a-tag>
                </a-descriptions-item>
                <a-descriptions-item label="点赞数">
                  <span style="color: #ff4757;">
                    <a-icon type="heart" /> {{ entity.LikeCount || 0 }}
                  </span>
                </a-descriptions-item>
                <a-descriptions-item label="创建时间">{{ entity.CreatedAt }}</a-descriptions-item>
                <a-descriptions-item label="更新时间">{{ entity.UpdatedAt }}</a-descriptions-item>
              </a-descriptions>
            </a-card>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import { GetTheData, SaveData } from '@/api/blog_comment'

export default {
  props: {
    afterSubmit: {
      type: Function,
      default: null
    }
  },
  data() {
    return {
      layout: {
        labelCol: { span: 6 },
        wrapperCol: { span: 18 }
      },
      visible: false,
      confirmLoading: false,
      entity: this.getInitialEntity(),

      rules: {
        Content: [{ required: true, message: '请输入评论内容' }],
        TargetType: [{ required: true, message: '请选择目标类型' }],
        TargetId: [{ required: true, message: '请输入目标ID' }],
        UserId: [{ required: true, message: '请输入评论用户ID' }],
        Status: [{ required: true, message: '请选择审核状态' }]
      }
    }
  },
  methods: {
    getInitialEntity() {
      return {
        Status: 'pending',
        Content: '',
        TargetType: 'article',
        TargetId: null,
        UserId: null,
        ParentId: null,
        IpAddress: '',
        UserAgent: ''
      }
    },
    init() {
      this.visible = true
      this.entity = this.getInitialEntity()

      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
      })
    },

    openForm(id) {
      if (id) {
        // 编辑时：先显示对话框
        this.visible = true
        this.$nextTick(() => {
          this.$refs['form'].clearValidate()
        })
        
        // 获取数据后再设置entity
        GetTheData(id).then(resJson => {
          this.entity = {
            ...resJson.Data
          }
        })
      } else {
        // 新建时：正常初始化
        this.init()
      }
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

    handleSubmit() {
      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }

        this.confirmLoading = true
        const data = {
          ...this.entity
        }

        SaveData(data).then(resJson => {
          this.confirmLoading = false

          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.afterSubmit()
            this.visible = false
          } else {
            this.$message.error(resJson.Msg)
          }
        })
      })
    }
  }
}
</script>

<style scoped>
/* 评论编辑表单样式 */
</style>