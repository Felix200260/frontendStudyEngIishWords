export class CardDto {
  id: number;
  userId: number;
  englishWord: string;
  russianWord: string;
  statusId: number | null;
  nextReviewDate: Date | null;
  reviewCount: number;
  createdAt: Date;
  deckId: number | null;

  constructor(data: any) {
    this.id = data.id;
    this.userId = data.userId;
    this.englishWord = data.englishWord || '';
    this.russianWord = data.russianWord || '';
    this.statusId = data.statusId || null;
    this.nextReviewDate = data.nextReviewDate ? new Date(data.nextReviewDate) : null;
    this.reviewCount = data.reviewCount || 0;
    this.createdAt = new Date(data.createdAt);
    this.deckId = data.deckId || null;
  }
}
