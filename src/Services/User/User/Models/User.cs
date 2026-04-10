namespace Users.API.Models
{
    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; } = default!;
        public string PasswordHash { get; set; } = default!;
        public string Role { get; set; } = default!;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    }
}
