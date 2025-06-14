using Microsoft.EntityFrameworkCore;
using studyEnglishWordsBackend.Models;

namespace studyEnglishWordsBackend.Service
{
    public class DeckCategoriesService
    {
        private readonly AppDbContext _context;

        public DeckCategoriesService(AppDbContext context)
        {
            _context = context;
        }

        // Получение категорий для конкретной колоды
        public async Task<List<CategoriesModal>> GetCategoriesByDeckIdAsync(int deckId)
        {
            return await _context.DeckCategories
                .Where(dc => dc.DeckId == deckId)
                .Include(dc => dc.Category)
                .Select(dc => dc.Category)
                .ToListAsync();
        }

        // Получение всех категорий для колод пользователя
        public async Task<Dictionary<int, List<CategoriesModal>>> GetCategoriesByUserIdAsync(int userId)
        {
            var result = await _context.DeckCategories
                .Include(dc => dc.Deck)
                .Include(dc => dc.Category)
                .Where(dc => dc.Deck.UserId == userId)
                .GroupBy(dc => dc.DeckId)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(dc => dc.Category).ToList()
                );

            return result;
        }
    }
}