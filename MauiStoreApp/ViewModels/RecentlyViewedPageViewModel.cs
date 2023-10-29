using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Views;
using MauiStoreApp.Services;

namespace MauiStoreApp.ViewModels
{
    public partial class RecentlyViewedPageViewModel : BaseViewModel
    {
        [ObservableProperty]
        public RecentlyViewedProductsService recentlyViewedProductsService;

        public RecentlyViewedPageViewModel(RecentlyViewedProductsService recentlyViewedProductsService)
        {
            RecentlyViewedProductsService = recentlyViewedProductsService;
        }

        public RecentlyViewedPageViewModel()
        {
        }

        [RelayCommand]
        public async Task Init()
        {
            RecentlyViewedProductsService.LoadProducts();

            await Task.CompletedTask;
        }

        [RelayCommand]
        private async Task ProductTapped(Product product)
        {
            IsBusy = true;

            if (product == null)
                return;

            var navigationParameter = new Dictionary<string, object>
            {
                { "Product", product }
            };

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", true, navigationParameter);

            IsBusy = false;
        }

        [RelayCommand]
        private async Task GoToHome()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        [RelayCommand]
        private async Task DeleteAll()
        {
            // fire alert to confirm
            var result = await Shell.Current.DisplayAlert("Brisanje", "Sigurno želite izbrisati sve nedavno gledane proizvode?", "Da", "Ne");

            if (!result)
                return; // exit if user cancels

            RecentlyViewedProductsService.RecentlyViewedProducts.Clear();
            RecentlyViewedProductsService.SaveProducts();

            await Task.CompletedTask;
        }
    }
}
