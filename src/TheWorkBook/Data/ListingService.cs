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

    public async Task<ListingDto> CreateListing(EditListingDto newListingDto)
    {
        string path = $"/{_version}/listing/add";
        ListingDto result = await _httpClient.MakePostRequest<ListingDto, EditListingDto>(path, newListingDto);
        return result;
    }

    public async Task DeactivateListing(int listingId)
    {
        string path = $"/{_version}/listing/deactivate";
        _ = await _httpClient.MakePostRequest<string, int>(path, listingId);
    }

    public async Task<EditListingDto> GetEditListing(int listingId)
    {
        var parameters = new NameValueCollection
                            {
                                {"id", listingId.ToString()}
                            };

        string path = $"/{_version}/listing/get".AttachParameters(parameters);
        EditListingDto listing = await _httpClient.MakeGetRequest<EditListingDto>(path);
        return listing;
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

    public async Task<List<ListingDto>> GetMyListings()
    {
        string path = $"/{_version}/listing/getMyListings";
        List<ListingDto> listings = await _httpClient.MakeGetRequest<List<ListingDto>>(path);
        return listings;
    }

    public async Task<ListingDto> UpdateListing(EditListingDto newListingDto)
    {
        string path = $"/{_version}/listing/update";
        ListingDto result = await _httpClient.MakePostRequest<ListingDto, EditListingDto>(path, newListingDto);
        return result;
    }
}
