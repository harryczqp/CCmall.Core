<template>
  <div class="dashboard-editor-container">
    <panel-group :obj="obj" />
    <div>
      <img :src="emptyGif" class="emptyGif">
    </div>
  </div>
</template>

<script>
import PanelGroup from './components/PanelGroup'
export default {
  name: 'DashboardEditor',
  components: {
    PanelGroup
  },
  data() {
    return {
      obj: {
        visitors: 12,
        messages: 13,
        purchases: 14,
        shoppings: 15
      },
      emptyGif: 'https://wpimg.wallstcn.com/0e03b7da-db9e-4819-ba10-9016ddfdaed3'
    }
  },
  mounted() {

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
</style>
