
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
        private readonly CartService _cartService;
        private readonly RecentlyViewedProductsService _recentlyViewedProductsService;

        public ProductDetailsViewModel(ProductService productService, CartService cartService, RecentlyViewedProductsService recentlyViewedProductsService)
        {
            _productService = productService;
            _cartService = cartService;
            _recentlyViewedProductsService = recentlyViewedProductsService;
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

            _recentlyViewedProductsService.AddProduct(product);

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", true, navigationParameter);

            IsBusy = false;
        }

        [RelayCommand]
        private async Task ShareProduct(Product product)
        {
            if (product == null)
                return;

            await Share.RequestAsync(new ShareTextRequest
            {
                Uri = product.Image,
                Title = product.Title,
                Text = "Pogledaj ovaj proizod na AStore!"
            });
        }

        [RelayCommand]
        private async Task AddToCart(Product product)
        {
            if (IsBusy)
                return;

            try
            {
                if (product == null)
                    return;

                _cartService.AddProductToCart(product);

                await Shell.Current.DisplayAlert("Obavijest", "Proizvod je dodan u košaricu.", "U redu");
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to add product to cart: {ex.Message}");
                await Shell.Current.DisplayAlert("Greška", "Greška prilikom dodavanja proizvoda u košaricu.", "U redu");
            }
        }
    }
}
