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
            <el-button
              type="primary"
              :icon="Plus"
              @click="dialogOpenAddDeck = true"
              >Добавить колоду</el-button
            >
          </div>

          <el-dialog
            v-model="dialogOpenAddDeck"
            title="Добавление колоды"
            width="500"
          >
            <el-form :model="form">
              <el-form-item
                label="Название колоды"
                :label-width="formLabelWidth"
              >
                <el-input v-model="form.name" autocomplete="off" />
              </el-form-item>
              <el-form-item label="Категория" :label-width="formLabelWidth">
                <el-select
                  v-model="form.region"
                  placeholder="Выбрать"
                  @change="handleCategoryChange"
                >
                  <el-option
                    v-for="category in categories"
                    :key="category.value"
                    :value="category.value"
                  >
                    <template #default>
                      <div
                        style="
                          display: flex;
                          justify-content: space-between;
                          align-items: center;
                        "
                      >
                        <span>{{ category.label }}</span>
                        <el-button
                          type="text"
                          size="mini"
                          @click.stop="confirmDeleteCategory(category)"
                        >
                          Удалить
                        </el-button>
                      </div>
                    </template>
                  </el-option>
                  <el-option label="+ Добавить" value="add" />
                </el-select>
              </el-form-item>
              <el-form-item label="Описание" :label-width="formLabelWidth">
                <el-input
                  v-model="textarea"
                  style="width: 240px"
                  :rows="2"
                  type="textarea"
                  placeholder="Введите описание"
                />
              </el-form-item>
              <!-- Диалог для добавления новой категории -->
              <el-dialog
                v-model="dialogAddCategoryVisible"
                title="Добавить категорию"
              >
                <el-form>
                  <el-form-item label="Название категории">
                    <el-input v-model="newCategoryName" />
                  </el-form-item>
                </el-form>
                <template #footer>
                  <el-button @click="dialogAddCategoryVisible = false"
                    >Отмена</el-button
                  >
                  <el-button type="primary" @click="addCategory"
                    >Добавить</el-button
                  >
                </template>
              </el-dialog>
              <!-- Диалог для удаления категории -->
              <el-dialog
                v-model="dialogDeleteCategoryVisible"
                title="Удалить категорию"
              >
                <p>
                  Вы уверены, что хотите удалить категорию "{{
                    categoryToDelete?.label
                  }}"?
                </p>
                <template #footer>
                  <el-button @click="dialogDeleteCategoryVisible = false"
                    >Отмена</el-button
                  >
                  <el-button type="danger" @click="deleteCategory"
                    >Удалить</el-button
                  >
                </template>
              </el-dialog>
            </el-form>

            <template #footer>
              <div class="dialog-footer">
                <el-button @click="dialogOpenAddDeck = false">Cancel</el-button>
                <el-button type="primary" @click="addDeck">Создать колоду</el-button>
              </div>
            </template>
          </el-dialog>
          <!---->
          <div class="outputFromSystem">
            <el-icon
              class=""
              style="margin-right: 8px; margin-top: 1px; margin-left: 8px"
            >
              <SwitchButton @click="logout" />
            </el-icon>
            <!--            <router-link to="/">Выйти</router-link>-->
            <button @click="logout">Выйти</button>
          </div>
        </div>
      </el-header>

      <el-main style="height: calc(100vh - 50px)">
        <template v-if="decks.length">
          <div>{{ decks.length }}</div>
          <!-- Список колод -->
          <el-row :gutter="20" style="margin-top: 20px; margin-left: 20px">
            <el-col
              v-for="(card, index) in paginatedDecks"
              :key="index"
              :span="6"
              style="margin-top: 50px"
            >
              <el-card
                class="clickable-card"
                @click="handleCardClick"
                style="max-width: 480px"
              >
                <template #header>
                  <div
                    class="flex"
                    style="
                      display: flex;
                      justify-content: space-between;
                      align-items: center;
                    "
                  >
                    <!-- Заголовок -->
                    <div class="card-header">
                      <span>{{ card.title }}</span>
                    </div>

                    <!-- Крестик -->
                    <div>
                      <button
                        class="close-button"
                        @click="dialogVisibleDeckModal = true"
                      >
                        ✖
                      </button>
                    </div>
                  </div>
                </template>

                <p class="text item">{{ card.description }}</p>
                <template #footer>Footer content</template>
              </el-card>

              <!--/Диалоговое окно о предупреждении о закрытие колоды=====================================================/-->
              <el-dialog
                v-model="dialogVisibleDeckModal"
                title="Предупреждение"
                width="500"
              >
                <span>Вы уверены что хотите удалить колоду?</span>
                <template #footer>
                  <div class="dialog-footer">
                    <el-button @click="dialogVisibleDeckModal = false"
                      >Cancel</el-button
                    >
                    <el-button
                      type="primary"
                      @click="dialogVisibleDeckModal = false"
                    >
                      Confirm
                    </el-button>
                  </div>
                </template>
              </el-dialog>
              <!--/Диалоговое окно=====================================================/-->
            </el-col>
          </el-row>
        </template>
        <template v-else>
          <!-- Заглушка при отсутствии карточек -->
          <div class="empty-placeholder">
            <div class="zaglushka" style="flex-grow: 1">
              <el-icon>
                <Plus />
              </el-icon>
              <p>
                Здесь будут отображаться ваши колоды. Нажмите "Добавить колоду",
                чтобы создать новую.
              </p>
              <el-button type="primary" @click="dialogOpenAddDeck = true">
                Добавить колоду
              </el-button>
            </div>
            <!--Пагинация-->
            <div class="footerForZaglushka">
              <el-pagination
                :current-page="currentPage"
                :page-size="pageSize"
                :total="decks.length"
                class="pagination"
                default-page-size="1"
                layout="prev, pager, next"
                background
                @current-change="currentPage = $event"
              />
            </div>
          </div>
        </template>
        <!-- Диалоговое ок но при нажатие "Добавить колоду" -->
        <el-dialog
          v-model="dialogFormVisible"
          title="Добавить колоду"
          width="700"
        >
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
              <el-button style="margin-top: 20px" size="default">
                <!--                @click="openWindowImportCards"-->
                <router-link to="/importCards"
                  >Импортировать</router-link
                ></el-button
              >
            </el-form-item>
          </el-form>
          <template #footer>
            <div class="dialog-footer">
              <el-button @click="dialogFormVisible = false">Cancel</el-button>
              <el-button type="primary" @click="dialogFormVisible = false">
                Confirm
              </el-button>
            </div>
          </template>
        </el-dialog>
      </el-main>
    </el-container>
  </el-container>
</template>

<script setup lang="ts">
import { computed, reactive, ref } from 'vue';
import { SwitchButton, Message, Setting } from '@element-plus/icons-vue';
import { onMounted } from 'vue';
import { Edit, Plus } from '@element-plus/icons-vue';
import Swiper from 'swiper/bundle';
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import { Navigation, Pagination } from 'swiper/modules';
import { useRouter } from 'vue-router';
import { useUserStore } from '@/stores/user';
import { ElMessageBox } from 'element-plus';
import { addDeckToDB } from '@/service/DeckService';

const router = useRouter();

const navigateToImportCards = () => {
  // router.push('/importCards')
};

//Список категорий , тегов и прочее для сортировки колод===========================================
const categories = ref([
  { label: 'Путешествия', value: 'travel' },
  { label: 'Работа', value: 'work' }
]);
//===========================================
//ологика для обработки выбора категории===========================================
const dialogAddCategoryVisible = ref(false);
const newCategoryName = ref('');

const handleCategoryChange = (value: string) => {
  if (value === 'add') {
    dialogAddCategoryVisible.value = true;
  }
};

const addCategory = () => {
  if (newCategoryName.value.trim()) {
    categories.value.push({
      label: newCategoryName.value,
      value: newCategoryName.value.toLowerCase().replace(/\s+/g, '_')
    });
    newCategoryName.value = '';
    dialogAddCategoryVisible.value = false;
  }
};
//===========================================
//Удаление категорий===========================================
const dialogDeleteCategoryVisible = ref(false);
const categoryToDelete = ref<{ label: string; value: string } | null>(null);

const confirmDeleteCategory = (category: { label: string; value: string }) => {
  //подтверждения удаления
  categoryToDelete.value = category;
  dialogDeleteCategoryVisible.value = true;
};

const deleteCategory = () => {
  if (categoryToDelete.value) {
    categories.value = categories.value.filter(
      (cat) => cat.value !== categoryToDelete.value?.value
    );
    categoryToDelete.value = null;
    dialogDeleteCategoryVisible.value = false;
  }
};

//===========================================

const dialogOpenAddDeck = ref(false);

const userStore = useUserStore();

const logout = () => {
  userStore.logout(); // Вызываем метод logout из Pinia
  router.push({ name: 'login' }); // Перенаправляем на страницу логина
};

// onMounted(() => {
//   new Swiper('.mySwiper', {
//     slidesPerView: 3,
//     grid: {
//       rows: 2
//     },
//     spaceBetween: 30,
//     pagination: {
//       el: '.swiper-pagination',
//       clickable: true
//     },
//     modules: [Navigation, Pagination]
//   })
// })

const handleCardClick = () => {
  // Обработка клика по карточке
  console.log('Card clicked!');
};

const dialogFormVisible = ref(false);
const formLabelWidth = '140px';

const form = reactive({
  name: '',
  region: '',
  date1: '',
  date2: '',
  delivery: false,
  type: [],
  resource: '',
  desc: ''
});

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
];

const textarea = ref('');

//Поиск====================================================================================
interface LinkItem {
  value: string;
  link: string;
}

const state = ref('');
const links = ref<LinkItem[]>([]);

const querySearch = (queryString: string, cb: any) => {
  const results = queryString
    ? links.value.filter(createFilter(queryString))
    : links.value;
  // call callback function to return suggestion objects
  cb(results);
};
const createFilter = (queryString: string) => {
  return (item: LinkItem) => {
    return item.value.toLowerCase().indexOf(queryString.toLowerCase()) === 0;
  };
};
const loadAll = () => {
  return [
    { value: 'vue', link: 'https://github.com/vuejs/vue' },
    { value: 'element', link: 'https://github.com/ElemeFE/element' },
    { value: 'cooking', link: 'https://github.com/ElemeFE/cooking' },
    { value: 'mint-ui', link: 'https://github.com/ElemeFE/mint-ui' },
    { value: 'vuex', link: 'https://github.com/vuejs/vuex' },
    { value: 'vue-router', link: 'https://github.com/vuejs/vue-router' },
    { value: 'babel', link: 'https://github.com/babel/babel' }
  ];
};
const handleSelect = (item: LinkItem) => {
  console.log(item);
};

const handleIconClick = (ev: Event) => {
  console.log(ev);
};

onMounted(() => {
  links.value = loadAll();
});
//Поиск====================================================================================

const action1 = (command: string) => {
  // Замените alert на нужное вам действие
  alert(`Клик на элемент: ${command}`);
};

const item = {
  date: '2016-05-02',
  name: 'Tom',
  address: 'No. 189, Grove St, Los Angeles'
};
const tableData = ref(Array.from({ length: 20 }).fill(item));

//Генерация колод========================================================================================
const decks = ref<{ title: string; description: string }[]>([]);
const addDeck = async () => {
  if (form.name && textarea.value) {
    const newDeck = {
      title: form.name,
      description: textarea.value
    };

    try {
      // Отправляем данные на сервер
      const response = await addDeckToDB(newDeck);

      if (response) {
        // Обновляем локальное состояние
        decks.value.push({ title: form.name, description: textarea.value });
        form.name = '';
        dialogFormVisible.value = false;

        // Убедитесь, что total в пагинации обновлен
        if (
          currentPage.value > Math.ceil(decks.value.length / pageSize.value)
        ) {
          currentPage.value = Math.ceil(decks.value.length / pageSize.value);
        }
      } else {
        console.error('Ошибка при добавлении колоды:', response);
      }
    } catch (error) {
      console.error('Ошибка при добавлении колоды:', error);
    }
  }
};

//Генерация колод========================================================================================
//Пагинация========================================================================================
const currentPage = ref(1); // Текущая страница
const pageSize = ref(2); // Количество колод на странице

const paginatedDecks = computed(() => {
  const start = (currentPage.value - 1) * pageSize.value;
  const end = start + pageSize.value;
  return decks.value.slice(start, end);
});

//Пагинация========================================================================================
//Удаление колод========================================================================================
const dialogVisibleDeckModal = ref(false);
//Удаление колод========================================================================================
</script>

<style scoped>
.layout-container-demo .el-header {
  position: relative;
  background-color: #e9eaec;
  color: white;
  display: flex;
  justify-content: flex-end;
  align-items: center;
  padding: 0 20px;
}

.layout-container-demo .el-aside {
  color: var(--el-text-color-primary);
  background: #e9eaec;
  height: 100vh;
}
/*.layout-container-demo .el-menu {
  border-right: none;
  height: 50px;
}*/

.toolbar {
  display: flex;
  align-items: center;
}

.toolbar > div:nth-child(-n + 3) {
  margin-right: 20px;
}

/* Стили для swiper */
/*.swiper {
  width: 100%;
  height: 100%;
}*/

/*
.swiper-slide {
  text-align: center;
  background: #0041cf;
  border-radius: 15%;
  box-shadow: 10px 10px 10px #5b71a2;
  display: flex;
  justify-content: center;
  align-items: center;
} */

.empty-placeholder {
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
  height: 100%;
  text-align: center;
  color: #9e9e9e;
  font-size: 16px;
}

.empty-placeholder .el-icon {
  font-size: 48px;
  color: #9e9e9e;
  margin-bottom: 16px;
}

.layout-container-demo .el-main {
  height: 100%;
  overflow: hidden; /* Убирает ненужный скролл */
}

.zaglushka {
}

.clickable-card {
  cursor: pointer;
}
</style>
