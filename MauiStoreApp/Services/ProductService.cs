using MauiStoreApp.Models;
using MauiStoreApp.Services;

namespace MauiStoreApp.Services
{
    public class ProductService : BaseService
    {
        public async Task<IEnumerable<Product>> GetProductsAsync()
        {
            return await GetAsync<IEnumerable<Product>>("products");
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await GetAsync<Product>($"products/{id}");
        }
    }
}
