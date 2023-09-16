
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
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

        [ObservableProperty]
        Product product;

        [RelayCommand]
        public async Task Init()
        { 
            await GetProductByIdAsync();
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
    }
}
