<template>
  <div class="welcome-container">
    <!-- 动态背景 -->
    <div class="animated-bg">
      <div class="floating-shapes">
        <div class="shape shape-1"></div>
        <div class="shape shape-2"></div>
        <div class="shape shape-3"></div>
        <div class="shape shape-4"></div>
        <div class="shape shape-5"></div>
        <div class="shape shape-6"></div>
      </div>
    </div>

    <!-- 主要内容区域 -->
    <div class="welcome-content">
      <!-- 顶部欢迎区域 -->
      <div class="hero-section">
        <div class="hero-content">
          <div class="welcome-badge">
            <a-icon type="crown" class="crown-icon" />
            <span>欢迎来到</span>
          </div>
          
          <h1 class="main-title">
            <span class="title-text" data-text="博客管理系统">博客管理系统</span>
          </h1>
          
          <p class="subtitle">
            一个现代化、功能丰富的内容管理平台
            <br />
            让创作变得简单，让管理变得高效
          </p>
          
          <div class="stats-row">
            <div class="stat-item" v-for="(stat, index) in stats" :key="index" :style="{animationDelay: (index * 0.2) + 's'}">
              <div class="stat-icon">
                <a-icon :type="stat.icon" />
              </div>
              <div class="stat-content">
                <div class="stat-number">{{ stat.number }}</div>
                <div class="stat-label">{{ stat.label }}</div>
              </div>
            </div>
          </div>
          
          <div class="action-buttons">
            <a-button type="primary" size="large" class="primary-btn" @click="handleStartExploring">
              <a-icon type="rocket" />
              开始探索
            </a-button>
            <a-button size="large" class="secondary-btn" @click="handleLearnMore">
              <a-icon type="book" />
              了解更多
            </a-button>
          </div>
        </div>
        
        <!-- 装饰元素 -->
        <div class="decoration-elements">
          <div class="glow-orb glow-orb-1"></div>
          <div class="glow-orb glow-orb-2"></div>
          <div class="glow-orb glow-orb-3"></div>
        </div>
      </div>

      <!-- 功能特性区域 -->
      <div class="features-section">
        <div class="section-header">
          <h2 class="section-title">核心功能</h2>
          <p class="section-subtitle">为现代内容创作者量身打造</p>
        </div>
        
        <div class="features-grid">
          <div 
            class="feature-card" 
            v-for="(feature, index) in features" 
            :key="index"
            :style="{animationDelay: (index * 0.1 + 0.5) + 's'}"
            @mouseenter="handleFeatureHover(index)"
            @mouseleave="handleFeatureLeave(index)">
            <div class="feature-icon">
              <a-icon :type="feature.icon" />
            </div>
            <h3 class="feature-title">{{ feature.title }}</h3>
            <p class="feature-description">{{ feature.description }}</p>
            <div class="feature-decoration"></div>
          </div>
        </div>
      </div>

      <!-- 技术栈展示 -->
      <div class="tech-section">
        <div class="section-header">
          <h2 class="section-title">技术栈</h2>
          <p class="section-subtitle">基于现代化技术构建</p>
        </div>
        
        <div class="tech-stack">
          <div class="tech-item" v-for="(tech, index) in techStack" :key="index" :style="{animationDelay: (index * 0.1) + 's'}">
            <div class="tech-logo">
              <img :src="tech.logo" :alt="tech.name" v-if="tech.logo" />
              <a-icon :type="tech.icon" v-else />
            </div>
            <span class="tech-name">{{ tech.name }}</span>
          </div>
        </div>
      </div>

      <!-- 底部CTA区域 -->
      <div class="cta-section">
        <div class="cta-content">
          <h2 class="cta-title">准备开始你的创作之旅？</h2>
          <p class="cta-subtitle">立即体验强大的内容管理功能</p>
          <a-button type="primary" size="large" class="cta-button" @click="handleGetStarted">
            <a-icon type="thunderbolt" />
            立即开始
          </a-button>
        </div>
        <div class="cta-decoration">
          <div class="pulse-ring"></div>
          <div class="pulse-ring pulse-ring-delay"></div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'Introduce',
  data() {
    return {
      stats: [
        { icon: 'file-text', number: '1000+', label: '文章管理' },
        { icon: 'project', number: '500+', label: '项目展示' },
        { icon: 'tool', number: '200+', label: '实用工具' },
        { icon: 'code', number: '50+', label: '技术栈' }
      ],
      features: [
        {
          icon: 'edit',
          title: '富文本编辑',
          description: '支持Markdown语法，所见即所得的编辑体验'
        },
        {
          icon: 'dashboard',
          title: '数据统计',
          description: '全面的数据分析和可视化图表展示'
        },
        {
          icon: 'team',
          title: '用户管理',
          description: '完善的权限控制和用户角色管理系统'
        },
        {
          icon: 'cloud',
          title: '云端同步',
          description: '数据安全备份，多设备无缝同步'
        },
        {
          icon: 'mobile',
          title: '响应式设计',
          description: '完美适配各种设备，随时随地管理内容'
        },
        {
          icon: 'api',
          title: 'API接口',
          description: '开放的API接口，支持第三方集成开发'
        }
      ],
      techStack: [
        { name: 'Vue.js', icon: 'code' },
        { name: 'Ant Design', icon: 'ant-design' },
        { name: '.NET Core', icon: 'dot-chart' },
        { name: 'ECharts', icon: 'bar-chart' },
        { name: 'Element UI', icon: 'layout' },
        { name: 'MySQL', icon: 'database' }
      ]
    }
  },
  mounted() {
    this.initAnimations()
  },
  methods: {
    initAnimations() {
      setTimeout(() => {
        document.querySelector('.welcome-content').classList.add('animate-in')
      }, 100)
    },
    
    handleStartExploring() {
      this.$message.success('开始探索功能！')
      this.$router.push('/Blog_Manage/Blog_Statistics/Dashboard')
    },
    
    handleLearnMore() {
      this.$message.info('了解更多信息！')
    },
    
    handleGetStarted() {
      this.$message.success('欢迎开始使用！')
      this.$router.push('/Blog_Manage/Blog_Article/List')
    },
    
    handleFeatureHover(index) {
      // 可以添加特定的悬停效果
    },
    
    handleFeatureLeave(index) {
      // 可以添加特定的离开效果
    }
  }
}
</script>

<style lang="less" scoped>
.welcome-container {
  min-height: 100vh;
  position: relative;
  overflow: hidden;
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
}

// 动态背景
.animated-bg {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  z-index: 1;
  
  .floating-shapes {
    position: relative;
    width: 100%;
    height: 100%;
    
    .shape {
      position: absolute;
      border-radius: 50%;
      background: rgba(255, 255, 255, 0.1);
      backdrop-filter: blur(10px);
      animation: float 20s infinite linear;
      
      &.shape-1 {
        width: 200px;
        height: 200px;
        top: 10%;
        left: 10%;
        animation-delay: 0s;
      }
      
      &.shape-2 {
        width: 150px;
        height: 150px;
        top: 20%;
        right: 15%;
        animation-delay: -5s;
      }
      
      &.shape-3 {
        width: 100px;
        height: 100px;
        top: 60%;
        left: 5%;
        animation-delay: -10s;
      }
      
      &.shape-4 {
        width: 180px;
        height: 180px;
        bottom: 20%;
        right: 10%;
        animation-delay: -15s;
      }
      
      &.shape-5 {
        width: 120px;
        height: 120px;
        top: 50%;
        left: 50%;
        animation-delay: -3s;
      }
      
      &.shape-6 {
        width: 80px;
        height: 80px;
        bottom: 10%;
        left: 30%;
        animation-delay: -8s;
      }
    }
  }
}

@keyframes float {
  0%, 100% {
    transform: translateY(0px) rotate(0deg);
    opacity: 0.7;
  }
  50% {
    transform: translateY(-20px) rotate(180deg);
    opacity: 1;
  }
}

// 主要内容区域
.welcome-content {
  position: relative;
  z-index: 2;
  padding: 0;
  opacity: 0;
  transform: translateY(30px);
  transition: all 0.8s ease;
  
  &.animate-in {
    opacity: 1;
    transform: translateY(0);
  }
}

// 英雄区域
.hero-section {
  display: flex;
  align-items: center;
  justify-content: center;
  min-height: 100vh;
  padding: 60px 40px;
  text-align: center;
  position: relative;
  
  .hero-content {
    max-width: 800px;
    margin: 0 auto;
    color: white;
  }
  
  .welcome-badge {
    display: inline-flex;
    align-items: center;
    gap: 8px;
    background: rgba(255, 255, 255, 0.15);
    backdrop-filter: blur(10px);
    padding: 8px 20px;
    border-radius: 50px;
    font-size: 14px;
    margin-bottom: 30px;
    animation: slideInDown 0.8s ease;
    
    .crown-icon {
      color: #ffd700;
      font-size: 16px;
    }
  }
  
  .main-title {
    font-size: 64px;
    font-weight: 800;
    margin: 0 0 20px 0;
    line-height: 1.1;
    animation: slideInUp 0.8s ease 0.2s both;
    
    .title-text {
      position: relative;
      display: inline-block;
      background: linear-gradient(45deg, #fff, #a8edea);
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      
      &::before {
        content: attr(data-text);
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: linear-gradient(45deg, #ff6b6b, #4ecdc4);
        -webkit-background-clip: text;
        -webkit-text-fill-color: transparent;
        background-clip: text;
        animation: shimmer 3s ease-in-out infinite;
        opacity: 0;
      }
    }
  }
  
  .subtitle {
    font-size: 18px;
    line-height: 1.6;
    margin-bottom: 40px;
    opacity: 0.9;
    animation: slideInUp 0.8s ease 0.4s both;
  }
  
  .stats-row {
    display: flex;
    justify-content: center;
    gap: 40px;
    margin: 50px 0;
    flex-wrap: wrap;
    
    .stat-item {
      display: flex;
      align-items: center;
      gap: 12px;
      background: rgba(255, 255, 255, 0.1);
      backdrop-filter: blur(10px);
      padding: 20px 25px;
      border-radius: 15px;
      border: 1px solid rgba(255, 255, 255, 0.2);
      animation: fadeInUp 0.8s ease both;
      transition: all 0.3s ease;
      
      &:hover {
        transform: translateY(-5px);
        background: rgba(255, 255, 255, 0.15);
        box-shadow: 0 10px 30px rgba(0, 0, 0, 0.2);
      }
      
      .stat-icon {
        font-size: 24px;
        color: #4ecdc4;
      }
      
      .stat-content {
        text-align: left;
        
        .stat-number {
          font-size: 24px;
          font-weight: 700;
          line-height: 1;
        }
        
        .stat-label {
          font-size: 12px;
          opacity: 0.8;
          margin-top: 2px;
        }
      }
    }
  }
  
  .action-buttons {
    display: flex;
    justify-content: center;
    gap: 20px;
    animation: slideInUp 0.8s ease 0.6s both;
    flex-wrap: wrap;
    
    .primary-btn {
      height: 50px;
      padding: 0 30px;
      font-size: 16px;
      font-weight: 600;
      border-radius: 25px;
      background: linear-gradient(45deg, #4ecdc4, #44a08d);
      border: none;
      box-shadow: 0 8px 20px rgba(78, 205, 196, 0.3);
      transition: all 0.3s ease;
      
      &:hover {
        transform: translateY(-3px);
        box-shadow: 0 12px 25px rgba(78, 205, 196, 0.4);
        background: linear-gradient(45deg, #44a08d, #4ecdc4);
      }
    }
    
    .secondary-btn {
      height: 50px;
      padding: 0 30px;
      font-size: 16px;
      font-weight: 600;
      border-radius: 25px;
      background: transparent;
      border: 2px solid rgba(255, 255, 255, 0.3);
      color: white;
      transition: all 0.3s ease;
      
      &:hover {
        transform: translateY(-3px);
        background: rgba(255, 255, 255, 0.1);
        border-color: rgba(255, 255, 255, 0.5);
        color: white;
      }
    }
  }
  
  .decoration-elements {
    position: absolute;
    width: 100%;
    height: 100%;
    pointer-events: none;
    
    .glow-orb {
      position: absolute;
      border-radius: 50%;
      background: radial-gradient(circle, rgba(78, 205, 196, 0.3), transparent 70%);
      animation: pulse 4s ease-in-out infinite;
      
      &.glow-orb-1 {
        width: 300px;
        height: 300px;
        top: 10%;
        left: -100px;
        animation-delay: 0s;
      }
      
      &.glow-orb-2 {
        width: 200px;
        height: 200px;
        top: 60%;
        right: -50px;
        animation-delay: 2s;
      }
      
      &.glow-orb-3 {
        width: 150px;
        height: 150px;
        bottom: 20%;
        left: 20%;
        animation-delay: 1s;
      }
    }
  }
}

@keyframes shimmer {
  0%, 100% { opacity: 0; }
  50% { opacity: 1; }
}

@keyframes pulse {
  0%, 100% {
    transform: scale(1);
    opacity: 0.3;
  }
  50% {
    transform: scale(1.1);
    opacity: 0.6;
  }
}

// 功能特性区域
.features-section {
  padding: 100px 40px;
  background: rgba(255, 255, 255, 0.95);
  backdrop-filter: blur(20px);
  
  .section-header {
    text-align: center;
    margin-bottom: 80px;
    
    .section-title {
      font-size: 42px;
      font-weight: 700;
      color: #2c3e50;
      margin-bottom: 15px;
      animation: slideInDown 0.8s ease;
    }
    
    .section-subtitle {
      font-size: 18px;
      color: #7f8c8d;
      animation: slideInUp 0.8s ease 0.2s both;
    }
  }
  
  .features-grid {
    display: grid;
    grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
    gap: 30px;
    max-width: 1200px;
    margin: 0 auto;
    
    .feature-card {
      background: white;
      padding: 40px 30px;
      border-radius: 20px;
      text-align: center;
      box-shadow: 0 10px 30px rgba(0, 0, 0, 0.1);
      transition: all 0.3s ease;
      position: relative;
      overflow: hidden;
      animation: fadeInUp 0.8s ease both;
      
      &:hover {
        transform: translateY(-10px);
        box-shadow: 0 20px 40px rgba(0, 0, 0, 0.15);
        
        .feature-icon {
          transform: scale(1.1);
          color: #4ecdc4;
        }
        
        .feature-decoration {
          transform: scale(1.5);
          opacity: 0.3;
        }
      }
      
      .feature-icon {
        font-size: 48px;
        color: #667eea;
        margin-bottom: 25px;
        transition: all 0.3s ease;
      }
      
      .feature-title {
        font-size: 22px;
        font-weight: 600;
        color: #2c3e50;
        margin-bottom: 15px;
      }
      
      .feature-description {
        font-size: 16px;
        color: #7f8c8d;
        line-height: 1.6;
        margin: 0;
      }
      
      .feature-decoration {
        position: absolute;
        top: -50px;
        right: -50px;
        width: 100px;
        height: 100px;
        background: linear-gradient(45deg, #667eea, #764ba2);
        border-radius: 50%;
        opacity: 0.1;
        transition: all 0.3s ease;
      }
    }
  }
}

// 技术栈区域
.tech-section {
  padding: 100px 40px;
  background: #f8f9fa;
  
  .section-header {
    text-align: center;
    margin-bottom: 60px;
    
    .section-title {
      font-size: 42px;
      font-weight: 700;
      color: #2c3e50;
      margin-bottom: 15px;
      animation: slideInDown 0.8s ease;
    }
    
    .section-subtitle {
      font-size: 18px;
      color: #7f8c8d;
      animation: slideInUp 0.8s ease 0.2s both;
    }
  }
  
  .tech-stack {
    display: flex;
    justify-content: center;
    gap: 30px;
    flex-wrap: wrap;
    max-width: 800px;
    margin: 0 auto;
    
    .tech-item {
      display: flex;
      flex-direction: column;
      align-items: center;
      gap: 15px;
      background: white;
      padding: 25px 20px;
      border-radius: 15px;
      box-shadow: 0 5px 15px rgba(0, 0, 0, 0.08);
      transition: all 0.3s ease;
      animation: fadeInUp 0.6s ease both;
      
      &:hover {
        transform: translateY(-5px);
        box-shadow: 0 10px 25px rgba(0, 0, 0, 0.15);
        
        .tech-logo {
          transform: scale(1.1);
        }
      }
      
      .tech-logo {
        font-size: 36px;
        color: #667eea;
        transition: all 0.3s ease;
        
        img {
          width: 36px;
          height: 36px;
        }
      }
      
      .tech-name {
        font-size: 14px;
        font-weight: 600;
        color: #2c3e50;
      }
    }
  }
}

// CTA区域
.cta-section {
  padding: 100px 40px;
  background: linear-gradient(135deg, #4ecdc4 0%, #44a08d 100%);
  text-align: center;
  position: relative;
  overflow: hidden;
  
  .cta-content {
    position: relative;
    z-index: 2;
    max-width: 600px;
    margin: 0 auto;
    color: white;
    
    .cta-title {
      font-size: 36px;
      font-weight: 700;
      margin-bottom: 15px;
      animation: slideInDown 0.8s ease;
    }
    
    .cta-subtitle {
      font-size: 18px;
      margin-bottom: 40px;
      opacity: 0.9;
      animation: slideInUp 0.8s ease 0.2s both;
    }
    
    .cta-button {
      height: 60px;
      padding: 0 40px;
      font-size: 18px;
      font-weight: 600;
      border-radius: 30px;
      background: white;
      color: #4ecdc4;
      border: none;
      box-shadow: 0 10px 25px rgba(0, 0, 0, 0.2);
      animation: slideInUp 0.8s ease 0.4s both;
      transition: all 0.3s ease;
      
      &:hover {
        transform: translateY(-3px);
        box-shadow: 0 15px 30px rgba(0, 0, 0, 0.25);
        color: #44a08d;
      }
    }
  }
  
  .cta-decoration {
    position: absolute;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    
    .pulse-ring {
      border: 3px solid rgba(255, 255, 255, 0.3);
      border-radius: 50%;
      width: 300px;
      height: 300px;
      animation: pulsate 2s ease-out infinite;
      
      &.pulse-ring-delay {
        animation-delay: 1s;
      }
    }
  }
}

@keyframes pulsate {
  0% {
    transform: scale(0.1, 0.1);
    opacity: 0;
  }
  50% {
    opacity: 1;
  }
  100% {
    transform: scale(1.2, 1.2);
    opacity: 0;
  }
}

// 通用动画
@keyframes slideInDown {
  from {
    opacity: 0;
    transform: translateY(-30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes slideInUp {
  from {
    opacity: 0;
    transform: translateY(30px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes fadeInUp {
  from {
    opacity: 0;
    transform: translateY(20px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

// 响应式设计
@media (max-width: 768px) {
  .hero-section {
    padding: 40px 20px;
    min-height: 80vh;
    
    .main-title {
      font-size: 36px;
    }
    
    .subtitle {
      font-size: 16px;
    }
    
    .stats-row {
      gap: 20px;
      
      .stat-item {
        padding: 15px 20px;
      }
    }
    
    .action-buttons {
      flex-direction: column;
      align-items: center;
      
      .primary-btn,
      .secondary-btn {
        width: 200px;
      }
    }
  }
  
  .features-section {
    padding: 60px 20px;
    
    .section-title {
      font-size: 28px;
    }
    
    .features-grid {
      grid-template-columns: 1fr;
      gap: 20px;
    }
  }
  
  .tech-section {
    padding: 60px 20px;
    
    .section-title {
      font-size: 28px;
    }
    
    .tech-stack {
      gap: 15px;
    }
  }
  
  .cta-section {
    padding: 60px 20px;
    
    .cta-title {
      font-size: 24px;
    }
    
    .cta-subtitle {
      font-size: 16px;
    }
  }
}

@media (max-width: 480px) {
  .floating-shapes .shape {
    display: none;
  }
  
  .hero-section {
    .main-title {
      font-size: 28px;
    }
    
    .stats-row {
      flex-direction: column;
      align-items: center;
      
      .stat-item {
        width: 100%;
        max-width: 280px;
        justify-content: center;
      }
    }
  }
  
  .features-grid .feature-card {
    padding: 30px 20px;
  }
  
  .tech-stack .tech-item {
    min-width: 100px;
  }
}
</style>