import { asyncRoutes, constantRoutes } from '@/router'
import { getRouter } from '@/api/router'

/**
 * Use meta.role to determine if the current user has permission
 * @param roles
 * @param route
 */
function hasPermission(roles, route) {
  if (route.meta && route.meta.roles) {
    return roles.some(role => route.meta.roles.includes(role))
  } else {
    return true
  }
}

/**
 * Filter asynchronous routing tables by recursion
 * @param routes asyncRoutes
 * @param roles
 */
export function filterAsyncRoutes(routes, roles) {
  const res = []

  routes.forEach(route => {
    const tmp = { ...route }
    if (hasPermission(roles, tmp)) {
      if (tmp.children) {
        tmp.children = filterAsyncRoutes(tmp.children, roles)
      }
      res.push(tmp)
    }
  })

  return res
}

const state = {
  routes: [],
  addRoutes: []
}

const mutations = {
  SET_ROUTES: (state, routes) => {
    state.addRoutes = routes
    state.routes = constantRoutes.concat(routes)
  }
}

const actions = {
  generateRoutes({ commit }, roles) {
    return new Promise(resolve => {
      getRouter().then(ret => {
        var dynamicRouter = dynamicRouterFilter(ret.data)
        dynamicRouter = asyncRoutes.concat(dynamicRouter)
        let accessedRoutes
        if (roles.includes('admin')) {
          accessedRoutes = dynamicRouter || []
        } else {
          accessedRoutes = filterAsyncRoutes(dynamicRouter, roles)
        }
        commit('SET_ROUTES', accessedRoutes)
        resolve(accessedRoutes)
      })
    })
  }
}

// 动态路由递归
function dynamicRouterFilter(data) {
  var list = []
  list = data
  list.filter(router => {
    router.component = () => import(router.component)
    if (router.children && router.children.length) {
      router.children = dynamicRouterFilter(router.children)
    }
    return true
  })
  return list
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
