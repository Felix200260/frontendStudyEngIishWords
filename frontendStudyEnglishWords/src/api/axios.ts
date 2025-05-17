import axios from 'axios';

const axiosInstance = axios.create({
  // baseURL: 'https://localhost:44317',
  baseURL: import.meta.env.VITE_API_URL,
  headers: {
    'Content-Type': 'application/json'
  }// замените на правильный URL вашего ASP.NET сервера
});

export default axiosInstance;
