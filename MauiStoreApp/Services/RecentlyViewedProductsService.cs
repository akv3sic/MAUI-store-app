using CommunityToolkit.Mvvm.ComponentModel;
using MauiStoreApp.Models;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace MauiStoreApp.Services
{
    public partial class RecentlyViewedProductsService: ObservableObject
    {
        public RecentlyViewedProductsService()
        {
        }

        [ObservableProperty]
        private Collection<Product> recentlyViewedProducts = new();

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

        public void LoadProducts()
        {
            var productsJson = Preferences.Get("recently_viewed", string.Empty);
            if (!string.IsNullOrEmpty(productsJson))
            {
                var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(productsJson);
                RecentlyViewedProducts = products ?? new ObservableCollection<Product>();
            }
        }

        public void SaveProducts()
        {
            var productsJson = JsonConvert.SerializeObject(RecentlyViewedProducts);
            Preferences.Set("recently_viewed", productsJson);
        }
    }
}
