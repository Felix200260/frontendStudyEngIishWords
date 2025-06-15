using Microsoft.EntityFrameworkCore;
 using studyEnglishWordsBackend.Dto;
 using studyEnglishWordsBackend.Interface;
 using studyEnglishWordsBackend.Models;
 
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
                 .Include(dc => dc.Categories)
                 .Select(dc => new CategoriesDto
                 {
                     Id = dc.Categories!.Id,
                     Title = dc.Categories.Title ?? ""
                 })
                 .ToListAsync();
 
             return categories;
         }
 
 
         public async Task<List<CategoriesDto>> GetAllUserCategoriesAsync(string userId)
         {
             var categories = await _context.Categories
                 .Where(c => c.UserId == userId)
                 .Select(c => new CategoriesDto
                 {
                     Id = c.Id,
                     Title = c.Title ?? ""
                 })
                 .ToListAsync();
 
             return categories;
         }
         
         public async Task<CategoriesDto> CreateCategoryAsync(string title, string userId)
         {
             // Проверяем, что категория с таким названием не существует у пользователя
             var existingCategory = await _context.Categories
                 .FirstOrDefaultAsync(c => c.Title == title && c.UserId == userId);
             
             if (existingCategory != null)
             {
                 throw new InvalidOperationException($"Категория '{title}' уже существует у данного пользователя");
             }
     
             var newCategory = new Categories()
             {
                 Title = title,
                 UserId = userId
             };
     
             _context.Categories.Add(newCategory);
             await _context.SaveChangesAsync();
         
             return new CategoriesDto
             {
                 Id = newCategory!.Id,
                 Title = newCategory.Title ?? ""
             };
         }
         
         public async Task<CategoriesDto> DeleteCategoryAsync(int categoryId, string userId)
         {
             var category = await _context.Categories
                 .FirstOrDefaultAsync(c => c.Id == categoryId && c.UserId == userId);
             if (category == null)
             {
                 throw new InvalidOperationException("Категория не найдена или не принадлежит пользователю");
             }

             var deckCategories = _context.DeckCategories
                 .Where(dc => dc.CategoriesId == categoryId);

             _context.DeckCategories.RemoveRange(deckCategories);
             _context.Categories.Remove(category);
             await _context.SaveChangesAsync();

             // Возвращаем удалённую категорию
             return new CategoriesDto
             {
                 Id = category.Id,
                 Title = category.Title ?? ""
             };
         }


     }
 }