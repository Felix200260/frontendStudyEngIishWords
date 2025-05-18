using System.ComponentModel.DataAnnotations.Schema;

namespace studyEnglishWordsBackend.Models
{
    [Table("categories")]
    public class Categories
    {
        [Column("id")]
        public int? Id { get; set; }

        [Column("title")]
        public string? Title { get; set; }
        
        [Column("user_id")]
        public int? UserId { get; set; }
    }
}