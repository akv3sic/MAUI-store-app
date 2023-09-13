using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    public class Geolocation
    {
        [JsonPropertyName("lat")]
        public string Lat { get; set; }

        [JsonPropertyName("long")]
        public string Long { get; set; }
    }
}
