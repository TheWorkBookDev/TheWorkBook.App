using TheWorkBook.Dto;
using TheWorkBook.Extensions;

namespace TheWorkBook.Data;

public class LocationService
{
    readonly HttpClient _httpClient;

    public LocationService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<LocationDto>> GetCounties()
    {
        List<LocationDto> locations = await _httpClient.MakeGetRequest<List<LocationDto>>("/v1/location/getCounties");
        return locations;
    }
}

