<template>
  <a-modal title="系统配置" :width="800" :visible="visible" :maskClosable="false" @ok="handleSubmit" @cancel="handleCancel"
    :confirmLoading="loading">

    <a-form-model :model="form" :rules="rules" ref="form" :labelCol="labelCol" :wrapperCol="wrapperCol">

      <a-form-model-item label="配置键名" prop="ConfigKey">
        <a-input v-model="form.ConfigKey" placeholder="请输入配置键名" :disabled="!!form.Id" allowClear />
        <div class="ant-form-explain">
          配置键名一旦创建不能修改，请仔细填写
        </div>
      </a-form-model-item>

      <a-form-model-item label="配置值" prop="ConfigValue">
        <a-textarea v-model="form.ConfigValue" placeholder="请输入配置值" :rows="4" allowClear />
      </a-form-model-item>

      <a-form-model-item label="配置说明" prop="Description">
        <a-textarea v-model="form.Description" placeholder="请输入配置说明" :rows="3" allowClear />
      </a-form-model-item>

    </a-form-model>
  </a-modal>
</template>

<script>
import { SaveData, GetTheData, ConfigKeyExists } from '@/api/blog_systemconfig'

export default {
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
        ConfigKey: [
          { required: true, message: '配置键名不能为空', trigger: 'blur' },
          { max: 100, message: '配置键名长度不能超过100个字符', trigger: 'blur' },
          {
            validator: this.validateConfigKey,
            trigger: 'blur'
          }
        ],
        Description: [
          { max: 255, message: '配置说明长度不能超过255个字符', trigger: 'blur' }
        ]
      }
    }
  },
  methods: {
    init() {
      this.form = {}
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
    async validateConfigKey(rule, value, callback) {
      if (!value) {
        callback()
        return
      }

      try {
        const exists = await ConfigKeyExists(value, this.form.Id)
        if (exists) {
          callback(new Error('配置键名已存在'))
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