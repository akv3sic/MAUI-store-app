using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    public class CartService : BaseService
    {
        public async Task<Cart> GetCartAsync(int cartId)
        {
            return await GetAsync<Cart>($"carts/{cartId}");
        }
    }
}
