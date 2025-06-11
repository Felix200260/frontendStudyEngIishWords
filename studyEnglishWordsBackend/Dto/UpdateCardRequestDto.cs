namespace studyEnglishWordsBackend.Dto;

public class UpdateCardRequestDto
{
    public string EnglishWord { get; set; } = string.Empty;
    public string RussianWord { get; set; } = string.Empty;
    public int DeckId { get; set; }
}