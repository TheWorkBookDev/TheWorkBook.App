using TheWorkBook.Dto;
using TheWorkBook.Extensions;

namespace TheWorkBook.Data;

public class UserService
{
    private const string _version = "v1";
    readonly HttpClient _httpClient;

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<UserDto> GetMyInfo()
    {
        UserDto user = await _httpClient.MakeGetRequest<UserDto>($"/{_version}/user/getMyInfo");

        // Authenticated request!
        return user;
    }

    public async Task<string> UpdateMyInfo(UserDto userDto)
    {
        string path = $"/{_version}/user/updateMyInfo";
        string result = await _httpClient.MakePostRequest<string, UserDto>(path, userDto);
        return result;
    }
}

