import axios from 'axios';

const axiosInstance = axios.create({
  baseURL: 'https://localhost:44317',
  headers: {
    'Content-Type': 'application/json'
  }// замените на правильный URL вашего ASP.NET сервера
});

export default axiosInstance;
