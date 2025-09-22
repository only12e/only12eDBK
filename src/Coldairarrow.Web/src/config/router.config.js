// eslint-disable-next-line
import { UserLayout, PageView, BlankLayout } from '@/layouts'

/**
 * 基础路由
 * @type { *[] }
 */
export const constantRouterMap = [
  {
    path: '/',
    redirect: '/Home/Login',
    hidden: true
  },
  {
    path: '/Home',
    component: UserLayout,
    redirect: '/Home/Login',
    hidden: true,
    children: [
      {
        path: '/Home/Login',
        name: 'Login',
        component: () => import('@/views/Home/Login')
      }
    ]
  },
  {
    path: '/welcome',
    component: BlankLayout,
    hidden: true,
    children: [
      {
        path: '/welcome',
        name: 'Welcome',
        component: () => import('@/views/Home/Introduce')
      }
    ]
  },
  {
    path: '/404',
    component: () => import('@/views/exception/404')
  },
  {
    path: '/blog-admin-bridge',
    name: 'BlogAdminBridge',
    component: () => import('@/views/BlogWebsite/AdminBridge'),
    hidden: true
  },
  {
    path: '/blog-website',
    name: 'BlogWebsite',
    component: () => import('@/views/BlogWebsite/Index'),
    hidden: true
  },
  {
    path: '/blog-website/articles',
    name: 'BlogArticles',
    component: () => import('@/views/BlogWebsite/Articles'),
    hidden: true
  },
  {
    path: '/blog-website/articles/:id',
    name: 'BlogArticleDetail',
    component: () => import('@/views/BlogWebsite/ArticleDetail'),
    hidden: true
  },
  {
    path: '/blog-website/projects',
    name: 'BlogProjects',
    component: () => import('@/views/BlogWebsite/Projects'),
    hidden: true
  },
  {
    path: '/blog-website/projects/:id',
    name: 'BlogProjectDetail',
    component: () => import('@/views/BlogWebsite/ProjectDetail'),
    hidden: true
  },
  {
    path: '/blog-website/tools',
    name: 'BlogTools',
    component: () => import('@/views/BlogWebsite/Tools'),
    hidden: true
  },
  {
    path: '/blog-website/tools/:id',
    name: 'BlogToolDetail',
    component: () => import('@/views/BlogWebsite/ToolDetail'),
    hidden: true
  },
  {
    path: '/blog-website/profile',
    name: 'BlogUserProfile',
    component: () => import('@/views/BlogWebsite/UserProfile'),
    hidden: true
  }
]
