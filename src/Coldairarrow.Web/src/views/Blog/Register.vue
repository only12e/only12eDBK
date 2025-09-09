<template>
  <div class="blog-register">
    <div class="register-container">
      <div class="register-form-wrapper">
        <div class="register-header">
          <h1 class="register-title">注册博客账户</h1>
          <p class="register-subtitle">加入我们，开始您的技术交流之旅</p>
        </div>

        <a-form
          :form="form"
          class="register-form"
          @submit="handleSubmit">
          
          <a-form-item>
            <a-input
              v-decorator="[
                'username',
                { 
                  rules: [
                    { required: true, message: '请输入用户名' },
                    { min: 3, max: 20, message: '用户名长度为3-20个字符' }
                  ]
                }
              ]"
              size="large"
              placeholder="用户名">
              <a-icon slot="prefix" type="user" style="color: rgba(0,0,0,.25)" />
            </a-input>
          </a-form-item>

          <a-form-item>
            <a-input
              v-decorator="[
                'email',
                { 
                  rules: [
                    { required: true, message: '请输入邮箱地址' },
                    { type: 'email', message: '请输入有效的邮箱地址' }
                  ]
                }
              ]"
              size="large"
              placeholder="邮箱地址">
              <a-icon slot="prefix" type="mail" style="color: rgba(0,0,0,.25)" />
            </a-input>
          </a-form-item>

          <a-form-item>
            <a-input-password
              v-decorator="[
                'password',
                { 
                  rules: [
                    { required: true, message: '请输入密码' },
                    { min: 8, message: '密码长度至少8位' }
                  ]
                }
              ]"
              size="large"
              placeholder="密码">
              <a-icon slot="prefix" type="lock" style="color: rgba(0,0,0,.25)" />
            </a-input-password>
          </a-form-item>

          <a-form-item>
            <a-input-password
              v-decorator="[
                'confirmPassword',
                { 
                  rules: [
                    { required: true, message: '请确认密码' },
                    { validator: this.validateConfirmPassword }
                  ]
                }
              ]"
              size="large"
              placeholder="确认密码">
              <a-icon slot="prefix" type="lock" style="color: rgba(0,0,0,.25)" />
            </a-input-password>
          </a-form-item>

          <div class="name-row">
            <a-form-item class="name-item">
              <a-input
                v-decorator="['firstName']"
                size="large"
                placeholder="名字（可选）">
              </a-input>
            </a-form-item>
            
            <a-form-item class="name-item">
              <a-input
                v-decorator="['lastName']"
                size="large"
                placeholder="姓氏（可选）">
              </a-input>
            </a-form-item>
          </div>

          <a-form-item class="register-options">
            <a-checkbox
              v-decorator="[
                'agree',
                {
                  valuePropName: 'checked',
                  rules: [{ required: true, message: '请同意服务条款' }]
                }
              ]">
              我已阅读并同意 <a href="#" @click.prevent>服务条款</a> 和 <a href="#" @click.prevent>隐私政策</a>
            </a-checkbox>
          </a-form-item>

          <a-form-item>
            <a-button
              type="primary"
              htmlType="submit"
              size="large"
              class="register-button"
              :loading="registerLoading"
              block>
              注册账户
            </a-button>
          </a-form-item>

          <div class="login-link">
            已有账户？
            <router-link to="/blog/login">立即登录</router-link>
          </div>
        </a-form>
      </div>

      <div class="register-decoration">
        <div class="decoration-content">
          <h2>开始您的技术之旅</h2>
          <p>注册后您可以享受更多服务</p>
          <ul class="benefits-list">
            <li><a-icon type="check-circle" /> 发表评论交流</li>
            <li><a-icon type="check-circle" /> 收藏喜欢的文章</li>
            <li><a-icon type="check-circle" /> 获得最新技术资讯</li>
            <li><a-icon type="check-circle" /> 参与技术讨论</li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogRegister',
  data() {
    return {
      registerLoading: false,
      form: this.$form.createForm(this)
    }
  },
  methods: {
    validateConfirmPassword(rule, value, callback) {
      const { form } = this
      if (value && value !== form.getFieldValue('password')) {
        callback('两次输入的密码不一致')
      } else {
        callback()
      }
    },

    handleSubmit(e) {
      e.preventDefault()
      this.form.validateFields(async (err, values) => {
        if (!err) {
          this.registerLoading = true
          try {
            await this.register(values)
          } catch (error) {
            console.error('注册失败:', error)
          } finally {
            this.registerLoading = false
          }
        }
      })
    },

    async register(registerData) {
      try {
        const response = await this.$http.post('/BlogApi/Register', {
          Username: registerData.username,
          Email: registerData.email,
          Password: registerData.password,
          ConfirmPassword: registerData.confirmPassword,
          FirstName: registerData.firstName,
          LastName: registerData.lastName
        })

        if (response.data.Success) {
          this.$message.success('注册成功！请登录您的账户')
          this.$router.push('/blog/login')
        } else {
          this.$message.error(response.data.Message || '注册失败')
        }
      } catch (error) {
        console.error('注册错误:', error)
        if (error.response && error.response.data && error.response.data.message) {
          this.$message.error(error.response.data.message)
        } else {
          this.$message.error('注册失败，请稍后重试')
        }
      }
    }
  }
}
</script>

<style lang="less" scoped>
.blog-register {
  min-height: 100vh;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 20px;
  
  .register-container {
    background: white;
    border-radius: 20px;
    overflow: hidden;
    box-shadow: 0 20px 60px rgba(0, 0, 0, 0.2);
    display: flex;
    max-width: 1000px;
    width: 100%;
    min-height: 700px;
    
    .register-form-wrapper {
      flex: 1;
      padding: 60px 50px;
      display: flex;
      flex-direction: column;
      justify-content: center;
      
      .register-header {
        text-align: center;
        margin-bottom: 40px;
        
        .register-title {
          font-size: 32px;
          font-weight: 700;
          color: #2c3e50;
          margin-bottom: 10px;
        }
        
        .register-subtitle {
          color: #7f8c8d;
          font-size: 16px;
          margin: 0;
        }
      }
      
      .register-form {
        .ant-form-item {
          margin-bottom: 20px;
        }
        
        .name-row {
          display: flex;
          gap: 15px;
          
          .name-item {
            flex: 1;
          }
        }
        
        .register-options {
          margin-bottom: 30px;
          
          .ant-checkbox-wrapper {
            color: #7f8c8d;
            font-size: 14px;
            
            a {
              color: #3498db;
              
              &:hover {
                color: #2980b9;
              }
            }
          }
        }
        
        .register-button {
          height: 50px;
          font-size: 16px;
          font-weight: 600;
          background: linear-gradient(45deg, #667eea, #764ba2);
          border: none;
          border-radius: 25px;
          margin-bottom: 20px;
          
          &:hover, &:focus {
            background: linear-gradient(45deg, #5a6fd8, #6a4190);
          }
        }
        
        .login-link {
          text-align: center;
          color: #7f8c8d;
          font-size: 14px;
          
          a {
            color: #3498db;
            text-decoration: none;
            font-weight: 500;
            
            &:hover {
              color: #2980b9;
            }
          }
        }
      }
    }
    
    .register-decoration {
      flex: 1;
      background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
      display: flex;
      align-items: center;
      justify-content: center;
      color: white;
      position: relative;
      overflow: hidden;
      
      &::before {
        content: '';
        position: absolute;
        top: -50%;
        left: -50%;
        width: 200%;
        height: 200%;
        background: radial-gradient(circle, rgba(255, 255, 255, 0.1) 0%, transparent 70%);
        animation: rotate 25s linear infinite;
      }
      
      .decoration-content {
        text-align: center;
        position: relative;
        z-index: 2;
        padding: 40px;
        
        h2 {
          font-size: 28px;
          font-weight: 700;
          margin-bottom: 15px;
          text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
        }
        
        p {
          font-size: 16px;
          margin-bottom: 30px;
          opacity: 0.9;
        }
        
        .benefits-list {
          list-style: none;
          padding: 0;
          margin: 0;
          
          li {
            display: flex;
            align-items: center;
            justify-content: center;
            gap: 10px;
            margin-bottom: 15px;
            font-size: 16px;
            
            .anticon {
              color: #2ecc71;
              font-size: 18px;
            }
          }
        }
      }
    }
  }
}

@keyframes rotate {
  from {
    transform: rotate(0deg);
  }
  to {
    transform: rotate(360deg);
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-register {
    padding: 10px;
    
    .register-container {
      flex-direction: column;
      max-width: 100%;
      min-height: auto;
      
      .register-form-wrapper {
        padding: 40px 30px;
        
        .register-header {
          .register-title {
            font-size: 24px;
          }
          
          .register-subtitle {
            font-size: 14px;
          }
        }
        
        .register-form {
          .name-row {
            flex-direction: column;
            gap: 0;
          }
        }
      }
      
      .register-decoration {
        min-height: 200px;
        
        .decoration-content {
          padding: 20px;
          
          h2 {
            font-size: 20px;
          }
          
          p {
            font-size: 14px;
          }
          
          .benefits-list {
            display: none;
          }
        }
      }
    }
  }
}
</style>