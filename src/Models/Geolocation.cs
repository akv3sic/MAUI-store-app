using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents a geographical location with latitude and longitude.
    /// </summary>
    public class Geolocation
    {
        /// <summary>
        /// Gets or sets the latitude of the geographical location.
        /// </summary>
        [JsonPropertyName("lat")]
        public string Lat { get; set; }

        /// <summary>
        /// Gets or sets the longitude of the geographical location.
        /// </summary>
        [JsonPropertyName("long")]
        public string Long { get; set; }
    }
}
