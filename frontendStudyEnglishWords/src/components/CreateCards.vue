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
        :key="idx"
        style="border-radius: 18px; margin-bottom: 20px; border: none; padding: 20px;"
      >
        <template #header>
          <div style="display: flex; justify-content: space-between; align-items: center">
            <div style="display: flex; align-items: center; margin-bottom: 12px;">
              <span style="font-size: 18px; font-weight: bold;">{{ idx + 1 }}</span>
            </div>
            <div style="font-size: 18px">
              <el-button @click="deleteCard(idx)">
                <el-icon>
                  <Delete />
                </el-icon>
              </el-button>
            </div>
          </div>
        </template>

        <div style="display: flex; align-items: flex-start;">
          <div style="display: flex; align-items: flex-start; width: 100%; gap: 16px;">
            <div style="flex: 1; min-width: 0; display: flex; flex-direction: column;">
              <el-input
                v-model="card.term"
                placeholder="Термин"
                style="background: #1a1f3c; color: #fff; border-radius: 8px; margin-bottom: 4px;"
              />
              <span style="margin-left: 10px; font-size: 12px; color: #b2b2b2;">ТЕРМИН</span>
            </div>
            <div style="flex: 1; min-width: 0; display: flex; flex-direction: column;">
              <el-input
                v-model="card.definition"
                placeholder="Определение"
                style="background: #1a1f3c; color: #fff; border-radius: 8px; margin-bottom: 4px;"
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
    <div style="display: flex; justify-content: center; align-items: center;">
      <button
        @click=""
        style="
      padding: 12px 32px;
      border: 1px solid white;
      background: #409eff;
      box-shadow: 5px 5px 5px #dcdfe6;
      border-radius: 32px;
      font-weight: bold;
      cursor: pointer;
      transition: border-color 0.2s;
      margin-top: 10px;
    "
        @mouseover="hover = true"
        @mouseleave="hover = false"
        :style="{ borderColor: hover ? '#fff' : '#c2c6c9' }"
      >
        Сохранить изменения
      </button>
    </div>

  </div>
</template>

<script setup lang="ts">
import { Delete, Close, Plus } from '@element-plus/icons-vue';
import { ref, watch } from 'vue';
import { CardDto } from '@/models/CardDto';
import { Deck } from '@/utils/IDeck';

const cards = ref<{ term: string; definition: string }[]>([]);
const hover = ref(false)

const inputNameDeck = ref('');
const inputDescriptionDeck = ref('');

const addCard = () => cards.value.push({ term: '', definition: '' });
const deleteCard = (idx: number) => cards.value.splice(idx, 1);

const props = defineProps<{
  deckId: number | null | undefined;
  existingCards: CardDto[];
  deckData: Deck | null;
}>();

watch(() => props.existingCards, (newCards) => {if (newCards && newCards.length > 0) {
  cards.value = newCards.map(card => ({
    term: card.englishWord,
    definition: card.russianWord
  }));
} else {
    cards.value = [{ term: '', definition: '' }];
  }
}, { immediate: true });

watch(() => props.deckData, (newDeck) => {
  if (newDeck) {
    inputNameDeck.value = newDeck.title;
    inputDescriptionDeck.value = newDeck.description ?? '';
  }
}, { immediate: true });

</script>

<style scoped>
.style-margin-bottom{
  margin-bottom: 10px;
}

</style>