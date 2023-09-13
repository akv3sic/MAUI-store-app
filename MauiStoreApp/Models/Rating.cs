using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    public class Rating
    {
        [JsonPropertyName("rate")]
        public double Rate { get; set; }

        [JsonPropertyName("count")]
        public int Count { get; set; }
    }
}
