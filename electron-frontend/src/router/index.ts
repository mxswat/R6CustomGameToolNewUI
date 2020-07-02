import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Host from '../views/Host.vue'
import Weapons from '../views/Weapons.vue'
import Credits from '../views/Credits.vue'

Vue.use(VueRouter)

  const routes = [
  {
    path: '/',
    name: 'Home',
    component: Home
  },
  {
    path: '/host',
    name: 'Host',
    component: Host,
    children: [
      {
        path: 'weapons',
        name: 'Weapons',
        component: Weapons,
        children: [
          
        ]
      },
    ]
  },
  {
    path: '/Credits',
    name: 'Credits',
    component: Credits
  },
]

const router = new VueRouter({
  mode: 'hash',
  base: process.env.BASE_URL,
  routes
})

export default router
