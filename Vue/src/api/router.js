import request from '@/utils/request'

export function getRouter() {
  return request({
    url: '/router/list',
    method: 'get'
  })
}
