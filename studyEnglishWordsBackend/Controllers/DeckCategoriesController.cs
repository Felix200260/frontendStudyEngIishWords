using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using studyEnglishWordsBackend.Service;

namespace studyEnglishWordsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeckCategoriesController : ControllerBase
    {
        private readonly DeckCategoriesService _deckCategoriesService;
        private readonly AppDbContext _context;

        public DeckCategoriesController(DeckCategoriesService deckCategoriesService, AppDbContext context)
        {
            _deckCategoriesService = deckCategoriesService;
            _context = context;
        }

        // GET api/deckcategories/user/1
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetCategoriesByUserId(int userId)
        {
            var categories = await _deckCategoriesService.GetCategoriesByUserIdAsync(userId);
            return Ok(categories);
        }

        // GET api/deckcategories/deck/1
        [HttpGet("deck/{deckId}")]
        public async Task<IActionResult> GetCategoriesByDeckId(int deckId)
        {
            var categories = await _deckCategoriesService.GetCategoriesByDeckIdAsync(deckId);
            return Ok(categories);
        }
        
        // GET api/deckcategories/test
        [HttpGet("test")]
        public async Task<IActionResult> TestConnection()
        {
            try
            {
                Console.WriteLine("=== НАЧАЛО ТЕСТА ===");
                var deckCategoriesCount = await _context.DeckCategories.CountAsync();
                Console.WriteLine($"DeckCategories count: {deckCategoriesCount}");
        
                var categoriesCount = await _context.Categories.CountAsync();
                Console.WriteLine($"Categories count: {categoriesCount}");
        
                var decksCount = await _context.Decks.CountAsync();
                Console.WriteLine($"Decks count: {decksCount}");

                return Ok(new 
                { 
                    deckCategories = deckCategoriesCount,
                    categories = categoriesCount,
                    decks = decksCount,
                    message = "Подключение работает"
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ОШИБКА: {ex.Message}");
                Console.WriteLine($"СТЕК: {ex.StackTrace}");
                return StatusCode(500, new { 
                    error = ex.Message, 
                    stackTrace = ex.StackTrace,
                    innerException = ex.InnerException?.Message 
                });
            }
        }
    }
}