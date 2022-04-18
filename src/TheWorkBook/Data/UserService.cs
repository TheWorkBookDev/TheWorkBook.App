using System.Collections.Specialized;
using TheWorkBook.Dto;
using TheWorkBook.Extensions;

namespace TheWorkBook.Data
{
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

        //public async Task<CategoryDto> GetCategoryAsync(int categoryId)
        //{
        //    var parameters = new NameValueCollection
        //                    {
        //                        {"CategoryId", categoryId.ToString()}
        //                    };

        //    string path = $"/{_version}/category/get".AttachParameters(parameters);
        //    CategoryDto category = await _httpClient.MakeGetRequest<CategoryDto>(path);
        //    return category;
        //}
    }
}
