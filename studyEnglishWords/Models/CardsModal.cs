using System.ComponentModel.DataAnnotations.Schema;

namespace studyEnglishWords.Models
{
    [Table("cards")]
    public class Card
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("english_word")]
        public string EnglishWord { get; set; }

        [Column("russian_word")]
        public string RussianWord { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        [Column("next_review_date")]
        public DateTime? NextReviewDate { get; set; }

        [Column("review_count")]
        public int ReviewCount { get; set; }

        [Column("created_at")]
        public DateTime CreatedAt { get; set; }

        [Column("deck_id")]
        public int? DeckId { get; set; }
    }
}