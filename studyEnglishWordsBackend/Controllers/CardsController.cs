using Microsoft.AspNetCore.Mvc;
using studyEnglishWordsBackend.Service;

namespace studyEnglishWordsBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CardsController : ControllerBase
    {
        private readonly ICardsService _cardsService;

        public CardsController(ICardsService cardsService)
        {
            _cardsService = cardsService;
        }

        [HttpGet("GetCardsByDeckId")]
        public async Task<IActionResult> GetCardsByDeckId([FromQuery] int deckId)
        {
            try
            {
                var cards = await _cardsService.GetCardsByDeckIdAsync(deckId);
                return Ok(cards);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ошибка получения карточек: {ex.Message}");
            }
        }
    }
}