import './assets/main.css'
import { createRouter, createWebHistory } from 'vue-router'

import { createApp } from 'vue'
import ElementPlus from 'element-plus'
import { createPinia } from 'pinia'
import 'element-plus/dist/index.css'
import App from './App.vue'

import LoginView from './views/LoginView.vue'
import RegistrationView from './views/RegistrationView.vue'
import MainView from '@/views/MainView.vue'
import { useUserStore } from '@/stores/user'
import ImportCards from '@/views/ImportCards.vue'
import './index.css'

const pinia = createPinia()
const app = createApp(App)

const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'registration',
      component: RegistrationView
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/main',
      name: 'main',
      component: MainView
    },
    {
      path: '/importCards',
      name: 'importCards',
      component: ImportCards
    }
  ]
})

//to.name === 'login' || to.name === 'registration') — проверяем, пытается ли пользователь перейти на страницы логина

app.use(pinia)
app.use(ElementPlus)
app.use(router)

// router.beforeEach((to, from, next) => {
//   const userStore = useUserStore()
//   userStore.loadUser()
//   const isRegistered = localStorage.getItem('isRegistered') === 'true' // Проверка, регистрировался ли пользователь
//   const isLoggedIn = userStore.name && userStore.email // Проверка, авторизован ли пользователь
//   // если незарегистрирован
//   if (!isRegistered && to.name !== 'registration') {
//     next({ name: 'registration' })
//     //зарегистрирован, но вышел
//   } else if (isRegistered && !isLoggedIn && to.name !== 'login') {
//     next({ name: 'login' })
//     //если авторизован, то просто перенаправляем на главное меню
//   } else if (isLoggedIn && (to.name === 'login' || to.name === 'registration')) {
//     next({ name: 'main' })
//   } else {
//     next()
//   }
// })

app.mount('#app')
