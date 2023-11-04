// -----------------------------------------------------------------------
// <copyright file="AuthService.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text;
using System.Text.Json;
using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    /// <summary>
    /// This class is responsible for authenticating the user and storing the token and userId in secure storage.
    /// </summary>
    public class AuthService : BaseService
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthService"/> class.
        /// </summary>
        public AuthService()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is logged in.
        /// </summary>
        /// <remarks>
        /// Checks if the token is stored in secure storage. If set to false, removes the token and userId from secure storage.
        /// </remarks>
        public bool IsUserLoggedIn
        {
            get
            {
                var token = SecureStorage.GetAsync("token").Result;
                return !string.IsNullOrEmpty(token);
            }

            set
            {
                if (!value)
                {
                    // remove token and userId from secure storage if IsUserLoggedIn is set to false
                    SecureStorage.Remove("token");
                    SecureStorage.Remove("userId");
                }
            }
        }

        /// <summary>
        /// Logs the user in.
        /// </summary>
        /// <param name="username">The username of the user.</param>
        /// <param name="password">The password of the user.</param>
        /// <returns>A task of type <see cref="LoginResponse"/>.</returns>
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password,
            };

            var content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("auth/login", content);

            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var loginResponse = JsonSerializer.Deserialize<LoginResponse>(responseContent);

            // fetch all users
            var usersResponse = await _httpClient.GetAsync("users");
            usersResponse.EnsureSuccessStatusCode();
            var usersResponseContent = await usersResponse.Content.ReadAsStringAsync();
            var users = JsonSerializer.Deserialize<List<User>>(usersResponseContent);

            // find the matching user and set the UserId in LoginResponse
            var user = users.FirstOrDefault(u => u.Username == username);
            if (user != null)
            {
                loginResponse.UserId = user.Id;
            }

            return loginResponse;
        }
    }
}
