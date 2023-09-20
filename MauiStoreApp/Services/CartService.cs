using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    public class CartService : BaseService
    {
        public async Task<Cart> GetCartAsync(int cartId)
        {
            return await GetAsync<Cart>($"carts/{cartId}");
        }

        public async Task<List<Cart>> GetCartByUserIdAsync(int userId)
        {
            return await GetAsync<List<Cart>>($"carts/user/{userId}");
        }
    }
}
