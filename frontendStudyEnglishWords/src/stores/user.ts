import { defineStore } from 'pinia';

export const useUserStore = defineStore('user', {
  state: () => ({
    name: '',
    email: '',
    pass: '',
    isAuthenticated: false
  }),
  actions: {
    setUser(data: {
      first_name: string;
      unique_email: string;
      password: string;
    }) {
      this.name = data.first_name;
      this.email = data.unique_email;
      this.pass = data.password;
      this.isAuthenticated = true;

      localStorage.setItem(
        'user',
        JSON.stringify({
          name: this.name,
          email: this.email
        })
      );
      console.log('User set:', this.name, this.email);
    },
    loadUser() {
      //todo ?WTF
      const savedUser = localStorage.getItem('user'); //todo ?WTF
      if (savedUser) {
        const { name, email } = JSON.parse(savedUser);
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
