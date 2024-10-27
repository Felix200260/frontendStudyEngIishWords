<script setup lang="ts"></script>

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
        placeholder="Please input"
      />
      <div class="mt-2.5">
        <el-button size="default" type="primary">Отменить импорт</el-button>
        <el-button size="default" type="danger">Импортировать</el-button>
      </div>
    </div>
  </div>
</template>

<style scoped></style>
