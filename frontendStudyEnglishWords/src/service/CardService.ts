import axiosInstance from '@/api/axios';
import { CardDto } from '@/models/CardDto';

const prefix = '/Cards';

// Получение карточек по ID колоды
export const getCardsByDeckId = async (deckId: number): Promise<CardDto[]> => {
  const { data } = await axiosInstance.get<CardDto[]>(`${prefix}/GetCardsByDeckId?deckId=${deckId}`);
  return data.map((card) => new CardDto(card));
};
