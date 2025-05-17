import { fileURLToPath, URL } from 'node:url';

import { defineConfig } from 'vite';
import vue from '@vitejs/plugin-vue';
import vueJsx from '@vitejs/plugin-vue-jsx';
import vueDevTools from 'vite-plugin-vue-devtools';

// https://vitejs.dev/config/
export default defineConfig({
  plugins: [
    vue(),
    vueDevTools({
      launchEditor:
        'C:\\Program Files\\JetBrains\\WebStorm 2024.3.5\\bin\\webstorm64.exe'
    }),
    vueJsx()
  ],
  resolve: {
    alias: {
      '@': fileURLToPath(new URL('./src', import.meta.url))
    }
  },
  server: {
    port: 5173, // Vite Dev Server порт
    proxy: {
      '/api': {
        target: 'http://localhost:44317', // URL вашего ASP.NET Core бэкенда
        changeOrigin: true,
        secure: false
      }
    }
  },
  build: {
    outDir: '../wwwroot' // Путь для выходных файлов фронтенда
  }
});
