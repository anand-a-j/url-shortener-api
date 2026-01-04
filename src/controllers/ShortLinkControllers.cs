using System.Security.Claims;
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
        public async Task<IActionResult> Create(CreateShortLinkDto dto)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("User not authenticated");

            int userId = int.Parse(userIdClaim.Value);

            var link = await _shortLinks.CreateShortLinkAsync(dto, userId);

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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null)
                return Unauthorized("User not authenticated");

            int userId = int.Parse(userIdClaim.Value);

            var links = await _shortLinks.GetUserLinksAsync(userId);

            return Ok(links);
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if(userIdClaim == null)
               return Unauthorized("User not authenticated");

            int userId = int.Parse(userIdClaim.Value);

            var deleted = await _shortLinks.DeleteShortLinkAsync(id, userId);

            if(!deleted)
              return NotFound("Link not found or you don't have access");

            return NoContent();
        }
    }
}