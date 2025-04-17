using Microsoft.AspNetCore.Mvc;
using studyEnglishWords.Models;
using studyEnglishWords.Service;

namespace studyEnglishWords.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeckController : ControllerBase
    {
        private readonly DeckService _deckService;

        public DeckController(DeckService deckService)
        {
            _deckService = deckService;
        }

        // Получение всех колод
        [HttpGet]
        public async Task<IActionResult> GetDecks()
        {
            var decks = await _deckService.GetAllDecksAsync();
            return Ok(decks);
        }

        // Добавление новой колоды
        [HttpPost]
        public async Task<IActionResult> AddDeck([FromBody] Deck deck)
        {
            var createdDeck = await _deckService.AddDeckAsync(deck);
            return Ok(new { message = "Deck created successfully", deckId = createdDeck.Id });
        }

        // Удаление колоды
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeck(int id)
        {
            var success = await _deckService.DeleteDeckAsync(id);
            if (!success) return NotFound(new { message = "Deck not found" });

            return Ok(new { message = "Deck deleted successfully" });
        }

        // Обновление колоды
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDeck(int id, [FromBody] Deck updatedDeck)
        {
            var success = await _deckService.UpdateDeckAsync(id, updatedDeck);
            if (!success) return NotFound(new { message = "Deck not found" });

            return Ok(new { message = "Deck updated successfully" });
        }
    }
}