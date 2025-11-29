using UrlShortenerApi.Links;

namespace UrlShortenerApi.Interfaces
{
    public interface IShortLinkService
    {
        Task<ShortLink> CreateShortLinkAsync(CreateShortLinkDto dto, int userId);
        Task<List<ShortLink>> GetUserLinksAsync(int userId);
        Task<string> ResolveUrlAsync(string code);
    }
}