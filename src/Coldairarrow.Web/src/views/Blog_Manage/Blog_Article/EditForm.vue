<template>
  <a-modal :title="entity.Id ? '编辑文章' : '新建文章'" :width="'90%'" :visible="visible" :confirmLoading="confirmLoading"
    @ok="handleSubmit" @cancel="() => { this.visible = false }" :okText="entity.Id ? '保存' : '创建文章'">
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-row :gutter="24">
          <a-col :md="18" :sm="24">
            <a-form-model-item label="文章标题" prop="Title">
              <a-input v-model="entity.Title" placeholder="请输入文章标题" />
            </a-form-model-item>

            <a-form-model-item label="文章摘要" prop="Summary">
              <a-textarea v-model="entity.Summary" placeholder="请输入文章摘要，用于列表展示" :rows="3" />
            </a-form-model-item>

            <a-form-model-item label="文章内容" prop="Content">
              <div id="editor" style="min-height: 400px; border: 1px solid #d9d9d9;"></div>
            </a-form-model-item>
          </a-col>

          <a-col :md="6" :sm="24">
            <a-card title="发布设置" size="small">
              <a-form-model-item label="状态" prop="Status" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-select v-model="entity.Status">
                  <a-select-option value="draft">草稿</a-select-option>
                  <a-select-option value="published">发布</a-select-option>
                  <a-select-option value="hidden">隐藏</a-select-option>
                </a-select>
              </a-form-model-item>

              <a-form-model-item label="分类" prop="CategoryId" :labelCol="{ span: 8 }" :wrapperCol="{ span: 16 }">
                <a-select v-model="entity.CategoryId" placeholder="请选择分类">
                  <a-select-option v-for="item in categories" :key="item.Id" :value="item.Id">
                    {{ item.Name }}
                  </a-select-option>
                </a-select>
              </a-form-model-item>

              <a-form-model-item :wrapperCol="{ offset: 2, span: 22 }">
                <a-checkbox v-model="entity.IsFeatured">
                  设为精选文章
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
import { GetTheData, SaveData, GetCategories } from '@/api/blog_article'
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
      categories: [],
      editor: null,

      rules: {
        Title: [{ required: true, message: '请输入文章标题' }],
        Content: [{ required: true, message: '请输入文章内容' }]
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
        Status: 'draft',
        IsFeatured: false,
        Title: '',
        Summary: '',
        Content: '',
        CoverImage: '',
        CategoryId: null
      }
    },
    init() {
      this.visible = true
      this.entity = this.getInitialEntity()

      this.$nextTick(() => {
        this.$refs['form'].clearValidate()
        this.initEditor()
      })

      this.getCategories()
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

    getCategories() {
      GetCategories().then(resJson => {
        this.categories = resJson.Data || []
      })
    },

    openForm(id) {
      this.init()

      if (id) {
        GetTheData({ id }).then(resJson => {
          this.entity = {
            ...resJson.Data,
            IsFeatured: resJson.Data.IsFeatured === 1
          }

          // 设置编辑器内容
          if (this.editor && this.entity.Content) {
            this.editor.txt.html(this.entity.Content)
          }
        })
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
/* 文章编辑表单样式 */
</style>