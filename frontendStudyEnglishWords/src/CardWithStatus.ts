interface CardWithStatus {
  id: number | null;        // ID карточки (null для новых)
  term: string;            // Английское слово
  definition: string;      // Русское слово
  isNew: boolean;         // Флаг новой карточки
  isModified: boolean;    // Флаг изменения
}
