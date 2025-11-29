using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.Interfaces;
using UrlShortenerApi.Links;

namespace UrlShortenerApi.Controllers
{
    [ApiController]
    [Route("api/links")]
    [Authorize]
    public class ShortLinksController : ControllerBase
    {
        private readonly IShortLinkService _shortLinks;

        public ShortLinksController(IShortLinkService links)
        {
            _shortLinks = links;
        }

        [HttpPost]
        public async Task<IActionResult> Create (CreateShortLinkDto dto)
        {
            int userId = int.Parse(User.FindFirst("sub")!.Value);

            var link = await _shortLinks.CreateShortLinkAsync(dto,userId);

            return Ok(new
            {
                link.Id,
                link.Code,
                link.OriginalUrl
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserLinks()
        {
            int userId = int.Parse(User.FindFirst("sub")!.Value);

            var links = await _shortLinks.GetUserLinksAsync(userId);

            return Ok(links);
        }
    }
}