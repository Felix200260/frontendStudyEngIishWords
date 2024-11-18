import axios from 'axios'

export async function sendUserAutoDate(dataForm: {
  first_name: string
  unique_email: string
  password: string
}) {
  try {
    const response = await axios.post('/api/user/register', {
      first_name: dataForm.first_name,
      unique_email: dataForm.unique_email,
      password: dataForm.password // Название полей должно совпадать с ожидаемыми на сервере
    })

    console.log('Успешно отправлено:', response.data)
  } catch (error) {
    console.error('Ошибка при отправке данных:', error)
  }
}
