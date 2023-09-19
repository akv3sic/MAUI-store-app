
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
using MauiStoreApp.Views;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiStoreApp.ViewModels
{
    [QueryProperty(nameof(Product), "Product")]
    public partial class ProductDetailsViewModel : BaseViewModel
    {

        private readonly ProductService _productService;

        public ProductDetailsViewModel(ProductService productService)
        {
            _productService = productService;
        }

        public ProductDetailsViewModel()
        {
        }   

        [ObservableProperty]
        Product product;

        public ObservableCollection<Product> CrossSellProducts { get; private set; } = new ObservableCollection<Product>();

        [RelayCommand]
        public async Task Init()
        { 
            await GetProductByIdAsync();
            await GetCrossSellProductsAsync();
        }

        private async Task GetProductByIdAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                var product = await _productService.GetProductByIdAsync(Product.Id);
                Product = product;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get product: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetCrossSellProductsAsync()
        {
            if (IsBusy || Product == null)
                return;

            try
            {
                IsBusy = true;
                var crossSellProducts = await _productService.GetProductsByCategoryAsync(Product.Category);
                CrossSellProducts.Clear();
                foreach (var crossSellProduct in crossSellProducts)
                {
                    if (crossSellProduct.Id != Product.Id) // exclude the current product
                    {
                        CrossSellProducts.Add(crossSellProduct);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get cross-sell products: {ex.Message}");
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
    }
}
