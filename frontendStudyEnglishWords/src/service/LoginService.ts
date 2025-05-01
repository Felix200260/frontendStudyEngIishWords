import api from '@/api/axios'

export async function LogingUser(data: { email: string; password: string }) {
  try {
    const response = await api.post('/api/User/login', {
      email: data.email,
      password: data.password
    });

    console.log('Вход выполнен:', response.data);
    return response.data;
  } catch (error: any) {
    console.error('Ошибка при входе:', error.response?.data || error.message);
    throw new Error(error.response?.data?.message || 'Ошибка при входе');
  }
}
