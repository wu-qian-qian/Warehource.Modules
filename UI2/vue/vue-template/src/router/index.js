import { createRouter, createWebHistory } from 'vue-router'
import { useUserStore } from '@/store/modules/userStore'
// 1. 导入 main.js 中创建的 Pinia 实例（关键：替换 usePinia()）
import { pinia } from '@/main'



// 定义路由规则
const routes = [
  //, meta: { requiresAuth: true }  是否登入
   { path: '/home',name: 'Home',component: () => import('@/views/Home.vue') },
    { path: '/user',name: 'User', component: () => import('@/views/User.vue') },
       { path: '/about', name: 'About',component: () => import('@/views/About.vue') },
  {
    path: '/', // 404 页面（匹配未定义的路径）
    name: 'Main',
    component: () => import('@/views/Main.vue')
  },
   {
    path: '/NotFound', // 404 页面（匹配未定义的路径）
    name: 'NotFound',
    component: () => import('@/views/NotFound.vue')
  }
]
const router = createRouter({
  history: createWebHistory(),
  routes
})

// 全局前置守卫
router.beforeEach((to, from, next) => {
  // 2. 直接使用导入的 pinia 实例获取 Store（无需 usePinia()）
  const userStore = useUserStore(pinia)

  if (to.meta.requiresAuth) {
    if (userStore.token) {
      if (userStore.navData.length === 0) {
        userStore.getNavData().then(() => next())
      } else {
        next()
      }
    } else {
      next({ path: '/home', query: { redirect: to.fullPath } })
    }
  } else {
    next()
  }
})

export default router