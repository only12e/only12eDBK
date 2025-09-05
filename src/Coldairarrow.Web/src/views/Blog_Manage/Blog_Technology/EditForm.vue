<template>
  <a-modal :title="entity.Id ? '编辑技术栈' : '新建技术栈'" :width="'90%'" :visible="visible" :confirmLoading="confirmLoading"
    @ok="handleSubmit" @cancel="() => { this.visible = false }" :okText="entity.Id ? '保存' : '创建技术栈'">
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-row :gutter="24">
          <a-col :md="18" :sm="24">
            <a-form-model-item label="技术名称" prop="Name">
              <a-input v-model="entity.Name" placeholder="请输入技术名称" />
            </a-form-model-item>

            <a-form-model-item label="技术描述" prop="Description">
              <a-textarea v-model="entity.Description" placeholder="请输入技术描述，用于列表展示" :rows="3" />
            </a-form-model-item>

            <a-form-model-item label="技术详细介绍" prop="Content">
              <div id="editor" style="min-height: 400px; border: 1px solid #d9d9d9;"></div>
            </a-form-model-item>
          </a-col>

          <a-col :md="6" :sm="24">
            <a-card title="技术栈设置" size="small">
              <a-form-model-item label="技术分类" prop="Category" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-select v-model="entity.Category" placeholder="请选择分类">
                  <a-select-option value="frontend">前端</a-select-option>
                  <a-select-option value="backend">后端</a-select-option>
                  <a-select-option value="database">数据库</a-select-option>
                  <a-select-option value="devtools">开发工具</a-select-option>
                  <a-select-option value="other">其他</a-select-option>
                </a-select>
              </a-form-model-item>

              <a-form-model-item label="熟练度" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-slider v-model="entity.ProficiencyLevel" :min="0" :max="100" :marks="proficiencyMarks" />
                <div style="text-align: center; margin-top: 8px; font-size: 14px; font-weight: 500;">
                  {{ entity.ProficiencyLevel }}%
                </div>
              </a-form-model-item>

              <a-form-model-item label="推荐度" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-slider v-model="entity.RecommendationLevel" :min="0" :max="100" :marks="recommendationMarks" />
                <div style="text-align: center; margin-top: 8px; font-size: 14px; font-weight: 500;">
                  {{ entity.RecommendationLevel }}%
                </div>
              </a-form-model-item>

              <a-form-model-item label="排序权重" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-input-number v-model="entity.SortOrder" placeholder="数字越大越靠前" style="width: 100%" :min="0" />
              </a-form-model-item>

              <a-form-model-item :wrapperCol="{ offset: 2, span: 22 }">
                <a-checkbox v-model="entity.IsFeatured">
                  设为精选技术栈
                </a-checkbox>
              </a-form-model-item>

              <a-form-model-item label="技术Logo" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <c-upload-img v-model="entity.LogoUrl" :maxCount="1"></c-upload-img>
                <div style="margin-top: 4px; font-size: 12px; color: #999;">
                  建议上传32x32或更大的正方形图片
                </div>
              </a-form-model-item>
            </a-card>
          </a-col>
        </a-row>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import { GetTheData, SaveData } from '@/api/blog_technology'
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
      
      proficiencyMarks: {
        0: '新手',
        25: '入门',
        50: '熟练',
        75: '精通',
        100: '专家'
      },
      
      recommendationMarks: {
        0: '不推荐',
        25: '一般',
        50: '推荐',
        75: '强烈推荐',
        100: '必备'
      },

      rules: {
        Name: [{ required: true, message: '请输入技术名称' }],
        Description: [{ required: true, message: '请输入技术描述' }],
        Category: [{ required: true, message: '请选择技术分类' }]
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
        IsFeatured: false,
        SortOrder: 0,
        ProficiencyLevel: 0,
        RecommendationLevel: 0,
        Name: '',
        Description: '',
        Content: '',
        LogoUrl: '',
        Category: 'frontend'
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
/* 技术栈编辑表单样式 */
.ant-slider-mark-text {
  font-size: 12px;
}
</style>