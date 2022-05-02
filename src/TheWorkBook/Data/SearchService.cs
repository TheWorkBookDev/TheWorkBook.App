using System.Collections.Specialized;
using TheWorkBook.Dto;
using TheWorkBook.Extensions;

namespace TheWorkBook.Data;

public class SearchService
{
    private const string _version = "v1";
    readonly HttpClient _httpClient;

    public SearchService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<SearchResponse> SearchListingsAsync(List<int> categories, List<int> locations)
    {
        var parameters = new NameValueCollection();
        if (categories != null && categories.Any())
        {
            categories.ForEach(c =>
            {
                parameters.Add("Categories", c.ToString());
            });
        }

        if (locations != null && locations.Any())
        {
            locations.ForEach(l =>
            {
                parameters.Add("Locations", l.ToString());
            });
        }

        string path = $"/{_version}/search/search".AttachParameters(parameters);
        SearchResponse category = await _httpClient.MakeGetRequest<SearchResponse>(path);
        return category;
    }
}
