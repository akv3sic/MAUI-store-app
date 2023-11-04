// -----------------------------------------------------------------------
// <copyright file="Rating.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents the rating of a product.
    /// </summary>
    public class Rating
    {
        /// <summary>
        /// Gets or sets the rate value of the product.
        /// </summary>
        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        /// <summary>
        /// Gets or sets the number of ratings for the product.
        /// </summary>
        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
