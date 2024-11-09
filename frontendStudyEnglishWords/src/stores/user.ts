import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', {
  state: () => ({
    name: '',
    email: '',
    pass: '',
    isAuthenticated: false // Статус авторизации
  }),
  actions: {
    setUser(data: { name: string; email: string; pass: string }) {
      this.name = data.name
      this.email = data.email
      this.pass = data.pass
      this.isAuthenticated = true

      localStorage.setItem(
        'user',
        JSON.stringify({
          name: this.name,
          email: this.email
        })
      )
      console.log('User set:', this.name, this.email)
    },
    loadUser() {
      const savedUser = localStorage.getItem('user')
      if (savedUser) {
        const { name, email } = JSON.parse(savedUser)
        this.name = name
        this.email = email
        this.isAuthenticated = true
        console.log('User loaded:', this.name, this.email)
      }
    },
    logout() {
      this.name = ''
      this.email = ''
      this.pass = ''
      this.isAuthenticated = false

      localStorage.removeItem('user')
      console.log('User logged out')
    }
  }
})
