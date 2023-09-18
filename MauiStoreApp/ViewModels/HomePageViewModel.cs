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

        public ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();
        public ObservableCollection<Category> Categories { get; private set; } = new ObservableCollection<Category>();

        public HomePageViewModel()
        {
            _productService = new ProductService();
            _categoryService = new CategoryService();
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
    }
}
