using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;

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
    public string Title { get; set; }

    [JsonPropertyName("userId")]
    public int UserId { get; set; }

    public string BudgetText
    {
        get
        {
            return Budget.ToString("0.####");
        }
    }

    public string CategoryName
    {
        get
        {
            string categoryName = "";
            if (this.Category != null)
                categoryName = this.Category.CategoryName;
            return categoryName;
        }
    }

    public string LocationName
    {
        get
        {
            string locationName = "Ireland";
            if (this.Location != null)
                locationName = this.Location.LocationName;
            return locationName;
        }
    }

    public string AdvertAge
    {
        get
        {
            double days = (DateTime.UtcNow - RecordCreatedUtc).TotalDays;
            if (days < 1)
            {
                return "today";
            }
            else if (days < 2)
            {
                return "yesterday";
            }
            else
            {
                return $"{days:0} days ago";
            }
        }
    }
}

