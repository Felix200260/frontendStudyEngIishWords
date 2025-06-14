using Microsoft.EntityFrameworkCore;
using studyEnglishWordsBackend.Models;

namespace studyEnglishWordsBackend.Service
{
    public interface ICardsService
    {
        Task<List<CardModal>> GetCardsByDeckIdAsync(int deckId);
        Task<CardModal> AddCardAsync(string englishWord, string russianWord, int deckId);
        Task<CardModal> UpdateCardAsync(int cardId, string englishWord, string russianWord, int deckId);
        Task DeleteCardAsync(int cardId);
    }

    public class CardsService : ICardsService
    {
        private readonly AppDbContext _context;

        public CardsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CardModal>> GetCardsByDeckIdAsync(int deckId)
        {
            try
            {
                Console.WriteLine($"🔍 Поиск карточек для колоды ID: {deckId}");
                
                var cards = await _context.Cards
                    .Where(c => c.DeckId == deckId)
                    .ToListAsync();
                
                Console.WriteLine($"✅ Найдено карточек: {cards.Count}");
                return cards;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка в GetCardsByDeckIdAsync: {ex.Message}");
                throw;
            }
        }

        public async Task<CardModal> AddCardAsync(string englishWord, string russianWord, int deckId)
        {
            var card = new CardModal
            {
                EnglishWord = englishWord,
                RussianWord = russianWord,
                DeckId = deckId,
                UserId = 1, // Временно захардкодил
                StatusId = 1,
                ReviewCount = 0,
                CreatedAt = DateTime.UtcNow,
                NextReviewDate = DateTime.UtcNow.AddDays(1)
            };

            _context.Cards.Add(card);
            await _context.SaveChangesAsync();
            return card;
        }

        public async Task<CardModal> UpdateCardAsync(int cardId, string englishWord, string russianWord, int deckId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card == null)
                throw new ArgumentException($"Карточка с ID {cardId} не найдена");

            card.EnglishWord = englishWord;
            card.RussianWord = russianWord;
            card.DeckId = deckId;

            await _context.SaveChangesAsync();
            return card;
        }

        public async Task DeleteCardAsync(int cardId)
        {
            var card = await _context.Cards.FindAsync(cardId);
            if (card == null)
                throw new ArgumentException($"Карточка с ID {cardId} не найдена");

            _context.Cards.Remove(card);
            await _context.SaveChangesAsync();
        }
    }
}
