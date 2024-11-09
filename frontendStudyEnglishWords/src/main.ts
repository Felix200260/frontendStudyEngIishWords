import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createRouter, createWebHistory } from 'vue-router'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'

import App from './App.vue'
import LoginView from './views/LoginView.vue'
import RegistrationView from './views/RegistrationView.vue'
import MainView from '@/views/MainView.vue'
import ImportCards from '@/views/ImportCards.vue'
import { useUserStore } from '@/stores/user'

const pinia = createPinia()
const app = createApp(App)

const router = createRouter({
  history: createWebHistory(),
  routes: [
    { path: '/', name: 'registration', component: RegistrationView },
    { path: '/login', name: 'login', component: LoginView },
    { path: '/main', name: 'main', component: MainView },
    { path: '/importCards', name: 'importCards', component: ImportCards }
  ]
})

// Глобальная проверка авторизации
router.beforeEach((to, from, next) => {
  const userStore = useUserStore()
  userStore.loadUser() // Загружаем данные пользователя из localStorage

  const isAuthenticated = userStore.isAuthenticated

  if (to.name === 'logout') {
    userStore.logout() // Сбрасываем авторизацию
    next({ name: 'login' })
  } else if (!isAuthenticated && to.name !== 'login' && to.name !== 'registration') {
    next({ name: 'login' }) // Перенаправляем на логин
  } else if (isAuthenticated && (to.name === 'login' || to.name === 'registration')) {
    next({ name: 'main' }) // Если авторизован, перенаправляем на главную
  } else {
    next() // Разрешаем переход
  }
})

app.use(pinia)
app.use(ElementPlus)
app.use(router)

app.mount('#app')
