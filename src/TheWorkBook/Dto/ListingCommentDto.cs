using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;

public class ListingCommentDto
{
    [JsonPropertyName("comment")]
    public string Comment { get; set; }

    [JsonPropertyName("date")]
    public DateTime Date { get; set; }

    [JsonPropertyName("listingCommentId")]
    public int ListingCommentId { get; set; }
}

