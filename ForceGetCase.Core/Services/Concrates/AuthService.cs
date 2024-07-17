using ForceGetCase.Core.Models.User;
using ForceGetCase.Core.Services.Abstracts;
using ForceGetCase.DataAccess.Context;
using ForceGetCase.Shared.Helper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ForceGetCase.Core.Services.Concrates
{
    public class AuthService : IAuthService
    {
        private readonly ForceGetDbContext _context;
        private readonly AuthSettings _authSettings;

        public AuthService(ForceGetDbContext context, IOptions<AuthSettings> authSettings)
        {
            _context = context;
            _authSettings = authSettings.Value;
        }

        public async Task<string> AuthenticateAsync(string username, string password)
        {
            // Kullanıcıyı veritabanında bul
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                // Kullanıcı bulunamadı
                return "User not found";
            }

            // Şifre doğrulama
            if (!PasswordHelper.VerifyPassword(password, user.PasswordHash))
            {
                // Şifre yanlış
                return "Invalid password";
            }

            // Token oluştur
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authSettings.Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
            new Claim(ClaimTypes.Name, user.Username)
                }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _authSettings.Issuer,
                Audience = _authSettings.Issuer
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

    }
}