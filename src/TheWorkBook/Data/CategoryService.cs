using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWorkBook.Dto;
using TheWorkBook.Extensions;

namespace TheWorkBook.Data
{
    public class CategoryService
    {
        readonly HttpClient _httpClient;

        public CategoryService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<List<CategoryDto>> GetCateogies()
        {
            List<CategoryDto> categories = await _httpClient.MakeGetRequest<List<CategoryDto>>("/v1/category/getCategories");
            return categories;
        }
    }
}
