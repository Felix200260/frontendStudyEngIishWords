export class CreateDeckDto {
  userId!: number;
  title!: string;
  description?: string;
  categories?: number[]; // 🎯 Массив ID категорий

  constructor(init?: Partial<CreateDeckDto>) {
    Object.assign(this, init);
  }
}
