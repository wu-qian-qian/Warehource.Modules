import { defineStore } from 'pinia'
import request from '@/utils/request'



export const useUserStore = defineStore('user', {
  state: () => ({
    token: localStorage.getItem('token'), // 从localStorage读取Token（刷新页面保持）
    userInfo: {}, // 用户基础信息（如用户名、角色）
    navData: [{ index: "/", title: "加载中" }] // 登录后加载的导航数据
  }),
  actions: {
    // 1. 登录方法：获取Token + 用户信息
    async login(data) {
      try {
        const res = await request.post('/user/user-login', data) // 登录接口（账号密码传参）
        if(res.data.isSuccess){
        // 存储Token到localStorage和状态
        localStorage.setItem('token', res.data.value)
        await this.getNavData()
        return true // 登录成功返回
        }
       else{
            console.error('登录失败:')
       }
     
      } catch (error) {
        console.error('登录失败:', error)
        return false // 登录失败返回
      }
    },

    // 2. 获取导航数据（需携带Token）
    async getNavData() {
      try {
      console.log(this.navData)
       this.navData = [
      {
        "index": "1",
        "title": "Home",
        "icon": "Home",
        "children": [
          { "index": "/home", "title": "首页", "icon": "HomeFilled" },
            { "index": "/user", "title": "用户", "icon": "User" },
              { "index": "/about", "title": "关于", "icon": "InfoFilled" }
        ]
      }
    ]
        // const res = await request.get('/api/navigations') // 导航接口（需Token验证）
        // this.navData = res.data // 存储导航数据到全局状态
      } catch (error) {
        console.error('获取导航数据失败:', error)
        this.navData = [{ index: "/", title: "加载中"}] // 失败时清空
      }
    },

    // 3. 登出方法：清空状态 + 移除Token
    logout() {
      this.token = ''
      this.userInfo = {}
      this.navData =[{ index: "/", title: "加载中"}]
      localStorage.removeItem('token') // 清除localStorage中的Token
    }
  }
})
