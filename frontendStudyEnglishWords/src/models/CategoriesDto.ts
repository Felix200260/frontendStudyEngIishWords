export class CategoriesDto {
  id!: number;
  title!: string;

  constructor(init?: Partial<CategoriesDto>) {
    Object.assign(this, init);
  }
}
