import { reactive } from 'vue';

export interface UserForm {
  first_name: string;
  email: string;
  password: string;
  checkPass?: string;
}

export const createUserForm = () =>
  reactive<UserForm>({
    first_name: '',
    email: '',
    password: '',
    checkPass: ''
  });
