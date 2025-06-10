using Microsoft.EntityFrameworkCore;
using studyEnglishWordsBackend.Models;

namespace studyEnglishWordsBackend.Service
{
    public class CategoriesService
    {
        private readonly AppDbContext _context;

        public CategoriesService(AppDbContext context)
        {
            _context = context;
        }

        // Получение всех категорий пользователя
        public async Task<List<Categories>> GetCategoriesByUserIdAsync(int userId)
        {
            // Пример для EF Core
            return await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
        //Удаление категорий
        public async Task<List<Categories>> DeleteCategoriesByUserIdAsync(int userId)
        {
            // Пример для EF Core
            return await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();
        }
    }
}

