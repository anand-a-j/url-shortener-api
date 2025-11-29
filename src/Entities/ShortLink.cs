using UrlShortenerApi.Entities;

public class ShortLink
{
    public int Id { get; set; }
    public string Code { get; set; } = null!;
    public string OriginalUrl { get; set; } = null!;
    public int ClickCount { get; set; } = 0;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public int UserId { get; set; }
    public User? user { get; set; }
}