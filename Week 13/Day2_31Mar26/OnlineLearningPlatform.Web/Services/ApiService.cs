using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace OnlineLearningPlatform.Web.Services;

public class ApiService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _config;

    public ApiService(
        HttpClient httpClient,
        IConfiguration config)
    {
        _httpClient = httpClient;
        _config = config;

        _httpClient.BaseAddress =
            new Uri(_config["ApiSettings:BaseUrl"]);
    }

    // LOGIN / REGISTER / CREATE

    public async Task<string>
        PostAsync(
            string url,
            object data,
            string? token = null)
    {
        var json =
            JsonConvert.SerializeObject(data);

        var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

        if (!string.IsNullOrEmpty(token))
        {
            _httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    token);
        }

        var response =
            await _httpClient.PostAsync(
                url,
                content);

        return await response.Content
            .ReadAsStringAsync();
    }

    // GET

    public async Task<string>
        GetAsync(
            string url,
            string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var response =
            await _httpClient.GetAsync(url);

        return await response.Content
            .ReadAsStringAsync();
    }

    // PUT

    public async Task<string>
        PutAsync(
            string url,
            object data,
            string token)
    {
        var json =
            JsonConvert.SerializeObject(data);

        var content =
            new StringContent(
                json,
                Encoding.UTF8,
                "application/json");

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var response =
            await _httpClient.PutAsync(url, content);

        return await response.Content
            .ReadAsStringAsync();
    }

    // DELETE

    public async Task<string>
        DeleteAsync(
            string url,
            string token)
    {
        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                token);

        var response =
            await _httpClient.DeleteAsync(url);

        return await response.Content
            .ReadAsStringAsync();
    }
}