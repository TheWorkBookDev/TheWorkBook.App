using System.Collections.Specialized;
using TheWorkBook.Dto;
using TheWorkBook.Extensions;

namespace TheWorkBook.Data
{
    public class CategoryService
    {
        private const string _version = "v1";
        readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetCategoriesAsync()
        {
            List<CategoryDto> categories = await _httpClient.MakeGetRequest<List<CategoryDto>>($"/{_version}/category/getCategories");
            return categories;
        }

        public async Task<CategoryDto> GetCategoryAsync(int categoryId)
        {
            var parameters = new NameValueCollection
                            {
                                {"CategoryId", categoryId.ToString()}
                            };

            string path = $"/{_version}/category/get".AttachParameters(parameters);
            CategoryDto category = await _httpClient.MakeGetRequest<CategoryDto>(path);
            return category;
        }
    }
}
