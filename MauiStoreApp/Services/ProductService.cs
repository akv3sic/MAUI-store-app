using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    public class ProductService : BaseService
    {
        public async Task<IEnumerable<Product>> GetProductsAsync(string sortOrder = "asc")
        {
            return await GetAsync<IEnumerable<Product>>($"products?sort={sortOrder}");
        }

        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await GetAsync<Product>($"products/{id}");
        }

        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category, string sortOrder = "asc")
        {
            return await GetAsync<IEnumerable<Product>>($"products/category/{category}?sort={sortOrder}");
        }
    }
}
