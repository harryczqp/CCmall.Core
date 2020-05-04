import * as signalR from '@microsoft/signalr'

const signal = new signalR.HubConnectionBuilder().withAutomaticReconnect().withUrl(process.env.VUE_APP_BASE_API + 'chatHub').build()

// 将创建的signal赋值给Vue实例
export default {
  install: function(Vue) {
    Vue.prototype.signalr = signal
  }
}
