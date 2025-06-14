import { CategoriesDto } from '@/models/CategoriesDto';

export class DeckDto {
  id?: number;
  userId!: number;
  title!: string;
  description?: string;
  categories?: CategoriesDto[];


  constructor(init?: Partial<DeckDto>) {
    Object.assign(this, init);
  }
}
