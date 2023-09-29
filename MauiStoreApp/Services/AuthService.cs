using MauiStoreApp.Models;
using System.Text;
using System.Text.Json;

namespace MauiStoreApp.Services
{
    public class AuthService : BaseService
    {
        public async Task<LoginResponse> LoginAsync(string username, string password)
        {
            var request = new LoginRequest
            {
                Username = username,
                Password = password
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
