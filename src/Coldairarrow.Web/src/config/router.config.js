// eslint-disable-next-line
import { UserLayout, PageView, BlankLayout } from '@/layouts'

/**
 * 基础路由
 * @type { *[] }
 */
export const constantRouterMap = [
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
  }
]
