using MauiStoreApp.Models;

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

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category)
        {
            return await GetAsync<IEnumerable<Product>>($"products/category/{category}");
        }
    }
}
