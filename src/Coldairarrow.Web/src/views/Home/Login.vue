<template>
  <div class="main">
    <a-spin :spinning="loading">
      <a-form id="formLogin" class="user-layout-login" ref="formLogin" :form="form" @submit="handleSubmit">
        <a-form-item>
          <a-input size="large" type="text" placeholder="请输入用户名" v-decorator="['userName', {
            rules: [{ required: true, message: '请输入用户名' }],
            validateTrigger: ['change', 'blur']
          }]">
            <a-icon slot="prefix" type="user" :style="{ color: 'rgba(0,0,0,.25)' }" />
          </a-input>
        </a-form-item>

        <a-form-item>
          <a-input size="large" type="password" autocomplete="false" placeholder="请输入密码" v-decorator="['password', {
            rules: [{ required: true, message: '请输入密码' }],
            validateTrigger: ['change', 'blur']
          }]">
            <a-icon slot="prefix" type="lock" :style="{ color: 'rgba(0,0,0,.25)' }" />
          </a-input>
        </a-form-item>
        <a-form-item>
          <a-checkbox v-decorator="['savePwd', { valuePropName: 'checked' }]">记住密码</a-checkbox>
        </a-form-item>

        <a-form-item style="margin-top:24px">
          <a-button size="large" type="primary" htmlType="submit" class="login-button">确定</a-button>
        </a-form-item>
      </a-form>
    </a-spin>
  </div>
</template>

<script>
import TokenCache from '@/utils/cache/TokenCache'

export default {
  data() {
    return {
      loading: false,
      form: this.$form.createForm(this)
    }
  },
  mounted() {
    var userName = localStorage.getItem('userName')
    var password = localStorage.getItem('password')
    if (userName && password) {
      this.form.setFieldsValue({ userName, password, savePwd: true })
    }
  },
  methods: {
    handleSubmit(e) {
      e.preventDefault()
      this.handleLogin()
    },
    handleLogin() {
      this.form.validateFields(['userName', 'password', 'savePwd'], (errors, values) => {
        //校验成功
        if (!errors) {
          var values = this.form.getFieldsValue()
          this.loading = true
          this.$http.post('/Base_Manage/Home/SubmitLogin', values).then(resJson => {
            this.loading = false

            if (resJson.Success) {
              TokenCache.setToken(resJson.Data)
              //保存密码
              if (values['savePwd']) {
                localStorage.setItem('userName', values['userName'])
                localStorage.setItem('password', values['password'])
              } else {
                localStorage.removeItem('userName')
                localStorage.removeItem('password')
              }
              this.$message.success('欢迎您！')
              // 跳转到管理系统仪表板
              this.$router.push({ path: '/Blog_Manage/Blog_Statistics/Dashboard' })
            } else {
              this.$message.error(resJson.Msg)
            }
          })
        }
      })
    }
  }
}
</script>

<style lang="less" scoped>
.user-layout-login {
  label {
    font-size: 14px;
  }

  .forge-password {
    font-size: 14px;
  }

  button.login-button {
    padding: 0 15px;
    font-size: 16px;
    height: 40px;
    width: 100%;
  }

  .user-login-other {
    text-align: left;
    margin-top: 24px;
    line-height: 22px;

    .item-icon {
      font-size: 24px;
      color: rgba(0, 0, 0, 0.2);
      margin-left: 16px;
      vertical-align: middle;
      cursor: pointer;
      transition: color 0.3s;

      &:hover {
        color: #1890ff;
      }
    }

    .register {
      float: right;
    }
  }
}
</style>
