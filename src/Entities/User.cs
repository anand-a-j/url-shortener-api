namespace UrlShortenerApi.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;
        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public ICollection<ShortLink> ShortLinks { get; set; } = new List<ShortLink>();
    }
}