using Microsoft.EntityFrameworkCore;
using studyEnglishWords.Models;

namespace studyEnglishWords
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}

        public DbSet<UserModal> Users { get; set; }
        public DbSet<Deck> Decks { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<CardStatus> CardStatuses { get; set; }
        public DbSet<CardStudyLog> CardStudyLogs { get; set; }
    }
}