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
        public async Task<List<Categories>> GetCategoriesByDeckIdAsync(int deckId)
        {
            return await _context.DeckCategories
                .Where(dc => dc.DeckId == deckId)
                .Include(dc => dc.Categories)
                .Select(dc => dc.Categories)
                .ToListAsync();
        }

        // Получение всех категорий для колод пользователя
        public async Task<Dictionary<int, List<Categories>>> GetCategoriesByUserIdAsync(int userId)
        {
            var result = await _context.DeckCategories
                .Include(dc => dc.Deck)
                .Include(dc => dc.Categories)
                .Where(dc => dc.Deck.UserId == userId)
                .GroupBy(dc => dc.DeckId)
                .ToDictionaryAsync(
                    g => g.Key,
                    g => g.Select(dc => dc.Categories).ToList()
                );

            return result;
        }
    }
}