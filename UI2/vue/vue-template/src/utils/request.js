import axios from 'axios'
import { useUserStore } from '@/store/modules/userStore' // 导入用户Store
import router from '@/router'

const request = axios.create({
  baseURL: "http://101.126.12.178:8080", // 环境变量（如http://localhost:3000）
  timeout: 5000
})

// 请求拦截器：添加Token到请求头
request.interceptors.request.use(
  (config) => {
    const userStore = useUserStore()
    if (userStore.token) {
      // 格式：Authorization: Bearer Token（后端约定，也可能是其他格式）
      config.headers.Authorization = `Bearer ${userStore.token}`
    }
    return config
  },
  (error) => Promise.reject(error)
)

// 响应拦截器：处理401未授权（Token过期/无效）
request.interceptors.response.use(
  (response) => response,
  (error) => {
    if (error.response?.status === 401) {
      const userStore = useUserStore()
      userStore.logout() // 清空状态
      router.push('/') // 跳登录页
      ElMessage.error('登录已过期，请重新登录') // 需导入Element Plus的Message
    }
    return Promise.reject(error)
  }
)

export default request
