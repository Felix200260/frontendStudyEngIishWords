import { defineStore } from 'pinia'

export const useUserStore = defineStore('user', {
  state: () => ({
    name: '',
    email: '',
    pass: ''
  }),
  actions: {
    setUser(data: { name: string; email: string; pass: string }) {
      this.name = data.name
      this.email = data.email
      this.pass = data.pass

      localStorage.setItem('user', JSON.stringify({ name: this.name, email: this.email }))
      console.log('User set:', this.name, this.email) // Отладка
    },
    loadUser() {
      const savedUser = localStorage.getItem('user')
      if (savedUser) {
        const { name, email } = JSON.parse(savedUser)
        this.name = name
        this.email = email
        console.log('User loaded:', this.name, this.email) // Отладка
      }
    },

    logout() {
      this.name = ''
      this.email = ''
      this.pass = ''
      localStorage.removeItem('user')
    }
  }
})
