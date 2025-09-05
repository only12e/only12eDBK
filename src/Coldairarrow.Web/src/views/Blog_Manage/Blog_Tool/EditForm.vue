<template>
  <a-modal :title="entity.Id ? '编辑工具' : '新建工具'" :width="'90%'" :visible="visible" :confirmLoading="confirmLoading"
    @ok="handleSubmit" @cancel="() => { this.visible = false }" :okText="entity.Id ? '保存' : '创建工具'">
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-row :gutter="24">
          <a-col :md="18" :sm="24">
            <a-form-model-item label="工具名称" prop="Name">
              <a-input v-model="entity.Name" placeholder="请输入工具名称" />
            </a-form-model-item>

            <a-form-model-item label="工具描述" prop="Description">
              <a-textarea v-model="entity.Description" placeholder="请输入工具描述，用于列表展示" :rows="3" />
            </a-form-model-item>

            <a-form-model-item label="工具详细介绍" prop="Content">
              <div id="editor" style="min-height: 400px; border: 1px solid #d9d9d9;"></div>
            </a-form-model-item>

            <a-row :gutter="16">
              <a-col :span="8">
                <a-form-model-item label="官方网站">
                  <a-input v-model="entity.OfficialUrl" placeholder="https://example.com" />
                </a-form-model-item>
              </a-col>
              <a-col :span="8">
                <a-form-model-item label="下载地址">
                  <a-input v-model="entity.DownloadUrl" placeholder="https://download.example.com" />
                </a-form-model-item>
              </a-col>
              <a-col :span="8">
                <a-form-model-item label="工具分类">
                  <a-input v-model="entity.Category" placeholder="如：编辑器、浏览器插件" />
                </a-form-model-item>
              </a-col>
            </a-row>
          </a-col>

          <a-col :md="6" :sm="24">
            <a-card title="工具设置" size="small">
              <a-form-model-item label="价格类型" prop="PriceType" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-select v-model="entity.PriceType">
                  <a-select-option value="free">免费</a-select-option>
                  <a-select-option value="paid">付费</a-select-option>
                  <a-select-option value="freemium">免费增值</a-select-option>
                </a-select>
              </a-form-model-item>

              <a-form-model-item label="个人评分" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-rate v-model="entity.Rating" allow-half />
                <div style="margin-top: 4px; font-size: 12px; color: #666;">{{ entity.Rating }} 分</div>
              </a-form-model-item>

              <a-form-model-item label="排序权重" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-input-number v-model="entity.SortOrder" placeholder="数字越大越靠前" style="width: 100%" :min="0" />
              </a-form-model-item>

              <a-form-model-item :wrapperCol="{ offset: 2, span: 22 }">
                <a-checkbox v-model="entity.IsRecommended">
                  设为推荐工具
                </a-checkbox>
              </a-form-model-item>

              <a-form-model-item label="封面图片" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <c-upload-img v-model="entity.CoverImage" :maxCount="1"></c-upload-img>
              </a-form-model-item>

              <a-form-model-item label="工具图标" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <c-upload-img v-model="entity.IconUrl" :maxCount="1"></c-upload-img>
              </a-form-model-item>
            </a-card>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import { GetTheData, SaveData } from '@/api/blog_tool'
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
        Name: [{ required: true, message: '请输入工具名称' }],
        Description: [{ required: true, message: '请输入工具描述' }]
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
        PriceType: 'free',
        IsRecommended: false,
        SortOrder: 0,
        Rating: 0,
        Name: '',
        Description: '',
        Content: '',
        CoverImage: '',
        IconUrl: '',
        OfficialUrl: '',
        DownloadUrl: '',
        Category: ''
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
            IsRecommended: resJson.Data.IsRecommended === 1
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
          IsRecommended: this.entity.IsRecommended ? 1 : 0
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
/* 工具编辑表单样式 */
</style>