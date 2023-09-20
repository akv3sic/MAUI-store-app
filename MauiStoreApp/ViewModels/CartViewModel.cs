
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiStoreApp.ViewModels
{
    public partial class CartViewModel : BaseViewModel
    {
        private readonly CartService _cartService;
        private readonly ProductService _productService;

        public CartViewModel(CartService cartService, ProductService productService)
        {
            _cartService = cartService;
            _productService = productService;
        }

        public CartViewModel()
        {
        }


        [ObservableProperty]
        Cart cart;

        public ObservableCollection<CartItemDetail> CartItems { get; private set; } = new ObservableCollection<CartItemDetail>();

        [RelayCommand]
        public async Task Init()
        {
            await GetCartByUserIdAsync();
        }

        private async Task GetCartByUserIdAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                // Get a list of Cart objects
                var carts = await _cartService.GetCartByUserIdAsync(1);

                // Get the first cart from the list (if any)
                var cart = carts?.FirstOrDefault();

                if (cart != null)
                {
                    CartItems.Clear();
                    foreach (var cartProduct in cart.Products)
                    {
                        // fetch product details by id
                        var productDetails = await _productService.GetProductByIdAsync(cartProduct.ProductId);
                        CartItems.Add(new CartItemDetail
                        {
                            Product = productDetails,
                            Quantity = cartProduct.Quantity
                        });
                    }
                }
                else
                {
                    Debug.WriteLine("No cart found for user.");
                    await Shell.Current.DisplayAlert("Notice", "No cart found for this user.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get cart: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
