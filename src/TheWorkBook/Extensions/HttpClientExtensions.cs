﻿using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace TheWorkBook.Extensions;

public static class HttpClientExtensions
{
    public static string Combine(string uri1, string uri2)
    {
        uri1 = uri1.TrimEnd('/');
        uri2 = uri2.TrimStart('/');
        return string.Format("{0}/{1}", uri1, uri2);
    }

    public static async Task<T> MakeGetRequest<T>(this HttpClient httpClient, string requestString)
    {
        Uri requestUri = new(Combine(httpClient.BaseAddress.ToString(), requestString));
        return await MakeGetRequest<T>(httpClient, requestUri);
    }

    public static async Task<T> MakeGetRequest<T>(this HttpClient httpClient, Uri requestUri)
    {
        string response;
        using (var httpResponse = await httpClient.GetAsync(requestUri, HttpCompletionOption.ResponseHeadersRead))
        {
            httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
            response = await httpResponse.Content.ReadAsStringAsync();
        }

        return GetResult<T>(response);
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

        using var httpResponse = await httpClient.PatchAsync(requestUri, requestContent);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
        string response = await httpResponse.Content.ReadAsStringAsync();
        return GetResult<T>(response);
    }

    public static async Task<T> MakePostRequest<T, U>(this HttpClient httpClient, string requestString, U postData)
    {
        Uri requestUri = new(Combine(httpClient.BaseAddress.ToString(), requestString));
        return await MakePostRequest<T, U>(httpClient, requestUri, postData);
    }

    public static async Task<T> MakePostRequest<T, U>(this HttpClient httpClient, Uri requestUri, U postData)
    {
        using var httpResponse = await httpClient.PostAsJsonAsync(requestUri, postData);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
        string response = await httpResponse.Content.ReadAsStringAsync();
        return GetResult<T>(response);
    }

    public static async Task<T> MakePutRequest<T, U>(this HttpClient httpClient, Uri requestUri, U postData)
    {
        using var httpResponse = await httpClient.PutAsJsonAsync(requestUri, postData);
        httpResponse.EnsureSuccessStatusCode(); // throws if not 200-299
        string response = await httpResponse.Content.ReadAsStringAsync();
        return GetResult<T>(response);
    }

    private static T GetResult<T>(string response)
    {
        if (string.IsNullOrEmpty(response))
        {
            return default;
        }

        T result;
        if (typeof(T) == typeof(string))
        {
            result = (T)(object)response;
        }
        else
        {
            result = JsonSerializer.Deserialize<T>(response);
        }
        return result;
    }
}
