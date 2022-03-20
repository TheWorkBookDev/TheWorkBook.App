using System.Text.Json.Serialization;

namespace TheWorkBook.Dto
{
	public class CategoryDto
    {
        [JsonPropertyName("categoryId")]
        public int CategoryId { get; set; }

        [JsonPropertyName("categoryName")]
        public string CategoryName { get; set; }

        [JsonPropertyName("displayOnMainNav")]
        public bool? DisplayOnMainNav { get; set; }

        [JsonPropertyName("hasChildCategory")]
        public bool HasChildCategory { get; set; }
    }
}
