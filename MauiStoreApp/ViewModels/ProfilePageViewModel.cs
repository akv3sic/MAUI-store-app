﻿using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
using MauiStoreApp.Views;

namespace MauiStoreApp.ViewModels
{
    /// <summary>
    /// Represents the view model for the profile page.
    /// </summary>
    public partial class ProfilePageViewModel : BaseViewModel
    {
        private readonly UserService _userService;
        private readonly AuthService _authService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilePageViewModel"/> class.
        /// </summary>
        /// <param name="userService">The user service.</param>
        /// <param name="authService">The authentication service.</param>
        public ProfilePageViewModel(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ProfilePageViewModel"/> class. This empty constructor is used for design-time data.
        /// </summary>
        public ProfilePageViewModel()
        {
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user is logged in.
        /// </summary>
        [ObservableProperty]
        public bool isUserLoggedIn;

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [ObservableProperty]
        User user;

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [RelayCommand]
        public async Task Init()
        {
            await GetUserByIdAsync();
            IsUserLoggedIn = _authService.IsUserLoggedIn;
        }

        private async Task GetUserByIdAsync()
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = true;

                // Read id from secure storage
                var userId = await SecureStorage.Default.GetAsync("userId");

                if (userId == null)
                {
                    Debug.WriteLine("User id not found in secure storage.");
                    return;
                }
                else
                {
                    var user = await _userService.GetUserAsync(int.Parse(userId));

                    if (user != null)
                    {
                        User = user;
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get user: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "Failed to retrieve user.", "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        /// <summary>
        /// Logs the user out.
        /// </summary>
        [RelayCommand]
        private async Task Logout()
        {
            // fire alert
            var result = await Shell.Current.DisplayAlert("Logout", "Are you sure you want to log out?", "Yes", "No");

            if (result)
            {
                _authService.IsUserLoggedIn = false;

                // Navigate to home page
                await Shell.Current.GoToAsync("//HomePage");
            }
        }

        /// <summary>
        /// Navigates to the login page.
        /// </summary>
        [RelayCommand]
        private async Task Login()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        /// <summary>
        /// Opens the FakeStoreAPI website in a browser.
        /// </summary>
        [RelayCommand]
        private static async Task OpenFakeStoreApi()
        {
            await Browser.OpenAsync("https://fakestoreapi.com/");
        }

        /// <summary>
        /// Opens the master thesis (full text in Croatian) in a browser.
        /// </summary>
        [RelayCommand]
        private static async Task OpenMasterThesis()
        {
            await Browser.OpenAsync("https://drive.google.com/file/d/19TEatq-Dr9WGvYuaFw2ARENorvGu_oyr/view?usp=sharing");
        }

        /// <summary>
        /// Deletes the user account.
        /// </summary>
        [RelayCommand]
        private async Task DeleteAccount()
        {
            // fire alert
            var result = await Shell.Current.DisplayAlert("Account Deletion", "Are you sure you want to delete your account?", "Yes", "No");

            if (result)
            {
                // Read id from secure storage
                var userId = await SecureStorage.Default.GetAsync("userId");

                if (userId == null)
                {
                    Debug.WriteLine("User id not found in secure storage.");
                    return;
                }
                else
                {
                    var isDeleted = await _userService.DeleteUserAccountAsync(int.Parse(userId));

                    if (isDeleted)
                    {
                        await Shell.Current.DisplayAlert("Info", "Account deleted successfully.", "OK");

                        // Navigate to home page
                        await Shell.Current.GoToAsync("//HomePage");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Error", "Failed to delete account.", "OK");
                    }
                }
            }
        }
    }
}
