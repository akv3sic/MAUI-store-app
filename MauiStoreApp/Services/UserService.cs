using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    /// <summary>
    /// Provides methods to interact with user data.
    /// </summary>
    public class UserService : BaseService
    {
        private readonly AuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="authService">An instance of the <see cref="AuthService"/> class used for authentication operations.</param>
        public UserService(AuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Retrieves user information by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user to retrieve information for.</param>
        /// <returns>A <see cref="User"/> object representing the user's information.</returns>
        public async Task<User> GetUserAsync(int userId)
        {
            return await GetAsync<User>($"users/{userId}");
        }

        /// <summary>
        /// Deletes a user account by user ID.
        /// </summary>
        /// <param name="userId">The ID of the user whose account is to be deleted.</param>
        /// <returns>A boolean value indicating whether the account deletion was successful.</returns>
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
