using System.ComponentModel.DataAnnotations.Schema;

namespace studyEnglishWords.Models
{
    [Table("decks")]
    public class Deck
    {
        [Column("id")]
        public int? Id { get; set; }

        [Column("user_id")]
        public int? UserId { get; set; }

        [Column("deck_name")]
        public string? Title { get; set; }
        
        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }
        
        [Column("description")]
        public string? Description { get; set; }

    }
}