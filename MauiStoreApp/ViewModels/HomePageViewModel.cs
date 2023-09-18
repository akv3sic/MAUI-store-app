using MauiStoreApp.Models;
using MauiStoreApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Views;
using Newtonsoft.Json;

namespace MauiStoreApp.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;

        public ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; private set; } = new ObservableCollection<Category>();

        public ObservableCollection<Product> RecentlyViewedProducts { get; private set; } = new ObservableCollection<Product>();

        public HomePageViewModel()
        {
            _productService = new ProductService();
            _categoryService = new CategoryService();

            LoadRecentlyViewedProducts();
        }

        [RelayCommand]
        private async Task GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

        [RelayCommand]
        private async Task GetProductsAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var products = await _productService.GetProductsAsync();
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get products: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task ProductTapped(Product product)
        {
            IsBusy = true;

            if (product == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Product", product }
            };

            AddToRecentlyViewedProducts(product);

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", true, navigationParameter);

            IsBusy = false;
        }

        [RelayCommand]
        private async Task CategoryTapped(Category category)
        {
            IsBusy = true;

            if (category == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Category", category }
            };

            await Shell.Current.GoToAsync($"{nameof(CategoryPage)}", true, navigationParameter);

            IsBusy = false;
        }

        private void AddToRecentlyViewedProducts(Product product)
        {
            // remove the product from the list if it already exists
            var existingProduct = RecentlyViewedProducts.FirstOrDefault(p => p.Id == product.Id);
            if (existingProduct != null)
            {
                RecentlyViewedProducts.Remove(existingProduct);
            }

            // add the product to the beginning of the list
            RecentlyViewedProducts.Insert(0, product);

            // if the list is longer than 8 items, remove the last item
            if (RecentlyViewedProducts.Count > 8)
            {
                RecentlyViewedProducts.RemoveAt(RecentlyViewedProducts.Count - 1);
            }

            // save the list to preferences
            var productsJson = JsonConvert.SerializeObject(RecentlyViewedProducts);
            Preferences.Set("recently_viewed", productsJson);
        }


        private void LoadRecentlyViewedProducts()
        {
            var productsJson = Preferences.Get("recently_viewed", string.Empty);
            if (!string.IsNullOrEmpty(productsJson))
            {
                var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(productsJson);
                RecentlyViewedProducts = new ObservableCollection<Product>(products);
            }
        }
    }
}
