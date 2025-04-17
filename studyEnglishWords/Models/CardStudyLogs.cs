using System.ComponentModel.DataAnnotations.Schema;

namespace studyEnglishWords.Models
{
    [Table("card_study_logs")]
    public class CardStudyLog
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("user_id")]
        public int UserId { get; set; }

        [Column("card_id")]
        public int CardId { get; set; }

        [Column("study_date")]
        public DateTime StudyDate { get; set; }

        [Column("correct_answer")]
        public bool CorrectAnswer { get; set; }

        [Column("review_count")]
        public int ReviewCount { get; set; }

        [Column("next_review_date")]
        public DateTime? NextReviewDate { get; set; }

        [Column("correct_answers_count")]
        public int CorrectAnswersCount { get; set; }

        [Column("incorrect_answers_count")]
        public int IncorrectAnswersCount { get; set; }

        [Column("total_time_spent_on_card")]
        public TimeSpan TotalTimeSpentOnCard { get; set; }

        [Column("difficulty_level_progress")]
        public string DifficultyLevelProgress { get; set; }

        [Column("avg_time_to_learn_card")]
        public TimeSpan AvgTimeToLearnCard { get; set; }

        [Column("usage_frequency")]
        public int UsageFrequency { get; set; }
    }
}