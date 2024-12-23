
using System.Text.Json.Serialization;

namespace studyEnglishWords.Models
{
    public class UserDto
    {
        public int Id { get; set; }
        [JsonPropertyName("first_name")]
        public string? First_name { get; set; }

        [JsonPropertyName("unique_email")]
        public string? UniqueEmail { get; set; }

        [JsonPropertyName("password")]
        public string? Password { get; set; }
    }
}