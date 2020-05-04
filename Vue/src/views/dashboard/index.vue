<template>
  <div class="dashboard-container">
    <el-button @click="sendmsg">
      <span>message</span>
    </el-button>
    <component :is="currentRole" />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import adminDashboard from './admin'
import editorDashboard from './editor'

export default {
  name: 'Dashboard',
  components: { adminDashboard, editorDashboard },
  data() {
    return {
      currentRole: 'editorDashboard'
    }
  },
  computed: {
    ...mapGetters(['roles'])
  },
  mounted() {
    this.signalr.start().catch(err => alert(err.message))
  },
  created() {
    if (this.roles.includes('admin')) {
      this.currentRole = 'editorDashboard'
    }
    // this.signalr.on("SendMessage").then(res => console.log(res));
  },
  methods: {
    sendmsg() {
      this.signalr.invoke('SendMessage', '123465', '54321')
      this.signalr.on('SendMessage', (user, message) => {
        console.log(message)
      })
    }
  }
}
</script>
