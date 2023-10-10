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
            if (!IsInternetAvailable())
            {
                return default;
            }

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

        protected async Task<HttpResponseMessage> DeleteAsync(string endpoint)
        {
            if (!IsInternetAvailable())
            {
                return null;
            }

            try
            {
                return await _httpClient.DeleteAsync(endpoint);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to delete data: {ex.Message}");
                await Shell.Current.DisplayAlert("Greška!", ex.Message, "U redu");
                return null;
            }
        }

        private bool IsInternetAvailable()
        {
            NetworkAccess accessType = Connectivity.NetworkAccess;

            if (accessType != NetworkAccess.Internet)
            {
                if (Shell.Current != null)
                {
                    if (accessType == NetworkAccess.ConstrainedInternet)
                        Shell.Current.DisplayAlert("Greška!", "Internet veza je ograničena.", "U redu");
                    else
                        Shell.Current.DisplayAlert("Greška!", "Internet veza nije dostupna.", "U redu");
                }
                return false;
            }

            return true;
        }
    }
}
