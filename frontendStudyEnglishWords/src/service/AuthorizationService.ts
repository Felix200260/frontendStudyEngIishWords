import axiosInstance from '@/api/axios';

export async function sendUserAutoDate(dataForm: {
  first_name: string;
  unique_email: string;
  password: string;
}) {
  try {
    const response = await axiosInstance.post('/api/user/register', {
      first_name: dataForm.first_name,
      unique_email: dataForm.unique_email,
      password: dataForm.password
    });

    console.log('Успешно отправлено:', response.data);
    return response.data;
  } catch (error) {
    console.error('Ошибка при отправке данных:', error);
    throw error;
  }
}
