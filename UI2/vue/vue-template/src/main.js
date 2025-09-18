// 补充导入 createApp（这一行是缺失的）
import { createApp } from 'vue';
//npm install pinia
import { createPinia } from 'pinia' // 导入Pinia
import App from './App.vue';
//npm install axios
import axios from 'axios';
//npm install element-plus --save
import ElementPlus from 'element-plus';
import 'element-plus/dist/index.css';
//npm install vue-router@4
import router from './router' // 引入路由配置
//npm install echarts --save

const app = createApp(App);
export const pinia = createPinia()
// 配置全局axios 后端地址
app.config.globalProperties.$axios = axios;
app.use(pinia);
app.use(router);
// 注册ElementPlus
app.use(ElementPlus);
// 挂载应用
app.mount('#app');