using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace MauiStoreApp.ViewModels
{
    /// <summary>
    /// Provides a base implementation for view models.
    /// </summary>
    public partial class BaseViewModel : ObservableObject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseViewModel"/> class.
        /// </summary>
        public BaseViewModel()
        {
        }

        private bool _isBusy;

        /// <summary>
        /// Gets or sets a value indicating whether the view model is busy performing an operation.
        /// </summary>
        public bool IsBusy
        {
            get => _isBusy;
            set => SetProperty(ref _isBusy, value);
        }

        /// <summary>
        /// Gets or sets the title of the page.
        /// </summary>
        [ObservableProperty]
        string title;

        /// <summary>
        /// Navigates back to the previous page in the navigation stack.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
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
