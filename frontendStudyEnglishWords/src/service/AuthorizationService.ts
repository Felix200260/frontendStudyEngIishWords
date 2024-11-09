import axios from 'axios'
export async function sendUserAutoDate(name: string, email: string, password: string) {
  try {
    const response = await axios.post('/api/user/register', {
      name,
      email,
      password
    })

    console.log('Успешно отправлено:', response.data)
    return response.data
  } catch (error) {
    console.log('гг, данные неотправлены')
    throw error
  }
}
