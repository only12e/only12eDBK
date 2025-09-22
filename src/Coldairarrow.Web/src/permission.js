import router from '@/router'
import NProgress from 'nprogress' // progress bar
import 'nprogress/nprogress.css' // progress bar style
import { setDocumentTitle, domTitle } from '@/utils/domUtil'
import TokenCache from '@/utils/cache/TokenCache'
import OperatorCache from '@/utils/cache/OperatorCache'
import { initRouter } from '@/utils/routerUtil'
import defaultSettings from '@/config/defaultSettings'

NProgress.configure({ showSpinner: false }) // NProgress Configuration

const whiteList = ['Login'] // no redirect whitelist

router.beforeEach((to, from, next) => {
  NProgress.start() // start progress bar
  to.meta && (typeof to.meta.title !== 'undefined' && setDocumentTitle(`${to.meta.title} - ${domTitle}`))

  // 如果访问的是登录页面，直接放行
  if (to.path === '/Home/Login') {
    next()
    return
  }

  // 博客页面路由（直接放行，不需要认证）
  if (to.path.startsWith('/blog-website') || to.path.startsWith('/blog-admin-bridge')) {
    next()
    return
  }

  // 管理端路由认证检查
  if (TokenCache.getToken()) {
    OperatorCache.init(() => {
      initRouter(to, from, next).then(() => {
        const redirect = decodeURIComponent(from.query.redirect || to.path)
        //桌面特殊处理
        if (to.path == defaultSettings.desktopPath || to.path == '/404') {
          next()
        } else {
          if (to.path === redirect) {
            next()
          } else {
            // 跳转到目的路由
            next({ path: redirect })
          }
        }
      })
    })
    return
  }

  // 白名单检查
  if (whiteList.includes(to.name)) {
    next()
    return
  }

  // 默认跳转到管理系统登录页面
  next({ path: '/Home/Login', query: { redirect: to.fullPath } })
  NProgress.done()
})

router.afterEach(() => {
  NProgress.done() // finish progress bar
})