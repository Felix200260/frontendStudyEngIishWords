export class DeckDto {
  deckId!: number;
  deckName!: string;

  constructor(init?: Partial<DeckDto>) {
    Object.assign(this, init);
  }
}
