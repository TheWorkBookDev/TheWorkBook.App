using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;
public class UserDto
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("mobile")]
    public string Mobile { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }
}

