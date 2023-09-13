using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    public class Cart
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userId")]
        public int UserId { get; set; }

        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("products")]
        public List<CartProduct> Products { get; set; }

        [JsonPropertyName("__v")]
        public int Version { get; set; }
    }
}
