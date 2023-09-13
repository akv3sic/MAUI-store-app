using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    public class Name
    {
        [JsonPropertyName("firstname")]
        public string Firstname { get; set; }

        [JsonPropertyName("lastname")]
        public string Lastname { get; set; }
    }
}
