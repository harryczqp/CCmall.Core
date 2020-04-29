<template>
  <div class="dashboard-container">
    <el-button @click="sendmsg">send</el-button>
    <component :is="currentRole" />
  </div>
</template>

<script>
import { mapGetters } from 'vuex'
import adminDashboard from './admin'
import editorDashboard from './editor'
import * as signalR from '@microsoft/signalr'

const hubUrl = 'http://localhost:5123/chatHub'
const connection = new signalR.HubConnectionBuilder().withAutomaticReconnect().withUrl(hubUrl).build()
connection.start().catch(err => alert(err.message))

export default {
  name: 'Dashboard',
  components: { adminDashboard, editorDashboard },
  data() {
    return {
      currentRole: 'editorDashboard'
    }
  },
  computed: {
    ...mapGetters([
      'roles'
    ])
  },
  created() {
    if (this.roles.includes('admin')) {
      this.currentRole = 'editorDashboard'
    }
  },
  methods: {
    sendmsg() {
      connection.invoke('SendPrivateMessage')
    }
  }
}
</script>
