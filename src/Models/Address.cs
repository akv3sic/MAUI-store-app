using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents an address.
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets the geolocation of the address.
        /// </summary>
        [JsonPropertyName("geolocation")]
        public Geolocation Geolocation { get; set; }

        /// <summary>
        /// Gets or sets the city of the address.
        /// </summary>
        [JsonPropertyName("city")]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the street of the address.
        /// </summary>
        [JsonPropertyName("street")]
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets the number of the address.
        /// </summary>
        [JsonPropertyName("number")]
        public int Number { get; set; }

        /// <summary>
        /// Gets or sets the zipcode of the address.
        /// </summary>
        [JsonPropertyName("zipcode")]
        public string Zipcode { get; set; }

        /// <summary>
        /// Gets the city with the first letter capitalized.
        /// </summary>
        public string CityCapitalized => $"{City?.ToUpper()[0]}{City?.ToLower()[1..]}";

        /// <summary>
        /// Gets the city and zipcode concatenated.
        /// </summary>
        public string CityAndZipcode => $"{CityCapitalized} {Zipcode}";

        /// <summary>
        /// Gets the full street with the first letter capitalized followed by the street number.
        /// </summary>
        public string FullStreet => $"{Street?.ToUpper()[0]}{Street?.ToLower()[1..]} {Number}";
    }
}
