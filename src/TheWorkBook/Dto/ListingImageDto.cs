using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;

public class ListingImageDto
{
    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }

    [JsonPropertyName("listingImageId")]
    public int ListingImageId { get; set; }
}
