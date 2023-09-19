
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
using MauiStoreApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiStoreApp.ViewModels
{
    [QueryProperty(nameof(Category), "Category")]
    public partial class CategoryPageViewModel : BaseViewModel
    {

        private readonly ProductService _productService;

        public CategoryPageViewModel(ProductService productService)
        {
            _productService = productService;
        }

        [ObservableProperty]
        Category category;

        [ObservableProperty]
        string sortOrder = "asc";

        public ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();

        [RelayCommand]
        public async Task Init()
        {
            await GetProductsByCategoryAsync();
        }

        private async Task GetProductsByCategoryAsync(string sortOrder = "asc")
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                var products = await _productService.GetProductsByCategoryAsync(Category.Name, sortOrder);
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
        private async Task SortProducts()
        {
            SortOrder = SortOrder == "asc" ? "desc" : "asc";
            await GetProductsByCategoryAsync(SortOrder);
        }
    }
}
