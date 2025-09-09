<template>
  <div class="blog-contact">
    <div class="contact-header">
      <h1 class="page-title">联系方式</h1>
      <p class="page-subtitle">有任何问题或建议，欢迎随时与我联系</p>
    </div>

    <div class="contact-content">
      <div class="contact-methods">
        <div class="method-card email">
          <div class="method-icon">
            <a-icon type="mail" />
          </div>
          <h3 class="method-title">邮箱联系</h3>
          <p class="method-description">最便捷的联系方式，通常24小时内回复</p>
          <a href="mailto:contact@example.com" class="method-link">
            contact@example.com
            <a-icon type="arrow-right" />
          </a>
        </div>

        <div class="method-card github">
          <div class="method-icon">
            <a-icon type="github" />
          </div>
          <h3 class="method-title">GitHub</h3>
          <p class="method-description">查看我的开源项目和代码贡献</p>
          <a href="https://github.com/username" target="_blank" class="method-link">
            github.com/username
            <a-icon type="arrow-right" />
          </a>
        </div>

        <div class="method-card wechat">
          <div class="method-icon">
            <a-icon type="wechat" />
          </div>
          <h3 class="method-title">微信</h3>
          <p class="method-description">扫码添加好友，技术交流更便捷</p>
          <div class="qr-code">
            <img src="/assets/wechat-qr.png" alt="微信二维码" />
            <p>扫码添加微信</p>
          </div>
        </div>

        <div class="method-card linkedin">
          <div class="method-icon">
            <a-icon type="linkedin" />
          </div>
          <h3 class="method-title">LinkedIn</h3>
          <p class="method-description">职业社交网络，了解我的工作经历</p>
          <a href="https://linkedin.com/in/username" target="_blank" class="method-link">
            linkedin.com/in/username
            <a-icon type="arrow-right" />
          </a>
        </div>
      </div>

      <div class="contact-form-section">
        <h2 class="form-title">在线留言</h2>
        <p class="form-subtitle">填写下面的表单，我会尽快回复您</p>
        
        <a-form
          :form="form"
          class="contact-form"
          @submit="handleSubmit">
          
          <div class="form-row">
            <a-form-item class="form-item-half">
              <a-input
                v-decorator="[
                  'name',
                  { 
                    rules: [{ required: true, message: '请输入您的姓名' }]
                  }
                ]"
                size="large"
                placeholder="您的姓名">
                <a-icon slot="prefix" type="user" />
              </a-input>
            </a-form-item>
            
            <a-form-item class="form-item-half">
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
                <a-icon slot="prefix" type="mail" />
              </a-input>
            </a-form-item>
          </div>

          <a-form-item>
            <a-input
              v-decorator="[
                'subject',
                { 
                  rules: [{ required: true, message: '请输入主题' }]
                }
              ]"
              size="large"
              placeholder="主题">
              <a-icon slot="prefix" type="book" />
            </a-input>
          </a-form-item>

          <a-form-item>
            <a-textarea
              v-decorator="[
                'message',
                { 
                  rules: [{ required: true, message: '请输入留言内容' }]
                }
              ]"
              :rows="6"
              placeholder="请输入您的留言内容..."
              class="message-textarea" />
          </a-form-item>

          <a-form-item class="submit-section">
            <a-button
              type="primary"
              htmlType="submit"
              size="large"
              class="submit-button"
              :loading="submitting"
              block>
              <a-icon type="send" />
              发送留言
            </a-button>
          </a-form-item>
        </a-form>
      </div>

      <div class="faq-section">
        <h2 class="faq-title">常见问题</h2>
        <a-collapse expand-icon-position="right">
          <a-collapse-panel key="1" header="如何快速获得回复？">
            <p>通过邮箱联系是最快的方式，我通常在24小时内回复邮件。请在邮件中详细描述您的问题。</p>
          </a-collapse-panel>
          
          <a-collapse-panel key="2" header="可以讨论技术合作吗？">
            <p>当然可以！我很乐意参与有趣的技术项目。请通过邮箱详细介绍项目情况和合作方式。</p>
          </a-collapse-panel>
          
          <a-collapse-panel key="3" header="提供技术咨询服务吗？">
            <p>是的，我提供前端、后端和全栈开发的技术咨询服务。具体详情请通过邮箱联系。</p>
          </a-collapse-panel>
          
          <a-collapse-panel key="4" header="如何获取源代码？">
            <p>大部分项目的源代码都托管在GitHub上，您可以访问我的GitHub主页查看和下载。</p>
          </a-collapse-panel>
        </a-collapse>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogContact',
  data() {
    return {
      form: this.$form.createForm(this),
      submitting: false
    }
  },
  methods: {
    handleSubmit(e) {
      e.preventDefault()
      this.form.validateFields(async (err, values) => {
        if (!err) {
          this.submitting = true
          try {
            await this.submitMessage(values)
          } catch (error) {
            console.error('发送留言失败:', error)
          } finally {
            this.submitting = false
          }
        }
      })
    },

    async submitMessage(messageData) {
      try {
        // 这里调用API发送留言
        // const response = await this.$http.post('/BlogApi/SubmitMessage', messageData)
        
        // 模拟发送成功
        await new Promise(resolve => setTimeout(resolve, 2000))
        
        this.$message.success('留言发送成功！我会尽快回复您')
        this.form.resetFields()
      } catch (error) {
        console.error('发送留言错误:', error)
        this.$message.error('留言发送失败，请稍后重试')
      }
    }
  }
}
</script>

<style lang="less" scoped>
.blog-contact {
  .contact-header {
    text-align: center;
    margin-bottom: 60px;
    
    .page-title {
      font-size: 36px;
      font-weight: 700;
      color: #2c3e50;
      margin-bottom: 15px;
    }
    
    .page-subtitle {
      font-size: 16px;
      color: #7f8c8d;
      margin: 0;
    }
  }
  
  .contact-content {
    .contact-methods {
      display: grid;
      grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
      gap: 25px;
      margin-bottom: 80px;
      
      .method-card {
        background: white;
        padding: 30px;
        border-radius: 15px;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
        text-align: center;
        transition: all 0.3s ease;
        position: relative;
        overflow: hidden;
        
        &::before {
          content: '';
          position: absolute;
          top: 0;
          left: 0;
          right: 0;
          height: 4px;
          background: linear-gradient(45deg, #667eea, #764ba2);
        }
        
        &:hover {
          transform: translateY(-5px);
          box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
        }
        
        .method-icon {
          width: 80px;
          height: 80px;
          border-radius: 50%;
          background: linear-gradient(45deg, #667eea, #764ba2);
          display: flex;
          align-items: center;
          justify-content: center;
          margin: 0 auto 20px;
          
          .anticon {
            font-size: 36px;
            color: white;
          }
        }
        
        .method-title {
          font-size: 20px;
          font-weight: 600;
          color: #2c3e50;
          margin-bottom: 10px;
        }
        
        .method-description {
          color: #7f8c8d;
          margin-bottom: 20px;
          line-height: 1.6;
        }
        
        .method-link {
          color: #667eea;
          text-decoration: none;
          font-weight: 500;
          display: inline-flex;
          align-items: center;
          gap: 8px;
          transition: all 0.3s ease;
          
          &:hover {
            color: #5a6fd8;
            transform: translateX(5px);
          }
        }
        
        .qr-code {
          img {
            width: 120px;
            height: 120px;
            border-radius: 8px;
            margin-bottom: 10px;
          }
          
          p {
            color: #667eea;
            font-weight: 500;
            margin: 0;
          }
        }
        
        &.email .method-icon {
          background: linear-gradient(45deg, #3498db, #2980b9);
        }
        
        &.github .method-icon {
          background: linear-gradient(45deg, #2c3e50, #34495e);
        }
        
        &.wechat .method-icon {
          background: linear-gradient(45deg, #2ecc71, #27ae60);
        }
        
        &.linkedin .method-icon {
          background: linear-gradient(45deg, #0077b5, #005885);
        }
      }
    }
    
    .contact-form-section {
      background: white;
      padding: 50px;
      border-radius: 20px;
      box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
      margin-bottom: 60px;
      
      .form-title {
        font-size: 28px;
        font-weight: 700;
        color: #2c3e50;
        text-align: center;
        margin-bottom: 10px;
      }
      
      .form-subtitle {
        color: #7f8c8d;
        text-align: center;
        margin-bottom: 40px;
      }
      
      .contact-form {
        max-width: 600px;
        margin: 0 auto;
        
        .form-row {
          display: flex;
          gap: 20px;
          
          .form-item-half {
            flex: 1;
          }
        }
        
        .ant-form-item {
          margin-bottom: 25px;
        }
        
        .message-textarea {
          resize: vertical;
        }
        
        .submit-section {
          text-align: center;
          margin-top: 30px;
          
          .submit-button {
            height: 50px;
            font-size: 16px;
            font-weight: 600;
            background: linear-gradient(45deg, #667eea, #764ba2);
            border: none;
            border-radius: 25px;
            max-width: 300px;
            
            &:hover, &:focus {
              background: linear-gradient(45deg, #5a6fd8, #6a4190);
            }
            
            .anticon {
              margin-right: 8px;
            }
          }
        }
      }
    }
    
    .faq-section {
      .faq-title {
        font-size: 28px;
        font-weight: 700;
        color: #2c3e50;
        text-align: center;
        margin-bottom: 40px;
      }
      
      :deep(.ant-collapse) {
        background: white;
        border-radius: 15px;
        border: none;
        box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
        
        .ant-collapse-item {
          border-bottom: 1px solid #f1f2f6;
          
          &:last-child {
            border-bottom: none;
          }
          
          .ant-collapse-header {
            padding: 20px 25px;
            font-size: 16px;
            font-weight: 500;
            color: #2c3e50;
            
            &:hover {
              background: #f8f9fa;
            }
          }
          
          .ant-collapse-content {
            border-top: none;
            
            .ant-collapse-content-box {
              padding: 0 25px 20px;
              color: #7f8c8d;
              line-height: 1.6;
            }
          }
        }
      }
    }
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-contact {
    .contact-header {
      .page-title {
        font-size: 28px;
      }
    }
    
    .contact-content {
      .contact-methods {
        grid-template-columns: 1fr;
        
        .method-card {
          padding: 25px;
        }
      }
      
      .contact-form-section {
        padding: 30px 20px;
        
        .contact-form {
          .form-row {
            flex-direction: column;
            gap: 0;
          }
          
          .submit-section {
            .submit-button {
              max-width: 100%;
            }
          }
        }
      }
    }
  }
}
</style>