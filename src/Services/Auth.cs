using System.Diagnostics.Tracing;
using Microsoft.EntityFrameworkCore;
using UrlShortenerApi.Data;
using UrlShortenerApi.DTOs.Auth;
using UrlShortenerApi.Entities;
using UrlShortenerApi.Helpers;
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

        public async Task<string> LoginAsync(LoginDtos dto)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Email == dto.Email);

            if(user == null) 
             throw new Exception("User doesn't exist or invaild email");

            bool vaildPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);

            if(!vaildPassword)
             throw new Exception("Invaild email or password");

             return _jwt.GenerateToken(user);
        }
    }
}