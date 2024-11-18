import { reactive } from 'vue'

export interface UserForm {
  first_name: string
  unique_email: string
  password: string
  checkPass?: string
}

export const createUserForm = () =>
  reactive<UserForm>({
    first_name: '',
    unique_email: '',
    password: '',
    checkPass: ''
  })
