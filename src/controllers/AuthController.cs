using Microsoft.AspNetCore.Mvc;
using UrlShortenerApi.DTOs.Auth;
using UrlShortenerApi.Interfaces;

namespace UrlShortenerApi.Controllers
{
    [ApiController]
    [Route("api/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _auth;

        public AuthController(IAuthService auth)
        {
            _auth = auth;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDtos dto)
        {
            var user = await _auth.RegisterAsync(dto);
            return Ok(new { user.Id, user.Username, user.Email });
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDtos dto)
        {
            var token = await _auth.LoginAsync(dto);
            return Ok(new { token });
        }
    }
}
