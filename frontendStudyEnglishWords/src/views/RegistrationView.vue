<template>
  <div class="form">
    <el-form
      ref="ruleFormRef"
      :model="dataForm"
      :rules="rules"
      status-icon
      :label-position="labelPositionAll"
      label-width="auto"
      class="main-form"
    >
      <div style="text-align: center">
        <el-text>Регистрация</el-text>
      </div>
      <!-- Поле для имени -->
      <el-form-item style="margin-top: 20px" label="Имя">
        <el-input v-model="dataForm.first_name" />
      </el-form-item>

      <!-- Поле для email -->
      <el-form-item
        prop="unique_email"
        label="Email"
        :rules="[
          {
            required: true,
            message: 'Введите email адрес',
            trigger: 'blur'
          },
          {
            type: 'email',
            message: 'Введите корректный email адрес',
            trigger: ['blur', 'change']
          }
        ]"
      >
        <el-input v-model="dataForm.unique_email" />
      </el-form-item>
      <el-alert v-if="emailError" title="Пользователь с таким email уже существует" type="error" />

      <!-- </el-form-item> -->

      <!-- Поле для пароля -->
      <el-form-item label="Пароль" prop="pass">
        <el-input
          v-model="dataForm.password"
          type="password"
          autocomplete="off"
        />
      </el-form-item>

      <!-- Поле для подтверждения пароля -->
      <el-form-item label="Подтвердите пароль" prop="checkPass">
        <el-input
          v-model="dataForm.checkPass"
          type="password"
          autocomplete="off"
        />
      </el-form-item>

      <!-- Кнопки отправки и сброса -->
      <el-form-item>
        <el-button type="primary" @click="submitForm(ruleFormRef)">
          Зарегистрироваться
        </el-button>
        <el-button @click="resetForm(ruleFormRef)">Reset</el-button>
      </el-form-item>

      <div style="text-align: center">
        <el-button>
          <router-link to="/login" style="text-decoration: none; color: black">Войти</router-link>
        </el-button>
      </div>
    </el-form>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue';
import type { FormProps, FormInstance, FormRules } from 'element-plus';
import { useUserStore } from '@/stores/user';
import { useRouter } from 'vue-router'; //добавил для возможности по нажатию кнопки переходить по route
import { sendUserAutoDate } from '@/service/AuthorizationService';

const labelPositionAll = ref<FormProps['labelPosition']>('top');
const emailError = ref('');

const router = useRouter(); //добавил для возможности по нажатию кнопки переходить по route
const userStore = useUserStore();
userStore.loadUser();

const submitForm = async (formEl: FormInstance | undefined) => {
  if (!formEl) return;
  emailError.value = ''; // сбрасываем ошибку перед отправкой
  formEl.validate(async (valid) => {
    if (valid) {
      try {
        // Отправляем данные на сервер
        await sendUserAutoDate({
          first_name: dataForm.first_name,
          unique_email: dataForm.unique_email,
          password: dataForm.password
        });

        // Устанавливаем пользователя без id
        userStore.setUser({
          id: 0,
          first_name: dataForm.first_name,
          unique_email: dataForm.unique_email,
          password: dataForm.password
        });

        router.push({ name: 'main' });
      } catch (error: any) {
        // Если это ошибка 409 (email уже существует)
        if (error.response && error.response.status === 409) {
          emailError.value = 'Пользователь с таким email уже существует';
        } else {
          emailError.value = 'Произошла ошибка регистрации';
        }
      }
    }
  });
};



const resetForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return; //todo ?WTF
  formEl.resetFields();
  //Вручную удаляю
  dataForm.first_name = '';
  dataForm.unique_email = '';
  dataForm.password = '';
  dataForm.checkPass = '';
};

const ruleFormRef = ref<FormInstance>(); //todo ?WTF

const messageValidate = (rule: any, value: any, callback: any) => {
  if (value === '') {
    callback(new Error('Введите пароль'));
  } else {
    if (dataForm.checkPass !== '') {
      if (!ruleFormRef.value) return;
      ruleFormRef.value.validateField('checkPass');
    }
    callback();
  }
};
const messageValidate2 = (rule: any, value: any, callback: any) => {
  if (value === '') {
    callback(new Error('Введите пароль ещё раз'));
  } else if (value !== dataForm.password) {
    callback(new Error('Пароли не совпадают!'));
  } else {
    callback();
  }
};

interface UserForm {
  first_name: string;
  unique_email: string;
  password: string;
  checkPass: string;
}

const dataForm = reactive<UserForm>({
  first_name: '',
  unique_email: '',
  password: '',
  checkPass: ''
});

const rules = reactive<FormRules<UserForm>>({
  password: [{ required: true, validator: messageValidate, trigger: 'blur' }], //todo ?WTF
  checkPass: [{ required: true, validator: messageValidate2, trigger: 'blur' }]
});
</script>

<style scoped>
.form {
  align-items: center;
  justify-content: center;
}
.main-form {
  background-color: white;
  padding: 20px;
  border-radius: 10px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.1);
  max-width: 300px;
  width: 100%;
  margin: 0 auto;
  height: 100vh;
}
</style>
