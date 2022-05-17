using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;
public class UserDto
{
    [JsonPropertyName("userId")]
    public int UserId { get; set; }
    
    [JsonPropertyName("firstName")]
    public string FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("mobile")]
    public string Mobile { get; set; }
}

