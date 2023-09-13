namespace MauiStoreApp.Services
{
    public class BaseService
    {
        protected readonly HttpClient _httpClient;

        public BaseService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://fakestoreapi.com/"),
            };
        }

        protected async Task<T> GetAsync<T>(string endpoint)
        {
            var response = await _httpClient.GetAsync(endpoint);


            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            return System.Text.Json.JsonSerializer.Deserialize<T>(responseContent);
        }
    }
}
