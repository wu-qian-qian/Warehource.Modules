Header.vue
<template>
  <div class="demo-type">
      <el-image :src="imageRes" style="width: 50px; height: 50px;float: left;"></el-image>
       <h2 style="margin: 10px;">xxxx系统</h2>
    <div style="margin-right: 10px;flex: 1;">
      <el-link type="primary" style="float: right;margin: 10px;" @click="openDrawer">{{ name }}</el-link>
      <el-avatar style="float: right;"
        src="https://cube.elemecdn.com/0/88/03b0d39583f48206768a7534e55bcpng.png"/>
         
    </div>
  </div>
 
  <el-drawer v-model="drawer" title="I am the title" :with-header="false"   class="login-drawer" >
       <div>
        <el-row>
             <el-image :src="imagelogin" style="width: 55px; height: 55px;float: left;"></el-image>
        </el-row>
        <el-row style="margin-bottom: 25px;margin-top: 25px;">
          <el-col :span="4">
              <el-image :src="imageuser" style="width: 25px; height: 25px;float: left;"></el-image>
          </el-col>
           <el-col :span="8">
             <el-input v-model="loginForm.username"  style="width: 240px;" placeholder="请输入用户名" />
          </el-col>
        </el-row>
        <el-row>
          <el-col :span="4">
             <el-image :src="imagepwd" style="width: 25px; height: 25px;float: left;"></el-image>
          </el-col>
           <el-col :span="8">
               <el-input
                v-model="loginForm.password"
                style="width: 240px;"
                type="password"
                placeholder="输入密码"
                show-password/>
          </el-col>
        </el-row>
        <el-row style="margin-top: 25px;">
          <el-col :span="12">
            <el-button type="primary" @click="handleLogin">登录</el-button>
          </el-col>
          <el-col :span="12">
            <el-button type="primary" @click="handleLogout">退出登录</el-button>
          </el-col>
        </el-row>
    </div>
  </el-drawer>
</template>

<script setup lang="ts">
import { ref, Text,reactive } from 'vue';  // 导入 ref
import { ElMessage } from 'element-plus'
import { useUserStore } from '@/store/modules/userStore'
import { useRouter } from 'vue-router'
const router = useRouter() // 获取路由实例

// 1. 导入 main.js 中创建的 Pinia 实例（关键：替换 usePinia()）
import { pinia } from '@/main'
const imageRes='/login.png'
const imagelogin='/login.png'
const imageuser='/user.png'
const imagepwd='/pwd.png'

const userStore = useUserStore(pinia)

// 1. 用 ref 定义响应式变量（初始值可以是空或默认值）
const name = ref("游客");  // 注意：ref 定义的变量需要用 .value 访问/修改
const drawer = ref(false)


// 登录表单数据
const loginForm = reactive({
  username: 'admin', // 测试账号
  password: 'qwe' // 测试密码
})
// 定义点击事件处理函数：切换抽屉状态
const openDrawer = () => {
  drawer.value = !drawer.value; // 取反操作（显示 ↔ 隐藏）
};

// 登录按钮点击事件
const handleLogin = async () => {
  // 调用Pinia的login方法
   
  const loginSuccess = await userStore.login(loginForm)
  if (loginSuccess) {
    ElMessage.success('登入成功')
    openDrawer()
  } else {
    ElMessage.error('账号或密码错误')
  }
}

const handleLogout=()=>{
// 清除 localStorage 中的 token
      userStore.logout(loginForm)
      router.push('/')
     ElMessage.success('登出')
      openDrawer()
}


</script>

<style scoped>
.demo-type {
  display: flex;
}
.demo-type > div:not(:last-child) {
  border-right: 1px solid var(--el-border-color);
}
</style>