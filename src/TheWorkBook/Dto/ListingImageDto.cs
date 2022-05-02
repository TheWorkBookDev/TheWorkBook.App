using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;

public class ListingImageDto
{
    [JsonPropertyName("listingImageId")]
    public int ListingImageId { get; set; }

    [JsonPropertyName("imageUrl")]
    public string ImageUrl { get; set; }
}
