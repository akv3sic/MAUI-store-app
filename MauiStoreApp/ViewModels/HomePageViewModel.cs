using MauiStoreApp.Models;
using MauiStoreApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Views;

namespace MauiStoreApp.ViewModels
{
    public partial class HomePageViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        private readonly CategoryService _categoryService;
        private readonly RecentlyViewedProductsService _recentlyViewedProductsService;

        public ObservableCollection<Product> Products { get; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; } = new ObservableCollection<Category>();

        bool isFirstRun;

        public HomePageViewModel(ProductService productService, CategoryService categoryService, RecentlyViewedProductsService recentlyViewedProductsService)
        {
            _productService = productService;
            _categoryService = categoryService;
            _recentlyViewedProductsService = recentlyViewedProductsService;
            isFirstRun = true;
        }

        public HomePageViewModel()
        {
        }

        [RelayCommand]
        public async Task Init()
        {
            if (isFirstRun)
            {
                await GetProductsAsync();
                await GetCategoriesAsync();
                _recentlyViewedProductsService.LoadProducts();
                isFirstRun = false;
            }
        }

        private async Task GetCategoriesAsync()
        {
            var categories = await _categoryService.GetCategoriesAsync();
            foreach (var category in categories)
            {
                Categories.Add(category);
            }
        }

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
            if (product == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Product", product }
            };

            _recentlyViewedProductsService.AddProduct(product);

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", true, navigationParameter);
        }

        [RelayCommand]
        private async Task CategoryTapped(Category category)
        {
            if (category == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Category", category }
            };

            await Shell.Current.GoToAsync($"{nameof(CategoryPage)}", true, navigationParameter);
        }
    }
}
