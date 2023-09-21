using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    public class LoginResponse
    {
        [JsonPropertyName("token")]
        public string Token { get; set; }
    }
}
