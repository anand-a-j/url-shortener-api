using UrlShortenerApi.DTOs.Auth;
using UrlShortenerApi.Entities;

namespace UrlShortenerApi.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDto dto);
        Task<string> LoginAsync(LoginDto dto);
    }
}