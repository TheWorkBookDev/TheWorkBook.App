using System.Text.Json.Serialization;

namespace TheWorkBook.Dto;

public class EditListingDto
{
    [JsonPropertyName("budget")]
    public decimal Budget { get; set; }

    [JsonPropertyName("categoryId")]
    public int CategoryId { get; set; }

    [JsonPropertyName("listingId")]
    public int ListingId { get; set; }

    [JsonPropertyName("locationId")]
    public int LocationId { get; set; }

    [JsonPropertyName("mainDescription")]
    public string MainDescription { get; set; }

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
        set
        {
            decimal budget;
            if (decimal.TryParse(value, out budget))
            {
                Budget = budget;
            }
        }
    }
}
