import { defineStore } from 'pinia';

export const useUserStore = defineStore('user', {
  state: () => ({
    id: null as number | null,
    name: '',
    email: '',
    pass: '',
    isAuthenticated: false
  }),
  actions: {
    setUser(data: {
      id: number;
      first_name: string;
      unique_email: string;
      password: string;
    }) {
      this.id = data.id;
      this.name = data.first_name;
      this.email = data.unique_email;
      this.pass = data.password;
      this.isAuthenticated = true;

      localStorage.setItem(
        'user',
        JSON.stringify({
          id: this.id,
          name: this.name,
          email: this.email
        })
      );
      console.log('User set:', this.id, this.name, this.email);
    },
    loadUser() {
      const savedUser = localStorage.getItem('user');
      if (savedUser) {
        const { id, name, email } = JSON.parse(savedUser);
        this.id = id;
        this.name = name;
        this.email = email;
        this.isAuthenticated = true;
        console.log('User loaded:', this.name, this.email);
      }
    },
    logout() {
      this.name = '';
      this.email = '';
      this.pass = '';
      this.isAuthenticated = false;

      localStorage.removeItem('user');
      console.log('User logged out');
    }
  }
});
