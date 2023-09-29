
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
using System.Diagnostics;

namespace MauiStoreApp.ViewModels
{
    public partial class LoginViewModel : BaseViewModel
    {
        private readonly AuthService _authService;

        public LoginViewModel(AuthService authService)
        {
            _authService = authService;
        }

        public LoginViewModel()
        {
        }

        [ObservableProperty]
        string username;

        [ObservableProperty]
        string password;

        LoginResponse loginResponse = new LoginResponse();

        [RelayCommand]
        public async Task Login()
        {
            if (IsBusy)
                return;

            try
            {
                IsBusy = true;
                loginResponse = await _authService.LoginAsync(Username, Password);

                if (loginResponse != null)
                {
                    Debug.WriteLine($"Login successful. Token: {loginResponse.Token}");

                    // save token to secure storage
                    await SecureStorage.Default.SetAsync("token", loginResponse.Token);

                    AuthService.IsUserLoggedIn = true;

                    var navigationStack = Shell.Current.Navigation.NavigationStack;

                    if (navigationStack.Count >= 2)
                    {
                        // go to previous page
                        await Shell.Current.Navigation.PopAsync();
                    }
                    else
                    {
                        // if there is no previous page, navigate to home page
                        await Shell.Current.GoToAsync("//HomePage");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
