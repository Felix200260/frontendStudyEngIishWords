import axiosInstance from '@/api/axios';
import { CategoriesDto } from '@/models/CategoriesDto';
const prefix = '/Categories';

//Получение категорий
export const getUserCategories = async (): Promise<CategoriesDto[]> => {
  const { data } = await axiosInstance.get<CategoriesDto[]>(`${prefix}/GetAllUserCategories`);
  return data;
};

// 🆕 Получение категорий для конкретной колоды
export const getCategoriesByDeck = async (deckId: number | undefined): Promise<CategoriesDto[]> => {
  const { data } = await axiosInstance.get<CategoriesDto[]>(`${prefix}/GetCategoriesByDeck/${deckId}`);
  return data;
};