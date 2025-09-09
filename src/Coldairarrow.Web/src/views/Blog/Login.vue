<template>
  <div class="blog-login">
    <div class="login-container">
      <div class="login-form-wrapper">
        <div class="login-header">
          <h1 class="login-title">登录博客</h1>
          <p class="login-subtitle">欢迎回来！请登录您的账户</p>
        </div>

        <a-form
          :form="form"
          class="login-form"
          @submit="handleSubmit">
          
          <a-form-item>
            <a-input
              v-decorator="[
                'username',
                { 
                  rules: [{ required: true, message: '请输入用户名或邮箱' }]
                }
              ]"
              size="large"
              placeholder="用户名或邮箱"
              :prefix="usernamePrefix">
              <a-icon slot="prefix" type="user" style="color: rgba(0,0,0,.25)" />
            </a-input>
          </a-form-item>

          <a-form-item>
            <a-input-password
              v-decorator="[
                'password',
                { 
                  rules: [{ required: true, message: '请输入密码' }]
                }
              ]"
              size="large"
              placeholder="密码">
              <a-icon slot="prefix" type="lock" style="color: rgba(0,0,0,.25)" />
            </a-input-password>
          </a-form-item>

          <a-form-item class="login-options">
            <a-checkbox
              v-decorator="[
                'rememberMe',
                {
                  valuePropName: 'checked',
                  initialValue: false,
                }
              ]">
              记住我
            </a-checkbox>
            <router-link to="/blog/forgot-password" class="forgot-password">忘记密码？</router-link>
          </a-form-item>

          <a-form-item>
            <a-button
              type="primary"
              htmlType="submit"
              size="large"
              class="login-button"
              :loading="loginLoading"
              block>
              登录
            </a-button>
          </a-form-item>

          <div class="register-link">
            还没有账户？
            <router-link to="/blog/register">立即注册</router-link>
          </div>
        </a-form>
      </div>

      <div class="login-decoration">
        <div class="decoration-content">
          <h2>欢迎来到我的技术博客</h2>
          <p>分享技术心得，记录成长足迹</p>
          <ul class="features-list">
            <li><a-icon type="check-circle" /> 最新技术文章</li>
            <li><a-icon type="check-circle" /> 实战项目分享</li>
            <li><a-icon type="check-circle" /> 开发工具推荐</li>
            <li><a-icon type="check-circle" /> 技术交流讨论</li>
          </ul>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogLogin',
  data() {
    return {
      loginLoading: false,
      form: this.$form.createForm(this)
    }
  },
  methods: {
    handleSubmit(e) {
      e.preventDefault()
      this.form.validateFields(async (err, values) => {
        if (!err) {
          this.loginLoading = true
          try {
            await this.login(values)
          } catch (error) {
            console.error('登录失败:', error)
          } finally {
            this.loginLoading = false
          }
        }
      })
    },

    async login(loginData) {
      try {
        // 调用登录API
        const response = await this.$http.post('/BlogApi/Login', {
          Username: loginData.username,
          Password: loginData.password,
          RememberMe: loginData.rememberMe
        })

        if (response.data.Success) {
          // 保存用户信息和token
          localStorage.setItem('blog_token', response.data.Token)
          localStorage.setItem('blog_user', JSON.stringify(response.data.User))
          
          this.$message.success('登录成功！')
          
          // 获取重定向地址
          const redirect = this.$route.query.redirect || '/blog/home'
          this.$router.push(redirect)
        } else {
          this.$message.error(response.data.Message || '登录失败')
        }
      } catch (error) {
        console.error('登录错误:', error)
        if (error.response && error.response.data && error.response.data.message) {
          this.$message.error(error.response.data.message)
        } else {
          this.$message.error('登录失败，请稍后重试')
        }
      }
    }
  },

  mounted() {
    // 如果已登录，直接跳转
    const token = localStorage.getItem('blog_token')
    if (token) {
      const redirect = this.$route.query.redirect || '/blog/home'
      this.$router.push(redirect)
    }
  }
}
</script>

<style lang="less" scoped>
.blog-login {
  min-height: 100vh;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  display: flex;
  align-items: center;
  justify-content: center;
  padding: 20px;
  
  .login-container {
    background: white;
    border-radius: 20px;
    overflow: hidden;
    box-shadow: 0 20px 60px rgba(0, 0, 0, 0.2);
    display: flex;
    max-width: 900px;
    width: 100%;
    min-height: 600px;
    
    .login-form-wrapper {
      flex: 1;
      padding: 60px 50px;
      display: flex;
      flex-direction: column;
      justify-content: center;
      
      .login-header {
        text-align: center;
        margin-bottom: 40px;
        
        .login-title {
          font-size: 32px;
          font-weight: 700;
          color: #2c3e50;
          margin-bottom: 10px;
        }
        
        .login-subtitle {
          color: #7f8c8d;
          font-size: 16px;
          margin: 0;
        }
      }
      
      .login-form {
        .ant-form-item {
          margin-bottom: 20px;
        }
        
        .login-options {
          display: flex;
          justify-content: space-between;
          align-items: center;
          margin-bottom: 30px;
          
          .forgot-password {
            color: #3498db;
            text-decoration: none;
            font-size: 14px;
            
            &:hover {
              color: #2980b9;
            }
          }
        }
        
        .login-button {
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
        
        .register-link {
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
    
    .login-decoration {
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
        right: -50%;
        width: 200%;
        height: 200%;
        background: radial-gradient(circle, rgba(255, 255, 255, 0.1) 0%, transparent 70%);
        animation: rotate 20s linear infinite;
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
        
        .features-list {
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
  .blog-login {
    padding: 10px;
    
    .login-container {
      flex-direction: column;
      max-width: 100%;
      min-height: auto;
      
      .login-form-wrapper {
        padding: 40px 30px;
        
        .login-header {
          .login-title {
            font-size: 24px;
          }
          
          .login-subtitle {
            font-size: 14px;
          }
        }
      }
      
      .login-decoration {
        min-height: 200px;
        
        .decoration-content {
          padding: 20px;
          
          h2 {
            font-size: 20px;
          }
          
          p {
            font-size: 14px;
          }
          
          .features-list {
            display: none;
          }
        }
      }
    }
  }
}
</style>