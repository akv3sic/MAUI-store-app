// -----------------------------------------------------------------------
// <copyright file="LoginResponse.cs" company="Kvesic, Matkovic, FSRE">
// Copyright (c) Kvesic, Matkovic, FSRE. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------

using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents the response from a login attempt, including an authentication token and user ID.
    /// </summary>
    public class LoginResponse
    {
        /// <summary>
        /// Gets or sets the authentication token for the user.
        /// </summary>
        [JsonPropertyName("token")]
        public string Token { get; set; }

        /// <summary>
        /// Gets or sets the user ID.
        /// </summary>
        [JsonPropertyName("userId")]
        public int UserId { get; set; }
    }
}
