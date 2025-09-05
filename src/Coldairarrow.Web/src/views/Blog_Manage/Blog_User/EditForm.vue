<template>
  <a-modal :title="entity.Id ? '编辑用户' : '新建用户'" :width="600" :visible="visible" :confirmLoading="confirmLoading"
    @ok="handleSubmit" @cancel="() => { this.visible = false }" :okText="entity.Id ? '保存' : '创建用户'">
    <a-spin :spinning="confirmLoading">
      <a-form-model ref="form" :model="entity" :rules="rules" v-bind="layout">
        <a-form-model-item label="用户名" prop="Username">
          <a-input v-model="entity.Username" placeholder="请输入用户名" />
        </a-form-model-item>

        <a-form-model-item label="昵称" prop="Nickname">
          <a-input v-model="entity.Nickname" placeholder="请输入昵称，留空则使用用户名" />
        </a-form-model-item>

        <a-form-model-item label="邮箱" prop="Email">
          <a-input v-model="entity.Email" placeholder="请输入邮箱地址" />
        </a-form-model-item>

        <a-form-model-item label="密码" prop="Password">
          <a-input-password v-model="entity.Password" :placeholder="entity.Id ? '留空表示不修改密码' : '请输入密码'" />
        </a-form-model-item>

        <a-form-model-item label="确认密码" prop="ConfirmPassword" v-if="entity.Password">
          <a-input-password v-model="entity.ConfirmPassword" placeholder="请再次输入密码确认" />
        </a-form-model-item>

        <a-form-model-item label="角色" prop="Role">
          <a-select v-model="entity.Role" placeholder="请选择用户角色">
            <a-select-option value="admin">管理员</a-select-option>
            <a-select-option value="user">普通用户</a-select-option>
          </a-select>
        </a-form-model-item>

        <a-form-model-item label="状态" prop="Status">
          <a-radio-group v-model="entity.Status">
            <a-radio :value="1">启用</a-radio>
            <a-radio :value="0">禁用</a-radio>
          </a-radio-group>
        </a-form-model-item>

        <a-form-model-item label="头像" prop="Avatar">
          <c-upload-img v-model="entity.Avatar" :maxCount="1"></c-upload-img>
        </a-form-model-item>
      </a-form-model>
    </a-spin>
  </a-modal>
</template>

<script>
import { GetTheData, SaveData, CheckUsernameExists, CheckEmailExists } from '@/api/blog_user'
import CUploadImg from '@/components/CUploadImg/CUploadImg'

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

      rules: {
        Username: [
          { required: true, message: '请输入用户名' },
          { min: 3, max: 50, message: '用户名长度在3-50个字符' },
          { validator: this.validateUsername }
        ],
        Email: [
          { type: 'email', message: '邮箱格式不正确' },
          { validator: this.validateEmail }
        ],
        Password: [
          { validator: this.validatePassword }
        ],
        ConfirmPassword: [
          { validator: this.validateConfirmPassword }
        ],
        Role: [
          { required: true, message: '请选择用户角色' }
        ]
      }
    }
  },
  methods: {
    getInitialEntity() {
      return {
        Username: '',
        Password: '',
        ConfirmPassword: '',
        Email: '',
        Nickname: '',
        Avatar: '',
        Role: 'user',
        Status: 1
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
        this.visible = true
        this.$nextTick(() => {
          this.$refs['form'].clearValidate()
        })
        
        GetTheData({ id }).then(resJson => {
          this.entity = {
            ...resJson.Data,
            Password: '',
            ConfirmPassword: ''
          }
        })
      } else {
        this.init()
      }
    },

    async validateUsername(rule, value, callback) {
      if (!value) {
        callback()
        return
      }

      try {
        const response = await CheckUsernameExists(value, this.entity.Id)
        if (response.Data) {
          callback(new Error('用户名已存在'))
        } else {
          callback()
        }
      } catch (error) {
        callback()
      }
    },

    async validateEmail(rule, value, callback) {
      if (!value) {
        callback()
        return
      }

      try {
        const response = await CheckEmailExists(value, this.entity.Id)
        if (response.Data) {
          callback(new Error('邮箱地址已存在'))
        } else {
          callback()
        }
      } catch (error) {
        callback()
      }
    },

    validatePassword(rule, value, callback) {
      if (!this.entity.Id && !value) {
        callback(new Error('新用户必须设置密码'))
        return
      }
      
      if (value && value.length < 6) {
        callback(new Error('密码长度至少6位'))
        return
      }
      
      callback()
    },

    validateConfirmPassword(rule, value, callback) {
      if (this.entity.Password && value !== this.entity.Password) {
        callback(new Error('两次输入的密码不一致'))
      } else {
        callback()
      }
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

        // 如果密码为空且是编辑模式，则不传密码字段
        if (this.entity.Id && !data.Password) {
          delete data.Password
        }
        delete data.ConfirmPassword

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
/* 用户编辑表单样式 */
</style>