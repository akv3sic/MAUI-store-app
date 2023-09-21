using System.Diagnostics;

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
            NetworkAccess accessType = Connectivity.Current.NetworkAccess;

            if (accessType != NetworkAccess.Internet)
            {
                if (Shell.Current != null)
                {
                    if (accessType == NetworkAccess.ConstrainedInternet)
                        await Shell.Current.DisplayAlert("Greška!", "Internet veza je ograničena.", "U redu");
                    else
                        await Shell.Current.DisplayAlert("Greška!", "Internet veza nije dostupna.", "U redu");
                }
                return default;
            }
            else
            {
                try
                {
                    var response = await _httpClient.GetAsync(endpoint);

                    response.EnsureSuccessStatusCode();

                    var responseContent = await response.Content.ReadAsStringAsync();
                    return System.Text.Json.JsonSerializer.Deserialize<T>(responseContent);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unable to get data: {ex.Message}");
                    await Shell.Current.DisplayAlert("Greška!", ex.Message, "U redu");

                    return default;
                }
            }   
        }
    }
}
