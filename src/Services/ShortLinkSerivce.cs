using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data;
using UrlShortenerApi.Helpers;
using UrlShortenerApi.Interfaces;
using UrlShortenerApi.Links;

namespace UrlShortenerApi.Services
{
    public class ShortLinkService : IShortLinkService
    {
        private readonly AppDbContext _db;

        public ShortLinkService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<ShortLink> CreateShortLinkAsync(CreateShortLinkDto dto, int userId)
        {
            string code;

            do
            {
                code = CodeGenerator.GenerateShortCode();
            }
            while (await _db.ShortLinks.AnyAsync(x => x.Code == code));

            var link = new ShortLink
            {
                OriginalUrl = dto.OriginalUrl,
                Code = code,
                UserId = userId,
                CreatedAt = DateTime.UtcNow,
            };

            _db.ShortLinks.Add(link);
            await _db.SaveChangesAsync();

            return link;
        }

        public async Task<List<ShortLink>> GetUserLinksAsync(int userId)
        {
            return await _db.ShortLinks.Where(x => x.UserId == userId).OrderByDescending(x => x.CreatedAt).ToListAsync();
        }

        public async Task<string> ResolveUrlAsync(string code)
        {
           var link = await _db.ShortLinks.FirstOrDefaultAsync(x=> x.Code == code);

           if(link == null) 
            throw new Exception("invaild shortcode");

            link.ClickCount++;
            await _db.SaveChangesAsync();

            return link.OriginalUrl;
        }
    }
}