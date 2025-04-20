export class DeckDto {
  userId!: number;
  title!: string;
  description?: string;

  constructor(init?: Partial<DeckDto>) {
    Object.assign(this, init);
  }
}
