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

// 🆕 Создание новой категории
export const createCategory = async (title: string): Promise<CategoriesDto> => {
  const { data } = await axiosInstance.post<CategoriesDto>(`${prefix}/CreateCategory`, title, {
    headers: { 'Content-Type': 'application/json' }
  });
  return data;
};

//  Удаление категории
export const deleteCategory = async (id: number): Promise<CategoriesDto> => {
  await axiosInstance.delete<CategoriesDto>(`${prefix}/DeleteCategory/${id}`);
};
