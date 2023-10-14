using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiStoreApp.ViewModels
{
    public partial class BaseViewModel : ObservableObject
    {
        public BaseViewModel()
        {
        }

        private bool _isBusy;
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        [ObservableProperty]
        string title;

        [RelayCommand]
        public async Task GoBack()
        {
            await Shell.Current.GoToAsync("..", true);
        }

        /// <summary>
        /// Navigates to the specified page using shell navigation.
        /// </summary>
        /// <param name="pageName">The name of the page to navigate to.</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        [RelayCommand]
        private async Task GoToRoute(string pageName)
        {
            await Shell.Current.GoToAsync($"//{pageName}");
        }
    }
}