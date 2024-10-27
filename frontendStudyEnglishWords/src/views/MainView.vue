<template>
  <el-container class="layout-container-demo" style="height: 100vh">
    <el-aside width="200px">
      <el-scrollbar>
        <el-menu>
          <el-sub-menu>
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
            <el-button type="primary" :icon="Plus" @click="dialogFormVisible = true"
              >Добавить колоду</el-button
            >
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
        <!--        &lt;!&ndash; Swiper &ndash;&gt;-->
        <!--        <div class="swiper mySwiper">-->
        <!--          <div class="swiper-wrapper">-->
        <!--            <div class="swiper-slide">Slide 1</div>-->
        <!--            <div class="swiper-slide">Slide 2</div>-->
        <!--            <div class="swiper-slide">Slide 3</div>-->
        <!--            <div class="swiper-slide">Slide 4</div>-->
        <!--            <div class="swiper-slide">Slide 5</div>-->
        <!--            <div class="swiper-slide">Slide 6</div>-->
        <!--            <div class="swiper-slide">Slide 7</div>-->
        <!--            <div class="swiper-slide">Slide 8</div>-->
        <!--            <div class="swiper-slide">Slide 9</div>-->
        <!--          </div>-->
        <!--          <div class="swiper-pagination"></div>-->
        <!--        </div>-->

        <el-dialog v-model="dialogFormVisible" title="Добавить колоду" width="700">
          <el-form :model="form">
            <el-form-item label="Название колоды" :label-width="formLabelWidth">
              <el-input v-model="form.name" autocomplete="off" />
            </el-form-item>
            <el-form-item :label-width="formLabelWidth">
              <el-input
                v-model="textarea"
                :autosize="{ minRows: 2, maxRows: 4 }"
                type="textarea"
                placeholder="Please input"
              />
              <el-button style="margin-top: 20px" size="default" @click="openWindowImportCards">
                <router-link to="/importCards">Импортировать</router-link></el-button
              >
            </el-form-item>
          </el-form>
          <template #footer>
            <div class="dialog-footer">
              <el-button @click="dialogFormVisible = false">Cancel</el-button>
              <el-button type="primary" @click="dialogFormVisible = false"> Confirm </el-button>
            </div>
          </template>
        </el-dialog>
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import { reactive, ref } from 'vue'
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

const dialogFormVisible = ref(false)
// const formLabelWidth = '140px'

const form = reactive({
  name: '',
  region: '',
  date1: '',
  date2: '',
  delivery: false,
  type: [],
  resource: '',
  desc: ''
})

const gridData = [
  {
    date: '2016-05-02',
    name: 'John Smith',
    address: 'No.1518,  Jinshajiang Road, Putuo District'
  },
  {
    date: '2016-05-04',
    name: 'John Smith',
    address: 'No.1518,  Jinshajiang Road, Putuo District'
  },
  {
    date: '2016-05-01',
    name: 'John Smith',
    address: 'No.1518,  Jinshajiang Road, Putuo District'
  },
  {
    date: '2016-05-03',
    name: 'John Smith',
    address: 'No.1518,  Jinshajiang Road, Putuo District'
  }
]

const textarea = ref('')

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
  background-color: #0a092d;
  color: white;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  padding: 0 20px;
}
.layout-container-demo .el-aside {
  color: var(--el-text-color-primary);
  background: #0a092d;
  height: 100vh;
}
.layout-container-demo .el-menu {
  border-right: none;
  height: 50px;
}
.layout-container-demo .el-main {
  padding: 0;
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
