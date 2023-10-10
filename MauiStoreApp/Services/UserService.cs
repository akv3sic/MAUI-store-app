using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    public class UserService : BaseService
    {
        private readonly AuthService _authService;

        public UserService(AuthService authService)
        {
            _authService = authService;
        }

        public async Task<User> GetUserAsync(int userId)
        {
            return await GetAsync<User>($"users/{userId}");
        }

        public async Task<bool> DeleteUserAccountAsync(int userId)
        {
            var response = await DeleteAsync($"users/{userId}");

            if (response != null && response.IsSuccessStatusCode)
            {
                // Log the user out after successful deletion
                _authService.IsUserLoggedIn = false;
                return true;
            }

            return false;
        }
    }
}
