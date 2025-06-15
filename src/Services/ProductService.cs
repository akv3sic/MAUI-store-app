using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    /// <summary>
    /// Provides services for managing and retrieving products.
    /// </summary>
    public class ProductService : BaseService
    {
        /// <summary>
        /// Asynchronously retrieves a collection of all available products, optionally sorted in a specific order.
        /// </summary>
        /// <param name="sortOrder">The order in which the products should be sorted. Defaults to "asc" for ascending.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Product"/> objects.
        /// </returns>
        public async Task<IEnumerable<Product>> GetProductsAsync(string sortOrder = "asc")
        {
            return await GetAsync<IEnumerable<Product>>($"products?sort={sortOrder}");
        }

        /// <summary>
        /// Asynchronously retrieves a product by its ID.
        /// </summary>
        /// <param name="id">The ID of the product to retrieve.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains the <see cref="Product"/> object with the specified ID.
        /// </returns>
        public async Task<Product> GetProductByIdAsync(int id)
        {
            return await GetAsync<Product>($"products/{id}");
        }

        /// <summary>
        /// Asynchronously retrieves a collection of products belonging to a specific category, optionally sorted in a specific order.
        /// </summary>
        /// <param name="category">The category of the products to retrieve.</param>
        /// <param name="sortOrder">The order in which the products should be sorted. Defaults to "asc" for ascending.</param>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Product"/> objects belonging to the specified category.
        /// </returns>
        public async Task<IEnumerable<Product>> GetProductsByCategoryAsync(string category, string sortOrder = "asc")
        {
            return await GetAsync<IEnumerable<Product>>($"products/category/{category}?sort={sortOrder}");
        }
    }
}
