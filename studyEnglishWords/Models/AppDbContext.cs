using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace studyEnglishWords.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
        
        public DbSet<User> Users { get; set; }
        
        [Table("users")]
        public class User
        {
            [Column("id")]
            public int Id { get; set; }
            [Column("first_name")]
            public string? First_name { get; set; }
            [Column("unique_email")]
            public string? UniqueEmail { get; set; }
            [Column("password")]
            public string? Password { get; set; }
        }
    }
    
}

