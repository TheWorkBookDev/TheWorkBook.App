using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;

public class LocationDto
{
    [JsonPropertyName("locationId")]
    public int LocationId { get; set; }

    [JsonPropertyName("locationName")]
    public string LocationName { get; set; }

    [JsonPropertyName("locationTypeId")]
    public byte LocationTypeId { get; set; }

    [JsonPropertyName("parentLocationId")]
    public int? ParentLocationId { get; set; }
}

