using System.Collections.Specialized;
using TheWorkBook.Dto;
using TheWorkBook.Extensions;

namespace TheWorkBook.Data;

public class ListingService
{
    private const string _version = "v1";
    readonly HttpClient _httpClient;

    public ListingService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<string> CreateListing(NewListingDto newListingDto)
    {
        string path = $"/{_version}/listing/add";
        string result = await _httpClient.MakePostRequest<string, NewListingDto>(path, newListingDto);
        return result;
    }

    public async Task<ListingDto> GetListing(int listingId)
    {
        var parameters = new NameValueCollection
                            {
                                {"id", listingId.ToString()}
                            };

        string path = $"/{_version}/listing/get".AttachParameters(parameters);
        ListingDto listing = await _httpClient.MakeGetRequest<ListingDto>(path);
        return listing;
    }
}
