import Vue from 'vue'
import VueRouter from 'vue-router'
import Home from '../views/Home.vue'
import Host from '../views/Host.vue'
import Weapons from '../views/Weapons.vue'
import Credits from '../views/Credits.vue'
import Maps from '../views/Maps.vue'
import Outfits from '../views/Outfits.vue'
import Requests from '../views/Requests.vue'
import ClientSocket from '../views/ClientSocket.vue'

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
      },
      {
        path: 'maps',
        name: 'Maps',
        component: Maps,
      },
      {
        path: 'outfits',
        name: 'Outfits',
        component: Outfits,
      },
      {
        path: 'requests',
        name: 'Requests',
        component: Requests,
      },
    ]
  },
  {
    path: '/Credits',
    name: 'Credits',
    component: Credits
  },
  {
    path: '/Client',
    name: 'Client',
    component: ClientSocket
  },
]

const router = new VueRouter({
  mode: 'hash',
  base: process.env.BASE_URL,
  routes
})

export default router
