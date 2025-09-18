<script setup>
import Navigation from './components/Navigation.vue'
import Header from './components/Header.vue'
</script>


<template>
  <div class="app-container">
    <el-container class="full-height">
      <!-- 第一部分：顶部Header -->
      <el-header class="header"><Header></Header></el-header>
     
      <!-- 第二部分：占据剩余所有空间 -->
      <el-container class="remaining-space">
        <el-aside class="aside">
          <Navigation msg="XXX系统" />
        </el-aside>
        <el-main class="main-content">
          <router-view />
        </el-main>
      </el-container>
    </el-container>
  </div>
</template>



<style scoped>
.app-container {
  /* 让整个应用占满浏览器视口高度 */
  height: 94vh;
  overflow: hidden;
}

.full-height {
  /* 让容器继承父元素高度 */
  height: 94vh;
  display: flex;
  flex-direction: column;
}

.header {
  /* 可以固定顶部高度，也可以根据内容自适应 */
  height: 60px;
  background-color: #f5f5f5;
  border-bottom: 1px solid #e5e5e5;
}

.remaining-space {
  /* 这部分会自动占据剩余所有空间 */
  flex: 1;
  display: flex;
  overflow: hidden;
}

.aside {
  /* 侧边栏宽度可根据需要调整 */
  width: 210px;
  background-color: #f0f2f5;
  border-right: 1px solid #e5e5e5;
  /* overflow-y: auto; 内容超出时可滚动 */
}
</style>