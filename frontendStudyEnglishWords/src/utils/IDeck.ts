import { CategoriesDto } from '@/models/CategoriesDto';

export interface Deck {
  id?: number;
  userId: number;
  title: string;
  description?: string;
  categories?: CategoriesDto[];
}
