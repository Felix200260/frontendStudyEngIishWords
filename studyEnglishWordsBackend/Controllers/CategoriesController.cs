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
            // Пока заглушка
            return "1";
        }
        
        // Создать новую категорию
        [HttpPost("CreateCategory")]
        public async Task<ActionResult<CategoriesDto>> CreateCategory([FromBody] string title)
        {
            var userId = GetCurrentUserId();
            
            try
            {
                var newCategory = await _categoriesesService.CreateCategoryAsync(title, userId);
                return Ok(newCategory);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка при создании категории" });
            }
        }
        
        // Удалить категорию по id
        [HttpDelete("DeleteCategory/{id}")]
        public async Task<ActionResult<CategoriesDto>> DeleteCategory(int id)
        {
            var userId = GetCurrentUserId();
            try
            {
                var deletedCategory = await _categoriesesService.DeleteCategoryAsync(id, userId);
                return Ok(deletedCategory);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception)
            {
                return StatusCode(500, new { message = "Ошибка при удалении категории" });
            }
        }

    }
}