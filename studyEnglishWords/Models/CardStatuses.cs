using System.ComponentModel.DataAnnotations.Schema;

namespace studyEnglishWords.Models
{
    [Table("card_statuses")]
    public class CardStatus
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("status_name")]
        public string StatusName { get; set; }

        [Column("status_description")]
        public string? StatusDescription { get; set; }
    }
}