// -----------------------------------------------------------------------
// <copyright file="CategoryPageViewModel.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Collections.ObjectModel;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiStoreApp.Models;
using MauiStoreApp.Services;
using MauiStoreApp.Views;

namespace MauiStoreApp.ViewModels
{
    /// <summary>
    /// Represents the view model for the category page.
    /// </summary>
    [QueryProperty(nameof(Category), "Category")]
    public partial class CategoryPageViewModel : BaseViewModel
    {
        private readonly ProductService _productService;
        private readonly RecentlyViewedProductsService _recentlyViewedProductsService;

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryPageViewModel"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        /// <param name="recentlyViewedProductsService">The recently viewed products service.</param>
        public CategoryPageViewModel(ProductService productService, RecentlyViewedProductsService recentlyViewedProductsService)
        {
            _productService = productService;
            _recentlyViewedProductsService = recentlyViewedProductsService;
        }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [ObservableProperty]
        Category category;

        /// <summary>
        /// Gets or sets the sort order.
        /// </summary>
        [ObservableProperty]
        string sortOrder = "asc";

        /// <summary>
        /// Gets or sets a value indicating whether the view model is busy with sorting.
        /// </summary>
        [ObservableProperty]
        bool isBusyWithSorting = false;

        /// <summary>
        /// Gets the products.
        /// </summary>
        public ObservableCollection<Product> Products { get; private set; } = new ObservableCollection<Product>();

        /// <summary>
        /// Initializes the category page view model.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        [RelayCommand]
        public async Task Init()
        {
            await GetProductsByCategoryAsync();
        }

        /// <summary>
        /// Gets the products by category.
        /// </summary>
        /// <param name="sortOrder">The sort order. Can be "asc" or "desc". Defaults to "asc".</param>
        private async Task GetProductsByCategoryAsync(string sortOrder = "asc")
        {
            if (IsBusy)
            {
                return;
            }

            try
            {
                IsBusy = !IsBusyWithSorting;

                var products = await _productService.GetProductsByCategoryAsync(Category.Name, sortOrder);
                Products.Clear();
                foreach (var product in products)
                {
                    Products.Add(product);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get products: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
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

            _recentlyViewedProductsService.AddProduct(product);

            await Shell.Current.GoToAsync($"{nameof(ProductDetailsPage)}", true, navigationParameter);

            IsBusy = false;
        }

        /// <summary>
        /// Sorts the products.
        /// </summary>
        [RelayCommand]
        private async Task SortProducts()
        {
            if (IsBusyWithSorting)
            {
                return;
            }

            IsBusyWithSorting = true;

            SortOrder = SortOrder == "asc" ? "desc" : "asc";
            await GetProductsByCategoryAsync(SortOrder);

            IsBusyWithSorting = false;
        }
    }
}
