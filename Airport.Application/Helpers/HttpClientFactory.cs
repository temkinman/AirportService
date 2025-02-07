namespace Airport.Application.Helpers;

public static class HttpClientFactory
{
    private static readonly HttpClient _httpClient;

    static HttpClientFactory()
    {
        _httpClient = new HttpClient
        {
            Timeout = TimeSpan.FromSeconds(30),
            BaseAddress = new("https://places-dev.continent.ru")
        };
    }

    public static HttpClient CreateClient()
    {
        return _httpClient;
    }
}