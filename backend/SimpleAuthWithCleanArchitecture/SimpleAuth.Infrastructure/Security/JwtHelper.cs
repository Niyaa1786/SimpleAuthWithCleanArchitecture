using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SimpleAuth.Application.DTOs.Shared;
using SimpleAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace SimpleAuth.Infrastructure.Helper
{
    public static class JwtHelper
    {
        public static TokenResult GenerateToken(User user, IConfiguration configuration)
        {
            var accessExpiry = DateTime.UtcNow.AddMinutes(15);
            var RefreshExpiry = DateTime.UtcNow.AddDays(7);
            var accessToken = GenerateAccessToken(user, configuration, accessExpiry);
            var refreshToken = GenerateRefreshToken();

            return new TokenResult
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessExpiry,
                RefreshTokenExpiration = RefreshExpiry
            };
            
        }

        private static string GenerateAccessToken(User user, IConfiguration configuration, DateTime accessExpiry)
        {
            var jwtSettings = configuration.GetSection("Jwt");
            var secrectKey = jwtSettings.GetValue<string>("SecretKey") ?? throw new Exception("JWT SecretKey is not configured.");

            var claims = new Dictionary<string, object>
            {
                [ClaimTypes.NameIdentifier] = user.Id.ToString(),
                [ClaimTypes.Name] = user.Username,
                [ClaimTypes.Email] = user.Email,
                [ClaimTypes.Role] = user.Role.ToString()
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secrectKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = jwtSettings.GetValue<string>("Issuer"),
                Audience = jwtSettings.GetValue<string>("Audience"),
                Claims = claims,
                Expires = accessExpiry,
                SigningCredentials = creds
            };

            var handler = new JsonWebTokenHandler();
            var token = handler.CreateToken(tokenDescriptor);

            return token;
        }

        private static string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}
