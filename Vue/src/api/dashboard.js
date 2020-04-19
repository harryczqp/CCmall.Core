import request from '@/utils/request'

export function getCount() {
  return request({
    url: '/dashboard/getlist',
    method: 'get'
  })
}

export function getTrace(data) {
  return request({
    url: '/dashboard/getTraceData',
    method: 'post',
    data
  })
}

export function getLevel() {
  return request({
    url: '/dashboard/getLevel',
    method: 'get'
  })
}

export function getFather() {
  return request({
    url: '/dashboard/getFather',
    method: 'get'
  })
}
