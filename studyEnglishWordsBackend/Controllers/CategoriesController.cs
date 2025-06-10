using Microsoft.AspNetCore.Mvc;
using studyEnglishWordsBackend.Service;

namespace studyEnglishWordsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly CategoriesService _categoriesService;

        public CategoriesController(CategoriesService categoriesService)
        {
            _categoriesService = categoriesService;
        }

        // GET api/categories?userId=1
        [HttpGet]
        public async Task<IActionResult> GetCategories([FromQuery] int userId)
        {
            if (userId == null)
                return BadRequest("Некорректный userId");

            var categories = await _categoriesService.GetCategoriesByUserIdAsync(userId);
            return Ok(categories);
        }
        
        [HttpGet]
        public async Task<IActionResult> DeleteCategories([FromQuery] int categoriesId)
        {
            if (categoriesId == null)
                return BadRequest("Некорректный categoriesId");

            var categories = await _categoriesService.GetCategoriesByUserIdAsync(categoriesId);
            return Ok(categories);
        }
    }
}