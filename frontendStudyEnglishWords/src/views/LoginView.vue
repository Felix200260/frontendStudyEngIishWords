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
        <el-button type="primary" @click="submitForm(ruleFormRef)"> Submit</el-button>
        <el-button @click="resetForm(ruleFormRef)">Reset</el-button>
      </el-form-item>
    </el-form>
  </div>
</template>

<script lang="ts" setup>
import { reactive, ref } from 'vue'
import type { FormProps, FormInstance, FormRules } from 'element-plus'

const labelPositionAll = ref<FormProps['labelPosition']>('top')

const submitForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.validate((valid) => {
    if (valid) {
      console.log('submit!')
    } else {
      console.log('error submit!')
    }
  })
}

const resetForm = (formEl: FormInstance | undefined) => {
  if (!formEl) return
  formEl.resetFields()
}

const ruleFormRef = ref<FormInstance>()

const messageValidate = (rule: any, value: any, callback: any) => {
  if (value === '') {
    callback(new Error('Введите пароль'))
  } else {
    if (dataForm.checkPass !== '') {
      if (!ruleFormRef.value) return
      ruleFormRef.value.validateField('checkPass')
    }
    callback()
  }
}
const messageValidate2 = (rule: any, value: any, callback: any) => {
  if (value === '') {
    callback(new Error('Введите пароль ещё раз'))
  } else if (value !== dataForm.pass) {
    callback(new Error('Пароли не совпадают!'))
  } else {
    callback()
  }
}

const dataForm = reactive({
  // сохранение данных формы
  email: '',
  pass: ''
})

const rules = reactive<FormRules<typeof dataForm>>({
  pass: [{ required: true, validator: messageValidate, trigger: 'blur' }],
  checkPass: [{ required: true, validator: messageValidate2, trigger: 'blur' }]
})
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
