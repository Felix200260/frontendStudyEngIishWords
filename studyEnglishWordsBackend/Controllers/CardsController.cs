using Microsoft.AspNetCore.Mvc;
using studyEnglishWordsBackend.Dto;
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
                // Логирование ошибки
                Console.WriteLine($"❌ Ошибка в GetCardsByDeckId: {ex.Message}");
                Console.WriteLine($"📍 StackTrace: {ex.StackTrace}");
                
                return StatusCode(500, new { 
                    message = "Ошибка при получении карточек", 
                    error = ex.Message 
                });
            }
        }

        [HttpPost]
        public async Task<IActionResult> AddCard([FromBody] AddCardRequestDto request)
        {
            try
            {
                var card = await _cardsService.AddCardAsync(request.EnglishWord, request.RussianWord, request.DeckId);
                return Ok(card);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка в AddCard: {ex.Message}");
                return StatusCode(500, new { message = "Ошибка при добавлении карточки", error = ex.Message });
            }
        }

        [HttpPut("{cardId}")]
        public async Task<IActionResult> UpdateCard(int cardId, [FromBody] UpdateCardRequestDto request)
        {
            try
            {
                var card = await _cardsService.UpdateCardAsync(cardId, request.EnglishWord, request.RussianWord, request.DeckId);
                return Ok(card);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка в UpdateCard: {ex.Message}");
                return StatusCode(500, new { message = "Ошибка при обновлении карточки", error = ex.Message });
            }
        }

        [HttpDelete("{cardId}")]
        public async Task<IActionResult> DeleteCard(int cardId)
        {
            try
            {
                await _cardsService.DeleteCardAsync(cardId);
                return Ok(new { message = "Карточка удалена" });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"❌ Ошибка в DeleteCard: {ex.Message}");
                return StatusCode(500, new { message = "Ошибка при удалении карточки", error = ex.Message });
            }
        }
    }
}
