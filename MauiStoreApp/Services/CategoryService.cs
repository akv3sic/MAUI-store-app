// -----------------------------------------------------------------------
// <copyright file="CategoryService.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using MauiStoreApp.Models;

namespace MauiStoreApp.Services
{
    /// <summary>
    /// Provides services for managing and retrieving product categories.
    /// </summary>
    public class CategoryService : BaseService
    {
        /// <summary>
        /// Asynchronously retrieves a collection of all available product categories.
        /// </summary>
        /// <returns>
        /// A task that represents the asynchronous operation.
        /// The task result contains an enumerable collection of <see cref="Category"/> objects.
        /// </returns>
        public async Task<IEnumerable<Category>> GetCategoriesAsync()
        {
            var categories = await GetAsync<IEnumerable<string>>("products/categories");

            // Maps each category name to a Category object.
            var categoryList = new List<Category>();
            foreach (var category in categories)
            {
                categoryList.Add(new Category { Name = category });
            }

            return categoryList;
        }
    }
}
