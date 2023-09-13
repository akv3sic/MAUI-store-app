using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    public class CartProduct
    {
        [JsonPropertyName("productId")]
        public int ProductId { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }
    }
}
