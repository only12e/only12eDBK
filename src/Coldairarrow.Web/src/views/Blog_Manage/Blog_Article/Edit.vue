<!-- <template>
  <div>
    <a-card :bordered="false">
      <a-form :form="form">
        <a-row :gutter="24">
          <a-col :md="18" :sm="24">
            <a-form-item label="文章标题" :labelCol="{ span: 3 }" :wrapperCol="{ span: 21 }">
              <a-input v-decorator="['Title', { rules: [{ required: true, message: '请输入文章标题' }] }]"
                placeholder="请输入文章标题" />
            </a-form-item>

            <a-form-item label="文章摘要" :labelCol="{ span: 3 }" :wrapperCol="{ span: 21 }">
              <a-textarea v-decorator="['Summary']" placeholder="请输入文章摘要，用于列表展示" :rows="3" />
            </a-form-item>

            <a-form-item label="文章内容" :labelCol="{ span: 3 }" :wrapperCol="{ span: 21 }">
              <div id="editor" style="min-height: 500px; border: 1px solid #d9d9d9;"></div>
            </a-form-item>
          </a-col>

          <a-col :md="6" :sm="24">
            <a-card title="发布设置" size="small">
              <a-form-item label="状态">
                <a-select v-decorator="['Status', { initialValue: 'draft' }]">
                  <a-select-option value="draft">草稿</a-select-option>
                  <a-select-option value="published">发布</a-select-option>
                  <a-select-option value="hidden">隐藏</a-select-option>
                </a-select>
              </a-form-item>

              <a-form-item label="分类">
                <a-select v-decorator="['CategoryId']" placeholder="请选择分类">
                  <a-select-option v-for="item in categories" :key="item.Id" :value="item.Id">
                    {{ item.Name }}
                  </a-select-option>
                </a-select>
              </a-form-item>

              <a-form-item>
                <a-checkbox v-decorator="['IsFeatured', { valuePropName: 'checked', initialValue: false }]">
                  设为精选文章
                </a-checkbox>
              </a-form-item>

              <a-form-item label="封面图片">
                <a-upload name="file" listType="picture-card" class="avatar-uploader" :showUploadList="false"
                  :action="uploadUrl" :headers="uploadHeaders" :beforeUpload="beforeUpload" @change="handleCoverChange">
                  <img v-if="coverImage" :src="coverImage" alt="封面"
                    style="width: 100%; height: 100px; object-fit: cover;" />
                  <div v-else>
                    <a-icon :type="coverUploading ? 'loading' : 'plus'" />
                    <div class="ant-upload-text">上传封面</div>
                  </div>
                </a-upload>
              </a-form-item>
            </a-card>
          </a-col>
        </a-row>

        <a-form-item :wrapperCol="{ span: 24 }" style="text-align: center; margin-top: 20px;">
          <a-button type="primary" @click="handleSave" :loading="saving" style="margin-right: 8px;">
            保存
          </a-button>
          <a-button @click="handleSaveAndPublish" :loading="saving" style="margin-right: 8px;">
            保存并发布
          </a-button>
          <a-button @click="$router.go(-1)">
            取消
          </a-button>
        </a-form-item>
      </a-form>
    </a-card>
  </div>
</template>

<script>
import { GetTheData, SaveData, GetCategories } from '@/api/blog_article'
import TokenCache from '@/utils/cache/TokenCache'

// 引入wangEditor
import E from 'wangeditor'

export default {
  mounted() {
    this.initEditor()
    this.getCategories()

    // 如果有id参数，则为编辑模式
    const id = this.$route.query.id
    if (id) {
      this.isEdit = true
      this.getData(id)
    }
  },
  beforeDestroy() {
    // 销毁编辑器
    if (this.editor) {
      this.editor.destroy()
    }
  },
  data() {
    return {
      form: this.$form.createForm(this),
      isEdit: false,
      saving: false,
      categories: [],
      editor: null,
      content: '',

      // 图片上传
      coverImage: '',
      coverUploading: false,
      uploadUrl: '/Base_Manage/Upload/UploadImg',
      uploadHeaders: {
        Authorization: 'Bearer ' + TokenCache.getToken()
      }
    }
  },
  methods: {
    initEditor() {
      this.editor = new E('#editor')

      // 配置服务器端地址
      this.editor.customConfig.uploadImgServer = '/Base_Manage/Upload/UploadImg'
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

      // 自定义上传图片返回格式
      this.editor.customConfig.uploadImgHooks = {
        success: function (xhr, editor, result) {
          // 图片上传并返回结果，图片插入成功之后触发
          console.log('upload success', result)
        },
        fail: function (xhr, editor, result) {
          console.log('upload fail', result)
        },
        error: function (xhr, editor) {
          console.log('upload error', xhr)
        }
      }

      // 监听内容变化
      this.editor.customConfig.onchange = (html) => {
        this.content = html
      }

      this.editor.create()
    },
    getCategories() {
      GetCategories().then(resJson => {
        this.categories = resJson.Data || []
      })
    },
    getData(id) {
      GetTheData({ id }).then(resJson => {
        const data = resJson.Data
        this.form.setFieldsValue({
          Title: data.Title,
          Summary: data.Summary,
          CategoryId: data.CategoryId,
          Status: data.Status,
          IsFeatured: data.IsFeatured === 1
        })
        this.content = data.Content
        this.editor.txt.html(data.Content)
        this.coverImage = data.CoverImage
      })
    },
    handleSave() {
      this.form.validateFields((err, values) => {
        if (err) return

        this.saving = true
        const data = {
          ...values,
          Content: this.content,
          CoverImage: this.coverImage,
          IsFeatured: values.IsFeatured ? 1 : 0
        }

        if (this.isEdit) {
          data.Id = this.$route.query.id
        }

        SaveData(data).then(resJson => {
          this.saving = false
          this.$message.success('保存成功!')
          this.$router.push('/Blog_Manage/Blog_Article/List')
        }).catch(() => {
          this.saving = false
        })
      })
    },
    handleSaveAndPublish() {
      this.form.setFieldsValue({ Status: 'published' })
      this.handleSave()
    },

    // 封面上传
    beforeUpload(file) {
      const isImg = file.type.indexOf('image/') === 0
      if (!isImg) {
        this.$message.error('只能上传图片文件!')
      }
      const isLt2M = file.size / 1024 / 1024 < 2
      if (!isLt2M) {
        this.$message.error('图片大小不能超过2MB!')
      }
      return isImg && isLt2M
    },
    handleCoverChange(info) {
      if (info.file.status === 'uploading') {
        this.coverUploading = true
        return
      }
      if (info.file.status === 'done') {
        this.coverUploading = false
        if (info.file.response && info.file.response.Success) {
          this.coverImage = info.file.response.Data
          this.$message.success('上传成功!')
        } else {
          this.$message.error('上传失败!')
        }
      }
    }
  }
}
</script>

<style scoped>
.avatar-uploader .ant-upload {
  width: 100%;
  height: 100px;
}

.avatar-uploader .ant-upload-select-picture-card i {
  font-size: 32px;
  color: #999;
}

.avatar-uploader .ant-upload-select-picture-card .ant-upload-text {
  margin-top: 8px;
  color: #666;
}
</style> -->