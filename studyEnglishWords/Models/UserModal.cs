using System.ComponentModel.DataAnnotations.Schema;

namespace studyEnglishWords.Models
{
    [Table("users")]
    public class UserModal
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string? FirstName { get; set; }

        [Column("unique_email")]
        public string? UniqueEmail { get; set; }

        [Column("password")]
        public string? Password { get; set; }
        
        [Column("created_at")]
        public DateTime CreatedAt { get; set; }
    }
}