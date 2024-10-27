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
          <div class="DeckSearch">
            <el-autocomplete
              v-model="state"
              :fetch-suggestions="querySearch"
              popper-class="my-autocomplete"
              placeholder="Найти колоду"
              @select="handleSelect"
            >
              <template #suffix>
                <el-icon class="el-input__icon" @click="handleIconClick">
                  <edit />
                </el-icon>
              </template>
              <template #default="{ item }">
                <div class="value">{{ item.value }}</div>
                <span class="link">{{ item.link }}</span>
              </template>
            </el-autocomplete>
          </div>
          <!--Добавить колоду-->
          <div class="addDeck">
            <el-button type="primary" :icon="Plus">Добавить колоду</el-button>
          </div>
          <!---->
          <div class="outputFromSystem">
            <el-icon class="" style="margin-right: 8px; margin-top: 1px; margin-left: 8px">
              <SwitchButton />
            </el-icon>
            <router-link to="/">Выйти</router-link>
          </div>
        </div>
      </el-header>

      <el-main style="height: calc(100vh - 50px)">
        <!-- Swiper -->
        <div class="swiper mySwiper">
          <div class="swiper-wrapper">
            <div class="swiper-slide">Slide 1</div>
            <div class="swiper-slide">Slide 2</div>
            <div class="swiper-slide">Slide 3</div>
            <div class="swiper-slide">Slide 4</div>
            <div class="swiper-slide">Slide 5</div>
            <div class="swiper-slide">Slide 6</div>
            <div class="swiper-slide">Slide 7</div>
            <div class="swiper-slide">Slide 8</div>
            <div class="swiper-slide">Slide 9</div>
          </div>
          <div class="swiper-pagination"></div>
        </div>
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import { ref } from 'vue'
import { SwitchButton, Message, Setting } from '@element-plus/icons-vue'
import { onMounted } from 'vue'
import { Edit, Plus } from '@element-plus/icons-vue'
import Swiper from 'swiper/bundle'
import 'swiper/css'
import 'swiper/css/navigation'
import 'swiper/css/pagination'
import { Navigation, Pagination } from 'swiper/modules'

onMounted(() => {
  new Swiper('.mySwiper', {
    slidesPerView: 3,
    grid: {
      rows: 2
    },
    spaceBetween: 30,
    pagination: {
      el: '.swiper-pagination',
      clickable: true
    },
    modules: [Navigation, Pagination]
  })
})

//Поиск====================================================================================
interface LinkItem {
  value: string
  link: string
}

const state = ref('')
const links = ref<LinkItem[]>([])

const querySearch = (queryString: string, cb: any) => {
  const results = queryString ? links.value.filter(createFilter(queryString)) : links.value
  // call callback function to return suggestion objects
  cb(results)
}
const createFilter = (queryString: string) => {
  return (item: LinkItem) => {
    return item.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0
  }
}
const loadAll = () => {
  return [
    { value: 'vue', link: 'https://github.com/vuejs/vue' },
    { value: 'element', link: 'https://github.com/ElemeFE/element' },
    { value: 'cooking', link: 'https://github.com/ElemeFE/cooking' },
    { value: 'mint-ui', link: 'https://github.com/ElemeFE/mint-ui' },
    { value: 'vuex', link: 'https://github.com/vuejs/vuex' },
    { value: 'vue-router', link: 'https://github.com/vuejs/vue-router' },
    { value: 'babel', link: 'https://github.com/babel/babel' }
  ]
}
const handleSelect = (item: LinkItem) => {
  console.log(item)
}

const handleIconClick = (ev: Event) => {
  console.log(ev)
}

onMounted(() => {
  links.value = loadAll()
})
//Поиск====================================================================================

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

.toolbar {
  display: flex;
  align-items: center;
}

.toolbar > div:nth-child(-n + 3) {
  margin-right: 20px;
}

/* Стили для swiper */
.swiper {
  width: 100%;
  height: 100%;
}

.swiper-slide {
  text-align: center;
  background: #0041cf;
  border-radius: 15%;
  box-shadow: 10px 10px 10px #5b71a2;
  display: flex;
  justify-content: center;
  align-items: center;
}
</style>
