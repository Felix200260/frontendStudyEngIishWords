import axios from 'axios'
import dataForm from '@/views/RegistrationView.vue'
export async function sendUserAutoDate() {
  try {
    const response = await axios.post('/api/user/register', {
      name: dataForm.name,
      email: dataForm.email,
      password: dataForm.pass
    })

    console.log('Успешно отправлено:', response.data)
    dataForm.name = ''
    dataForm.email = ''
    dataForm.pass = ''
    dataForm.checkPass = ''
  } catch (error) {
    console.log('гг, данные неотправлены')
  }
}
