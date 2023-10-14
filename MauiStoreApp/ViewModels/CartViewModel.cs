
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
        private readonly AuthService _authService;

        bool isFirstRun;

        public CartViewModel(CartService cartService, ProductService productService, AuthService authService)
        {
            _cartService = cartService;
            _productService = productService;
            _authService = authService;
            isFirstRun = true;
        }

        public CartViewModel()
        {
        }

        [ObservableProperty]
        public bool isUserLoggedIn;


        private int cartId;

        [ObservableProperty]
        private bool isBusyWithCartModification;

        public ObservableCollection<CartItemDetail> CartItems { get; private set; } = new ObservableCollection<CartItemDetail>();

        [RelayCommand]
        public async Task Init()
        {
            if (isFirstRun)
            {
                await GetCartByUserIdAsync();
                isFirstRun = false;
            }
            IsUserLoggedIn = _authService.IsUserLoggedIn;
        }

        private async Task GetCartByUserIdAsync()
        {
            if (_authService.IsUserLoggedIn)
            {

                if (IsBusy)
                    return;

                int userId;

                var userIdStr = await SecureStorage.GetAsync("userId");
                if (!int.TryParse(userIdStr, out userId))
                {
                    Debug.WriteLine("Failed to get or parse userId from SecureStorage.");
                    await Shell.Current.DisplayAlert("Greška", "Greška prilikom dohvata korisničkih podataka.", "U redu");
                    return;
                }

                try
                {
                    IsBusy = true;

                    // Get a list of Cart objects
                    var carts = await _cartService.GetCartByUserIdAsync(userId);

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
                        // set cartId
                        cartId = cart.Id;
                    }
                    else
                    {
                        Debug.WriteLine("No cart found for user.");
                        await Shell.Current.DisplayAlert("Obavijest", "Nema košarice za ovog korisnika.", "U redu");
                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Unable to get cart: {ex.Message}");
                    await Shell.Current.DisplayAlert("Greška", "Greška prilikom dohvata košarice.", "U redu");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        [RelayCommand]
        public async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync("LoginPage");
        }

        [RelayCommand]
        public async Task DeleteCart()
        {
            if (IsBusy || IsBusyWithCartModification)
                return;

            try
            {
                // Ask the user for confirmation
                var userResponse = await Shell.Current.DisplayAlert("Potvrda", "Jeste li sigurni da želite obrisati košaricu?", "Da", "Ne");
                if (!userResponse)
                {
                    Debug.WriteLine("Cart deletion cancelled by the user.");
                    return; // exit if the user cancels the deletion
                }

                if (CartItems.Count > 0)
                {
                    IsBusyWithCartModification = true;

                    // Delete the cart
                    var response = await _cartService.DeleteCartAsync(cartId);

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Cart deleted.");
                        CartItems.Clear();
                        await Shell.Current.DisplayAlert("Obavijest", "Košarica je uspješno obrisana.", "U redu");
                    }
                    else
                    {
                        Debug.WriteLine("Failed to delete cart.");
                        await Shell.Current.DisplayAlert("Greška", "Greška prilikom brisanja košarice.", "U redu");
                    }
                }
                else
                {
                    Debug.WriteLine("No cart found.");
                    await Shell.Current.DisplayAlert("Obavijest", "Košarica nije pronađena.", "U redu");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to delete cart: {ex.Message}");
                await Shell.Current.DisplayAlert("Greška", "Greška prilikom brisanja košarice.", "U redu");
            }
            finally
            {
                IsBusyWithCartModification = false;
            }
        }
    }
}
