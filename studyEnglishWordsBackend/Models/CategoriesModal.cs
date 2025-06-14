using System.ComponentModel.DataAnnotations.Schema;

namespace studyEnglishWordsBackend.Models
{
    [Table("categories")]
    public class CategoriesModal
    {
        [Column("id")]
        public int? Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }
        
        [Column("user_id")]
        public string? UserId  { get; set; }
        
        
        
        // Навигационные свойства
        public virtual ICollection<DeckCategoriesModal> DeckCategories { get; set; } = new List<DeckCategoriesModal>();
    }
}