using System.Text.Json.Serialization;

namespace TheWorkBook.Dto
{
    public class ListingDto
    {
        [JsonPropertyName("budget")]
        public decimal Budget { get; set; }
        
        [JsonPropertyName("category")]
        public CategoryDto Category { get; set; }
        
        [JsonPropertyName("comments")]
        public List<ListingCommentDto> Comments { get; set; }
        
        [JsonPropertyName("listingId")]
        public int ListingId { get; set; }
        
        [JsonPropertyName("listingImages")]        
        public List<ListingImageDto> ListingImages { get; set; }
        
        [JsonPropertyName("location")]
        public LocationDto Location { get; set; }

        [JsonPropertyName("mainDescription")]
        public string MainDescription { get; set; }
        
        [JsonPropertyName("recordCreatedUtc")]
        public DateTime RecordCreatedUtc { get; set; }
        
        [JsonPropertyName("recordUpdatedUtc")]
        public DateTime RecordUpdatedUtc { get; set; }
        
        [JsonPropertyName("statusId")]
        public byte StatusId { get; set; }

        [JsonPropertyName("title")]
        public int Title { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}
