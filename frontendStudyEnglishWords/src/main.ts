import { createApp } from 'vue'
import { createPinia } from 'pinia'
import { createRouter, createWebHistory } from 'vue-router'
import ElementPlus from 'element-plus'
import 'element-plus/dist/index.css'
import axios from 'axios'

import App from './App.vue'
import LoginView from './views/LoginView.vue'
import RegistrationView from './views/RegistrationView.vue'
import MainView from '@/views/MainView.vue'
import ImportCards from '@/views/ImportCards.vue'
import { useUserStore } from '@/stores/user'

// Проверка работы бэкенда
async function checkBackend(): Promise<boolean> {
  try {
    const response = await axios.get('http://localhost:5000/api/health') // URL для проверки бэкенда
    console.log('Backend is running:', response.data)
    return true
  } catch (error) {
    console.warn('Backend is not running:', error)
    return false
  }
}

async function main() {
  // Проверка работы бэкенда перед запуском приложения
  const backendAvailable = await checkBackend()
  if (!backendAvailable) {
    console.warn('Backend is not available. Starting app in offline mode.')
  }

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
      next({ name: 'registration' }) // Разрешаем доступ к странице регистрации
    } else {
      next() // Разрешаем переход
    }
  })

  app.use(pinia)
  app.use(ElementPlus)
  app.use(router)

  app.mount('#app') // Монтируем приложение только после завершения всех проверок
}

main() // Запуск приложения
