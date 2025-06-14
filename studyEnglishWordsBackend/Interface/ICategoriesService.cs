using studyEnglishWordsBackend.Dto;

namespace studyEnglishWordsBackend.Interface
{
    public interface ICategoriesService
    {
        Task<List<CategoriesDto>> GetCategoriesByDeckIdAsync(int deckId, string userId);
        Task<List<CategoriesDto>> GetAllUserCategoriesAsync(string userId);
    }
}