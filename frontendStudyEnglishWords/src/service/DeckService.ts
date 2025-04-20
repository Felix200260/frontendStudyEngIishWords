import axiosInstance from '@/api/axios';
import { DeckDto } from '@/models/DescDto';
const prefix = '/api/Deck';

// Получение списка всех колод
export const getDecks = async (): Promise<DeckDto[]> => {
  const { data } = await axiosInstance.get<DeckDto[]>(`${prefix}`);
  return data.map((deck) => new DeckDto(deck));
};

// Добавление новой колоды
export const addDeckToDB = async (
  newDeck: { title: string; description: string }
): Promise<{ userId: number; title: string; description: string }> => {
  const { data } = await axiosInstance.post<{ userId: number; title: string; description: string }>(
    `${prefix}`,
    newDeck
  );
  return data;
};



// Удаление колоды
export const deleteDeck = async (
  deckId: number
): Promise<{ message: string }> => {
  const { data } = await axiosInstance.delete<{ message: string }>(
    `${prefix}/${deckId}`
  );
  return data;
};

//Обновление колод
//для изменения уже существующей(ранее созданной колоды)
export const updateDeck = async (
  deckId: number,
  updatedDeck: DeckDto
): Promise<{ message: string }> => {
  const { data } = await axiosInstance.put<{ message: string }>(
    `${prefix}/${deckId}`,
    updatedDeck
  );
  return data;
};
