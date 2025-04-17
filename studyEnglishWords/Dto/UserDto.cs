
using System.Text.Json.Serialization;

namespace studyEnglishWords.Dto
{
    public class UserDto
    {
        public int Id { get; set; }
        [JsonPropertyName("first_name")]
        public string? firstName { get; set; }

        [JsonPropertyName("unique_email")]
        public string? uniqueEmail { get; set; }

        [JsonPropertyName("password")]
        public string? password { get; set; }
    }
}