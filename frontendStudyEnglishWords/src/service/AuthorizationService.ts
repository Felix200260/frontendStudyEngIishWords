import axios from 'axios'

export async function sendUserAutoDate(dataForm: { name: string; email: string; pass: string }) {
  try {
    const response = await axios.post('/api/user/register', {
      name: dataForm.name,
      email: dataForm.email,
      password: dataForm.pass // Название поля должно совпадать с ожидаемым бэкендом
    })

    console.log('Успешно отправлено:', response.data)
  } catch (error) {
    console.error('Ошибка при отправке данных:', error)
  }
}
