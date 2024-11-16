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
        <el-text>Вход</el-text>
      </div>

      <!-- Поле для email -->
      <el-form-item
        prop="email"
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
        <el-input v-model="dataForm.email" />
      </el-form-item>

      <!-- Поле для пароля -->
      <el-form-item label="Пароль" prop="pass">
        <el-input v-model="dataForm.pass" type="password" autocomplete="off" />
      </el-form-item>

      <!-- Кнопки отправки и сброса -->
      <el-form-item>
        <el-button type="primary" @click="submitForm(ruleFormRef)">Войти</el-button>
        <el-button @click="resetForm(ruleFormRef)">Сбросить</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue'
import { useRouter } from 'vue-router'
import { useUserStore } from '@/stores/user' // Подключение Pinia
import axios from 'axios' // Для запросов на сервер
import type { FormProps, FormInstance, FormRules } from 'element-plus'
import { sendUserAutoDate } from '@/service/AuthorizationService'

// Инициализация
const userStore = useUserStore()
const router = useRouter()

const labelPositionAll = ref<FormProps['labelPosition']>('top')

// Данные формы
const dataForm = reactive({
  name: '',
  email: '',
  pass: ''
})

// Валидация
const rules = reactive<FormRules<typeof dataForm>>({
  email: [
    { required: true, message: 'Введите email адрес', trigger: 'blur' },
    { type: 'email', message: 'Введите корректный email адрес', trigger: ['blur', 'change'] }
  ],
  pass: [{ required: true, message: 'Введите пароль', trigger: 'blur' }]
})

// Отправка данных формы
const submitForm = async (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.validate(async (valid) => {
    if (valid) {
      console.log('Данные отправляются...!')

      try {
        // Отправляем данные на сервер
        const response = await sendUserAutoDate(dataForm)

        console.log('Ответ сервера:', response)

        // Сохраняем данные в Pinia
        userStore.setUser({
          name: dataForm.name,
          email: dataForm.email,
          pass: dataForm.pass
        })

        // Очистка полей формы
        dataForm.name = ''
        dataForm.email = ''
        dataForm.pass = ''

        // Перенаправляем пользователя
        router.push({ name: 'main' })
      } catch (error) {
        console.error('Ошибка при отправке данных:', error)
      }
    } else {
      console.log('Ошибка при валидации формы')
    }
  })
}

// Сброс формы
const resetForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.resetFields()
}

const ruleFormRef = ref<FormInstance>()
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
