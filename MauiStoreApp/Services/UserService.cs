using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    public class UserService : BaseService
    {
        public async Task<User> GetUserAsync(int userId)
        {
            return await GetAsync<User>($"users/{userId}");
        }
    }
}
