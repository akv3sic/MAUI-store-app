using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using MauiStoreApp.Models;
using Newtonsoft.Json;

namespace MauiStoreApp.Services
{
    /// <summary>
    /// Manages and persists the list of recently viewed products.
    /// </summary>
    public partial class RecentlyViewedProductsService : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RecentlyViewedProductsService"/> class.
        /// </summary>
        public RecentlyViewedProductsService()
        {
        }

        /// <summary>
        /// Gets or sets the list of recently viewed products.
        /// </summary>
        [ObservableProperty]
        private Collection<Product> recentlyViewedProducts = new();

        /// <summary>
        /// Adds a product to the list of recently viewed products.
        /// If the product already exists in the list, it is moved to the front.
        /// The list is capped at 8 products, and older products are removed.
        /// The list is then saved to persistent storage.
        /// </summary>
        /// <param name="product">The product to add.</param>
        public void AddProduct(Product product)
        {
            var existingProduct = RecentlyViewedProducts.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                RecentlyViewedProducts.Remove(existingProduct);
            }

            RecentlyViewedProducts.Insert(0, product);

            if (RecentlyViewedProducts.Count > 8)
            {
                RecentlyViewedProducts.RemoveAt(RecentlyViewedProducts.Count - 1);
            }

            SaveProducts();
        }

        /// <summary>
        /// Loads the list of recently viewed products from persistent storage.
        /// </summary>
        public void LoadProducts()
        {
            var productsJson = Preferences.Get("recently_viewed", string.Empty);
            if (!string.IsNullOrEmpty(productsJson))
            {
                var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(productsJson);
                RecentlyViewedProducts = products ?? new ObservableCollection<Product>();
            }
        }

        /// <summary>
        /// Saves the list of recently viewed products to persistent storage.
        /// </summary>
        public void SaveProducts()
        {
            var productsJson = JsonConvert.SerializeObject(RecentlyViewedProducts);
            Preferences.Set("recently_viewed", productsJson);
        }
    }
}
