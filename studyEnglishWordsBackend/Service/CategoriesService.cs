using Microsoft.EntityFrameworkCore;
using studyEnglishWordsBackend.Dto;
using studyEnglishWordsBackend.Interface;

namespace studyEnglishWordsBackend.Service
{
    public class CategoriesService : ICategoriesService
    {
        private readonly AppDbContext _context;

        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<CategoriesDto>> GetCategoriesByDeckIdAsync(int deckId, string userId)
        {
            var categories = await _context.DeckCategories
                .Where(dc => dc.DeckId == deckId)
                .Include(dc => dc.Category)
                .Select(dc => new CategoriesDto
                {
                    Id = dc.Category.Id ?? 0,
                    Title = dc.Category.Title ?? ""
                })
                .ToListAsync();

            return categories;
        }


        public async Task<List<CategoriesDto>> GetAllUserCategoriesAsync(string userId)
        {
            var categories = await _context.Categories
                .Select(c => new CategoriesDto
                {
                    Id = c.Id ?? 0,
                    Title = c.Title ?? ""
                })
                .ToListAsync();

            return categories;
        }

    }
}