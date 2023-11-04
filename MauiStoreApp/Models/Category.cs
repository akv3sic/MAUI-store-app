﻿// -----------------------------------------------------------------------
// <copyright file="Category.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents a product category with a name and associated image.
    /// </summary>
    public class Category
    {
        /// <summary>
        /// Gets or sets the name of the category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the filename of the image associated with the category.
        /// </summary>
        public string ImageName
        {
            get
            {
                if (string.IsNullOrWhiteSpace(Name))
                {
                    return null;
                }

                string formattedName = Name.Replace(" ", "_").Replace("'", "").ToLower();
                return formattedName + ".jpg";
            }
        }
    }
}
