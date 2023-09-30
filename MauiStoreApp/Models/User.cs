using System.Text.Json.Serialization;

namespace MauiStoreApp.Models
{
    public class User
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("email")]
        public string Email { get; set; }

        [JsonPropertyName("username")]
        public string Username { get; set; }

        [JsonPropertyName("password")]
        public string Password { get; set; }

        [JsonPropertyName("name")]
        public Name Name { get; set; }

        [JsonPropertyName("phone")]
        public string Phone { get; set; }

        [JsonPropertyName("address")]
        public Address Address { get; set; }

        public string FullName => $"{Name?.Firstname?.ToUpper()[0]}{Name?.Firstname?.ToLower()[1..]} {Name?.Lastname?.ToUpper()[0]}{Name?.Lastname?.ToLower()[1..]}";

        public string AvatarInitials => $"{Name?.Firstname?.ToUpper()[0]}{Name?.Lastname?.ToUpper()[0]}";
    }
}
