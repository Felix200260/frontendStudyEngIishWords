using Microsoft.EntityFrameworkCore;
using studyEnglishWordsBackend.Models;

namespace studyEnglishWordsBackend.Service
{
    public interface ICardsService
    {
        Task<List<Card>> GetCardsByDeckIdAsync(int deckId);
    }

    public class CardsService : ICardsService
    {
        private readonly AppDbContext _context;

        public CardsService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Card>> GetCardsByDeckIdAsync(int deckId)
        {
            return await _context.Cards
                .Where(c => c.DeckId == deckId)
                .ToListAsync();
        }
    }
}