<template>
  <div class="line-chart-container">
    <!-- 图表容器 -->
    <div ref="chartRef" class="chart-wrapper"></div>
   
    <!-- 控制按钮 -->
    <div class="chart-controls">
      <button @click="toggleDataView">
        <i class="fas" :class="isDataView ? 'fa-chart-line' : 'fa-table'"></i>
        {{ isDataView ? '显示图表' : '显示数据' }}
      </button>
    </div>
   
    <!-- 数据表格视图 -->
    <div v-if="isDataView" class="data-table">
      <table>
        <thead>
          <tr>
            <th>时间</th>
            <th>完成任务</th>
            <th>未完成任务</th>
          </tr>
        </thead>
        <tbody>
          <tr v-for="(item, index) in chartData" :key="index">
            <td>{{ item.name }}</td>
            <td :class="'text-green-600' ">{{ item.value.toFixed(2) }}</td>
            <td :class="'text-red-600'">
              {{ item.growth  }}
            </td>
          </tr>
        </tbody>
      </table>
    </div>
  </div>
</template>

<script setup>
import { ref, onMounted, onUnmounted, watch } from 'vue';
import * as echarts from 'echarts';

// 图表容器引用
const chartRef = ref(null);
// 图表实例
let chartInstance = null;

// 数据视图切换
const isDataView = ref(false);


// 图表数据
const chartData = ref([])

// 初始化图表
const initChart = () => {
  // 确保容器存在
  if (!chartRef.value) return;
 
  // 销毁已存在的图表实例
  if (chartInstance) {
    chartInstance.dispose();
  }
 
  // 创建新图表实例
  chartInstance = echarts.init(chartRef.value, null);
 
  // 配置图表选项
  //echarts的配置
  const option = {
    // 标题
    title: {
      text: '当天完成任务数',
      subtext: '数据来源：xxx系统',
      left: 'center'
    },
   
    // 提示框
    tooltip: {
      trigger: 'axis',
      backgroundColor: 'rgba(255, 255, 255, 0.9)',
      borderColor: '#ddd',
      borderWidth: 1,
      textStyle: {
        color: '#333'
      },
      formatter: function(params) {
        const param = params[0];
        const dataItem = chartData.value[param.dataIndex];
        return `<div class="tooltip-content">
          <h4>${param.name}</h4>
          <p>任务完成数：<span style="color: ${'green'}"> ${param.value.toFixed(2)}</span> </p>
          <p>未完成：<span style="color: ${'red'}">
            ${dataItem.growth}</span></p>
        </div>`;
      }
    },

    // 网格
    grid: {
      left: '3%',
      right: '4%',
      bottom: '3%',
      containLabel: true
    },
   
    // x轴配置
    xAxis: {
      type: 'category',
      boundaryGap: false,
      data: chartData.value.map(item => item.name),
      axisLabel: {
        interval: 0
      }
    },
   
    // y轴配置
    yAxis: {
      type: 'value',
      name: '任务完成数',
      min: 0,
      axisLabel: {
        formatter: '{value}'
      }
    },
   
    // 系列数据
    series: [
      {
        name: '任务数',
        type: 'line',
        data: chartData.value.map(item => item.value),
        smooth: true, // 平滑曲线
        symbol: 'circle', // 标记点样式
        symbolSize: 8,
        emphasis: {
          scale: true,
          symbolSize: 10
        },
        lineStyle: {
          width: 3,
        },
        areaStyle: {
          color: {
            type: 'linear',
            x: 0,
            y: 0,
            x2: 0,
            y2: 1,
            colorStops: [{
              offset: 0, color: 'rgba(54, 162, 235, 0.4)'
            }, {
              offset: 1, color: 'rgba(54, 162, 235, 0)'
            }]
          }
        },
        // 动画效果
        animationDuration: 1500,
        animationEasing: 'elasticOut'
      }
      ,
      {
        name: '目标销售额',
        type: 'line',
        data: chartData.value.map(item => item.growth),
        smooth: true,
        symbol: 'diamond',
        symbolSize: 8,
        emphasis: {
          scale: true,
          symbolSize: 10
        },
        lineStyle: {
          width: 3,
          color: '#ff6384',
          type: 'dashed' // 虚线样式区分目标销售额
        },  
        itemStyle: {
          color: '#ff0000', // 节点默认颜色
          borderWidth: 10 // 节点边框宽度
        },
        areaStyle: {
          color: {
            type: 'linear',
            x: 0,
            y: 0,
            x2: 0,
            y2: 1,
            colorStops: [{
              offset: 0, color: 'rgba(255, 99, 132, 0.3)'
            }, {
              offset: 1, color: 'rgba(255, 99, 132, 0)'
            }]
          }
        },
        animationDuration: 1500,
        animationEasing: 'elasticOut',
        animationDelay: 500 // 延迟动画，区分两条线的加载效果
      }
    ]
  };
 
  // 设置图表选项
  chartInstance.setOption(option);
 
};

// 切换数据/图表视图
const toggleDataView = () => {
  isDataView.value = !isDataView.value;
  // 如果切换回图表视图，重新初始化图表
  if (!isDataView.value) {
    initChart();
  }
};


// 窗口大小变化时重绘图表
const handleResize = () => {
  if (chartInstance) {
    chartInstance.resize();
  }
};

// 组件挂载时初始化图表
onMounted(() => {
    chartData.value=[
  { name: '7点', value: 45.2, growth: 30 },
  { name: '8点', value: 52.8, growth: 21 },
  { name: '9点', value: 61.5, growth: 66 },
  { name: '10点', value: 58.3, growth: 67 },
  { name: '11点', value: 65.7, growth: 77 },
  { name: '12点', value: 72.9, growth:120},
  { name: '13点', value: 80.5, growth: 86 },
  { name: '14点', value: 85.3, growth: 52 },
  { name: '15点', value: 200.1, growth: 73 },
  { name: '16点', value: 98.7, growth: 68 },
  { name: '17点', value: 112.5, growth: 138 },
  { name: '18点', value: 180.8, growth: 186 }
];
  initChart();
  window.addEventListener('resize', handleResize);
});

// 组件卸载时销毁图表
onUnmounted(() => {
  if (chartInstance) {
    chartInstance.dispose();
    chartInstance = null;
  }
  window.removeEventListener('resize', handleResize);
});

// 监听主题变化

</script>

<style scoped>
.line-chart-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: 20px;
}

h2 {
  text-align: center;
  color: #333;
  margin-bottom: 20px;
}

.chart-wrapper {
  width: 100%;
  height: 500px;
  border-radius: 8px;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
  transition: all 0.3s ease;
}

.chart-controls {
  display: flex;
  justify-content: center;
  gap: 10px;
  margin: 20px 0;
}

button {
  padding: 8px 16px;
  background-color: #36a2eb;
  color: white;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  display: flex;
  align-items: center;
  gap: 6px;
  transition: background-color 0.3s;
}

button:hover {
  background-color: #258cd1;
}

.data-table {
  margin-top: 20px;
  overflow-x: auto;
}

table {
  width: 100%;
  border-collapse: collapse;
  border-radius: 8px;
  overflow: hidden;
  box-shadow: 0 2px 10px rgba(0, 0, 0, 0.1);
}

th, td {
  padding: 12px 15px;
  text-align: left;
  border-bottom: 1px solid #ddd;
}

th {
  background-color: #f8f9fa;
  font-weight: bold;
}

tr:hover {
  background-color: #f5f5f5;
}

.text-green-600 {
  color: #22c55e;
}

.text-red-600 {
  color: #ef4444;
}

</style>