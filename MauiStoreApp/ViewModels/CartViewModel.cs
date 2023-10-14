
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
        private readonly AuthService _authService;

        bool isFirstRun;

        public CartViewModel(CartService cartService, ProductService productService, AuthService authService)
        {
            _cartService = cartService;
            _authService = authService;
            isFirstRun = true;
        }

        public CartViewModel()
        {
        }

        [ObservableProperty]
        public bool isUserLoggedIn;


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
            else
            {
                // sync the cart items
                if(CartItems.Count != _cartService.GetCartItems().Count)
                {
                    CartItems.Clear();
                    foreach (var cartItem in _cartService.GetCartItems())
                    {
                        CartItems.Add(cartItem);
                    }
                }   
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

                    await _cartService.RefreshCartItemsByUserIdAsync(userId);
                    CartItems.Clear();
                    foreach (var cartItem in _cartService.GetCartItems())
                    {
                        CartItems.Add(cartItem);
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
                    var response = await _cartService.DeleteCartAsync();

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
