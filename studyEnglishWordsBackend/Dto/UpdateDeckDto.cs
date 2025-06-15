namespace studyEnglishWordsBackend.Dto
{
    public class UpdateDeckDto
    {
        public int UserId { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<int> Categories { get; set; } = new List<int>();
    }
}