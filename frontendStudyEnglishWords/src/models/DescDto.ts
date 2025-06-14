export class DeckDto {
  userId!: number;
  title!: string;
  description?: string;
  categoryId?: number[];

  constructor(init?: Partial<DeckDto>) {
    Object.assign(this, init);
  }
}
