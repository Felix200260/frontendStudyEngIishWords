import axiosInstance from '@/api/axios';
import { CardDto } from '@/models/CardDto';

const prefix = '/Cards';

// Получение карточек по ID колоды
export const getCardsByDeckId = async (deckId: number): Promise<CardDto[]> => {
  const { data } = await axiosInstance.get<CardDto[]>(`${prefix}/GetCardsByDeckId?deckId=${deckId}`);
  return data.map((card) => new CardDto(card));
};

// Добавление новой карточки
export const addCardById = async (cardData: {
  englishWord: string;
  russianWord: string;
  deckId: number;
}): Promise<CardDto> => {
  const { data } = await axiosInstance.post<CardDto>(`${prefix}`, cardData);
  return new CardDto(data);
};

// Обновление карточки
export const updateCardById = async (cardId: number, cardData: {
  englishWord: string;
  russianWord: string;
  deckId: number;
}): Promise<CardDto> => {
  const { data } = await axiosInstance.put<CardDto>(`${prefix}/${cardId}`, cardData);
  return new CardDto(data);
};

// Удаление карточки
export const deleteCardById = async (cardId: number): Promise<void> => {
  await axiosInstance.delete(`${prefix}/${cardId}`);
};
