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

        [RelayCommand]
        public async Task Init()
        {
            LoadRecentlyViewedProducts();

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
    }
}
