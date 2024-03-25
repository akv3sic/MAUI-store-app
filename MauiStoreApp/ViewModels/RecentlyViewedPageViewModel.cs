// -----------------------------------------------------------------------
// <copyright file="RecentlyViewedPageViewModel.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
using MauiStoreApp.Views;

namespace MauiStoreApp.ViewModels
{
    /// <summary>
    /// Represents the view model for the recently viewed products page.
    /// </summary>
    public partial class RecentlyViewedPageViewModel : BaseViewModel
    {
        /// <summary>
        /// Gets or sets the recently viewed products service.
        /// </summary>
        [ObservableProperty]
        public RecentlyViewedProductsService recentlyViewedProductsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentlyViewedPageViewModel"/> class.
        /// </summary>
        /// <param name="recentlyViewedProductsService">The recently viewed products service.</param>
        public RecentlyViewedPageViewModel(RecentlyViewedProductsService recentlyViewedProductsService)
        {
            RecentlyViewedProductsService = recentlyViewedProductsService;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RecentlyViewedPageViewModel"/> class. This empty constructor is used for design-time data.
        /// </summary>
        public RecentlyViewedPageViewModel()
        {
        }

        /// <summary>
        /// Initializes the view model.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [RelayCommand]
        public async Task Init()
        {
            RecentlyViewedProductsService.LoadProducts();

            await Task.CompletedTask;
        }

        /// <summary>
        /// Handles the product tapped event.
        /// </summary>
        /// <param name="product">The tapped product.</param>
        [RelayCommand]
        private async Task ProductTapped(Product product)
        {
            IsBusy = true;

            if (product == null)
            {
                return;
            }

            var navigationParameter = new Dictionary<string, object>
            {
                { "Product", product },
            };

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", true, navigationParameter);

            IsBusy = false;
        }

        /// <summary>
        /// Navigates to the home page.
        /// </summary>
        [RelayCommand]
        private async Task GoToHome()
        {
            await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
        }

        /// <summary>
        /// Deletes all recently viewed products.
        /// </summary>
        [RelayCommand]
        private async Task DeleteAll()
        {
            // fire alert to confirm
            var result = await Shell.Current.DisplayAlert("Delete All", "Are you sure you want to delete all recently viewed products?", "Yes", "No");

            if (!result)
            {
                return; // Exit if user cancels
            }

            RecentlyViewedProductsService.RecentlyViewedProducts.Clear();
            RecentlyViewedProductsService.SaveProducts();

            await Task.CompletedTask;
        }
    }
}
