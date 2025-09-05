<template>
  <a-modal :title="entity.Id ? '编辑项目' : '新建项目'" :width="'90%'" :visible="visible" :confirmLoading="confirmLoading"
    @ok="handleSubmit" @cancel="() => { this.visible = false }" :okText="entity.Id ? '保存' : '创建项目'">
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-row :gutter="24">
          <a-col :md="18" :sm="24">
            <a-form-model-item label="项目名称" prop="Name">
              <a-input v-model="entity.Name" placeholder="请输入项目名称" />
            </a-form-model-item>

            <a-form-model-item label="项目简介" prop="Description">
              <a-textarea v-model="entity.Description" placeholder="请输入项目简介，用于列表展示" :rows="3" />
            </a-form-model-item>

            <a-form-model-item label="项目详细说明" prop="Content">
              <div id="editor" style="min-height: 400px; border: 1px solid #d9d9d9;"></div>
            </a-form-model-item>

            <a-row :gutter="16">
              <a-col :span="8">
                <a-form-model-item label="演示地址">
                  <a-input v-model="entity.DemoUrl" placeholder="https://demo.example.com" />
                </a-form-model-item>
              </a-col>
              <a-col :span="8">
                <a-form-model-item label="GitHub地址">
                  <a-input v-model="entity.GithubUrl" placeholder="https://github.com/user/repo" />
                </a-form-model-item>
              </a-col>
              <a-col :span="8">
                <a-form-model-item label="相关网站">
                  <a-input v-model="entity.WebsiteUrl" placeholder="https://website.com" />
                </a-form-model-item>
              </a-col>
            </a-row>
          </a-col>

          <a-col :md="6" :sm="24">
            <a-card title="项目设置" size="small">
              <a-form-model-item label="项目状态" prop="Status" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-select v-model="entity.Status">
                  <a-select-option value="active">进行中</a-select-option>
                  <a-select-option value="completed">已完成</a-select-option>
                  <a-select-option value="archived">已归档</a-select-option>
                </a-select>
              </a-form-model-item>

              <a-form-model-item label="难度级别" prop="DifficultyLevel" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-select v-model="entity.DifficultyLevel" placeholder="请选择难度">
                  <a-select-option value="beginner">初级</a-select-option>
                  <a-select-option value="intermediate">中级</a-select-option>
                  <a-select-option value="advanced">高级</a-select-option>
                  <a-select-option value="expert">专家</a-select-option>
                </a-select>
              </a-form-model-item>

              <a-form-model-item label="排序权重" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-input-number v-model="entity.SortOrder" placeholder="数字越大越靠前" style="width: 100%" :min="0" />
              </a-form-model-item>

              <a-form-model-item :wrapperCol="{ offset: 2, span: 22 }">
                <a-checkbox v-model="entity.IsFeatured">
                  设为精选项目
                </a-checkbox>
              </a-form-model-item>

              <a-form-model-item label="封面图片" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <c-upload-img v-model="entity.CoverImage" :maxCount="1"></c-upload-img>
              </a-form-model-item>
            </a-card>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import { GetTheData, SaveData } from '@/api/blog_project'
import CUploadImg from '@/components/CUploadImg/CUploadImg'
import TokenCache from '@/utils/cache/TokenCache'

// 引入wangEditor
import E from 'wangeditor'

export default {
  components: {
    CUploadImg
  },
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
      editor: null,

      rules: {
        Name: [{ required: true, message: '请输入项目名称' }],
        Description: [{ required: true, message: '请输入项目简介' }],
        Content: [{ required: true, message: '请输入项目详细说明' }]
      }
    }
  },
  beforeDestroy() {
    // 销毁编辑器
    if (this.editor && typeof this.editor.destroy === 'function') {
      this.editor.destroy()
      this.editor = null
    }
  },
  methods: {
    getInitialEntity() {
      return {
        Status: 'active',
        IsFeatured: false,
        SortOrder: 0,
        Name: '',
        Description: '',
        Content: '',
        CoverImage: '',
        DemoUrl: '',
        GithubUrl: '',
        WebsiteUrl: '',
        DifficultyLevel: 'intermediate'
      }
    },
    init() {
      this.visible = true
      this.entity = this.getInitialEntity()

      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
        this.initEditor()
      })
    },

    initEditor() {
      // 销毁已存在的编辑器
      if (this.editor && typeof this.editor.destroy === 'function') {
        this.editor.destroy()
        this.editor = null
      }

      this.editor = new E('#editor')

      // 配置服务器端地址
      this.editor.customConfig.uploadImgServer = `${this.$rootUrl}/Base_Manage/Upload/UploadFileByForm`
      this.editor.customConfig.uploadImgHeaders = {
        Authorization: 'Bearer ' + TokenCache.getToken()
      }
      this.editor.customConfig.uploadFileName = 'file'
      this.editor.customConfig.uploadImgMaxSize = 5 * 1024 * 1024 // 5M
      this.editor.customConfig.uploadImgMaxLength = 10
      this.editor.customConfig.uploadImgTimeout = 60000
      
      // 配置上传图片的钩子函数
      this.editor.customConfig.uploadImgHooks = {
        before: function (xhr, editor, files) {
        },
        success: function (xhr, editor, result) {
          
        },
        fail: function (xhr, editor, result) {
          editor.$message && editor.$message.error('图片上传失败')
        },
        error: function (xhr, editor) {
          editor.$message && editor.$message.error('图片上传出错')
        },
        timeout: function (xhr, editor) {
          editor.$message && editor.$message.error('图片上传超时')
        }
      }

      // 配置菜单
      this.editor.customConfig.menus = [
        'head',
        'bold',
        'italic',
        'underline',
        'strikeThrough',
        'foreColor',
        'backColor',
        'link',
        'list',
        'justify',
        'quote',
        'emoticon',
        'image',
        'table',
        'video',
        'code',
        'undo',
        'redo'
      ]

      // 监听内容变化
      this.editor.customConfig.onchange = (html) => {
        this.entity.Content = html
      }

      this.editor.create()

      // 确保编辑器内容为空
      this.editor.txt.html('')
    },

    openForm(id) {
      if (id) {
        // 编辑时：先显示对话框和初始化编辑器，但不重置entity
        this.visible = true
        this.$nextTick(() => {
          this.$refs['form'].clearValidate()
          this.initEditor()
        })
        
        // 获取数据后再设置entity
        GetTheData(id).then(resJson => {
          this.entity = {
            ...resJson.Data,
            IsFeatured: resJson.Data.IsFeatured === 1
          }

          // 设置编辑器内容
          if (this.editor && this.entity.Content) {
            this.editor.txt.html(this.entity.Content)
          }
        })
      } else {
        // 新建时：正常初始化
        this.init()
      }
    },

    // 用于List组件批量更新状态
    updateStatus(id, status) {
      return GetTheData(id).then(resJson => {
        const data = {
          ...resJson.Data,
          Status: status,
          IsFeatured: resJson.Data.IsFeatured === 1 ? 1 : 0
        }
        return SaveData(data)
      })
    },

    handleSubmit() {
      // 确保内容同步
      if (this.editor) {
        this.entity.Content = this.editor.txt.html()
      }

      this.$refs['form'].validate(valid => {
        if (!valid) {
          return
        }

        this.confirmLoading = true
        const data = {
          ...this.entity,
          IsFeatured: this.entity.IsFeatured ? 1 : 0
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
/* 项目编辑表单样式 */
</style>