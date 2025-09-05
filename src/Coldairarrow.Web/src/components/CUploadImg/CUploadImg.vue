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
        // 处理上传成功
        if (file.response && file.response.errno === 0) {
          // 新格式：wangEditor格式
          if (file.response.data && file.response.data.length > 0) {
            file.url = file.response.data[0]
          }
        } else if (file.response && file.response.url) {
          // 旧格式：兼容原有格式
          file.url = file.response.url
        }
      }

      if (file.status == 'done' || file.status == 'removed') {
        var urls = this.fileList.filter(x => x.status == 'done').map(x => x.url || (x.response && x.response.url))
        var newValue = this.maxCount == 1 ? (urls[0] || '') : urls
        this.internelValue = newValue
        //双向绑定
        this.$emit('input', newValue)
      } else if (file.status == 'error') {
        // 处理上传失败
        const errorMessage = file.response && file.response.message ? file.response.message : '文件上传失败'
        this.$message.error(errorMessage)
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