using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.Interfaces;

namespace UrlShortenerApi.Controllers
{
    [ApiController]
    public class RedirectController : ControllerBase
    {
        private readonly IShortLinkService _shortLinks;

        public RedirectController(IShortLinkService shortLinks)
        {
            _shortLinks = shortLinks;
        }

        [HttpGet("r/{code}")]
        public async Task<IActionResult> RedirectTo(string code)
        {
            var url = await _shortLinks.ResolveUrlAsync(code);
            return Redirect(url);
        }
    }
}