using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;

public class SearchResponse
{
    [JsonPropertyName("listings")]
    public List<ListingDto> Listings { get; set; }
}

