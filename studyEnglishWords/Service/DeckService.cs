using Microsoft.EntityFrameworkCore;
using studyEnglishWords.Models;

namespace studyEnglishWords.Service
{
    public class DeckService
    {
        private readonly AppDbContext _context;

        public DeckService(AppDbContext context)
        {
            _context = context;
        }

        // Получение всех колод
        public async Task<List<Deck>> GetAllDecksAsync()
        {
            return await _context.Decks.ToListAsync();
        }

        // Добавление новой колоды
        public async Task<Deck> AddDeckAsync(Deck deck)
        {
            _context.Decks.Add(deck);
            await _context.SaveChangesAsync();
            return deck;
        }

        // Удаление колоды
        public async Task<bool> DeleteDeckAsync(int deckId)
        {
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck == null) return false;

            _context.Decks.Remove(deck);
            await _context.SaveChangesAsync();
            return true;
        }

        // Обновление данных колоды
        public async Task<bool> UpdateDeckAsync(int deckId, Deck updatedDeck)
        {
            var deck = await _context.Decks.FindAsync(deckId);
            if (deck == null) return false;

            deck.Title = updatedDeck.Title;
            deck.UserId = updatedDeck.UserId;
            deck.Description = updatedDeck.Description;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}