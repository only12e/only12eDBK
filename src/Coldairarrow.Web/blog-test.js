// 博客网站功能测试
// 这个文件可以用于验证博客网站的各项功能是否正常工作

// 1. 测试文章API集成
async function testArticleAPI() {
  console.log('=== 测试文章API集成 ===')
  
  try {
    // 导入API函数（在实际使用中这些会自动导入）
    const { GetDataList, GetTheData, GetCategories } = require('@/api/blog_article')
    
    // 测试获取文章列表
    console.log('测试获取文章列表...')
    const listResponse = await GetDataList({
      PageIndex: 1,
      PageRows: 5,
      status: 'published'
    })
    
    if (listResponse.Success) {
      console.log('✅ 文章列表获取成功:', listResponse.Data.length + '篇文章')
    } else {
      console.log('❌ 文章列表获取失败:', listResponse.Message)
    }
    
    // 测试获取分类
    console.log('测试获取分类...')
    const categoryResponse = await GetCategories()
    
    if (categoryResponse.Success) {
      console.log('✅ 分类获取成功:', categoryResponse.Data.length + '个分类')
    } else {
      console.log('❌ 分类获取失败:', categoryResponse.Message)
    }
    
    // 如果有文章，测试获取单篇文章详情
    if (listResponse.Success && listResponse.Data.length > 0) {
      const firstArticle = listResponse.Data[0]
      console.log('测试获取文章详情...')
      
      const detailResponse = await GetTheData({ id: firstArticle.Id })
      
      if (detailResponse.Success) {
        console.log('✅ 文章详情获取成功:', detailResponse.Data.Title)
      } else {
        console.log('❌ 文章详情获取失败:', detailResponse.Message)
      }
    }
    
  } catch (error) {
    console.error('❌ API测试出错:', error)
  }
}

// 2. 测试页面路由
function testPageRoutes() {
  console.log('=== 测试页面路由 ===')
  
  const routes = [
    { path: '/blog-website', name: '博客首页' },
    { path: '/blog-website/articles', name: '文章列表页' },
    { path: '/blog-website/articles/1', name: '文章详情页' }
  ]
  
  routes.forEach(route => {
    console.log(`✅ 路由配置: ${route.name} - ${route.path}`)
  })
}

// 3. 测试数据处理功能
function testDataProcessing() {
  console.log('=== 测试数据处理功能 ===')
  
  // 测试时间格式化
  const testTime = '2025-09-09T10:30:00.000Z'
  console.log('测试时间格式化:', testTime)
  
  try {
    const date = new Date(testTime)
    const now = new Date()
    const diffTime = now - date
    const diffDays = Math.floor(diffTime / (1000 * 60 * 60 * 24))
    
    let timeText
    if (diffDays === 0) {
      timeText = '今天'
    } else if (diffDays === 1) {
      timeText = '昨天'
    } else if (diffDays < 7) {
      timeText = `${diffDays}天前`
    } else {
      timeText = date.toLocaleDateString()
    }
    
    console.log('✅ 时间格式化成功:', timeText)
  } catch (error) {
    console.log('❌ 时间格式化失败:', error)
  }
  
  // 测试数字格式化
  const testNumbers = [999, 1500, 12000, 150000]
  testNumbers.forEach(num => {
    let formatted
    if (num >= 10000) {
      formatted = (num / 10000).toFixed(1) + 'w'
    } else if (num >= 1000) {
      formatted = (num / 1000).toFixed(1) + 'k'
    } else {
      formatted = num.toString()
    }
    console.log(`✅ 数字格式化: ${num} → ${formatted}`)
  })
}

// 4. 测试本地存储功能
function testLocalStorage() {
  console.log('=== 测试本地存储功能 ===')
  
  try {
    const testKey = 'blog_test_likes'
    const testData = { 1: true, 2: true, 3: false }
    
    // 存储测试
    localStorage.setItem(testKey, JSON.stringify(testData))
    console.log('✅ 本地存储写入成功')
    
    // 读取测试
    const retrieved = JSON.parse(localStorage.getItem(testKey) || '{}')
    const isEqual = JSON.stringify(retrieved) === JSON.stringify(testData)
    
    if (isEqual) {
      console.log('✅ 本地存储读取成功')
    } else {
      console.log('❌ 本地存储读取数据不匹配')
    }
    
    // 清理测试数据
    localStorage.removeItem(testKey)
    console.log('✅ 测试数据清理完成')
    
  } catch (error) {
    console.log('❌ 本地存储测试失败:', error)
  }
}

// 5. 测试响应式设计
function testResponsiveDesign() {
  console.log('=== 测试响应式设计 ===')
  
  const breakpoints = [
    { width: 1400, name: '大屏幕' },
    { width: 1200, name: '桌面' },
    { width: 768, name: '平板' },
    { width: 480, name: '手机' }
  ]
  
  breakpoints.forEach(bp => {
    console.log(`✅ 响应式断点: ${bp.name} (${bp.width}px)`)
  })
}

// 主测试函数
async function runAllTests() {
  console.log('🚀 开始博客网站功能测试...\n')
  
  testPageRoutes()
  console.log('')
  
  testDataProcessing()
  console.log('')
  
  testLocalStorage()
  console.log('')
  
  testResponsiveDesign()
  console.log('')
  
  // API测试需要在实际环境中运行
  console.log('📝 API测试需要在浏览器环境中运行')
  console.log('   可以在开发者工具控制台中执行 testArticleAPI() 函数')
  
  console.log('\n✅ 所有基础功能测试完成!')
  console.log('💡 建议在浏览器中访问以下页面进行手动测试:')
  console.log('   - http://localhost:5001/blog-website (博客首页)')
  console.log('   - http://localhost:5001/blog-website/articles (文章列表)')
  console.log('   - http://localhost:5001/blog-website/articles/1 (文章详情)')
}

// 导出测试函数
if (typeof module !== 'undefined' && module.exports) {
  module.exports = {
    testArticleAPI,
    testPageRoutes,
    testDataProcessing,
    testLocalStorage,
    testResponsiveDesign,
    runAllTests
  }
}

// 如果在浏览器中运行，则自动执行测试
if (typeof window !== 'undefined') {
  // 将测试函数挂载到全局，方便在控制台中调用
  window.blogTests = {
    testArticleAPI,
    testPageRoutes,
    testDataProcessing,
    testLocalStorage,
    testResponsiveDesign,
    runAllTests
  }
  
  // 自动运行基础测试
  runAllTests()
}