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
    this.signalr.on('ReceiveMessage', (user, message) => {
      console.log(message)
    })
  },
  created() {
    if (this.roles.includes('admin')) {
      this.currentRole = 'editorDashboard'
    }
  },
  methods: {
    sendmsg() {
      this.signalr.invoke('SendMessage', '123465', '54321')
    }
  }
}
</script>
