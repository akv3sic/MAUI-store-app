using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents a product in the shopping cart, including the product ID and quantity.
    /// </summary>
    public class CartProduct
    {
        /// <summary>
        /// Gets or sets the ID of the product.
        /// </summary>
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        /// <summary>
        /// Gets or sets the quantity of the product in the cart.
        /// </summary>
        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
