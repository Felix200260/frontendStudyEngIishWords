<template>
  <div class="import">
    <div>
      <h3>Импортировать данные</h3>
      <span>Скопируйте и вставьте свои данные</span>
    </div>
    <div>
      <el-input
        v-model="textarea"
        :autosize="{ minRows: 2, maxRows: 4 }"
        type="textarea"
        placeholder="Слово 1 Определение 1
Слово 2 Определение 2
Слово З Определение З"
      />
      <div class="mt-2.5">
        <el-button size="default" type="primary"
          ><router-link to="main">Отменить импорт</router-link></el-button
        >
        <el-button size="default" type="danger"
          ><router-link to="main">Импортировать</router-link></el-button
        >
      </div>
    </div>
  </div>
</template>
<script setup lang="ts">
import { ref } from 'vue';

const textarea = ref('');
</script>

<style scoped></style>
