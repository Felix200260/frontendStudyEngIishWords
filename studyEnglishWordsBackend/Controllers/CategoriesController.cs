using Microsoft.AspNetCore.Mvc;
using studyEnglishWordsBackend.Dto;
using studyEnglishWordsBackend.Interface;

namespace studyEnglishWordsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesService _categoriesesService;

        public CategoriesController(ICategoriesService categoriesesService)
        {
            _categoriesesService = categoriesesService;
        }

        // Получить категории для определённой колоды
        [HttpGet("GetCategoriesByDeck/{deckId}")]
        public async Task<ActionResult<List<CategoriesDto>>> GetCategoriesByDeck(int deckId)
        {
            var userId = GetCurrentUserId(); 
            
            var categories = await _categoriesesService.GetCategoriesByDeckIdAsync(deckId, userId);
            return Ok(categories);
        }

        // Получить все категории пользователя
        [HttpGet("GetAllUserCategories")]
        public async Task<ActionResult<List<CategoriesDto>>> GetAllUserCategories()
        {
            var userId = GetCurrentUserId();
            
            var categories = await _categoriesesService.GetAllUserCategoriesAsync(userId);
            return Ok(categories);
        }

        private string GetCurrentUserId()
        {
            // Пока заглушка, потом добавишь авторизацию
            return "1";
        }
    }
}