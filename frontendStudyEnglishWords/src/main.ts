import './assets/main.css'
import { createRouter, createWebHistory } from 'vue-router'

import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import App from './App.vue'

import LoginView from './views/LoginView.vue'

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'login',
      component: LoginView
    }
  ]
})

const app = createApp(App)
app.use(router)
app.use(ElementPlus)
app.mount('#app')
