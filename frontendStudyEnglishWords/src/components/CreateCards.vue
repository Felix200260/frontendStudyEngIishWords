<template>
  <div>
    <div style="display: flex; justify-content: space-between; align-items: center">
      <div class="style-margin-bottom"><h1>Создание новых карточек:</h1></div>
      <el-button placeholder="Удалить" @click="$emit('close')">
        <el-icon>
          <Close />
        </el-icon>
      </el-button>
    </div>

    <el-input v-model="inputNameDeck" class="style-margin-bottom" placeholder="Название колоды" />
    <el-input v-model="inputDescriptionDeck" class="style-margin-bottom" placeholder="Описание колоды" />

    <el-card class="style-margin-bottom">
      <template #header>
        <div>Карточки:</div>
      </template>

      <el-card
        v-for="(card, idx) in cards"
        :data-card-index="idx"
        :key="idx"
        style="border-radius: 18px; margin-bottom: 20px; border: none; padding: 20px;"
      >
        <template #header>
          <div style="display: flex; justify-content: space-between; align-items: center">
            <div style="display: flex; align-items: center; margin-bottom: 12px;">
              <span style="font-size: 18px; font-weight: bold;">{{ idx + 1 }}</span>
              <!-- Индикатор статуса -->
              <div style="margin-left: 15px; font-size: 12px;">
                <el-tag v-if="card.isNew" type="success" size="small">🆕 Новая</el-tag>
                <el-tag v-else-if="card.isModified" type="warning" size="small">✏️ Изменена</el-tag>
                <el-tag v-else type="info" size="small">💾 Сохранена</el-tag>
              </div>
            </div>
            <div style="font-size: 18px">
              <el-tooltip content="Удалить карточку">
                <el-button @click="removeCard(idx)" type="danger">
                  <el-icon>
                    <Delete />
                  </el-icon>
                </el-button>
              </el-tooltip>
            </div>
          </div>
        </template>

        <div style="display: flex; align-items: flex-start;">
          <div style="display: flex; align-items: flex-start; width: 100%; gap: 16px;">
            <div style="flex: 1; min-width: 0; display: flex; flex-direction: column;">
              <el-input
                v-model="card.term"
                placeholder="Термин (английское слово)"
                style="background: #1a1f3c; color: #fff; border-radius: 8px; margin-bottom: 4px;"
                @input="markAsModified(idx)"
              />
              <span style="margin-left: 10px; font-size: 12px; color: #b2b2b2;">ТЕРМИН</span>
            </div>
            <div style="flex: 1; min-width: 0; display: flex; flex-direction: column;">
              <el-input
                v-model="card.definition"
                placeholder="Определение (русское слово)"
                style="background: #1a1f3c; color: #fff; border-radius: 8px; margin-bottom: 4px;"
                @input="markAsModified(idx)"
              />
              <span style="margin-left: 10px; font-size: 12px; color: #b2b2b2;">ОПРЕДЕЛЕНИЕ</span>
            </div>
          </div>
        </div>

        <template #footer>
          <div style="display: flex; justify-content: center; margin-top: 24px;">
            <button
              @click="addCard"
              style="
                padding: 12px 32px;
                border: 1px solid white;
                background: #409eff;
                box-shadow: 5px 5px 5px #dcdfe6;
                border-radius: 32px;
                font-weight: bold;
                cursor: pointer;
                transition: border-color 0.2s;
              "
              @mouseover="hover = true"
              @mouseleave="hover = false"
              :style="{ borderColor: hover ? '#fff' : '#c2c6c9' }"
            >
              Добавить карточку
            </button>
          </div>
        </template>
      </el-card>

      <el-empty v-if="cards.length === 0" description="Упс... А тут нет карточек!" />

      <template #footer>
        <div v-if="cards.length === 0" style="display: flex; justify-content: center;">
          <el-button
            @click="addCard"
            style="
              padding: 12px 32px;
              border: 1px solid white;
              background: #409eff;
              box-shadow: 5px 5px 5px #dcdfe6;
              border-radius: 32px;
              font-weight: bold;
              cursor: pointer;
              color: black;
              transition: border-color 0.2s;
            "
            @mouseover="hover = true"
            @mouseleave="hover = false"
            :style="{ borderColor: hover ? '#fff' : '#c2c6c9' }"
          >
            Добавить карточку
          </el-button>
        </div>
      </template>
    </el-card>

    <!-- Кнопка сохранения -->
    <div style="display: flex; justify-content: center; align-items: center;">
      <button
        @click="saveChanges"
        :disabled="isSaving"
        style="
          padding: 12px 32px;
          border: 1px solid white;
          background: #67C23A;
          box-shadow: 5px 5px 5px #dcdfe6;
          border-radius: 32px;
          font-weight: bold;
          cursor: pointer;
          transition: border-color 0.2s;
          margin-top: 10px;
        "
        @mouseover="hover = true"
        @mouseleave="hover = false"
        :style="{
          borderColor: hover ? '#fff' : '#c2c6c9',
          opacity: isSaving ? 0.6 : 1,
          cursor: isSaving ? 'not-allowed' : 'pointer'
        }"
      >
        {{ isSaving ? '💾 Сохранение...' : '💾 Сохранить изменения' }}
      </button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { Delete, Close } from '@element-plus/icons-vue';
import { nextTick, ref, watch } from 'vue';
import { CardDto } from '@/models/CardDto';
import { Deck } from '@/utils/IDeck';
import { addCardById, deleteCardById, updateCardById } from '@/service/CardService';
import { ElMessage } from 'element-plus';

// Интерфейс для карточки с дополнительными флагами
interface CardWithStatus {
  id: number | null;
  term: string;
  definition: string;
  isNew: boolean;
  isModified: boolean;
}

// Реактивные переменные
const cards = ref<CardWithStatus[]>([]);
const hover = ref(false);
const isSaving = ref(false);
const inputNameDeck = ref('');
const inputDescriptionDeck = ref('');

// Props
const props = defineProps<{
  deckId: number | null | undefined;
  existingCards: CardDto[];
  deckData: Deck | null;
}>();

// Emits
const emit = defineEmits<{
  close: [];
  cardsUpdated: [];
}>();

// Добавление новой карточки
const addCard = () => {
  cards.value.push({
    id: null,
    term: '',
    definition: '',
    isNew: true,
    isModified: false
  });

  // Автоматический скролл к новой карточке
  nextTick(() => {
    const newCardIndex = cards.value.length - 1;
    const newCardElement = document.querySelector(`[data-card-index="${newCardIndex}"]`);

    if (newCardElement) {
      newCardElement.scrollIntoView({
        behavior: 'smooth',    // Плавная анимация
        block: 'center',       // Карточка в центре экрана
        inline: 'nearest'      // Горизонтальное выравнивание
      });
    }
  });
};





// Удаление карточки
const removeCard = async (idx: number) => {
  const card = cards.value[idx];

  // Если карточка уже в БД - удаляем из БД
  if (card.id && !card.isNew) {
    try {
      await deleteCardById(card.id);
      ElMessage.success('Карточка удалена из БД');
    } catch (error) {
      ElMessage.error('Ошибка удаления карточки');
      console.error('Ошибка удаления:', error);
      return;
    }
  }

  // Удаляем из массива
  cards.value.splice(idx, 1);
};

// Отметка об изменении
const markAsModified = (idx: number) => {
  if (!cards.value[idx].isNew) {
    cards.value[idx].isModified = true;
  }
};

// Сохранение изменений
const saveChanges = async () => {
  if (!props.deckId) {
    ElMessage.error('Не выбрана колода для сохранения');
    return;
  }

  isSaving.value = true;

  try {
    let savedCount = 0;
    let updatedCount = 0;

    for (const card of cards.value) {
      // Пропускаем пустые карточки
      if (!card.term.trim() || !card.definition.trim()) {
        continue;
      }

      const cardData = {
        englishWord: card.term,
        russianWord: card.definition,
        deckId: props.deckId
      };

      // Добавляем новые карточки
      if (card.isNew && card.id === null) {
        const newCard = await addCardById(cardData);
        card.id = newCard.id;
        card.isNew = false;
        card.isModified = false;
        savedCount++;
      }
      // Обновляем измененные карточки
      else if (card.isModified && card.id !== null) {
        await updateCardById(card.id, cardData);
        card.isModified = false;
        updatedCount++;
      }
    }

    if (savedCount > 0 || updatedCount > 0) {
      ElMessage.success(`Сохранено: ${savedCount} новых, обновлено: ${updatedCount} карточек!`);
      emit('cardsUpdated'); // Уведомляем родительский компонент
    } else {
      ElMessage.info('Нет изменений для сохранения');
    }

  } catch (error) {
    ElMessage.error('Ошибка сохранения карточек');
    console.error('Ошибка сохранения:', error);
  } finally {
    isSaving.value = false;
  }
};

// Следим за существующими карточками
watch(() => props.existingCards, (newCards) => {
  if (newCards && newCards.length > 0) {
    cards.value = newCards.map(card => ({
      id: card.id,
      term: card.englishWord,
      definition: card.russianWord,
      isNew: false,
      isModified: false
    }));
  } else {
    cards.value = [{
      id: null,
      term: '',
      definition: '',
      isNew: true,
      isModified: false
    }];
  }
}, { immediate: true });

// Следим за данными колоды
watch(() => props.deckData, (newDeck) => {
  if (newDeck) {
    inputNameDeck.value = newDeck.title;
    inputDescriptionDeck.value = newDeck.description ?? '';
  }
}, { immediate: true });
</script>

<style scoped>
.style-margin-bottom {
  margin-bottom: 10px;
}
</style>
