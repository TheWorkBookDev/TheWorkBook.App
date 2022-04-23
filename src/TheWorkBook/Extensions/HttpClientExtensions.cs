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
            try
            {
                string result;
                using (var httpResponse = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
                {
                    httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
                    result = await httpResponse.Content.ReadAsStringAsync();
                }

                return JsonSerializer.Deserialize<T>(result);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static async Task<T> MakePatchRequest<T, U>(this HttpClient httpClient, string requestString, U postData)
        {
            Uri requestUri = new(Combine(httpClient.BaseAddress.ToString(), requestString));
            return await MakePatchRequest<T, U>(httpClient, requestUri, postData);
        }

        public static async Task<T> MakePatchRequest<T, U>(this HttpClient httpClient, Uri requestUri, U postData)
        {
            string serializedDoc = JsonSerializer.Serialize(postData);
            var requestContent = new StringContent(serializedDoc, Encoding.UTF8, "application/json-patch+json");

            try
            {
                using var httpResponse = await httpClient.PatchAsync(requestUri, requestContent);
                httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
                string response = await httpResponse.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<T>(response);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string Combine(string uri1, string uri2)
        {
            uri1 = uri1.TrimEnd('/');
            uri2 = uri2.TrimStart('/');
            return string.Format("{0}/{1}", uri1, uri2);
        }
    }
}
