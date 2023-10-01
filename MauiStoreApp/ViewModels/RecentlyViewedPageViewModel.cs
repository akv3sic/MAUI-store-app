using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Views;
using Newtonsoft.Json;
using System.Collections.ObjectModel;

namespace MauiStoreApp.ViewModels
{
    public partial class RecentlyViewedPageViewModel : BaseViewModel
    {
        public ObservableCollection<Product> RecentlyViewedProducts { get; private set; } = new ObservableCollection<Product>();

        public RecentlyViewedPageViewModel()
        {
        }

        [ObservableProperty]
        public bool isPageEmpty;

        [RelayCommand]
        public async Task Init()
        {
            LoadRecentlyViewedProducts();
            IsPageEmpty = RecentlyViewedProducts.Count == 0;

            await Task.CompletedTask;
        }

        private void LoadRecentlyViewedProducts()
        {
            var productsJson = Preferences.Get("recently_viewed", string.Empty);
            if (!string.IsNullOrEmpty(productsJson))
            {
                var products = JsonConvert.DeserializeObject<ObservableCollection<Product>>(productsJson);
                RecentlyViewedProducts.Clear();
                foreach (var product in products)
                {
                    RecentlyViewedProducts.Add(product);
                }
            }
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
            var result = await Shell.Current.DisplayAlert("Brisanje", "Sigurno želite izbrisati sve nedano gledane proizvode?", "Da", "Ne");

            if (!result)
                return; // exit if user cancels

            Preferences.Remove("recently_viewed");
            RecentlyViewedProducts.Clear();
            IsPageEmpty = true;

            await Task.CompletedTask;
        }
    }
}
