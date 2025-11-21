using System.Diagnostics.Tracing;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data;
using UrlShortenerApi.Dios.Auth;
using UrlShortenerApi.Entities;
using UrlShortenerApi.Interfaces;

namespace UrlShortenerApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;
        private readonly JwtHelper _jwt;

        public AuthService(AppDbContext db, JwtHelper jwt)
        {
            _db = db;
            _jwt = jwt;
        }

        public async Task<User> RegisterAsync(RegisterDtos dto)
        {
            var exists = await _db.Users.AnyAsync(x => x.Email == dto.Email);

            if(exists) throw new Exception("Email Already registered!!!");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password)
            };


            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            return user;
        }
    }
}