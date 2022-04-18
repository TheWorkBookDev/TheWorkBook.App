using System.Text.Json.Serialization;

namespace TheWorkBook.Dto
{
    public class ListingCommentDto
    {
        [JsonPropertyName("listingCommentId")]
        public int ListingCommentId { get; set; }
        
        [JsonPropertyName("comment")]
        public string Comment { get; set; }
        
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }
    }
}
