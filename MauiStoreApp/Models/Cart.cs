using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents a shopping cart.
    /// </summary>
    public class Cart
    {
        /// <summary>
        /// Gets or sets the unique identifier of the cart.
        /// </summary>
        [JsonPropertyName("id")]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the user to whom the cart belongs.
        /// </summary>
        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the date the cart was created or last updated.
        /// </summary>
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the list of products in the cart.
        /// </summary>
        [JsonPropertyName("products")]
        public List<CartProduct> Products { get; set; }

        /// <summary>
        /// Gets or sets the version of the cart.
        /// </summary>
        [JsonPropertyName("__v")]
        public int Version { get; set; }
    }
}
