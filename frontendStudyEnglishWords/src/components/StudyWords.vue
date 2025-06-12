<template>
  <div class="study-container">
    <!-- Шапка с названием колоды и кнопкой закрытия -->
    <div class="study-header">
      <h1 class="deck-title">{{ deckName }}</h1>
      <el-button @click="$emit('closeWindowStudyWords')" class="close-btn">
        <el-icon><Close /></el-icon>
      </el-button>
    </div>

    <!-- Карточка для изучения -->
    <div class="card-container" v-if="currentCard">

      <!-- Основная карточка -->
      <div class="study-card" :class="{ 'flipped': isFlipped }">
        <div class="card-content">
          <div class="card-question" v-if="!isFlipped">
            {{ currentCard.englishWord }}
          </div>
          <div class="card-answer" v-else>
            {{ currentCard.russianWord }}
          </div>
        </div>
      </div>

      <!-- Кнопка для переворота карточки -->
      <div class="flip-section">
        <el-button
          type="primary"
          @click="flipCard"
          class="flip-btn"
        >
          {{ isFlipped ? 'Показать вопрос' : 'Показать ответ' }}
        </el-button>
      </div>
    </div>

    <!-- Навигация -->
    <div class="navigation">
      <el-button
        @click="previousCard"
        :disabled="currentIndex === 0"
        class="nav-btn"
      >
        <el-icon><ArrowLeft /></el-icon>
      </el-button>

      <span class="card-counter">
      {{ currentIndex + 1 }} / {{ totalCards }}
    </span>

      <el-button
        @click="nextCard"
        :disabled="currentIndex === totalCards - 1"
        class="nav-btn"
      >
        <el-icon><ArrowRight /></el-icon>
      </el-button>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted, watch, onUnmounted } from 'vue';
import {
  Close,
  ArrowLeft,
  ArrowRight
} from '@element-plus/icons-vue';
import { getCardsByDeckId } from '@/service/CardService'; // импорт твоего API
import type { CardDto } from '@/models/CardDto';

const props = defineProps<{
  deckId?: number;
  deckName?: string;
  cards?: CardDto[];
}>();

const emit = defineEmits<{
  closeWindowStudyWords: [];
}>();

// Состояние компонента
const currentIndex = ref(0);
const isFlipped = ref(false);
const cards = ref<CardDto[]>([]);
const loading = ref(false);

// Вычисляемые свойства
const currentCard = computed(() => {
  return cards.value[currentIndex.value];
});

const totalCards = computed(() => {
  return cards.value.length;
});

const deckName = computed(() => {
  return props.deckName || "Название колоды";
});

// Загрузка карточек из API
const loadCards = async () => {
  if (!props.deckId) return;

  try {
    loading.value = true;
    cards.value = await getCardsByDeckId(props.deckId);
  } catch (error) {
    console.error('Ошибка загрузки карточек:', error);
    cards.value = [];
  } finally {
    loading.value = false;
  }
};

const initializeCards = () => {
  if (props.cards?.length) {
    cards.value = props.cards;
 } else if (props.deckId) {
    loadCards();
  }
};



const flipCard = () => {
  isFlipped.value = !isFlipped.value;
};

const nextCard = () => {
  if (currentIndex.value < totalCards.value - 1) {
    currentIndex.value++;
    resetCardState();
  }
};

const previousCard = () => {
  if (currentIndex.value > 0) {
    currentIndex.value--;
    resetCardState();
  }
};

const resetCardState = () => {
  isFlipped.value = false;
};

watch(() => [props.cards, props.deckId], () => {
  currentIndex.value = 0;
  resetCardState();
  initializeCards();
}, { immediate: true });
</script>


<style scoped>
.study-container {
  padding: 20px;
  display: flex;
  flex-direction: column;
  color: black;
}

.study-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 30px;
}

.deck-title {
  color: black;
  padding: 12px 24px;
  border-radius: 8px;
  margin: 0;
  font-size: 1.5rem;
  font-weight: 600;
}

.close-btn {
  border-radius: 50%;
  width: 40px;
  height: 40px;
  padding: 0;
}

.card-container {
  flex: 1;
  display: flex;
  flex-direction: column;
  max-width: 800px;
  margin: 0 auto;
  width: 100%;
}

.hint-section {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.hint-btn {
  background: rgba(255, 255, 255, 0.1);
  border: 1px solid rgba(255, 255, 255, 0.3);
  color: black;
}

.hint-btn:hover {
  background: rgba(255, 255, 255, 0.2);
}

.favorite-btn .el-button {
  background: rgba(255, 255, 255, 0.1);
  border: 1px solid rgba(255, 255, 255, 0.3);
  color: black;
}

.favorite-btn .el-button--warning {
  background: #f59e0b;
  border-color: #f59e0b;
}

.study-card {
  background: rgba(0, 117, 253, 0.1);
  backdrop-filter: blur(10px);
  border: 1px solid rgba(255, 255, 255, 0.2);
  border-radius: 16px;
  padding: 40px;
  margin: 20px 0;
  min-height: 300px;
  display: flex;
  flex-direction: column;
  justify-content: center;
  transition: all 0.3s ease;
  position: relative;
}

.study-card:hover {
  transform: translateY(-5px);
  box-shadow: 0 20px 40px rgba(0, 0, 0, 0.3);
}

.card-content {
  text-align: center;
  flex: 1;
  display: flex;
  align-items: center;
  justify-content: center;
}

.card-question,
.card-answer {
  font-size: 1.8rem;
  line-height: 1.4;
  font-weight: 500;
  color: black;
}

.hint-content {
  margin-top: 20px;
  padding-top: 20px;
  border-top: 1px solid rgba(255, 255, 255, 0.2);
}

.hint-text {
  font-size: 1.1rem;
  color: rgba(255, 255, 255, 0.8);
  font-style: italic;
  text-align: center;
  margin: 0;
}

.flip-section {
  text-align: center;
}

.flip-btn {
  padding: 12px 30px;
  font-size: 1.1rem;
  border-radius: 25px;
}

.navigation {
  display: flex;
  justify-content: center;
  align-items: center;
  gap: 20px;
  margin-top: 30px;
  padding: 20px;
  background: #0075FD19;
  border-radius: 50px;
  backdrop-filter: blur(10px);
  width: fit-content;
  margin-left: auto;
  margin-right: auto;
}

.nav-btn {
  border-radius: 50%;
  width: 50px;
  height: 50px;
  padding: 0;
  background: rgba(255, 255, 255, 0.1);
  border: 1px solid black;
  color: black;
}

.nav-btn:hover:not(:disabled) {
  background: rgba(255, 255, 255, 0.2);
  transform: scale(1.1);
}

.nav-btn:disabled {
  opacity: 0.3;
  cursor: not-allowed;
}

.card-counter {
  font-size: 1.2rem;
  font-weight: 600;
  color: black;
  min-width: 80px;
  text-align: center;
}

/* Адаптивность */
@media (max-width: 768px) {
  .study-container {
    padding: 15px;
  }

  .deck-title {
    font-size: 1.2rem;
    padding: 10px 20px;
  }

  .study-card {
    padding: 30px 20px;
    min-height: 250px;
  }

  .card-question,
  .card-answer {
    font-size: 1.4rem;
  }

  .hint-section {
    flex-direction: column;
    gap: 10px;
    align-items: stretch;
  }
}
</style>