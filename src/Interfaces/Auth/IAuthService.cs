using UrlShortenerApi.Dios.Auth;
using UrlShortenerApi.Entities;

namespace UrlShortenerApi.Interfaces
{
    public interface IAuthService
    {
        Task<User> RegisterAsync(RegisterDtos dto);
        Task<string> LoginAsync(LoginDtos dto);
    }
}