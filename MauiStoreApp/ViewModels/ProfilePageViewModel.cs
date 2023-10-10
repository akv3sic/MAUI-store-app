
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
        private readonly AuthService _authService;

        public ProfilePageViewModel(UserService userService, AuthService authService)
        {
            _userService = userService;
            _authService = authService;
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
            IsUserLoggedIn = _authService.IsUserLoggedIn;
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
        private async Task Logout()
        {
           // fire alert
           var result = await Shell.Current.DisplayAlert("Odjava", "Jeste li sigurni da se želite odjaviti?", "Da", "Ne");

            if (result)
            {
                _authService.IsUserLoggedIn = false;

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

        [RelayCommand]
        private async Task DeleteAccount()
        {
            // fire alert
            var result = await Shell.Current.DisplayAlert("Brisanje računa", "Jeste li sigurni da želite izbrisati račun?", "Da", "Ne");

            if (result)
            {
                // read id from secure storage
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
                        await Shell.Current.DisplayAlert("Obavijest", "Račun je uspješno izbrisan.", "U redu");

                        // navigate to home page
                        await Shell.Current.GoToAsync("//HomePage");
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Greška", "Greška prilikom brisanja računa.", "U redu");
                    }
                }
            }
        }
    }
}
