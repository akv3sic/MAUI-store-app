// -----------------------------------------------------------------------
// <copyright file="BaseService.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Diagnostics;

namespace MauiStoreApp.Services
{
    /// <summary>
    /// This class provides base functionality for other service classes.
    /// </summary>
    public class BaseService
    {
        /// <summary>
        /// An instance of <see cref="HttpClient"/>.
        /// </summary>
        protected readonly HttpClient _httpClient;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseService"/> class.
        /// </summary>
        public BaseService()
        {
            _httpClient = new HttpClient
            {
                BaseAddress = new Uri("https://fakestoreapi.com/"),
            };
        }

        /// <summary>
        /// Sends a GET request to the specified endpoint and returns the response as an instance of type <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type of the response object.</typeparam>
        /// <param name="endpoint">The endpoint to send the GET request to.</param>
        /// <returns>A task of type <typeparamref name="T"/>.</returns>
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
                await Shell.Current.DisplayAlert("Error!", "Unable to get data.", "OK");
                return default;
            }
        }

        /// <summary>
        /// Sends a DELETE request to the specified endpoint and returns the response.
        /// </summary>
        /// <param name="endpoint">The endpoint to send the DELETE request to.</param>
        /// <returns>A task of type <see cref="HttpResponseMessage"/>.</returns>
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
                await Shell.Current.DisplayAlert("Error!", "Unable to delete data.", "OK");
                return null;
            }
        }

        /// <summary>
        /// Checks if an internet connection is available.
        /// </summary>
        /// <returns><c>true</c> if an internet connection is available; otherwise, <c>false</c>.</returns>
        private bool IsInternetAvailable()
        {
            NetworkAccess accessType = Connectivity.NetworkAccess;

            if (accessType != NetworkAccess.Internet)
            {
                if (Shell.Current != null)
                {
                    if (accessType == NetworkAccess.ConstrainedInternet)
                    {
                        Shell.Current.DisplayAlert("Error!", "Internet access is limited.", "OK");
                    }
                    else
                    {
                        Shell.Current.DisplayAlert("Error!", "No internet access.", "OK");
                    }
                }

                return false;
            }

            return true;
        }
    }
}
