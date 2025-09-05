<template>
  <a-modal
    title="友情链接"
    :width="800"
    :visible="visible"
    :maskClosable="false"
    @ok="handleSubmit"
    @cancel="handleCancel"
    :confirmLoading="loading">
    
    <a-form-model
      :model="form"
      :rules="rules"
      ref="form"
      :labelCol="labelCol"
      :wrapperCol="wrapperCol">

      <a-form-model-item
        label="链接名称"
        prop="Name">
        <a-input
          v-model="form.Name"
          placeholder="请输入链接名称"
          allowClear />
      </a-form-model-item>

      <a-form-model-item
        label="链接地址"
        prop="Url">
        <a-input
          v-model="form.Url"
          placeholder="请输入链接地址，如：https://www.example.com"
          allowClear />
      </a-form-model-item>

      <a-form-model-item
        label="Logo图片"
        prop="LogoUrl">
        <c-upload-file v-model="form.LogoUrl" :listType="'picture'"></c-upload-file>
        <div class="ant-form-explain">
          建议尺寸：200x200像素，支持png、jpg、gif格式
        </div>
      </a-form-model-item>

      <a-form-model-item
        label="链接描述"
        prop="Description">
        <a-textarea
          v-model="form.Description"
          placeholder="请输入链接描述"
          :rows="4"
          allowClear />
      </a-form-model-item>

      <a-row>
        <a-col :span="12">
          <a-form-model-item
            label="状态"
            prop="Status"
            :labelCol="{ span: 10 }"
            :wrapperCol="{ span: 14 }">
            <a-radio-group v-model="form.Status">
              <a-radio :value="1">启用</a-radio>
              <a-radio :value="0">禁用</a-radio>
            </a-radio-group>
          </a-form-model-item>
        </a-col>
        <a-col :span="12">
          <a-form-model-item
            label="排序权重"
            prop="SortOrder"
            :labelCol="{ span: 10 }"
            :wrapperCol="{ span: 14 }">
            <a-input-number
              v-model="form.SortOrder"
              placeholder="数值越大越靠前"
              :min="0"
              :max="9999"
              style="width: 100%;" />
          </a-form-model-item>
        </a-col>
      </a-row>

    </a-form-model>
  </a-modal>
</template>

<script>
import { SaveData, GetTheData, NameExists, UrlExists } from '@/api/blog_friendlylink'
import CUploadFile from '@/components/CUploadFile/CUploadFile'

export default {
  components: {
    CUploadFile
  },
  props: {
    afterSubmit: Function
  },
  data() {
    return {
      visible: false,
      loading: false,
      form: {},
      labelCol: { span: 5 },
      wrapperCol: { span: 18 },
      rules: {
        Name: [
          { required: true, message: '链接名称不能为空', trigger: 'blur' },
          { max: 100, message: '链接名称长度不能超过100个字符', trigger: 'blur' },
          {
            validator: this.validateName,
            trigger: 'blur'
          }
        ],
        Url: [
          { required: true, message: '链接地址不能为空', trigger: 'blur' },
          { max: 255, message: '链接地址长度不能超过255个字符', trigger: 'blur' },
          { type: 'url', message: '请输入有效的URL地址', trigger: 'blur' },
          {
            validator: this.validateUrl,
            trigger: 'blur'
          }
        ],
        LogoUrl: [
          { max: 255, message: 'Logo图片URL长度不能超过255个字符', trigger: 'blur' }
        ],
        Description: [
          { max: 500, message: '链接描述长度不能超过500个字符', trigger: 'blur' }
        ],
        Status: [
          { required: true, message: '请选择状态', trigger: 'change' }
        ],
        SortOrder: [
          { type: 'number', min: 0, max: 9999, message: '排序权重范围为0-9999', trigger: 'blur' }
        ]
      }
    }
  },
  methods: {
    init() {
      this.form = {
        Status: 1,
        SortOrder: 0
      }
      this.resetForm()
    },
    resetForm() {
      this.$nextTick(() => {
        this.$refs.form && this.$refs.form.resetFields()
      })
    },
    openForm(id) {
      this.visible = true
      this.$nextTick(() => {
        this.init()
        if (id) {
          this.getTheData(id)
        }
      })
    },
    async validateName(rule, value, callback) {
      if (!value) {
        callback()
        return
      }

      try {
        const exists = await NameExists(value, this.form.Id)
        if (exists) {
          callback(new Error('链接名称已存在'))
        } else {
          callback()
        }
      } catch (error) {
        callback()
      }
    },
    async validateUrl(rule, value, callback) {
      if (!value) {
        callback()
        return
      }

      try {
        const exists = await UrlExists(value, this.form.Id)
        if (exists) {
          callback(new Error('链接地址已存在'))
        } else {
          callback()
        }
      } catch (error) {
        callback()
      }
    },
    getTheData(id) {
      this.loading = true
      GetTheData({ id }).then(resJson => {
        this.loading = false
        if (resJson.Success) {
          this.form = { ...resJson.Data }
        } else {
          this.$message.error(resJson.Msg || '获取数据失败')
        }
      })
    },
    handleSubmit() {
      this.$refs.form.validate(valid => {
        if (!valid) return

        this.loading = true
        SaveData(this.form).then(resJson => {
          this.loading = false
          if (resJson.Success) {
            this.$message.success('操作成功!')
            this.visible = false
            this.afterSubmit && this.afterSubmit()
          } else {
            this.$message.error(resJson.Msg || '操作失败')
          }
        })
      })
    },
    handleCancel() {
      this.visible = false
    }
  }
}
</script>