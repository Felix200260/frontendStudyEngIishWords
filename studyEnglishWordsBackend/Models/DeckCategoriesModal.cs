using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace studyEnglishWordsBackend.Models
{
    [Table("deck_categories")]
    public class DeckCategoriesModal
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("deck_id")]
        public int DeckId { get; set; }

        [Column("categories_id")]
        public int CategoriesId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Навигационные св-ва
        public DeckModal? Deck { get; set; }
        
        public Categories? Categories { get; set; }
    }
}