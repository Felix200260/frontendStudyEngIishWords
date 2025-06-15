export class UpdateDeckDto {
  userId!: number;
  title!: string;
  description?: string;
  categories?: number[]; // 🎯 Массив ID категорий

  constructor(init?: Partial<UpdateDeckDto>) {
    Object.assign(this, init);
  }
}
