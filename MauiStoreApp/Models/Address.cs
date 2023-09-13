using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    public class Address
    {
        [JsonPropertyName("geolocation")]
        public Geolocation Geolocation { get; set; }

        [JsonPropertyName("city")]
        public string City { get; set; }

        [JsonPropertyName("street")]
        public string Street { get; set; }

        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }
    }
}
