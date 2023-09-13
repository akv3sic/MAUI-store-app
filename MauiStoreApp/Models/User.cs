namespace MauiStoreApp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public Name Name { get; set; }
        public string Phone { get; set; }
        public Address Address { get; set; }
    }
}
