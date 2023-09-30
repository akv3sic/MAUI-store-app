
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
using MauiStoreApp.Views;
using System.Diagnostics;

namespace MauiStoreApp.ViewModels
{
    public partial class ProfilePageViewModel : BaseViewModel
    {
        private readonly UserService _userService;

        public ProfilePageViewModel(UserService userService)
        {
            _userService = userService;
        }

        public ProfilePageViewModel()
        {
        }

        [ObservableProperty]
        public bool isUserLoggedIn;

        [ObservableProperty]
        User user;

        [RelayCommand]
        public async Task Init()
        {
            await GetUserByIdAsync();
            IsUserLoggedIn = AuthService.IsUserLoggedIn;
        }

        private async Task GetUserByIdAsync()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;

                // read id from secure storage
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
                await Shell.Current.DisplayAlert("Greška pri dohvaćanu podataka sa servera!", ex.Message, "U redu");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private static async Task Logout()
        {
           // fire alert
           var result = await Shell.Current.DisplayAlert("Odjava", "Jeste li sigurni da se želite odjaviti?", "Da", "Ne");

            if (result)
            {
                AuthService.IsUserLoggedIn = false;

                // navigate to home page
                await Shell.Current.GoToAsync("//HomePage");
            }
        }

        [RelayCommand]
        private async Task Login()
        {
            await Shell.Current.GoToAsync($"{nameof(LoginPage)}");
        }

        [RelayCommand]
        private static async Task OpenFakeStoreApi()
        {
            await Browser.OpenAsync("https://fakestoreapi.com/");
        }
    }
}
