import axiosInstance from '@/api/axios';
import { DeckDto } from '@/models/DeckDto';
import { CreateDeckDto } from '@/models/CreateDeckDto';
const prefix = '/Deck';

// Получение колод пользователя
// Получение колод пользователя
export const getUserDecks = async (userId: number): Promise<DeckDto[]> => {
  const { data } = await axiosInstance.get<DeckDto[]>(`${prefix}?userId=${userId}`);
  return data.map((deck) => new DeckDto(deck));
};



// Добавление новой колоды
export const addDeckToDB = async (
  deck: CreateDeckDto
): Promise<DeckDto> => {
  const { data } = await axiosInstance.post<DeckDto>(`${prefix}`, deck);
  return data;
};


// Удаление колоды
export const deleteDeck = async (
  deckId: number | undefined
): Promise<{ deckId: number }> => {
  const { data } = await axiosInstance.delete<{ deckId: number }>(`${prefix}/${deckId}`);
  return data ;
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
