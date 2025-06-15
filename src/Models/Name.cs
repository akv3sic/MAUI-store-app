using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    /// <summary>
    /// Represents a person's first and last name.
    /// </summary>
    public class Name
    {
        /// <summary>
        /// Gets or sets the first name of the person.
        /// </summary>
        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        /// <summary>
        /// Gets or sets the last name of the person.
        /// </summary>
        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }
    }
}
