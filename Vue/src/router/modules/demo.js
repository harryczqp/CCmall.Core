/** When your routing table is too long, you can split it into small modules **/

import Layout from '@/layout'

const demoRouter = {
  path: '/demo',
  component: Layout,
  redirect: '/table/complex-table',
  name: 'Demo',
  meta: {
    title: 'tabledemo',
    icon: 'table'
  },
  children: [
    {
      path: 'complex-table',
      component: () => import('@/views/Demo/demo-table'),
      name: 'ComplexTable',
      meta: { title: 'testtabel', icon: 'table' }
    },
    {
      path: 'index',
      component: () => import('@/views/pdf/index'),
      name: 'PDF',
      meta: { title: 'pdf', icon: 'pdf' }
    }
  ]
}
export default demoRouter
