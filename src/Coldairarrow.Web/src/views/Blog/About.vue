<template>
  <div class="blog-about">
    <div class="about-hero">
      <div class="hero-content">
        <div class="avatar-section">
          <img src="/assets/profile-avatar.jpg" alt="头像" class="profile-avatar" />
        </div>
        <h1 class="hero-title">关于我</h1>
        <p class="hero-subtitle">一个热爱技术的全栈开发工程师</p>
      </div>
    </div>

    <div class="about-content">
      <section class="intro-section">
        <h2 class="section-title">个人简介</h2>
        <div class="intro-text">
          <p>
            你好！我是一名充满热情的全栈开发工程师，拥有多年的软件开发经验。
            我专注于现代Web技术栈的开发，包括前端的Vue.js、React以及后端的.NET Core、Node.js等技术。
          </p>
          <p>
            我相信技术能够改变世界，通过代码可以创造出有价值的产品。我喜欢学习新技术，
            分享开发经验，并且积极参与开源社区的建设。
          </p>
          <p>
            这个博客是我记录技术心得、分享项目经验、推荐好用工具的地方。
            希望我的分享能够对你有所帮助！
          </p>
        </div>
      </section>

      <section class="skills-section">
        <h2 class="section-title">技能专长</h2>
        <div class="skills-grid">
          <div class="skill-category">
            <h3 class="category-title">
              <a-icon type="code" />
              前端开发
            </h3>
            <div class="skills-list">
              <div class="skill-item">
                <span class="skill-name">Vue.js</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 90%"></div>
                </div>
                <span class="skill-level">90%</span>
              </div>
              <div class="skill-item">
                <span class="skill-name">React</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 85%"></div>
                </div>
                <span class="skill-level">85%</span>
              </div>
              <div class="skill-item">
                <span class="skill-name">TypeScript</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 80%"></div>
                </div>
                <span class="skill-level">80%</span>
              </div>
            </div>
          </div>

          <div class="skill-category">
            <h3 class="category-title">
              <a-icon type="database" />
              后端开发
            </h3>
            <div class="skills-list">
              <div class="skill-item">
                <span class="skill-name">.NET Core</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 88%"></div>
                </div>
                <span class="skill-level">88%</span>
              </div>
              <div class="skill-item">
                <span class="skill-name">Node.js</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 75%"></div>
                </div>
                <span class="skill-level">75%</span>
              </div>
              <div class="skill-item">
                <span class="skill-name">MySQL</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 82%"></div>
                </div>
                <span class="skill-level">82%</span>
              </div>
            </div>
          </div>

          <div class="skill-category">
            <h3 class="category-title">
              <a-icon type="cloud" />
              DevOps
            </h3>
            <div class="skills-list">
              <div class="skill-item">
                <span class="skill-name">Docker</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 78%"></div>
                </div>
                <span class="skill-level">78%</span>
              </div>
              <div class="skill-item">
                <span class="skill-name">Git</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 92%"></div>
                </div>
                <span class="skill-level">92%</span>
              </div>
              <div class="skill-item">
                <span class="skill-name">CI/CD</span>
                <div class="skill-bar">
                  <div class="skill-progress" style="width: 70%"></div>
                </div>
                <span class="skill-level">70%</span>
              </div>
            </div>
          </div>
        </div>
      </section>

      <section class="timeline-section">
        <h2 class="section-title">职业历程</h2>
        <div class="timeline">
          <div class="timeline-item">
            <div class="timeline-dot"></div>
            <div class="timeline-content">
              <h3 class="timeline-title">高级全栈工程师</h3>
              <p class="timeline-company">科技公司 • 2022年至今</p>
              <p class="timeline-description">
                负责公司核心产品的架构设计和开发，带领团队完成多个重要项目，
                提升了系统性能和用户体验。
              </p>
            </div>
          </div>
          <div class="timeline-item">
            <div class="timeline-dot"></div>
            <div class="timeline-content">
              <h3 class="timeline-title">全栈开发工程师</h3>
              <p class="timeline-company">互联网公司 • 2020-2022年</p>
              <p class="timeline-description">
                参与多个Web应用的开发，熟练掌握前后端技术栈，
                具备独立完成项目的能力。
              </p>
            </div>
          </div>
          <div class="timeline-item">
            <div class="timeline-dot"></div>
            <div class="timeline-content">
              <h3 class="timeline-title">前端开发工程师</h3>
              <p class="timeline-company">软件公司 • 2018-2020年</p>
              <p class="timeline-description">
                专注于前端开发，使用Vue.js和React构建用户界面，
                积累了丰富的前端开发经验。
              </p>
            </div>
          </div>
        </div>
      </section>

      <section class="contact-section">
        <h2 class="section-title">联系方式</h2>
        <div class="contact-grid">
          <div class="contact-item">
            <a-icon type="mail" class="contact-icon" />
            <div class="contact-info">
              <h4>邮箱</h4>
              <p>contact@example.com</p>
            </div>
          </div>
          <div class="contact-item">
            <a-icon type="github" class="contact-icon" />
            <div class="contact-info">
              <h4>GitHub</h4>
              <p>github.com/username</p>
            </div>
          </div>
          <div class="contact-item">
            <a-icon type="linkedin" class="contact-icon" />
            <div class="contact-info">
              <h4>LinkedIn</h4>
              <p>linkedin.com/in/username</p>
            </div>
          </div>
          <div class="contact-item">
            <a-icon type="wechat" class="contact-icon" />
            <div class="contact-info">
              <h4>微信</h4>
              <p>WeChat_ID</p>
            </div>
          </div>
        </div>
      </section>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogAbout'
}
</script>

<style lang="less" scoped>
.blog-about {
  .about-hero {
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    color: white;
    padding: 80px 0;
    margin: -40px -20px 60px;
    text-align: center;
    
    .hero-content {
      .avatar-section {
        margin-bottom: 30px;
        
        .profile-avatar {
          width: 120px;
          height: 120px;
          border-radius: 50%;
          border: 4px solid rgba(255, 255, 255, 0.3);
          object-fit: cover;
        }
      }
      
      .hero-title {
        font-size: 42px;
        font-weight: 700;
        margin-bottom: 15px;
        text-shadow: 0 2px 4px rgba(0, 0, 0, 0.3);
      }
      
      .hero-subtitle {
        font-size: 18px;
        opacity: 0.9;
        margin: 0;
      }
    }
  }
  
  .about-content {
    .section-title {
      font-size: 28px;
      font-weight: 700;
      color: #2c3e50;
      margin-bottom: 30px;
      text-align: center;
      position: relative;
      
      &::after {
        content: '';
        position: absolute;
        bottom: -10px;
        left: 50%;
        transform: translateX(-50%);
        width: 50px;
        height: 3px;
        background: linear-gradient(45deg, #667eea, #764ba2);
        border-radius: 2px;
      }
    }
    
    .intro-section {
      margin-bottom: 80px;
      
      .intro-text {
        max-width: 800px;
        margin: 0 auto;
        
        p {
          font-size: 16px;
          line-height: 1.8;
          color: #5a6c7d;
          margin-bottom: 20px;
          text-align: justify;
        }
      }
    }
    
    .skills-section {
      margin-bottom: 80px;
      
      .skills-grid {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
        gap: 40px;
        
        .skill-category {
          background: white;
          padding: 30px;
          border-radius: 15px;
          box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
          
          .category-title {
            font-size: 20px;
            font-weight: 600;
            color: #2c3e50;
            margin-bottom: 25px;
            display: flex;
            align-items: center;
            gap: 10px;
            
            .anticon {
              font-size: 24px;
              color: #667eea;
            }
          }
          
          .skills-list {
            .skill-item {
              margin-bottom: 20px;
              
              .skill-name {
                display: block;
                font-weight: 500;
                color: #2c3e50;
                margin-bottom: 8px;
              }
              
              .skill-bar {
                width: 100%;
                height: 8px;
                background: #f1f2f6;
                border-radius: 4px;
                overflow: hidden;
                position: relative;
                
                .skill-progress {
                  height: 100%;
                  background: linear-gradient(45deg, #667eea, #764ba2);
                  border-radius: 4px;
                  transition: width 1s ease;
                }
              }
              
              .skill-level {
                font-size: 14px;
                color: #7f8c8d;
                float: right;
                margin-top: 5px;
              }
            }
          }
        }
      }
    }
    
    .timeline-section {
      margin-bottom: 80px;
      
      .timeline {
        max-width: 800px;
        margin: 0 auto;
        position: relative;
        
        &::before {
          content: '';
          position: absolute;
          left: 30px;
          top: 0;
          bottom: 0;
          width: 2px;
          background: linear-gradient(to bottom, #667eea, #764ba2);
        }
        
        .timeline-item {
          position: relative;
          margin-bottom: 40px;
          padding-left: 80px;
          
          .timeline-dot {
            position: absolute;
            left: 21px;
            top: 5px;
            width: 18px;
            height: 18px;
            border-radius: 50%;
            background: linear-gradient(45deg, #667eea, #764ba2);
            border: 3px solid white;
            box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
          }
          
          .timeline-content {
            background: white;
            padding: 25px;
            border-radius: 15px;
            box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
            
            .timeline-title {
              font-size: 18px;
              font-weight: 600;
              color: #2c3e50;
              margin-bottom: 5px;
            }
            
            .timeline-company {
              color: #667eea;
              font-weight: 500;
              margin-bottom: 10px;
            }
            
            .timeline-description {
              color: #7f8c8d;
              line-height: 1.6;
              margin: 0;
            }
          }
        }
      }
    }
    
    .contact-section {
      .contact-grid {
        display: grid;
        grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
        gap: 25px;
        max-width: 800px;
        margin: 0 auto;
        
        .contact-item {
          background: white;
          padding: 25px;
          border-radius: 15px;
          box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
          display: flex;
          align-items: center;
          gap: 20px;
          transition: all 0.3s ease;
          
          &:hover {
            transform: translateY(-5px);
            box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
          }
          
          .contact-icon {
            font-size: 32px;
            color: #667eea;
          }
          
          .contact-info {
            h4 {
              font-size: 16px;
              font-weight: 600;
              color: #2c3e50;
              margin: 0 0 5px 0;
            }
            
            p {
              color: #7f8c8d;
              margin: 0;
              font-size: 14px;
            }
          }
        }
      }
    }
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .blog-about {
    .about-hero {
      padding: 60px 20px;
      margin: -40px -15px 40px;
      
      .hero-content {
        .hero-title {
          font-size: 32px;
        }
        
        .hero-subtitle {
          font-size: 16px;
        }
      }
    }
    
    .about-content {
      .skills-section {
        .skills-grid {
          grid-template-columns: 1fr;
          gap: 25px;
          
          .skill-category {
            padding: 20px;
          }
        }
      }
      
      .timeline-section {
        .timeline {
          &::before {
            left: 20px;
          }
          
          .timeline-item {
            padding-left: 60px;
            
            .timeline-dot {
              left: 11px;
            }
            
            .timeline-content {
              padding: 20px;
            }
          }
        }
      }
      
      .contact-section {
        .contact-grid {
          grid-template-columns: 1fr;
        }
      }
    }
  }
}
</style>