// -----------------------------------------------------------------------
// <copyright file="CartViewModel.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;

namespace MauiStoreApp.ViewModels
{
    /// <summary>
    /// Represents the view model for the cart page.
    /// </summary>
    public partial class CartViewModel : BaseViewModel
    {
        private readonly CartService _cartService;
        private readonly AuthService _authService;

        bool isFirstRun;

        /// <summary>
        /// Initializes a new instance of the <see cref="CartViewModel"/> class.
        /// </summary>
        /// <param name="cartService">An instance of the <see cref="CartService"/> class used for cart operations.</param>
        /// <param name="authService">An instance of the <see cref="AuthService"/> class used for authentication operations.</param>
        public CartViewModel(CartService cartService, AuthService authService)
        {
            _cartService = cartService;
            _authService = authService;
            isFirstRun = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CartViewModel"/> class.
        /// </summary>
        public CartViewModel()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is logged in.
        /// </summary>
        [ObservableProperty]
        public bool isUserLoggedIn;

        /// <summary>
        /// Gets or sets a value indicating whether the view model is busy with cart modification.
        /// </summary>
        [ObservableProperty]
        private bool isBusyWithCartModification;

        /// <summary>
        /// Gets the cart items.
        /// </summary>
        public ObservableCollection<CartItemDetail> CartItems { get; private set; } = new ObservableCollection<CartItemDetail>();

        /// <summary>
        /// Initializes the cart view model.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
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
                this.SyncCartItems();
            }
            IsUserLoggedIn = _authService.IsUserLoggedIn;
        }

        private async Task GetCartByUserIdAsync()
        {
            if (_authService.IsUserLoggedIn)
            {
                if (IsBusy)
                {
                    return;
                }

                int userId;
                var userIdStr = await SecureStorage.GetAsync("userId");
                if (!int.TryParse(userIdStr, out userId))
                {
                    Debug.WriteLine("Failed to get or parse userId from SecureStorage.");
                    await Shell.Current.DisplayAlert("Error", "Failed to retrieve user.", "OK");
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
                    await Shell.Current.DisplayAlert("Error", "Failed to retrieve cart.", "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }

        /// <summary>
        /// Navigates to the login page.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [RelayCommand]
        public async Task GoToLoginPage()
        {
            await Shell.Current.GoToAsync("LoginPage");
        }

        /// <summary>
        /// Deletes the cart.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [RelayCommand]
        public async Task DeleteCart()
        {
            if (IsBusy || IsBusyWithCartModification)
            {
                return;
            }

            try
            {
                var userResponse = await Shell.Current.DisplayAlert("Confirm", "Are you sure you want to delete the cart?", "Yes", "No");
                if (!userResponse)
                {
                    Debug.WriteLine("Cart deletion cancelled by the user.");
                    return;
                }

                if (CartItems.Count > 0)
                {
                    IsBusyWithCartModification = true;

                    var response = await _cartService.DeleteCartAsync();

                    if (response != null && response.IsSuccessStatusCode)
                    {
                        Debug.WriteLine("Cart deleted.");
                        CartItems.Clear();

                        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();

                        string infoText = "Cart deleted successfully.";
                        ToastDuration duration = ToastDuration.Short;
                        var toast = Toast.Make(infoText, duration);

                        await toast.Show(cancellationTokenSource.Token);
                    }
                    else
                    {
                        Debug.WriteLine("Failed to delete cart.");
                        await Shell.Current.DisplayAlert("Error", "Failed to delete cart.", "OK");
                    }
                }
                else
                {
                    Debug.WriteLine("No cart found.");
                    await Shell.Current.DisplayAlert("Error", "No cart found.", "OK");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to delete cart: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to delete cart.", "OK");
            }
            finally
            {
                IsBusyWithCartModification = false;
            }
        }

        /// <summary>
        /// Increases the quantity of the specified product in the cart.
        /// </summary>
        /// <param name="product">The product whose quantity to increase.</param>
        [RelayCommand]
        public void IncreaseProductQuantity(Product product)
        {
            try
            {
                _cartService.IncreaseProductQuantity(product.Id);
                SyncCartItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to increase product quantity: {ex.Message}");
                Shell.Current.DisplayAlert("Error", "Failed to increase product quantity.", "OK");
            }
        }

        /// <summary>
        /// Decreases the quantity of the specified product in the cart.
        /// </summary>
        /// <param name="product">The product whose quantity to decrease.</param>
        [RelayCommand]
        public void DecreaseProductQuantity(Product product)
        {
            try
            {
                _cartService.DecreaseProductQuantity(product.Id);
                SyncCartItems();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to decrease product quantity: {ex.Message}");
                Shell.Current.DisplayAlert("Error", "Failed to decrease product quantity.", "OK");
            }
        }

        private void SyncCartItems()
        {
            var updatedCartItems = _cartService.GetCartItems();
            foreach (var item in CartItems.ToList())
            {
                if (!updatedCartItems.Any(ci => ci.Product.Id == item.Product.Id))
                {
                    CartItems.Remove(item);
                }
            }

            foreach (var updatedItem in updatedCartItems)
            {
                var existingItem = CartItems.FirstOrDefault(ci => ci.Product.Id == updatedItem.Product.Id);
                if (existingItem == null)
                {
                    CartItems.Add(updatedItem);
                }
                else
                {
                    existingItem.Quantity = updatedItem.Quantity;
                }
            }
        }
    }
}
