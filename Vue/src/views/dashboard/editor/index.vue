<template>
  <div class="dashboard-editor-container">
    <panel-group :obj="data" />
    <div>
      <img :src="emptyGif" class="emptyGif">
    </div>
    <div>
      <el-row :gutter="20">
        <el-col :span="6">
          <el-input v-model="obj.visitors" type="number" placeholder="visitors" />
        </el-col>
        <el-col :span="6">
          <el-input v-model="obj.messages" type="number" placeholder="messages" />
        </el-col>
        <el-col :span="6">
          <el-input v-model="obj.purchases" type="number" placeholder="purchases" />
        </el-col>
        <el-col :span="6">
          <el-input v-model="obj.shoppings" type="number" placeholder="shoppings" />
        </el-col>
        <el-button @click="sendData">send</el-button>
      </el-row>
    </div>
  </div>
</template>

<script>
import PanelGroup from './components/PanelGroup'
import { setDashData } from '@/api/dashboard'
export default {
  name: 'DashboardEditor',
  components: {
    PanelGroup
  },
  data() {
    return {
      data: {},
      obj: {
        visitors: 0,
        messages: 0,
        purchases: 0,
        shoppings: 0
      },
      emptyGif:
        'https://wpimg.wallstcn.com/0e03b7da-db9e-4819-ba10-9016ddfdaed3'
    }
  },
  mounted() {
    this.signalr.on('GetDashData', ret => {
      this.data = ret
    })
  },
  methods: {
    sendData() {
      setDashData(this.obj).then(res => {
        this.obj = res.data
      })
    }
  }
}
</script>

<style lang="scss" scoped>
.emptyGif {
  display: block;
  width: 45%;
  margin: 0 auto;
}

.dashboard-editor-container {
  background-color: #e3e3e3;
  min-height: 100vh;
  padding: 32px;
}

.tab {
  display: block;
  width: 100%;
  margin: 0 auto;
}
</style>
