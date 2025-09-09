<template>
  <div class="clearfix">
    <a-upload
      :action="`${$rootUrl}/Base_Manage/Upload/UploadFileByForm`"
      listType="picture-card"
      :headers="headers"
      :fileList="fileList"
      @preview="handlePreview"
      @change="handleChange"
      accept="image/*"
      :multiple="this.multiple()"
    >
      <div v-if="fileList.length < maxCount">
        <a-icon type="plus" />
        <div class="ant-upload-text">选择</div>
      </div>
    </a-upload>
    <a-modal :visible="previewVisible" :footer="null" @cancel="handleCancel">
      <img alt="example" style="width: 100%" :src="previewImage" />
    </a-modal>
  </div>
</template>
<script>
import TypeHelper from '@/utils/helper/TypeHelper'
import TokenCache from '@/utils/cache/TokenCache'
const uuid = require('uuid')

export default {
  props: {
    value: '', //字符串或字符串数组
    maxCount: {
      type: Number,
      default: 1
    }
  },
  mounted() {
    // 不要直接修改 props，让父组件保持数据控制权
    this.checkType(this.value)
    this.refresh()
  },
  data() {
    return {
      previewVisible: false,
      previewImage: '',
      fileList: [],
      internelValue: {},
      headers: { Authorization: 'Bearer ' + TokenCache.getToken() },
    }
  },
  watch: {
    value(val) {
      //内部触发事件不处理,仅回传数据
      if (val == this.internelValue) {
        return
      }

      this.checkType(val)
      
      // 不要直接修改 props
      this.refresh()
    }
  },
  methods: {
    multiple() {
      return this.maxCount > 1
    },
    checkType(val) {
      // 如果值为 null 或 undefined，不进行类型检查
      if (val == null) {
        return
      }
      
      if (this.maxCount == 1 && TypeHelper.isArray(val)) {
        throw 'maxCount=1时model不能为Array'
      }
      if (this.maxCount > 1 && !TypeHelper.isArray(val)) {
        throw 'maxCount>1时model必须为Array<String>'
      }
    },
    refresh() {
      if (this.maxCount < 1) {
        throw 'maxCount必须>=1'
      }
      
      // 清空 fileList
      this.fileList = []
      
      if (this.value) {
        let urls = []
        if (TypeHelper.isString(this.value)) {
          urls.push(this.value)
        } else if (TypeHelper.isArray(this.value)) {
          urls.push(...this.value)
        } else {
          throw 'value必须为字符串或数组'
        }

        this.fileList = urls.map(x => {
          return { name: x, uid: uuid.v4(), status: 'done', url: x }
        })
      }
    },
    handleCancel() {
      this.previewVisible = false
    },
    handlePreview(file) {
      this.previewImage = file.url || file.thumbUrl
      this.previewVisible = true
    },
    handleChange({ file, fileList }) {
      this.fileList = fileList

      if (file.status == 'done') {
        this.handleUploadSuccess(file)
      } else if (file.status == 'error') {
        this.handleUploadError(file)
      } else if (file.status == 'removed') {
        // 文件被移除时触发URL更新
        this.updateUrlValues()
      }

      // 更新URL值（上传完成或文件被移除时）
      if (file.status == 'done' || file.status == 'removed') {
        this.updateUrlValues()
      }
    },

    handleUploadSuccess(file) {
      let extractedUrl = null
      
      try {
        // 处理不同格式的返回数据
        if (file.response && file.response.errno === 0) {
          // wangEditor格式 {errno: 0, data: [...]}
          if (file.response.data && Array.isArray(file.response.data) && file.response.data.length > 0) {
            extractedUrl = file.response.data[0]
          } else if (file.response.url) {
            extractedUrl = file.response.url
          }
        } else if (file.response && file.response.url) {
          // 直接URL格式 {url: "..."}
          extractedUrl = file.response.url
        } else if (file.response && file.response.Success) {
          // 统一返回格式 {Success: true, Data: {...}}
          if (file.response.Data && typeof file.response.Data === 'object') {
            if (file.response.Data.url) {
              extractedUrl = file.response.Data.url
            } else if (file.response.Data.data && Array.isArray(file.response.Data.data)) {
              extractedUrl = file.response.Data.data[0]
            } else if (file.response.Data.errno === 0 && file.response.Data.data) {
              extractedUrl = file.response.Data.data[0]
            }
          } else if (typeof file.response.Data === 'string') {
            extractedUrl = file.response.Data
          }
        }

        // 验证提取的URL
        if (extractedUrl && typeof extractedUrl === 'string' && extractedUrl.trim()) {
          file.url = extractedUrl.trim()
          // 同时更新fileList中对应的文件对象
          const fileInList = this.fileList.find(f => f.uid === file.uid)
          if (fileInList) {
            fileInList.url = extractedUrl.trim()
          }
          this.$message.success('图片上传成功')
        } else {
          throw new Error('未能从服务器响应中提取到有效的图片URL')
        }
      } catch (error) {
        console.error('处理上传成功响应时发生错误:', error, file.response)
        this.$message.error(`图片上传处理失败: ${error.message}`)
      }
    },

    handleUploadError(file) {
      let errorMessage = '图片上传失败'
      
      try {
        if (file.response) {
          if (file.response.message) {
            errorMessage = file.response.message
          } else if (file.response.Msg) {
            errorMessage = file.response.Msg
          } else if (file.response.errno === 1 && file.response.message) {
            errorMessage = file.response.message
          } else if (typeof file.response === 'string') {
            errorMessage = file.response
          }
        } else if (file.error) {
          errorMessage = file.error.message || file.error.toString()
        }
      } catch (error) {
        console.error('处理上传错误响应时发生异常:', error)
        errorMessage = '图片上传失败，请重试'
      }

      console.error('图片上传失败:', {
        fileName: file.name,
        fileSize: file.size,
        response: file.response,
        error: file.error
      })
      
      this.$message.error(errorMessage)
    },

    updateUrlValues() {
      try {
        const urls = this.fileList
          .filter(x => x.status == 'done')
          .map(x => {
            // 优先使用已经设置的file.url
            if (x.url && typeof x.url === 'string') {
              return x.url.trim()
            }
            
            // 备用方案：从response中提取URL
            if (x.response && x.response.url && typeof x.response.url === 'string') {
              return x.response.url.trim()
            }
            
            return null
          })
          .filter(url => url && url !== '') // 过滤掉空值

        const newValue = this.maxCount == 1 ? (urls[0] || '') : urls
        this.internelValue = newValue
        
        // 双向绑定
        this.$emit('input', newValue)
        
      } catch (error) {
        console.error('更新URL值时发生错误:', error)
        this.$message.error('处理图片数据时发生错误')
      }
    }
  }
}
</script>
<style>
/* you can make up upload button and sample style by using stylesheets */
.ant-upload-select-picture-card i {
  font-size: 32px;
  color: #999;
}

.ant-upload-select-picture-card .ant-upload-text {
  margin-top: 8px;
  color: #666;
}
</style>