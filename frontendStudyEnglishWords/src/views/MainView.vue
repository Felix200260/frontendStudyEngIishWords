<template>
  <el-container class="layout-container-demo" style="height: 100vh">
    <el-aside width="200px">
      <el-scrollbar>
        <el-menu :default-openeds="['1', '3']">
          <el-sub-menu index="1">
            <template #title>
              <el-icon><message /></el-icon>Колоды
            </template>
            <el-menu-item-group>
              <el-menu-item index="1">Колода 1</el-menu-item>
              <el-menu-item index="2">Колода 2</el-menu-item>
              <el-menu-item index="3">Колода 3</el-menu-item>
              <el-menu-item index="4">Колода 4</el-menu-item>
            </el-menu-item-group>
          </el-sub-menu>
        </el-menu>
      </el-scrollbar>
    </el-aside>

    <el-container>
      <el-header style="text-align: right; font-size: 12px; height: 50px">
        <div class="toolbar">
          <el-dropdown @command="action1">
            <el-icon style="margin-right: 8px; margin-top: 1px">
              <setting />
            </el-icon>
            <template #dropdown>
              <el-dropdown-menu>
                <el-dropdown-item command="action1">View</el-dropdown-item>
                <el-dropdown-item command="action2">Add</el-dropdown-item>
                <el-dropdown-item command="action3">Delete</el-dropdown-item>
              </el-dropdown-menu>
            </template>
          </el-dropdown>
          <span>Tom</span>
          <el-icon style="margin-right: 8px; margin-top: 1px; margin-left: 8px">
            <SwitchButton />
          </el-icon>
          <router-link to="/">Выйти</router-link>
        </div>
      </el-header>

      <el-main style="height: calc(100vh - 50px)"> </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { Menu as IconMenu, SwitchButton, Message, Setting } from '@element-plus/icons-vue'

const action1 = (command: string) => {
  // Замените alert на нужное вам действие
  alert(`Клик на элемент: ${command}`)
}

const item = {
  date: '2016-05-02',
  name: 'Tom',
  address: 'No. 189, Grove St, Los Angeles'
}
const tableData = ref(Array.from({ length: 20 }).fill(item))
</script>

<style scoped>
.layout-container-demo .el-header {
  position: relative;
  background-color: #79bbff;
  color: var(--el-text-color-primary);
  display: flex;
  justify-content: flex-end;
  align-items: center;
  padding: 0 20px;
  border-radius: 10px 10px 0 0; /* Закругляем верхний блок */
}
.layout-container-demo .el-aside {
  color: var(--el-text-color-primary);
  background: #79bbff;
  height: 100vh;
  border-radius: 10px 0 0 10px; /* Закругляем боковой блок */
}
.layout-container-demo .el-menu {
  border-right: none;
}
.layout-container-demo .el-main {
  padding: 0;
  border-radius: 0 10px 10px 0; /* Закругляем главный контент */
}
.layout-container-demo .toolbar {
  display: inline-flex;
  align-items: center;
  justify-content: center;
  height: 100%;
}
</style>
