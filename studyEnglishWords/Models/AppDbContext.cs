using Microsoft.EntityFrameworkCore;

namespace studyEnglishWords.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        
        // Таблица для пользователей
        public DbSet<User> Users { get; set; }
        
        // Модель данных для пользователя
        public class User
        {
            public int Id { get; set; } // Первичный ключ
            public string? Name { get; set; }
            public string? Email { get; set; }
            public string? Password { get; set; }
        }
    }
    
}

