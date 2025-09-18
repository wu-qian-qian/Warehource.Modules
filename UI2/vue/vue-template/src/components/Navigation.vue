<template>
  <el-radio-group v-model="isCollapse" style="margin-bottom: 20px" >
    <el-radio-button :value="false">expand</el-radio-button>
    <el-radio-button :value="true">collapse</el-radio-button>
  </el-radio-group>
  <el-menu
    default-active="0"
    class="el-menu-vertical-demo"
    :collapse="isCollapse"
    router
    @open="handleOpen"
    @close="handleClose">
 <!-- 遍历一级菜单 -->
    <template v-for="(item, index) in userStore.navData" :key="item.index">
      <!-- 有子菜单的情况 -->
      <el-sub-menu v-if="item.children && item.children.length" :index="item.index">
        <template #title>
          <el-icon>
            <component :is="getIconComponent(item.icon)" />
          </el-icon>
          <span>{{ item.title }}</span>
        </template>
       
        <!-- 遍历二级菜单 -->
        <el-menu-item
          v-for="(child, childIndex) in item.children"
          :key="child.index"
          :index="child.index"
        >
          <el-icon>
            <component :is="getIconComponent(child.icon)" />
          </el-icon>
          <span>{{ child.title }}</span>
        </el-menu-item>
      </el-sub-menu>

      <!-- 无子菜单的一级菜单 -->
      <el-menu-item v-else :index="item.index">
        <el-icon>
          <component :is="getIconComponent(item.icon)" />
        </el-icon>
        <span>{{ item.title }}</span>
      </el-menu-item>
    </template>
  </el-menu>
</template>

<script lang="ts" setup>
import { ref,onMounted } from 'vue'
import {
  Menu as IconMenu,
  Location,
  HomeFilled,
  User,
  InfoFilled,
  Setting
} from '@element-plus/icons-vue'
import { pinia } from '@/main'
import { useUserStore } from '@/store/modules/userStore'
const userStore = useUserStore(pinia)
// 加载状态
const loading = ref(true)
// 折叠状态
const isCollapse = ref(false)

// 图标映射表（将接口返回的图标名称映射到实际组件）
const iconMap = {
  Location,
  User,
  InfoFilled,
  HomeFilled,
  Setting
  // 添加其他可能用到的图标
}

// 获取图标组件
const getIconComponent = (iconName) => {
  return iconMap[iconName] || Location // 默认图标
}




const handleOpen = (key: string, keyPath: string[]) => {
  console.log(key, keyPath)
}
const handleClose = (key: string, keyPath: string[]) => {
  console.log(key, keyPath)
}
</script>

<style>

</style>