<template>
  <div id="blog-website">
    <!-- 导航栏 -->
    <nav class="blog-navbar">
      <div class="nav-container">
        <div class="nav-brand">
          <h1>我的技术博客</h1>
        </div>
        <ul class="nav-menu" :class="{ active: mobileMenuOpen }">
          <li><a href="#" @click="setCurrentPage('home')" :class="{ active: currentPage === 'home' }">首页</a></li>
          <li><a href="#" @click="setCurrentPage('projects')" :class="{ active: currentPage === 'projects' }">项目</a></li>
          <li><a href="#" @click="setCurrentPage('tools')" :class="{ active: currentPage === 'tools' }">工具</a></li>
          <li><a href="#" @click="setCurrentPage('techstack')" :class="{ active: currentPage === 'techstack' }">技术栈</a></li>
        </ul>
        <div class="nav-auth">
          <div v-if="!isLoggedIn" class="auth-buttons">
            <button @click="showLoginModal = true" class="login-btn">登录</button>
            <button @click="showRegisterModal = true" class="register-btn">注册</button>
          </div>
          <div v-else class="user-info">
            <img :src="userInfo.avatar || '/assets/default-avatar.png'" :alt="userInfo.nickname" class="user-avatar" />
            <span class="user-name">{{ userInfo.nickname || userInfo.username }}</span>
            <div class="dropdown">
              <button class="dropdown-btn">
                <i class="arrow-down"></i>
              </button>
              <div class="dropdown-menu">
                <a href="#" @click="logout">退出登录</a>
              </div>
            </div>
          </div>
        </div>
        <div class="mobile-menu-btn" @click="toggleMobileMenu">
          <span></span>
          <span></span>
          <span></span>
        </div>
      </div>
    </nav>

    <!-- 主要内容 -->
    <main class="blog-main">
      <!-- 首页 -->
      <div v-if="currentPage === 'home'" class="page-content">
        <div class="home-container">
          <!-- 左侧区域：热门项目排行榜和评论排行 -->
          <aside class="home-sidebar">
            <!-- 热门项目排行榜 -->
            <div class="ranking-card">
              <h3 class="card-title">
                <i class="icon-star"></i>
                热门项目排行榜
              </h3>
              <div class="ranking-list">
                <div v-for="(project, index) in topProjects" :key="project.id" class="ranking-item" @click="openProjectDetail(project)">
                  <div class="rank-number" :class="`rank-${index + 1}`">{{ index + 1 }}</div>
                  <div class="project-info">
                    <h4 class="project-name">{{ project.name }}</h4>
                    <p class="project-desc">{{ project.shortDesc }}</p>
                    <div class="project-stats">
                      <span><i class="icon-eye"></i>{{ project.viewCount }}</span>
                      <span><i class="icon-like"></i>{{ project.likeCount }}</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- 评论排行 -->
            <div class="ranking-card">
              <h3 class="card-title">
                <i class="icon-comment"></i>
                评论排行
              </h3>
              <div class="ranking-list">
                <div v-for="(article, index) in topCommentArticles" :key="article.id" class="ranking-item" @click="openArticleDetail(article)">
                  <div class="rank-number" :class="`rank-${index + 1}`">{{ index + 1 }}</div>
                  <div class="article-info">
                    <h4 class="article-title">{{ article.title }}</h4>
                    <div class="article-stats">
                      <span><i class="icon-comment"></i>{{ article.commentCount }}</span>
                      <span><i class="icon-eye"></i>{{ article.viewCount }}</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </aside>

          <!-- 中间区域：文章列表 -->
          <div class="home-main">
            <div class="articles-header">
              <h2 class="section-title">最新文章</h2>
              <div class="article-filters">
                <button v-for="category in articleCategories" :key="category" 
                        @click="filterByCategory(category)" 
                        :class="{ active: selectedCategory === category }"
                        class="filter-btn">
                  {{ category }}
                </button>
              </div>
            </div>
            
            <div class="articles-list">
              <article v-for="article in filteredArticles" :key="article.id" class="article-card" @click="openArticleDetail(article)">
                <div class="article-cover">
                  <img :src="article.coverImage || '/assets/default-article-cover.jpg'" :alt="article.title" />
                  <div class="article-category">{{ article.category }}</div>
                </div>
                <div class="article-content">
                  <h3 class="article-title">{{ article.title }}</h3>
                  <p class="article-summary">{{ article.summary }}</p>
                  <div class="article-meta">
                    <span class="article-date">{{ formatDate(article.createdAt) }}</span>
                    <div class="article-stats">
                      <span><i class="icon-eye"></i>{{ article.viewCount }}</span>
                      <span><i class="icon-comment"></i>{{ article.commentCount }}</span>
                      <span><i class="icon-like"></i>{{ article.likeCount }}</span>
                    </div>
                  </div>
                  <div class="article-tags">
                    <span v-for="tag in article.tags" :key="tag" class="tag">{{ tag }}</span>
                  </div>
                </div>
              </article>
            </div>
          </div>

          <!-- 右侧区域：我熟练度最高的技术栈和推荐工具 -->
          <aside class="home-sidebar-right">
            <!-- 技术栈熟练度 -->
            <div class="tech-card">
              <h3 class="card-title">
                <i class="icon-code"></i>
                技术栈熟练度
              </h3>
              <div class="tech-list">
                <div v-for="tech in topTechStack" :key="tech.name" class="tech-item">
                  <div class="tech-info">
                    <span class="tech-name">{{ tech.name }}</span>
                    <span class="tech-level">{{ tech.level }}%</span>
                  </div>
                  <div class="tech-bar">
                    <div class="tech-progress" :style="{ width: tech.level + '%' }"></div>
                  </div>
                </div>
              </div>
            </div>

            <!-- 推荐工具 -->
            <div class="tools-card">
              <h3 class="card-title">
                <i class="icon-tool"></i>
                推荐工具
              </h3>
              <div class="tools-list">
                <div v-for="tool in recommendedTools.slice(0, 5)" :key="tool.id" class="tool-item-mini" @click="openToolDetail(tool)">
                  <img :src="tool.iconUrl || '/assets/default-tool-icon.png'" :alt="tool.name" class="tool-icon-mini" />
                  <div class="tool-info-mini">
                    <h4 class="tool-name-mini">{{ tool.name }}</h4>
                    <p class="tool-desc-mini">{{ tool.shortDesc }}</p>
                    <div class="tool-rating">
                      <span v-for="i in 5" :key="i" class="star" :class="{ active: i <= tool.rating }">★</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </aside>
        </div>
      </div>

      <!-- 项目页面 -->
      <div v-if="currentPage === 'projects'" class="page-content">
        <div class="projects-container">
          <aside class="projects-sidebar">
            <!-- 项目分类 -->
            <div class="filter-card">
              <h3 class="card-title">项目分类</h3>
              <div class="filter-options">
                <button v-for="category in projectCategories" :key="category" 
                        @click="filterProjects(category)" 
                        :class="{ active: selectedProjectCategory === category }"
                        class="filter-btn">
                  {{ category }}
                </button>
              </div>
            </div>

            <!-- 技术栈筛选 -->
            <div class="filter-card">
              <h3 class="card-title">技术栈</h3>
              <div class="tech-filters">
                <span v-for="tech in allTechStack" :key="tech" 
                      @click="toggleTechFilter(tech)"
                      :class="{ active: selectedTechs.includes(tech) }"
                      class="tech-filter-tag">
                  {{ tech }}
                </span>
              </div>
            </div>
          </aside>

          <div class="projects-main">
            <div class="projects-header">
              <h2 class="section-title">项目展示</h2>
              <div class="projects-sort">
                <select v-model="projectSortBy" @change="sortProjects" class="sort-select">
                  <option value="latest">最新发布</option>
                  <option value="popular">最受欢迎</option>
                  <option value="views">浏览量</option>
                </select>
              </div>
            </div>

            <div class="projects-grid">
              <div v-for="project in filteredProjects" :key="project.id" class="project-card" @click="openProjectDetail(project)">
                <div class="project-cover">
                  <img :src="project.coverImage || '/assets/default-project-cover.jpg'" :alt="project.name" />
                  <div class="project-overlay">
                    <div class="project-links">
                      <a v-if="project.demoUrl" :href="project.demoUrl" target="_blank" @click.stop class="project-link">
                        <i class="icon-eye"></i>演示
                      </a>
                      <a v-if="project.githubUrl" :href="project.githubUrl" target="_blank" @click.stop class="project-link">
                        <i class="icon-github"></i>代码
                      </a>
                    </div>
                  </div>
                  <div class="project-status" :class="project.status">{{ getStatusText(project.status) }}</div>
                </div>
                <div class="project-content">
                  <h3 class="project-name">{{ project.name }}</h3>
                  <p class="project-description">{{ project.description }}</p>
                  <div class="project-meta">
                    <span class="project-date">{{ formatDate(project.createdAt) }}</span>
                    <div class="project-stats">
                      <span><i class="icon-eye"></i>{{ project.viewCount }}</span>
                      <span><i class="icon-like"></i>{{ project.likeCount }}</span>
                    </div>
                  </div>
                  <div class="project-tech">
                    <span v-for="tech in project.technologies" :key="tech" class="tech-tag">{{ tech }}</span>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- 工具页面 -->
      <div v-if="currentPage === 'tools'" class="page-content">
        <div class="tools-container">
          <div class="tools-header">
            <h2 class="section-title">开发工具推荐</h2>
            <p class="section-subtitle">分享我使用过的优质开发工具和插件</p>
          </div>

          <div class="tools-categories">
            <button v-for="category in toolCategories" :key="category" 
                    @click="filterTools(category)" 
                    :class="{ active: selectedToolCategory === category }"
                    class="category-btn">
              {{ category }}
            </button>
          </div>

          <div class="tools-grid">
            <div v-for="tool in filteredTools" :key="tool.id" class="tool-card" @click="openToolDetail(tool)">
              <div class="tool-header">
                <img :src="tool.iconUrl || '/assets/default-tool-icon.png'" :alt="tool.name" class="tool-icon" />
                <div class="tool-basic-info">
                  <h3 class="tool-name">{{ tool.name }}</h3>
                  <div class="tool-rating">
                    <span v-for="i in 5" :key="i" class="star" :class="{ active: i <= tool.rating }">★</span>
                    <span class="rating-text">({{ tool.rating }}/5)</span>
                  </div>
                </div>
                <div class="tool-category-tag">{{ tool.category }}</div>
              </div>
              
              <div class="tool-content">
                <p class="tool-description">{{ tool.description }}</p>
                
                <div class="tool-features">
                  <h4>主要特性</h4>
                  <ul class="feature-list">
                    <li v-for="feature in tool.features" :key="feature">{{ feature }}</li>
                  </ul>
                </div>

                <div class="tool-images" v-if="tool.screenshots && tool.screenshots.length > 0">
                  <img v-for="(img, index) in tool.screenshots.slice(0, 2)" :key="index" 
                       :src="img" :alt="`${tool.name} 截图 ${index + 1}`" class="tool-screenshot" />
                </div>
              </div>

              <div class="tool-footer">
                <div class="tool-stats">
                  <span><i class="icon-download"></i>{{ tool.downloads || 'N/A' }}</span>
                  <span><i class="icon-star"></i>{{ tool.stars || 'N/A' }}</span>
                </div>
                <div class="tool-actions">
                  <a v-if="tool.officialUrl" :href="tool.officialUrl" target="_blank" @click.stop class="tool-link official">
                    <i class="icon-link"></i>官网
                  </a>
                  <a v-if="tool.downloadUrl" :href="tool.downloadUrl" target="_blank" @click.stop class="tool-link download">
                    <i class="icon-download"></i>下载
                  </a>
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>

      <!-- 技术栈页面 -->
      <div v-if="currentPage === 'techstack'" class="page-content">
        <div class="techstack-container">
          <div class="techstack-left">
            <div class="techstack-header">
              <h2 class="section-title">技术栈熟练度</h2>
              <p class="section-subtitle">我的技术栈熟练度和推荐度可视化展示</p>
            </div>

            <!-- ECharts 图表容器 -->
            <div class="charts-container">
              <!-- 技能雷达图 -->
              <div class="chart-card">
                <h3 class="chart-title">技能雷达图</h3>
                <div id="skillRadarChart" class="chart" style="height: 400px;"></div>
              </div>

              <!-- 技术栈熟练度柱状图 -->
              <div class="chart-card">
                <h3 class="chart-title">技术栈熟练度</h3>
                <div id="techStackChart" class="chart" style="height: 400px;"></div>
              </div>

              <!-- 学习进度时间线 -->
              <div class="chart-card">
                <h3 class="chart-title">学习进度时间线</h3>
                <div id="learningTimelineChart" class="chart" style="height: 400px;"></div>
              </div>

              <!-- 项目技术分布饼图 -->
              <div class="chart-card">
                <h3 class="chart-title">项目技术分布</h3>
                <div id="techDistributionChart" class="chart" style="height: 400px;"></div>
              </div>
            </div>
          </div>

          <aside class="techstack-sidebar">
            <!-- 技术栈详细信息 -->
            <div class="tech-detail-card">
              <h3 class="card-title">技术栈详情</h3>
              <div class="tech-categories">
                <div v-for="category in techCategories" :key="category.name" class="tech-category">
                  <h4 class="category-name">
                    <i :class="category.icon"></i>
                    {{ category.name }}
                  </h4>
                  <div class="tech-items">
                    <div v-for="tech in category.items" :key="tech.name" class="tech-detail-item">
                      <div class="tech-detail-header">
                        <span class="tech-name">{{ tech.name }}</span>
                        <span class="tech-level">{{ tech.level }}%</span>
                      </div>
                      <div class="tech-bar">
                        <div class="tech-progress" :style="{ width: tech.level + '%', backgroundColor: tech.color }"></div>
                      </div>
                      <div class="tech-experience">
                        <span class="experience-text">{{ tech.experience }}</span>
                      </div>
                    </div>
                  </div>
                </div>
              </div>
            </div>

            <!-- 学习计划 -->
            <div class="learning-plan-card">
              <h3 class="card-title">学习计划</h3>
              <div class="learning-list">
                <div v-for="item in learningPlan" :key="item.id" class="learning-item">
                  <div class="learning-header">
                    <span class="learning-tech">{{ item.tech }}</span>
                    <span class="learning-status" :class="item.status">{{ item.statusText }}</span>
                  </div>
                  <div class="learning-progress">
                    <div class="progress-bar">
                      <div class="progress-fill" :style="{ width: item.progress + '%' }"></div>
                    </div>
                    <span class="progress-text">{{ item.progress }}%</span>
                  </div>
                  <p class="learning-desc">{{ item.description }}</p>
                </div>
              </div>
            </div>
          </aside>
        </div>
      </div>

      <!-- 项目页面 -->
      <div v-if="currentPage === 'projects'" class="page-content">
        <div class="projects-container">
          <!-- 左侧区域：热门项目排行榜和评论排行 -->
          <aside class="home-sidebar">
            <!-- 热门项目排行榜 -->
            <div class="ranking-card">
              <h3 class="card-title">🔥 热门项目排行榜</h3>
              <div class="ranking-list">
                <div v-for="(project, index) in hotProjects" :key="project.id" class="ranking-item">
                  <span class="rank">{{ index + 1 }}</span>
                  <div class="project-info">
                    <h4 class="project-name">{{ project.name }}</h4>
                    <p class="project-stats">⭐ {{ project.stars }} | 👁 {{ project.views }}</p>
                  </div>
                </div>
              </div>
            </div>

            <!-- 评论排行 -->
            <div class="ranking-card">
              <h3 class="card-title">💬 评论排行</h3>
              <div class="ranking-list">
                <div v-for="(comment, index) in hotComments" :key="index" class="ranking-item">
                  <span class="rank">{{ index + 1 }}</span>
                  <div class="comment-info">
                    <h4 class="comment-author">{{ comment.author }}</h4>
                    <p class="comment-preview">{{ comment.content }}</p>
                  </div>
                </div>
              </div>
            </div>
          </aside>

          <!-- 中间区域：项目列表 -->
          <main class="home-main">
            <div class="content-header">
              <h2>我的项目</h2>
              <p class="content-subtitle">技术积累与实践成果</p>
            </div>
            
            <div class="projects-grid">
              <div v-for="project in projects" :key="project.id" class="project-card" @click="showDetail(project)">
                <div class="project-image">
                  <img :src="project.coverImage" :alt="project.name" />
                </div>
                <div class="project-content">
                  <h3 class="project-title">{{ project.name }}</h3>
                  <p class="project-summary">{{ project.summary }}</p>
                  <div class="project-tags">
                    <span v-for="tag in project.technologies" :key="tag" class="tag">{{ tag }}</span>
                  </div>
                  <div class="project-stats">
                    <span class="stat">⭐ {{ project.stars }}</span>
                    <span class="stat">🍴 {{ project.forks }}</span>
                    <span class="stat">👁 {{ project.views }}</span>
                  </div>
                </div>
              </div>
            </div>
          </main>

          <!-- 右侧区域：技术栈和工具 -->
          <aside class="home-sidebar">
            <!-- 技术栈 -->
            <div class="tech-card">
              <h3 class="card-title">🚀 我的技术栈</h3>
              <div class="tech-tags">
                <span v-for="tech in myTechStack" :key="tech.name" class="tech-tag" :style="{ backgroundColor: tech.color }">
                  {{ tech.name }}
                </span>
              </div>
            </div>

            <!-- 推荐工具 -->
            <div class="tech-card">
              <h3 class="card-title">🛠 推荐工具</h3>
              <div class="tools-list">
                <div v-for="tool in recommendedTools" :key="tool.name" class="tool-item">
                  <img :src="tool.iconUrl" :alt="tool.name" class="tool-icon" />
                  <div class="tool-info">
                    <h4 class="tool-name">{{ tool.name }}</h4>
                    <p class="tool-description">{{ tool.description }}</p>
                  </div>
                  <div class="tool-rating">
                    <div class="stars">
                      <span v-for="n in 5" :key="n" :class="{ active: n <= tool.rating }">★</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </aside>
        </div>
      </div>

      <!-- 工具页面 -->
      <div v-if="currentPage === 'tools'" class="page-content">
        <div class="tools-container">
          <!-- 左侧区域：热门项目排行榜和评论排行 -->
          <aside class="home-sidebar">
            <!-- 热门项目排行榜 -->
            <div class="ranking-card">
              <h3 class="card-title">🔥 热门项目排行榜</h3>
              <div class="ranking-list">
                <div v-for="(project, index) in hotProjects" :key="project.id" class="ranking-item">
                  <span class="rank">{{ index + 1 }}</span>
                  <div class="project-info">
                    <h4 class="project-name">{{ project.name }}</h4>
                    <p class="project-stats">⭐ {{ project.stars }} | 👁 {{ project.views }}</p>
                  </div>
                </div>
              </div>
            </div>

            <!-- 评论排行 -->
            <div class="ranking-card">
              <h3 class="card-title">💬 评论排行</h3>
              <div class="ranking-list">
                <div v-for="(comment, index) in hotComments" :key="index" class="ranking-item">
                  <span class="rank">{{ index + 1 }}</span>
                  <div class="comment-info">
                    <h4 class="comment-author">{{ comment.author }}</h4>
                    <p class="comment-preview">{{ comment.content }}</p>
                  </div>
                </div>
              </div>
            </div>
          </aside>

          <!-- 中间区域：工具列表 -->
          <main class="home-main">
            <div class="content-header">
              <h2>开发工具</h2>
              <p class="content-subtitle">我使用过的工具和插件推荐</p>
            </div>
            
            <div class="tools-grid">
              <div v-for="tool in allTools" :key="tool.id" class="tool-card" @click="showDetail(tool)">
                <div class="tool-icon-container">
                  <img :src="tool.iconUrl" :alt="tool.name" class="tool-icon-large" />
                </div>
                <div class="tool-content">
                  <h3 class="tool-title">{{ tool.name }}</h3>
                  <p class="tool-summary">{{ tool.description }}</p>
                  <div class="tool-tags">
                    <span v-for="tag in tool.tags" :key="tag" class="tag">{{ tag }}</span>
                  </div>
                  <div class="tool-rating">
                    <div class="stars">
                      <span v-for="n in 5" :key="n" :class="{ active: n <= tool.rating }">★</span>
                    </div>
                    <span class="rating-text">{{ tool.rating }}/5</span>
                  </div>
                </div>
              </div>
            </div>
          </main>

          <!-- 右侧区域：技术栈和工具 -->
          <aside class="home-sidebar">
            <!-- 技术栈 -->
            <div class="tech-card">
              <h3 class="card-title">🚀 我的技术栈</h3>
              <div class="tech-tags">
                <span v-for="tech in myTechStack" :key="tech.name" class="tech-tag" :style="{ backgroundColor: tech.color }">
                  {{ tech.name }}
                </span>
              </div>
            </div>

            <!-- 推荐工具 -->
            <div class="tech-card">
              <h3 class="card-title">🛠 推荐工具</h3>
              <div class="tools-list">
                <div v-for="tool in recommendedTools" :key="tool.name" class="tool-item">
                  <img :src="tool.iconUrl" :alt="tool.name" class="tool-icon" />
                  <div class="tool-info">
                    <h4 class="tool-name">{{ tool.name }}</h4>
                    <p class="tool-description">{{ tool.description }}</p>
                  </div>
                  <div class="tool-rating">
                    <div class="stars">
                      <span v-for="n in 5" :key="n" :class="{ active: n <= tool.rating }">★</span>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </aside>
        </div>
      </div>

      <!-- 技术栈页面 -->
      <div v-if="currentPage === 'techstack'" class="page-content">
        <div class="techstack-container">
          <!-- 左侧区域：技术栈图表 -->
          <aside class="techstack-sidebar">
            <div class="chart-card">
              <h3 class="card-title">技术栈熟练度</h3>
              <div id="skillChart" class="chart-container"></div>
            </div>
            
            <div class="chart-card">
              <h3 class="card-title">技术栈推荐度</h3>
              <div id="recommendChart" class="chart-container"></div>
            </div>
          </aside>

          <!-- 右侧区域：技术栈详情 -->
          <main class="techstack-main">
            <div class="content-header">
              <h2>技术栈详情</h2>
              <p class="content-subtitle">我的技术能力图谱</p>
            </div>

            <div class="techstack-categories">
              <div v-for="category in techCategories" :key="category.name" class="category-section">
                <h3 class="category-title">{{ category.name }}</h3>
                <div class="tech-grid">
                  <div v-for="tech in category.technologies" :key="tech.name" class="tech-item">
                    <div class="tech-icon">
                      <img :src="tech.iconUrl" :alt="tech.name" />
                    </div>
                    <div class="tech-details">
                      <h4 class="tech-name">{{ tech.name }}</h4>
                      <div class="tech-proficiency">
                        <span class="proficiency-label">熟练度：</span>
                        <div class="proficiency-bar">
                          <div class="proficiency-fill" :style="{ width: tech.proficiency + '%' }"></div>
                        </div>
                        <span class="proficiency-text">{{ tech.proficiency }}%</span>
                      </div>
                      <div class="tech-recommendation">
                        <span class="recommendation-label">推荐度：</span>
                        <div class="stars">
                          <span v-for="n in 5" :key="n" :class="{ active: n <= tech.recommendation }">★</span>
                        </div>
                      </div>
                      <p class="tech-description">{{ tech.description }}</p>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </main>
        </div>
      </div>

      <!-- 关于我 -->
      <section id="about" class="about-section">
        <div class="section-container">
          <div class="about-content">
            <div class="about-text">
              <h2 class="section-title">关于我</h2>
              <p>你好！我是一名充满热情的全栈开发工程师，拥有多年的软件开发经验。我专注于现代Web技术栈的开发，包括前端的Vue.js、React以及后端的.NET Core、Node.js等技术。</p>
              <p>我相信技术能够改变世界，通过代码可以创造出有价值的产品。我喜欢学习新技术，分享开发经验，并且积极参与开源社区的建设。</p>
              <div class="skills">
                <div class="skill-category">
                  <h4>前端技术</h4>
                  <div class="skill-tags">
                    <span class="skill-tag">Vue.js</span>
                    <span class="skill-tag">React</span>
                    <span class="skill-tag">TypeScript</span>
                  </div>
                </div>
                <div class="skill-category">
                  <h4>后端技术</h4>
                  <div class="skill-tags">
                    <span class="skill-tag">.NET Core</span>
                    <span class="skill-tag">Node.js</span>
                    <span class="skill-tag">MySQL</span>
                  </div>
                </div>
              </div>
            </div>
            <div class="about-avatar">
              <img src="/assets/profile-avatar.jpg" alt="头像" />
            </div>
          </div>
        </div>
      </section>

      <!-- 联系我 -->
      <section id="contact" class="contact-section">
        <div class="section-container">
          <div class="section-header">
            <h2 class="section-title">联系方式</h2>
            <p class="section-subtitle">有任何问题或建议，欢迎随时与我联系</p>
          </div>
          <div class="contact-content">
            <div class="contact-methods">
              <div class="contact-item">
                <i class="icon-mail"></i>
                <div>
                  <h4>邮箱</h4>
                  <p>contact@example.com</p>
                </div>
              </div>
              <div class="contact-item">
                <i class="icon-github"></i>
                <div>
                  <h4>GitHub</h4>
                  <p>github.com/username</p>
                </div>
              </div>
              <div class="contact-item">
                <i class="icon-wechat"></i>
                <div>
                  <h4>微信</h4>
                  <p>WeChat_ID</p>
                </div>
              </div>
            </div>
            <div class="contact-form">
              <h3>在线留言</h3>
              <form @submit.prevent="submitMessage">
                <div class="form-row">
                  <input v-model="messageForm.name" type="text" placeholder="您的姓名" required />
                  <input v-model="messageForm.email" type="email" placeholder="邮箱地址" required />
                </div>
                <input v-model="messageForm.subject" type="text" placeholder="主题" required />
                <textarea v-model="messageForm.message" rows="5" placeholder="请输入您的留言内容..." required></textarea>
                <button type="submit" :disabled="submitting">
                  {{ submitting ? '发送中...' : '发送留言' }}
                </button>
              </form>
            </div>
          </div>
        </div>
      </section>
    </main>

    <!-- 底部 -->
    <footer class="blog-footer">
      <div class="footer-container">
        <div class="footer-content">
          <div class="footer-section">
            <h3>关于博客</h3>
            <p>分享技术心得，记录成长足迹</p>
          </div>
          <div class="footer-section">
            <h3>联系方式</h3>
            <p>Email: contact@example.com</p>
          </div>
          <div class="footer-section">
            <h3>友情链接</h3>
            <div class="links">
              <a href="https://vuejs.org" target="_blank">Vue.js</a>
              <a href="https://github.com" target="_blank">GitHub</a>
            </div>
          </div>
        </div>
        <div class="footer-bottom">
          <p>&copy; 2024 我的技术博客. All rights reserved.</p>
        </div>
      </div>
    </footer>

    <!-- 登录弹窗 -->
    <div v-if="showLoginModal" class="modal-overlay" @click="showLoginModal = false">
      <div class="modal-content" @click.stop>
        <div class="modal-header">
          <h2>登录</h2>
          <button @click="showLoginModal = false" class="close-btn">&times;</button>
        </div>
        <form @submit.prevent="login" class="auth-form">
          <input v-model="loginForm.username" type="text" placeholder="用户名或邮箱" required />
          <input v-model="loginForm.password" type="password" placeholder="密码" required />
          <label class="checkbox-label">
            <input v-model="loginForm.rememberMe" type="checkbox" />
            记住我
          </label>
          <button type="submit" :disabled="loginLoading">
            {{ loginLoading ? '登录中...' : '登录' }}
          </button>
        </form>
        <div class="auth-footer">
          还没有账户？<a href="#" @click="showRegisterModal = true; showLoginModal = false">立即注册</a>
        </div>
      </div>
    </div>

    <!-- 注册弹窗 -->
    <div v-if="showRegisterModal" class="modal-overlay" @click="showRegisterModal = false">
      <div class="modal-content" @click.stop>
        <div class="modal-header">
          <h2>注册</h2>
          <button @click="showRegisterModal = false" class="close-btn">&times;</button>
        </div>
        <form @submit.prevent="register" class="auth-form">
          <input v-model="registerForm.username" type="text" placeholder="用户名" required />
          <input v-model="registerForm.email" type="email" placeholder="邮箱地址" required />
          <input v-model="registerForm.password" type="password" placeholder="密码" required />
          <input v-model="registerForm.confirmPassword" type="password" placeholder="确认密码" required />
          <button type="submit" :disabled="registerLoading">
            {{ registerLoading ? '注册中...' : '注册' }}
          </button>
        </form>
        <div class="auth-footer">
          已有账户？<a href="#" @click="showLoginModal = true; showRegisterModal = false">立即登录</a>
        </div>
      </div>
    </div>

    <!-- 详情弹窗 -->
    <div v-if="showDetailModal" class="modal-overlay detail-modal" @click="showDetailModal = false">
      <div class="modal-content detail-content" @click.stop>
        <div class="modal-header">
          <h2>{{ currentDetail.title || currentDetail.name }}</h2>
          <button @click="showDetailModal = false" class="close-btn">&times;</button>
        </div>
        <div class="detail-body">
          <div v-if="currentDetail.coverImage || currentDetail.iconUrl" class="detail-image">
            <img :src="currentDetail.coverImage || currentDetail.iconUrl" :alt="currentDetail.title || currentDetail.name" />
          </div>
          <div class="detail-info">
            <p class="detail-description">{{ currentDetail.summary || currentDetail.description }}</p>
            <div v-if="currentDetail.tags || currentDetail.technologies" class="detail-tags">
              <span v-for="tag in (currentDetail.tags || currentDetail.technologies)" :key="tag" class="tag">{{ tag }}</span>
            </div>
            <div v-if="currentDetail.demoUrl || currentDetail.githubUrl || currentDetail.officialUrl" class="detail-links">
              <a v-if="currentDetail.demoUrl" :href="currentDetail.demoUrl" target="_blank" class="detail-link">演示</a>
              <a v-if="currentDetail.githubUrl" :href="currentDetail.githubUrl" target="_blank" class="detail-link">代码</a>
              <a v-if="currentDetail.officialUrl" :href="currentDetail.officialUrl" target="_blank" class="detail-link">官网</a>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: 'BlogWebsite',
  data() {
    return {
      currentPage: 'home', // 当前页面：home, projects, tools, techstack
      mobileMenuOpen: false,
      isLoggedIn: false,
      userInfo: {},
      showLoginModal: false,
      showRegisterModal: false,
      showDetailModal: false,
      currentDetail: {},
      loginLoading: false,
      registerLoading: false,
      submitting: false,
      loginForm: {
        username: '',
        password: '',
        rememberMe: false
      },
      registerForm: {
        username: '',
        email: '',
        password: '',
        confirmPassword: ''
      },
      // 文章数据
      latestArticles: [
        {
          id: 1,
          title: 'Vue.js 3.0 新特性详解',
          summary: '详细介绍Vue.js 3.0的新特性，包括Composition API、Teleport等',
          category: '前端开发',
          coverImage: '/assets/vue-cover.jpg',
          createdAt: '2024-01-15',
          tags: ['Vue.js', 'JavaScript', '前端'],
          viewCount: 1250,
          likeCount: 89,
          commentCount: 23
        },
        {
          id: 2,
          title: '.NET Core微服务架构实战',
          summary: '从零开始构建基于.NET Core的微服务架构系统',
          category: '后端开发',
          coverImage: '/assets/dotnet-cover.jpg',
          createdAt: '2024-01-12',
          tags: ['.NET Core', '微服务', '后端'],
          viewCount: 896,
          likeCount: 65,
          commentCount: 31
        },
        {
          id: 3,
          title: 'Docker容器化部署实践',
          summary: '使用Docker进行应用容器化部署的最佳实践',
          category: 'DevOps',
          coverImage: '/assets/docker-cover.jpg',
          createdAt: '2024-01-10',
          tags: ['Docker', '容器化', 'DevOps'],
          viewCount: 743,
          likeCount: 52,
          commentCount: 19
        }
      ],
      // 文章分类和筛选
      articleCategories: ['全部', '前端开发', '后端开发', 'DevOps', '数据库', '算法'],
      selectedCategory: '全部',
      // 热门项目数据
      topProjects: [
        {
          id: 1,
          name: '博客管理系统',
          shortDesc: '现代化博客管理平台',
          viewCount: 2543,
          likeCount: 198
        },
        {
          id: 2,
          name: '在线代码编辑器',
          shortDesc: '多语言在线编程环境',
          viewCount: 1876,
          likeCount: 156
        },
        {
          id: 3,
          name: '数据可视化平台',
          shortDesc: '企业级数据分析工具',
          viewCount: 1432,
          likeCount: 132
        }
      ],
      // 评论排行数据
      topCommentArticles: [
        {
          id: 1,
          title: 'React Hooks最佳实践',
          commentCount: 56,
          viewCount: 2198
        },
        {
          id: 2,
          title: 'Spring Boot进阶教程',
          commentCount: 43,
          viewCount: 1765
        },
        {
          id: 3,
          title: 'MySQL性能优化指南',
          commentCount: 38,
          viewCount: 1543
        }
      ],
      // 技术栈数据
      topTechStack: [
        { name: 'JavaScript', level: 95 },
        { name: 'Vue.js', level: 92 },
        { name: '.NET Core', level: 88 },
        { name: 'Python', level: 85 },
        { name: 'MySQL', level: 82 }
      ],
      featuredProjects: [
        {
          id: 1,
          name: '博客管理系统',
          description: '基于Vue.js和.NET Core的现代化博客管理系统',
          status: 'completed',
          coverImage: '/assets/blog-project.jpg',
          demoUrl: '#',
          githubUrl: 'https://github.com',
          technologies: ['Vue.js', '.NET Core', 'MySQL']
        },
        {
          id: 2,
          name: '在线代码编辑器',
          description: '支持多语言的在线代码编辑和运行平台',
          status: 'active',
          coverImage: '/assets/editor-project.jpg',
          demoUrl: '#',
          githubUrl: 'https://github.com',
          technologies: ['React', 'Node.js', 'Monaco Editor']
        }
      ],
      recommendedTools: [
        {
          id: 1,
          name: 'VS Code',
          description: '最受欢迎的代码编辑器，功能强大，扩展丰富',
          iconUrl: '/assets/vscode-icon.png',
          rating: 5,
          officialUrl: 'https://code.visualstudio.com'
        },
        {
          id: 2,
          name: 'Postman',
          description: 'API开发和测试的必备工具',
          iconUrl: '/assets/postman-icon.png',
          rating: 4.5,
          officialUrl: 'https://www.postman.com'
        },
        {
          id: 3,
          name: 'Git',
          description: '分布式版本控制系统，团队协作必备',
          iconUrl: '/assets/git-icon.png',
          rating: 5,
          officialUrl: 'https://git-scm.com'
        }
      ],
      // 留言表单
      messageForm: {
        name: '',
        email: '',
        subject: '',
        message: ''
      },
      // 热门项目排行榜数据
      hotProjects: [
        {
          id: 1,
          name: '智能聊天机器人',
          stars: 1200,
          views: 8500
        },
        {
          id: 2,
          name: '数据可视化平台',
          stars: 890,
          views: 6200
        },
        {
          id: 3,
          name: '微服务框架',
          stars: 756,
          views: 4800
        },
        {
          id: 4,
          name: '前端组件库',
          stars: 623,
          views: 3900
        },
        {
          id: 5,
          name: '代码生成器',
          stars: 445,
          views: 2100
        }
      ],
      // 热门评论数据
      hotComments: [
        {
          author: '张三',
          content: '这篇关于Vue3的文章写得非常棒！'
        },
        {
          author: '李四',
          content: '.NET Core的教程很实用，谢谢分享'
        },
        {
          author: '王五',
          content: 'Docker部署实践很有参考价值'
        },
        {
          author: '赵六',
          content: '技术栈分享让我受益匪浅'
        },
        {
          author: '孙七',
          content: '期待更多优质内容！'
        }
      ],
      // 项目数据
      projects: [
        {
          id: 1,
          name: '智能聊天机器人',
          summary: '基于NLP技术的智能对话系统，支持多轮对话和情感分析',
          coverImage: '/assets/chatbot-cover.jpg',
          technologies: ['Python', 'TensorFlow', 'Flask'],
          stars: 1200,
          forks: 180,
          views: 8500,
          demoUrl: 'https://demo.example.com',
          githubUrl: 'https://github.com/example/chatbot'
        },
        {
          id: 2,
          name: '数据可视化平台',
          summary: '企业级数据分析和可视化解决方案，支持多种图表类型',
          coverImage: '/assets/dataviz-cover.jpg',
          technologies: ['Vue.js', 'D3.js', 'ECharts'],
          stars: 890,
          forks: 134,
          views: 6200,
          demoUrl: 'https://dataviz.example.com',
          githubUrl: 'https://github.com/example/dataviz'
        },
        {
          id: 3,
          name: '微服务框架',
          summary: '轻量级微服务开发框架，内置服务发现和负载均衡',
          coverImage: '/assets/microservice-cover.jpg',
          technologies: ['.NET Core', 'Docker', 'Consul'],
          stars: 756,
          forks: 98,
          views: 4800,
          demoUrl: 'https://micro.example.com',
          githubUrl: 'https://github.com/example/microservice'
        },
        {
          id: 4,
          name: '前端组件库',
          summary: '现代化Vue.js组件库，提供丰富的UI组件和工具',
          coverImage: '/assets/ui-lib-cover.jpg',
          technologies: ['Vue.js', 'TypeScript', 'Sass'],
          stars: 623,
          forks: 87,
          views: 3900,
          demoUrl: 'https://ui.example.com',
          githubUrl: 'https://github.com/example/ui-lib'
        },
        {
          id: 5,
          name: '代码生成器',
          summary: '智能代码生成工具，支持多种编程语言和框架',
          coverImage: '/assets/codegen-cover.jpg',
          technologies: ['Node.js', 'TypeScript', 'Template'],
          stars: 445,
          forks: 56,
          views: 2100,
          demoUrl: 'https://codegen.example.com',
          githubUrl: 'https://github.com/example/codegen'
        },
        {
          id: 6,
          name: '在线协作编辑器',
          summary: '实时协作的在线文档编辑器，支持多人同时编辑',
          coverImage: '/assets/editor-cover.jpg',
          technologies: ['React', 'Socket.io', 'MongoDB'],
          stars: 367,
          forks: 45,
          views: 1800,
          demoUrl: 'https://editor.example.com',
          githubUrl: 'https://github.com/example/editor'
        }
      ],
      // 工具数据
      allTools: [
        {
          id: 1,
          name: 'Visual Studio Code',
          description: '微软开发的免费代码编辑器，功能强大，扩展丰富，是前端开发的首选工具',
          iconUrl: '/assets/vscode-icon.png',
          rating: 5,
          tags: ['编辑器', '前端', 'JavaScript'],
          officialUrl: 'https://code.visualstudio.com'
        },
        {
          id: 2,
          name: 'Postman',
          description: 'API开发和测试的专业工具，支持REST、GraphQL等多种API类型',
          iconUrl: '/assets/postman-icon.png',
          rating: 4.5,
          tags: ['API', '测试', '开发工具'],
          officialUrl: 'https://www.postman.com'
        },
        {
          id: 3,
          name: 'Docker',
          description: '容器化平台，简化应用部署和运维，提高开发效率',
          iconUrl: '/assets/docker-icon.png',
          rating: 5,
          tags: ['容器', 'DevOps', '部署'],
          officialUrl: 'https://www.docker.com'
        },
        {
          id: 4,
          name: 'Git',
          description: '分布式版本控制系统，代码管理和团队协作的必备工具',
          iconUrl: '/assets/git-icon.png',
          rating: 5,
          tags: ['版本控制', '团队协作', '开发工具'],
          officialUrl: 'https://git-scm.com'
        },
        {
          id: 5,
          name: 'Figma',
          description: '在线UI设计工具，支持实时协作和原型设计',
          iconUrl: '/assets/figma-icon.png',
          rating: 4.5,
          tags: ['设计', 'UI/UX', '原型'],
          officialUrl: 'https://www.figma.com'
        },
        {
          id: 6,
          name: 'Chrome DevTools',
          description: '浏览器开发者工具，前端调试和性能分析的利器',
          iconUrl: '/assets/chrome-icon.png',
          rating: 5,
          tags: ['调试', '前端', '浏览器'],
          officialUrl: 'https://developers.google.com/web/tools/chrome-devtools'
        }
      ],
      // 我的技术栈
      myTechStack: [
        { name: 'JavaScript', color: '#f7df1e' },
        { name: 'Vue.js', color: '#4fc08d' },
        { name: 'React', color: '#61dafb' },
        { name: '.NET Core', color: '#512bd4' },
        { name: 'Python', color: '#3776ab' },
        { name: 'Node.js', color: '#339933' },
        { name: 'Docker', color: '#2496ed' },
        { name: 'MySQL', color: '#4479a1' }
      ],
      // 技术栈分类数据
      techCategories: [
        {
          name: '前端技术',
          technologies: [
            {
              name: 'JavaScript',
              iconUrl: '/assets/js-icon.png',
              proficiency: 95,
              recommendation: 5,
              description: '现代Web开发的核心语言，熟练掌握ES6+新特性'
            },
            {
              name: 'Vue.js',
              iconUrl: '/assets/vue-icon.png',
              proficiency: 92,
              recommendation: 5,
              description: '渐进式前端框架，易学易用，生态完善'
            },
            {
              name: 'React',
              iconUrl: '/assets/react-icon.png',
              proficiency: 88,
              recommendation: 4,
              description: 'Facebook开发的前端库，组件化开发的典范'
            },
            {
              name: 'TypeScript',
              iconUrl: '/assets/ts-icon.png',
              proficiency: 85,
              recommendation: 5,
              description: 'JavaScript的超集，提供静态类型检查'
            }
          ]
        },
        {
          name: '后端技术',
          technologies: [
            {
              name: '.NET Core',
              iconUrl: '/assets/dotnet-icon.png',
              proficiency: 90,
              recommendation: 5,
              description: '跨平台的.NET实现，性能优异，生态丰富'
            },
            {
              name: 'Python',
              iconUrl: '/assets/python-icon.png',
              proficiency: 82,
              recommendation: 4,
              description: '简洁优雅的编程语言，在AI和数据分析领域表现突出'
            },
            {
              name: 'Node.js',
              iconUrl: '/assets/nodejs-icon.png',
              proficiency: 78,
              recommendation: 4,
              description: '基于V8引擎的JavaScript运行时，适合构建高并发应用'
            }
          ]
        },
        {
          name: '数据库',
          technologies: [
            {
              name: 'MySQL',
              iconUrl: '/assets/mysql-icon.png',
              proficiency: 85,
              recommendation: 5,
              description: '最流行的关系型数据库，稳定可靠'
            },
            {
              name: 'Redis',
              iconUrl: '/assets/redis-icon.png',
              proficiency: 80,
              recommendation: 5,
              description: '高性能的内存数据库，缓存和会话存储的首选'
            },
            {
              name: 'MongoDB',
              iconUrl: '/assets/mongodb-icon.png',
              proficiency: 75,
              recommendation: 4,
              description: '文档型NoSQL数据库，适合敏捷开发'
            }
          ]
        },
        {
          name: 'DevOps',
          technologies: [
            {
              name: 'Docker',
              iconUrl: '/assets/docker-icon.png',
              proficiency: 88,
              recommendation: 5,
              description: '容器化技术的领导者，简化部署和运维'
            },
            {
              name: 'Git',
              iconUrl: '/assets/git-icon.png',
              proficiency: 92,
              recommendation: 5,
              description: '分布式版本控制系统，代码管理的标准工具'
            },
            {
              name: 'nginx',
              iconUrl: '/assets/nginx-icon.png',
              proficiency: 78,
              recommendation: 5,
              description: '高性能的Web服务器和反向代理服务器'
            }
          ]
        }
      ]
    }
  },
  computed: {
    // 筛选后的文章列表
    filteredArticles() {
      if (this.selectedCategory === '全部') {
        return this.latestArticles
      }
      return this.latestArticles.filter(article => article.category === this.selectedCategory)
    }
  },
  methods: {
    // 页面导航方法
    setCurrentPage(page) {
      this.currentPage = page
      this.mobileMenuOpen = false
    },
    
    toggleMobileMenu() {
      this.mobileMenuOpen = !this.mobileMenuOpen
    },

    // 文章筛选方法
    filterByCategory(category) {
      this.selectedCategory = category
    },

    formatDate(dateStr) {
      const date = new Date(dateStr)
      return date.toLocaleDateString('zh-CN')
    },

    getStatusText(status) {
      const statusMap = {
        active: '进行中',
        completed: '已完成',
        archived: '已归档'
      }
      return statusMap[status] || '未知'
    },

    openArticleDetail(article) {
      this.currentDetail = article
      this.showDetailModal = true
    },

    openProjectDetail(project) {
      this.currentDetail = project
      this.showDetailModal = true
    },

    openToolDetail(tool) {
      this.currentDetail = tool
      this.showDetailModal = true
    },

    showDetail(item) {
      this.currentDetail = item
      this.showDetailModal = true
    },

    async login() {
      if (this.loginForm.password !== this.loginForm.username) {
        alert('用户名或密码错误')
        return
      }

      this.loginLoading = true
      try {
        // 模拟登录
        await new Promise(resolve => setTimeout(resolve, 1000))
        
        this.isLoggedIn = true
        this.userInfo = {
          username: this.loginForm.username,
          nickname: this.loginForm.username,
          avatar: ''
        }
        
        this.showLoginModal = false
        this.loginForm = { username: '', password: '', rememberMe: false }
        
        alert('登录成功！')
      } catch (error) {
        alert('登录失败，请稍后重试')
      } finally {
        this.loginLoading = false
      }
    },

    async register() {
      if (this.registerForm.password !== this.registerForm.confirmPassword) {
        alert('两次输入的密码不一致')
        return
      }

      this.registerLoading = true
      try {
        // 模拟注册
        await new Promise(resolve => setTimeout(resolve, 1000))
        
        this.showRegisterModal = false
        this.registerForm = { username: '', email: '', password: '', confirmPassword: '' }
        
        alert('注册成功！请登录您的账户')
        this.showLoginModal = true
      } catch (error) {
        alert('注册失败，请稍后重试')
      } finally {
        this.registerLoading = false
      }
    },

    logout() {
      this.isLoggedIn = false
      this.userInfo = {}
      alert('已退出登录')
    },

    async submitMessage() {
      this.submitting = true
      try {
        // 模拟发送留言
        await new Promise(resolve => setTimeout(resolve, 1000))
        
        this.messageForm = { name: '', email: '', subject: '', message: '' }
        alert('留言发送成功！我会尽快回复您')
      } catch (error) {
        alert('留言发送失败，请稍后重试')
      } finally {
        this.submitting = false
      }
    }
  },

  mounted() {
    // 检查登录状态
    const token = localStorage.getItem('blog_token')
    if (token) {
      this.isLoggedIn = true
      try {
        const userStr = localStorage.getItem('blog_user')
        this.userInfo = JSON.parse(userStr)
      } catch (error) {
        localStorage.removeItem('blog_token')
        localStorage.removeItem('blog_user')
      }
    }
  }
}
</script>

<style scoped>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

#blog-website {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  line-height: 1.6;
  color: #333;
}

/* 导航栏 */
.blog-navbar {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  z-index: 1000;
}

.nav-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
  display: flex;
  align-items: center;
  justify-content: space-between;
  height: 70px;
}

.nav-brand h1 {
  color: white;
  font-size: 24px;
  font-weight: 700;
}

.nav-menu {
  display: flex;
  list-style: none;
  gap: 30px;
}

.nav-menu a {
  color: rgba(255, 255, 255, 0.9);
  text-decoration: none;
  font-weight: 500;
  padding: 8px 16px;
  border-radius: 20px;
  transition: all 0.3s ease;
}

.nav-menu a:hover {
  background: rgba(255, 255, 255, 0.2);
  color: white;
}

.nav-auth {
  display: flex;
  align-items: center;
  gap: 15px;
}

.auth-buttons {
  display: flex;
  gap: 10px;
}

.login-btn, .register-btn {
  padding: 8px 20px;
  border: none;
  border-radius: 20px;
  font-weight: 500;
  cursor: pointer;
  transition: all 0.3s ease;
}

.login-btn {
  background: transparent;
  color: white;
  border: 2px solid rgba(255, 255, 255, 0.3);
}

.login-btn:hover {
  background: rgba(255, 255, 255, 0.1);
}

.register-btn {
  background: #ff6b6b;
  color: white;
}

.register-btn:hover {
  background: #ff5252;
}

.user-info {
  display: flex;
  align-items: center;
  gap: 10px;
  color: white;
}

.user-avatar {
  width: 32px;
  height: 32px;
  border-radius: 50%;
}

.mobile-menu-btn {
  display: none;
  flex-direction: column;
  cursor: pointer;
}

.mobile-menu-btn span {
  width: 25px;
  height: 3px;
  background: white;
  margin: 3px 0;
  transition: 0.3s;
}

/* 主要内容 */
.blog-main {
  margin-top: 70px;
}

/* 共用样式 */
.section-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
}

.section-header {
  text-align: center;
  margin-bottom: 50px;
}

.section-title {
  font-size: 36px;
  font-weight: 700;
  color: #2c3e50;
  margin-bottom: 15px;
}

.section-subtitle {
  font-size: 16px;
  color: #7f8c8d;
  margin: 0;
}

/* 首页横幅 */
.hero-section {
  background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
  color: white;
  padding: 120px 0;
  position: relative;
  overflow: hidden;
}

.hero-content {
  max-width: 600px;
  text-align: center;
  margin: 0 auto;
  position: relative;
  z-index: 2;
}

.hero-title {
  font-size: 48px;
  font-weight: 700;
  margin-bottom: 20px;
}

.hero-subtitle {
  font-size: 18px;
  margin-bottom: 40px;
  opacity: 0.9;
}

.hero-actions {
  display: flex;
  gap: 20px;
  justify-content: center;
}

.hero-btn {
  padding: 15px 30px;
  border: none;
  border-radius: 25px;
  font-weight: 600;
  font-size: 16px;
  cursor: pointer;
  transition: all 0.3s ease;
}

.hero-btn.primary {
  background: #ff6b6b;
  color: white;
}

.hero-btn.primary:hover {
  background: #ff5252;
  transform: translateY(-2px);
}

.hero-btn.secondary {
  background: transparent;
  color: white;
  border: 2px solid rgba(255, 255, 255, 0.3);
}

.hero-btn.secondary:hover {
  background: rgba(255, 255, 255, 0.1);
}

.hero-decoration {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
}

.floating-card {
  position: absolute;
  background: rgba(255, 255, 255, 0.1);
  padding: 15px;
  border-radius: 10px;
  backdrop-filter: blur(10px);
  display: flex;
  align-items: center;
  gap: 10px;
  animation: float 6s ease-in-out infinite;
}

.floating-card.card1 {
  top: 20%;
  left: 10%;
}

.floating-card.card2 {
  top: 30%;
  right: 15%;
  animation-delay: 2s;
}

.floating-card.card3 {
  bottom: 25%;
  left: 15%;
  animation-delay: 4s;
}

/* 文章部分 */
.articles-section {
  padding: 80px 0;
}

.articles-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
  gap: 30px;
}

.article-card {
  background: white;
  border-radius: 15px;
  overflow: hidden;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
  transition: all 0.3s ease;
  cursor: pointer;
}

.article-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
}

.article-cover {
  position: relative;
  height: 200px;
}

.article-cover img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.article-category {
  position: absolute;
  top: 15px;
  left: 15px;
  background: linear-gradient(45deg, #667eea, #764ba2);
  color: white;
  padding: 5px 12px;
  border-radius: 20px;
  font-size: 12px;
}

.article-content {
  padding: 25px;
}

.article-title {
  font-size: 18px;
  font-weight: 600;
  margin-bottom: 12px;
}

.article-summary {
  color: #7f8c8d;
  margin-bottom: 15px;
  display: -webkit-box;
  -webkit-line-clamp: 2;
  -webkit-box-orient: vertical;
  overflow: hidden;
}

.article-meta {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 15px;
}

.article-date {
  color: #95a5a6;
  font-size: 14px;
}

.article-stats {
  display: flex;
  gap: 15px;
}

.article-stats span {
  color: #95a5a6;
  font-size: 14px;
}

.article-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
}

.tag {
  background: #e3f2fd;
  color: #1976d2;
  padding: 4px 8px;
  border-radius: 12px;
  font-size: 12px;
}

/* 项目部分 */
.projects-section {
  padding: 80px 0;
  background: #f8f9fa;
}

.projects-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(400px, 1fr));
  gap: 30px;
}

.project-card {
  background: white;
  border-radius: 15px;
  overflow: hidden;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
  transition: all 0.3s ease;
  cursor: pointer;
}

.project-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 8px 30px rgba(0, 0, 0, 0.12);
}

.project-cover {
  position: relative;
  height: 250px;
}

.project-cover img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}

.project-overlay {
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  align-items: center;
  justify-content: center;
  opacity: 0;
  transition: opacity 0.3s ease;
}

.project-card:hover .project-overlay {
  opacity: 1;
}

.project-links {
  display: flex;
  gap: 15px;
}

.project-links a {
  background: rgba(255, 255, 255, 0.2);
  color: white;
  padding: 10px 20px;
  border-radius: 20px;
  text-decoration: none;
  transition: all 0.3s ease;
}

.project-links a:hover {
  background: rgba(255, 255, 255, 0.3);
}

.project-status {
  position: absolute;
  top: 15px;
  right: 15px;
  padding: 5px 12px;
  border-radius: 20px;
  font-size: 12px;
  color: white;
}

.project-status.active {
  background: #3498db;
}

.project-status.completed {
  background: #27ae60;
}

.project-content {
  padding: 25px;
}

.project-name {
  font-size: 20px;
  font-weight: 600;
  margin-bottom: 12px;
}

.project-description {
  color: #7f8c8d;
  margin-bottom: 15px;
}

.project-tech {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
}

.tech-tag {
  background: #e8f5e8;
  color: #27ae60;
  padding: 4px 8px;
  border-radius: 12px;
  font-size: 12px;
}

/* 工具部分 */
.tools-section {
  padding: 80px 0;
}

.tools-list {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.tool-item {
  background: white;
  padding: 25px;
  border-radius: 15px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
  display: flex;
  align-items: center;
  gap: 20px;
  transition: all 0.3s ease;
  cursor: pointer;
}

.tool-item:hover {
  transform: translateY(-2px);
  box-shadow: 0 6px 25px rgba(0, 0, 0, 0.12);
}

.tool-icon {
  width: 60px;
  height: 60px;
}

.tool-icon img {
  width: 100%;
  height: 100%;
  object-fit: contain;
  border-radius: 10px;
}

.tool-info {
  flex: 1;
}

.tool-name {
  font-size: 18px;
  font-weight: 600;
  margin-bottom: 8px;
}

.tool-description {
  color: #7f8c8d;
  margin: 0;
}

.tool-rating .stars {
  color: #ffd700;
  font-size: 16px;
}

.tool-rating .stars span {
  color: #ddd;
}

.tool-rating .stars span.active {
  color: #ffd700;
}

/* 关于部分 */
.about-section {
  padding: 80px 0;
  background: #f8f9fa;
}

.about-content {
  display: grid;
  grid-template-columns: 2fr 1fr;
  gap: 60px;
  align-items: center;
}

.about-text h2 {
  color: #2c3e50;
  margin-bottom: 30px;
}

.about-text p {
  margin-bottom: 20px;
  line-height: 1.8;
  color: #5a6c7d;
}

.skills {
  margin-top: 30px;
}

.skill-category {
  margin-bottom: 20px;
}

.skill-category h4 {
  color: #2c3e50;
  margin-bottom: 10px;
}

.skill-tags {
  display: flex;
  flex-wrap: wrap;
  gap: 10px;
}

.skill-tag {
  background: #667eea;
  color: white;
  padding: 6px 12px;
  border-radius: 15px;
  font-size: 14px;
}

.about-avatar {
  text-align: center;
}

.about-avatar img {
  width: 200px;
  height: 200px;
  border-radius: 50%;
  object-fit: cover;
  border: 5px solid white;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
}

/* 联系部分 */
.contact-section {
  padding: 80px 0;
}

.contact-content {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 60px;
}

.contact-methods {
  display: flex;
  flex-direction: column;
  gap: 25px;
}

.contact-item {
  display: flex;
  align-items: center;
  gap: 20px;
  padding: 25px;
  background: white;
  border-radius: 15px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
}

.contact-item i {
  font-size: 32px;
  color: #667eea;
}

.contact-item h4 {
  margin-bottom: 5px;
  color: #2c3e50;
}

.contact-item p {
  color: #7f8c8d;
  margin: 0;
}

.contact-form {
  background: white;
  padding: 40px;
  border-radius: 15px;
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.08);
}

.contact-form h3 {
  margin-bottom: 30px;
  color: #2c3e50;
}

.form-row {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 15px;
  margin-bottom: 15px;
}

.contact-form input,
.contact-form textarea {
  width: 100%;
  padding: 12px;
  border: 2px solid #e1e5e9;
  border-radius: 8px;
  font-size: 14px;
  transition: border-color 0.3s ease;
}

.contact-form input:focus,
.contact-form textarea:focus {
  outline: none;
  border-color: #667eea;
}

.contact-form button {
  width: 100%;
  padding: 12px;
  background: linear-gradient(45deg, #667eea, #764ba2);
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
}

.contact-form button:hover:not(:disabled) {
  background: linear-gradient(45deg, #5a6fd8, #6a4190);
}

.contact-form button:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

/* 底部 */
.blog-footer {
  background: #2c3e50;
  color: white;
  padding: 60px 0 30px;
}

.footer-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 0 20px;
}

.footer-content {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 40px;
  margin-bottom: 30px;
}

.footer-section h3 {
  color: #3498db;
  margin-bottom: 20px;
}

.footer-section p {
  color: #bdc3c7;
}

.links {
  display: flex;
  flex-direction: column;
  gap: 10px;
}

.links a {
  color: #bdc3c7;
  text-decoration: none;
  transition: color 0.3s ease;
}

.links a:hover {
  color: #3498db;
}

.footer-bottom {
  border-top: 1px solid #34495e;
  padding-top: 30px;
  text-align: center;
}

.footer-bottom p {
  color: #95a5a6;
  margin: 0;
}

/* 弹窗样式 */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.7);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 2000;
}

.modal-content {
  background: white;
  border-radius: 15px;
  max-width: 400px;
  width: 90%;
  max-height: 90vh;
  overflow-y: auto;
}

.detail-modal .modal-content {
  max-width: 800px;
}

.modal-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: 20px;
  border-bottom: 1px solid #f1f2f6;
}

.modal-header h2 {
  margin: 0;
  color: #2c3e50;
}

.close-btn {
  background: none;
  border: none;
  font-size: 24px;
  cursor: pointer;
  color: #7f8c8d;
}

.close-btn:hover {
  color: #2c3e50;
}

.auth-form {
  padding: 20px;
}

.auth-form input {
  width: 100%;
  padding: 12px;
  margin-bottom: 15px;
  border: 2px solid #e1e5e9;
  border-radius: 8px;
  font-size: 14px;
}

.auth-form input:focus {
  outline: none;
  border-color: #667eea;
}

.checkbox-label {
  display: flex;
  align-items: center;
  gap: 8px;
  margin-bottom: 20px;
  font-size: 14px;
  color: #7f8c8d;
}

.auth-form button {
  width: 100%;
  padding: 12px;
  background: linear-gradient(45deg, #667eea, #764ba2);
  color: white;
  border: none;
  border-radius: 8px;
  font-size: 16px;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.3s ease;
}

.auth-form button:hover:not(:disabled) {
  background: linear-gradient(45deg, #5a6fd8, #6a4190);
}

.auth-form button:disabled {
  opacity: 0.7;
  cursor: not-allowed;
}

.auth-footer {
  padding: 20px;
  text-align: center;
  color: #7f8c8d;
  font-size: 14px;
  border-top: 1px solid #f1f2f6;
}

.auth-footer a {
  color: #667eea;
  text-decoration: none;
}

.auth-footer a:hover {
  text-decoration: underline;
}

.detail-body {
  padding: 20px;
}

.detail-image {
  margin-bottom: 20px;
}

.detail-image img {
  width: 100%;
  max-height: 300px;
  object-fit: cover;
  border-radius: 10px;
}

.detail-description {
  margin-bottom: 15px;
  line-height: 1.6;
  color: #5a6c7d;
}

.detail-tags {
  margin-bottom: 20px;
}

.detail-links {
  display: flex;
  gap: 10px;
}

.detail-link {
  padding: 8px 16px;
  background: #667eea;
  color: white;
  text-decoration: none;
  border-radius: 20px;
  font-size: 14px;
  transition: all 0.3s ease;
}

.detail-link:hover {
  background: #5a6fd8;
  color: white;
}

/* 动画 */
@keyframes float {
  0%, 100% {
    transform: translateY(0);
  }
  50% {
    transform: translateY(-10px);
  }
}

/* 响应式设计 */
@media (max-width: 768px) {
  .nav-menu {
    position: fixed;
    top: 70px;
    left: -100%;
    width: 100%;
    height: calc(100vh - 70px);
    background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
    flex-direction: column;
    align-items: center;
    justify-content: flex-start;
    padding-top: 50px;
    transition: left 0.3s ease;
  }

  .nav-menu.active {
    left: 0;
  }

  .mobile-menu-btn {
    display: flex;
  }

  .hero-title {
    font-size: 32px;
  }

  .hero-actions {
    flex-direction: column;
    align-items: center;
  }

  .hero-btn {
    width: 200px;
  }

  .section-title {
    font-size: 28px;
  }

  .articles-grid,
  .projects-grid {
    grid-template-columns: 1fr;
  }

  .about-content {
    grid-template-columns: 1fr;
    gap: 40px;
    text-align: center;
  }

  .contact-content {
    grid-template-columns: 1fr;
    gap: 40px;
  }

  .form-row {
    grid-template-columns: 1fr;
  }

  .modal-content {
    margin: 20px;
  }
}

/* 图标样式 */
.icon-code::before { content: "💻"; }
.icon-database::before { content: "🗄️"; }
.icon-mobile::before { content: "📱"; }
.icon-eye::before { content: "👁️"; }
.icon-like::before { content: "❤️"; }
.icon-github::before { content: "🐱"; }
.icon-mail::before { content: "📧"; }
.icon-wechat::before { content: "💬"; }

.arrow-down::before { content: "▼"; }

/* 新增页面布局样式 */
.page-content {
  min-height: 100vh;
  padding: 20px 0;
}

/* 首页样式 */
.home-container {
  max-width: 1400px;
  margin: 0 auto;
  padding: 0 20px;
  display: grid;
  grid-template-columns: 300px 1fr 300px;
  gap: 30px;
}

.home-sidebar,
.home-sidebar-right {
  display: flex;
  flex-direction: column;
  gap: 20px;
}

.home-main {
  background: white;
  border-radius: 12px;
  box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
  padding: 20px;
}

.articles-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.article-filters {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
}

.filter-btn {
  padding: 6px 16px;
  border: 1px solid #ddd;
  background: white;
  border-radius: 20px;
  cursor: pointer;
  transition: all 0.3s ease;
}

.filter-btn:hover,
.filter-btn.active {
  background: #667eea;
  color: white;
  border-color: #667eea;
}

.articles-list {
  display: grid;
  gap: 20px;
}

.article-card {
  display: flex;
  gap: 20px;
  padding: 20px;
  border: 1px solid #eee;
  border-radius: 12px;
  cursor: pointer;
  transition: all 0.3s ease;
}

.article-card:hover {
  box-shadow: 0 4px 20px rgba(0, 0, 0, 0.1);
  transform: translateY(-2px);
}

.article-cover {
  flex: 0 0 200px;
  position: relative;
  border-radius: 8px;
  overflow: hidden;
}

.article-cover img {
  width: 100%;
  height: 140px;
  object-fit: cover;
}

.article-category {
  position: absolute;
  top: 10px;
  left: 10px;
  background: #667eea;
  color: white;
  padding: 4px 12px;
  border-radius: 12px;
  font-size: 12px;
}

.article-content {
  flex: 1;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.article-title {
  font-size: 18px;
  font-weight: 600;
  margin-bottom: 10px;
  color: #333;
}

.article-summary {
  color: #666;
  line-height: 1.6;
  margin-bottom: 15px;
  flex: 1;
}

.article-meta {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 10px;
}

.article-date {
  color: #999;
  font-size: 14px;
}

.article-stats {
  display: flex;
  gap: 15px;
  font-size: 14px;
  color: #666;
}

.article-stats span {
  display: flex;
  align-items: center;
  gap: 4px;
}

.article-tags {
  display: flex;
  gap: 8px;
  flex-wrap: wrap;
}

.tag {
  background: #f0f2f5;
  color: #666;
  padding: 2px 8px;
  border-radius: 12px;
  font-size: 12px;
}

/* 排行榜样式 */
.ranking-card,
.tech-card,
.tools-card {
  background: white;
  border-radius: 12px;
  box-shadow: 0 2px 12px rgba(0, 0, 0, 0.1);
  padding: 20px;
}

.card-title {
  font-size: 16px;
  font-weight: 600;
  color: #333;
  margin-bottom: 15px;
  display: flex;
  align-items: center;
  gap: 8px;
}

.ranking-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.ranking-item {
  display: flex;
  gap: 12px;
  padding: 12px;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s ease;
}

.ranking-item:hover {
  background: #f8f9ff;
}

.rank-number {
  flex: 0 0 24px;
  height: 24px;
  border-radius: 50%;
  background: #ddd;
  color: white;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 12px;
  font-weight: 600;
}

.rank-number.rank-1 {
  background: #ffd700;
}

.rank-number.rank-2 {
  background: #c0c0c0;
}

.rank-number.rank-3 {
  background: #cd7f32;
}

.project-info,
.article-info {
  flex: 1;
}

.project-name {
  font-size: 14px;
  font-weight: 600;
  margin-bottom: 4px;
  color: #333;
}

.project-desc {
  font-size: 12px;
  color: #666;
  margin-bottom: 8px;
}

.project-stats {
  display: flex;
  gap: 10px;
  font-size: 12px;
  color: #999;
}

/* 技术栈样式 */
.tech-list {
  display: flex;
  flex-direction: column;
  gap: 12px;
}

.tech-item {
  display: flex;
  flex-direction: column;
  gap: 4px;
}

.tech-info {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.tech-name {
  font-size: 14px;
  font-weight: 500;
  color: #333;
}

.tech-level {
  font-size: 12px;
  color: #667eea;
  font-weight: 600;
}

.tech-bar {
  height: 6px;
  background: #f0f2f5;
  border-radius: 3px;
  overflow: hidden;
}

.tech-progress {
  height: 100%;
  background: linear-gradient(90deg, #667eea, #764ba2);
  border-radius: 3px;
  transition: width 0.3s ease;
}

/* 工具推荐样式 */
.tool-item-mini {
  display: flex;
  gap: 12px;
  padding: 12px;
  border-radius: 8px;
  cursor: pointer;
  transition: all 0.3s ease;
}

.tool-item-mini:hover {
  background: #f8f9ff;
}

.tool-icon-mini {
  width: 32px;
  height: 32px;
  border-radius: 6px;
}

.tool-info-mini {
  flex: 1;
}

.tool-name-mini {
  font-size: 14px;
  font-weight: 500;
  margin-bottom: 4px;
  color: #333;
}

.tool-desc-mini {
  font-size: 12px;
  color: #666;
  margin-bottom: 4px;
}

.tool-rating {
  display: flex;
  gap: 2px;
}

.star {
  color: #ddd;
  font-size: 12px;
}

.star.active {
  color: #ffd700;
}

/* 导航样式增强 */
.nav-menu a.active {
  background: rgba(255, 255, 255, 0.2);
  color: white;
}

/* 响应式设计 */
@media (max-width: 1200px) {
  .home-container {
    grid-template-columns: 250px 1fr 250px;
    gap: 20px;
  }
}

@media (max-width: 992px) {
  .home-container {
    grid-template-columns: 1fr;
    gap: 20px;
  }
  
  .home-sidebar,
  .home-sidebar-right {
    order: 2;
  }
  
  .home-main {
    order: 1;
  }
}

@media (max-width: 768px) {
  .article-card {
    flex-direction: column;
  }
  
  .article-cover {
    flex: none;
    height: 160px;
  }
}
</style>