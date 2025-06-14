using System.ComponentModel.DataAnnotations.Schema;

namespace studyEnglishWordsBackend.Models
{
    [Table("deck_categories")]
    public class DeckCategoriesModal
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("deck_id")]
        public int DeckId { get; set; }

        [Column("category_id")]
        public int CategoryId { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        // Навигационные св-ва
        public DeckModal Deck { get; set; }
        public CategoriesModal Category { get; set; }
    }
}