using System.Text;
using System.Text.Json;

namespace TheWorkBook.Extensions
{
    public static class HttpClientExtensions
    {
        public static async Task<T> MakeGetRequest<T>(this HttpClient httpClient, string requestString)
        {
            Uri requestUri = new(Combine(httpClient.BaseAddress.ToString(), requestString));
            return await MakeGetRequest<T>(httpClient, requestUri);
        }

        public static async Task<T> MakeGetRequest<T>(this HttpClient httpClient, Uri requestUri)
        {
            string result;
            using (var httpResponse = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
            {
                httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
                result = await httpResponse.Content.ReadAsStringAsync();
            }

            return JsonSerializer.Deserialize<T>(result);
        }

        public static string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }
    }
}
