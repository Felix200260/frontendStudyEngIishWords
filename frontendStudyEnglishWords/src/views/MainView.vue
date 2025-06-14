<template>
  <el-container class="layout-container-demo" style="height: 100vh">
    <el-header style="height: 60px; padding: 0 20px; display: flex; align-items: center; justify-content: space-between; background-color: #fff; border-bottom: 1px solid #e4e7ed;">
      <div style="display: flex; align-items: center;">
        <span style="color: black; font-size: 18px; font-weight: 600;">
          StudyEnglishWords
        </span>
      </div>

      <div style="display: flex; align-items: center; gap: 15px;">
        <!-- Поиск колоды -->
        <div class="DeckSearch">
          <el-autocomplete
            v-model="state"
            clearable
            placeholder="Найти колоду"
            :fetch-suggestions="querySearch"
            @select="handleSelect"
            style="width: 200px;"
          >
            <template #suffix>
              <el-icon class="el-input__icon" @click="handleIconClick">
                <Edit />
              </el-icon>
            </template>
            <template #default="{ item }">
              <div>{{ item.value }}</div>
            </template>
          </el-autocomplete>
        </div>

        <!-- Добавить колоду -->
        <el-button
          type="primary"
          :icon="Plus"
          @click="dialogOpenAddDeck = true"
        >
          Добавить колоду
        </el-button>

        <!-- Выход из системы -->
        <el-icon
          style="cursor: pointer; font-size: 16px; color: black;"
          @click="logout"
        >
          <SwitchButton />
        </el-icon>

        <!-- Профиль -->
        <div style="display: flex; align-items: center; gap: 8px; color: black;">
          <el-icon>
            <UserFilled />
          </el-icon>
          <span>{{ userStore.email }}</span>
        </div>
      </div>
    </el-header>

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
          <div class="m-4">
            <el-select
              v-model="form.categories"
              multiple
              placeholder="Выбрать"
              style="width: 240px"
            >
              <el-option
                v-for="category in categories"
                :key="category.id"
                :label="category.title"
                :value="category"
              >
                <template #default>
                  <div
                    style="
                      display: flex;
                      justify-content: space-between;
                      align-items: center;
                    "
                  >
                    <span>{{ category.title }}</span>
                    <el-button
                      type="text"
                      size="small"
                      @click.stop="confirmDeleteCategory(category)"
                    >
                      Удалить
                    </el-button>
                  </div>
                </template>
              </el-option>
              <el-button
                style="
                  display: flex;
                  justify-content: center;
                  align-items: center;
                  margin: 10px;
                "
                :label="`Добавить`"
                @click="createNewCategory()"
              >
                <template #default>
                  <span style="color: #409eff; font-weight: bold">
                    <el-icon
                      style="vertical-align: middle; margin-right: 4px"
                    ><Plus
                    /></el-icon>
                    Добавить
                  </span>
                </template>
              </el-button>
            </el-select>
          </div>
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
          <el-button type="primary" @click="addDeck"
          >Создать колоду</el-button
          >
        </div>
      </template>
    </el-dialog>
      <!---->
      <!----Редактирование колоды-->
      <el-dialog
        v-model="dialogOpenEditDeck"
        title="Редактирование колоды"
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
            <div class="m-4">
              <el-select
                v-model="form.categories"
                multiple
                collapse-tags
                :max-collapse-tags="1"
                placeholder="Выбрать категорию"
                style="width: 240px"
              >
                <el-option
                  v-for="category in categories"
                  :key="category.id"
                  :label="category.title"
                  :value="category.id"
                >
                  <template #default>
                    <div
                      style="
                        display: flex;
                        justify-content: space-between;
                        align-items: center;
                      "
                    >
                      <span>{{ category.title }}</span>
                      <el-button
                        type="text"
                        size="small"
                        @click.stop="deleteCategory(category.id)"
                      >
                        Удалить
                      </el-button>
                    </div>
                  </template>
                </el-option>
                <!--Добавить категорию-->

                <!--                <el-button-->
                <!--                  style="-->
                <!--                    display: flex;-->
                <!--                    justify-content: center;-->
                <!--                    align-items: center;-->
                <!--                    margin: 10px;-->
                <!--                  "-->
                <!--                  :label="`Добавить категорию`"-->
                <!--                  @click="handleCategoryChange('add')"-->
                <!--                >-->
                <!--                  <template #default>-->
                <!--                    <span style="color: #409eff; font-weight: bold">-->
                <!--                      <el-icon-->
                <!--                        style="vertical-align: middle; margin-right: 4px"-->
                <!--                      ><Plus-->
                <!--                      /></el-icon>-->
                <!--                      Добавить категорию-->
                <!--                    </span>-->
                <!--                  </template>-->
                <!--                </el-button>-->


                <template #footer>
                  <el-button v-if="!isAdding" text bg size="small" @click="onAddCategories">
                    Вести название новой категории
                  </el-button>
                  <template v-else>
                    <el-input
                      v-model="categoriesName"
                      class="option-input"
                      placeholder="Введите название категории"
                      size="small"
                    />
                    <el-button type="primary" size="small" @click="createNewCategory()">
                      Добавить категорию
                    </el-button>
                    <el-button size="small" @click="clearCategories">Отмена</el-button>
                  </template>
                </template>

                <!--Добавить категорию-->
              </el-select>
            </div>
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
            <el-button type="primary" @click="saveEditedDeck"
            >Изменить колоду</el-button
            >
          </div>
        </template>
      </el-dialog>

      <el-scrollbar
        style="height: calc(100vh - 50px)"
        class="scrollbar-demo-item"
      >
        <el-main>
          <template v-if="decks.length && showDecksList">
            <div>Вы создали вот столько колод: {{ decks.length }}</div>
            <!-- Список колод -->
            <el-row :gutter="20" style="margin-top: 20px; margin-left: 20px">
              <el-col
                v-for="(card, index) in filteredDecks"
                :key="index"
                :span="6"
                style="margin-top: 50px"
              >
                <el-card
                  class="clickable-card"
                  @click="() => handleCardClick(card)"
                  style="max-width: 480px"
                >
                  <template #header>
                    <div style="display: flex; justify-content: space-between">
                      <!-- Заголовок -->
                      <div class="card-header">
                        <span>{{ card.title }}</span>
                      </div>

                      <!-- Редактирование -->
                      <div class="mr-5" style="display: flex">
                        <div style="margin-right: 10px">
                          <el-tooltip content="Редактировать параметры колоды">
                            <el-button
                              class="edit-button"
                              @click.stop="openEditDeck(card)"
                            >
                              <el-icon>
                                <Edit />
                              </el-icon>
                            </el-button>
                          </el-tooltip>
                        </div>
                        <!-- Крестик -->
                        <div>
                          <el-tooltip content="Удалить колоду">
                            <el-button
                              class="close-button"
                              @click.stop="handleDeleteClick(card.id)"
                            >
                              ✖
                            </el-button>
                          </el-tooltip>
                        </div>
                      </div>
                    </div>
                  </template>

                  <div>
                    <div style="margin: 10px">{{ card.description }}</div>
                  </div>

                  <template #footer>
                    <div style="display: flex; flex-wrap: wrap">
                      <template v-if="card.id && deckCategories.get(card.id)?.length">
                        <div
                          v-for="(category, index) in deckCategories.get(card.id)"
                          :key="category.id"
                          style="margin-right: 5px; margin-top: 5px"
                        >
                          <el-tag :type="getTagColor(index)">
                            {{ category.title }}
                          </el-tag>
                        </div>
                      </template>
                      <template v-else>
                        <div style="margin-right: 5px; margin-top: 5px">
                          <el-tag type="info">Без категории</el-tag>
                        </div>
                      </template>
                    </div>
                  </template>
                </el-card>
              </el-col>
            </el-row>
          </template>
          <template v-else-if="!showDecksList && selectedDeckId && !isOpenWindowStudyWords">
            <!--/Создание карт=====================================================/-->
            <CreateCards
              :deck-id="selectedDeckId"
              :existing-cards="selectedDeckCards"
              :deck-data="selectedDeck"
              @close="showDecksList = true"
              @deck-updated="handleDeckUpdated"
              @open-study-window="handleOpenStudyWindow"
            ></CreateCards>
            <!--/Создание карт=====================================================/-->
          </template>
          <template v-if="isOpenWindowStudyWords">
            <StudyWords
              :deck-id="selectedDeck?.id"
              :deck-name="selectedDeck?.title"
              :cards="selectedDeckCards"
              @closeWindowStudyWords="handleCloseWindowStudyWords"
            >
            </StudyWords>
          </template>
          <template v-else-if="!decks.length">
            <div class="empty-placeholder">
              <el-icon><Box /></el-icon>
              <p>У вас пока нет колод</p>
              <el-button type="primary" @click="dialogOpenAddDeck = true">
                Создать первую колоду
              </el-button>
            </div>
          </template>
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
                >Отмена
                </el-button>
                <el-button
                  type="primary"
                  @click="
                        () =>
                          deckIdToDelete !== null && removeDeck(deckIdToDelete)
                      "
                >
                  Да, удалить
                </el-button>
              </div>
            </template>
          </el-dialog>
          <!--/Диалоговое окно=====================================================/-->
          <el-dialog
            v-model="dialogFormVisible"
            title="Добавить колоду"
            width="700"
          >
            <el-form :model="form">
              <el-form-item
                label="Название колоды"
                :label-width="formLabelWidth"
              >
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
                  <router-link to="/importCards">Импортировать </router-link>
                </el-button>
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
      </el-scrollbar>
  </el-container>
</template>

<script setup lang="ts">
import { computed, onMounted, reactive, ref } from 'vue';
import {
  Edit,
  Plus,
  SwitchButton,
  UserFilled
} from '@element-plus/icons-vue';
import 'swiper/css';
import 'swiper/css/navigation';
import 'swiper/css/pagination';
import { useRouter } from 'vue-router';
import { useUserStore } from '@/stores/user';
import {
  addDeckToDB,
  deleteDeck,
  getUserDecks,
  updateDeck
} from '@/service/DeckService';
import { DeckDto } from '@/models/DeckDto';
import { createCategory, getCategoriesByDeck, getUserCategories } from '@/service/CategoriesService';
import { CategoriesDto } from '@/models/CategoriesDto';
import CreateCards from '@/components/CreateCards.vue';
import { getCardsByDeckId } from '@/service/CardService';
import { CardDto } from '@/models/CardDto';
import { ElMessage } from 'element-plus';
import StudyWords from '@/components/StudyWords.vue';

const router = useRouter();

const navigateToImportCards = () => {
  // router.push('/importCards')
};

//Список категорий , тегов и прочее для сортировки колод===========================================
const categories = ref<CategoriesDto[]>([]);
//===========================================
//Логика для обработки выбора категории===========================================
const categoriesName = ref('')

// Функция для создания категории
const createNewCategory = async () => {
  if (!categoriesName.value?.trim()) {
    console.warn('Название категории не может быть пустым');
    return;
  }
  try {
    console.log("categoriesName.value.trim():", categoriesName.value.trim());
    const newCategory = await createCategory(categoriesName.value.trim());

    // Добавляем в локальный список категорий
    categories.value.push(newCategory);

    // Очищаем поле и закрываем диалог
    categoriesName.value = '';

    console.log('✅ Категория создана:', newCategory);
  } catch (error) {
    console.error('❌ Ошибка при создании категории:', error);
  }
};


const handleDeckUpdated = (updatedDeck: DeckDto) => {
  // Обновляем колоду в списке
  const index = decks.value.findIndex(deck => deck.id === updatedDeck.id);
  if (index !== -1) {
    decks.value[index] = updatedDeck;
  }

  // Обновляем выбранную колоду
  selectedDeck.value = updatedDeck;

  console.log('🔄 Колода обновлена в MainView');
};
//===========================================
//Удаление категорий===========================================
const dialogDeleteCategoryVisible = ref(false);
const categoryToDelete = ref<{ label: string; value: string } | null>(null);

  const deleteCategory = async (categoryId: number) => {
  try {
    // Здесь будет вызов API для удаления
    // await deleteCategoryApi(categoryId);

    // Удаляем из локального списка
    categories.value = categories.value.filter(cat => cat.id !== categoryId);

    console.log('✅ Категория удалена:', categoryId);
    } catch (error) {
    console.error('❌ Ошибка при удалении категории:', error);
    }
  };
//===========================================

const dialogOpenAddDeck = ref(false);
const dialogOpenEditDeck = ref(false);

const userStore = useUserStore();

const logout = () => {
  userStore.logout(); // Вызываем метод logout из Pinia
  router.push({ name: 'login' }); // Перенаправляем на страницу логина
};

const selectedDeckId = ref<number | undefined | null>(null);
const selectedDeckCards = ref<CardDto[]>([]);
const selectedDeck = ref<DeckDto | null>(null);

const handleCardClick = async (deck: DeckDto) => {
  try {
    if (!deck.id) {
        console.error('ID колоды отсутствует');
        return;
      }
      selectedDeckId.value = deck.id;
      selectedDeck.value = deck;
      selectedDeckCards.value = await getCardsByDeckId(deck.id);
      showDecksList.value = false;
  } catch (error) {
      console.error('Ошибка загрузки карточек:', error);
  }
};

const dialogFormVisible = ref(false);
const formLabelWidth = '140px';

const form = reactive({
  name: '',
  categories: [] as CategoriesDto[],
  date1: '',
  date2: '',
  delivery: false,
  type: [],
  resource: '',
  desc: ''
});

const textarea = ref('');

const editingDeckId = ref<number | null>(null);

//Поиск====================================================================================
interface LinkItem {
  value: string;
  link: string;
}

const state = ref('');
const links = ref<LinkItem[]>([]);

const handleIconClick = (ev: Event) => {
  console.log(ev);
};

onMounted(async () => {
  const userId = 1;
  categories.value = await getUserCategories();
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
const decks = ref<DeckDto[]>([]);
const addDeck = async () => {
  if (form.name && textarea.value) {
    const now = new Date().toISOString();

    const newDeck = {
      userId: userStore.id ?? 1,
      categories: form.categories,
      title: form.name,
      description: textarea.value
    };

    try {
      const response = await addDeckToDB(newDeck);

      if (response) {
        decks.value.push(response);
        form.name = response.title;
        textarea.value = response.description;

        if (
          currentPage.value > Math.ceil(decks.value.length / pageSize.value)
        ) {
          currentPage.value = Math.ceil(decks.value.length / pageSize.value);
        }
        dialogOpenAddDeck.value = false;
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
// Обработка удаления колоды
const handleDeleteClick = (deckId: number | undefined) => {
  deckIdToDelete.value = deckId;
  dialogVisibleDeckModal.value = true;
};
const confirmDeleteDeck = async () => {
  if (deckIdToDelete.value !== null && deckIdToDelete.value !== undefined) {
    await removeDeck(deckIdToDelete.value);
  }
};
//Удаление колод========================================================================================
//Получение колод пользователя========================================================================================
const loadUserDecks = async () => {
  try {
    userStore.id = 1;
    if (userStore.id === null) {
      console.error('ID пользователя не определен');
      return;
    }

    const userDecks = await getUserDecks(userStore.id);
    decks.value = userDecks;
    await loadAllDeckCategories();
  } catch (error) {
    console.error('Ошибка при загрузке колод:', error);
  }
};

// Вызывай функцию при монтировании компонента
onMounted(() => {
  loadUserDecks();
});

const deckIdToDelete = ref<number | undefined>(undefined);

const removeDeck = async (deckId: number | undefined) => {
  const response = await deleteDeck(deckId);
  if (response) {
    decks.value = decks.value.filter((deck) => deck.id !== deckId);
  }
  dialogVisibleDeckModal.value = false;
  return response;
};

//Получение колод пользователя========================================================================================
//Методы загрузки карточек для изучения========================================================================================
const isLoadingCards = ref(false);
const selectedDeckForStudy = ref<any>(null);

const loadCardsForStudy = async (deck: any) => {
  try {
    isLoadingCards.value = true;
    selectedDeckForStudy.value = deck;

    const cards = await getCardsByDeckId(deck.id);
    selectedDeckCards.value = cards;

    if (cards.length === 0) {
      ElMessage.warning('В этой колоде нет карточек');
      return;
    }

    isOpenWindowStudyWords.value = true;
  } catch (error) {
    ElMessage.error('Ошибка загрузки карточек');
    console.error(error);
  } finally {
    isLoadingCards.value = false;
  }
};
//Методы загрузки карточек для изучения========================================================================================
//Редактирование параметров колоды========================================================================================
const openEditDeck = (deck: DeckDto) => {
  event?.stopPropagation();
  if (!deck.id) {
      console.error('ID колоды отсутствует');
      return;
    }
  editingDeckId.value = deck.id;
  form.name = deck.title;
  form.categories = deck.categories || [];
  textarea.value = deck.description ?? '';
  dialogOpenEditDeck.value = true;
  showDecksList.value = true;
};
const saveEditedDeck = async () => {
  if (!editingDeckId.value) return;
  const updatedDeck = new DeckDto({
    userId: userStore.id ?? 1,
    title: form.name,
    categories: form.categories,
    description: textarea.value
  });
  const response = await updateDeck(editingDeckId.value, updatedDeck);
  if (response) {
    const idx = decks.value.findIndex(
      (deck) => deck.id === editingDeckId.value
    );
    if (idx !== -1) {
      decks.value[idx] = { ...decks.value[idx], ...updatedDeck };
    }
    dialogOpenEditDeck.value = false;
  }
};
//Редактирование параметров колоды========================================================================================
const showDecksList = ref(true);
//================================================================================================================ПОиск колоды
const deckSuggestions = computed(() =>
  decks.value.map(deck => ({
    value: deck.title,
    link: deck.id // или другая инфа, если нужно
  }))
);

const createFilter = (queryString: string) => {
  return (deck: { value: string; link: any }) =>
    deck.value.toLowerCase().includes(queryString.toLowerCase());
};


const querySearch = (queryString: string, cb: (results: any[]) => void) => {
  const results = queryString
    ? deckSuggestions.value.filter(createFilter(queryString))
    : deckSuggestions.value;
  cb(results);
};

const handleSelect = (item: any) => {
  // тут можешь что-то делать при выборе
  console.log('Выбрано:', item);
};

const filteredDecks = computed(() => {
  if (!state.value) return decks.value;
  return decks.value.filter(deck =>
    deck.title.toLowerCase().includes(state.value.toLowerCase())
  );
});
//================================================================================================================
const confirmDeleteCategory = (category: { id: number; title: string }) => {
  categoryToDelete.value = { label: category.title, value: category.title };
  dialogDeleteCategoryVisible.value = true;
};

const isOpenWindowStudyWords = ref(false);
const handleOpenStudyWindow = () => {
  selectedDeckForStudy.value = selectedDeck.value;
  isOpenWindowStudyWords.value = true;
}

const handleCloseWindowStudyWords = () => {
  isOpenWindowStudyWords.value = false;
}

//Категории================================================================================================================
const isAdding = ref(false)

const onAddCategories = () => {
  isAdding.value = true
}

const clearCategories = () => {
  categoriesName.value = ''
  isAdding.value = false
}

const deckCategories = ref<Map<number, CategoriesDto[]>>(new Map());

// 🆕 Функция для загрузки категорий конкретной колоды
const loadDeckCategories = async (deckId: number) => {
  try {
    const categories = await getCategoriesByDeck(deckId);
    deckCategories.value.set(deckId, categories);
  } catch (error) {
    console.error(`Ошибка загрузки категорий для колоды ${deckId}:`, error);
    deckCategories.value.set(deckId, []); // Пустой массив при ошибке
  }
};

// 🆕 Функция для загрузки категорий всех колод
const loadAllDeckCategories = async () => {
  for (const deck of decks.value) {
    if (deck.id) {
      await loadDeckCategories(deck.id);
    }
  }
};

// 🎨 Массив цветов для тегов
const tagColors = ['primary', 'success', 'info', 'warning', 'danger'];

// 🎨 Функция для получения цвета тега
const getTagColor = (index: number) => {
  return tagColors[index % tagColors.length];
};


//Категории================================================================================================================
</script>

<style scoped>
.layout-container-demo .el-header {
  position: relative;
  border-bottom: 2px solid #e9eaec;
  color: white;
  display: flex;
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

.toolbar > div:nth-child(-n+3) {
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
.option-input {
  width: 100%;
  margin-bottom: 8px;
}
</style>
