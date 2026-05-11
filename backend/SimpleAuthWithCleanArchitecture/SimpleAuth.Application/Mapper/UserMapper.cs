using Riok.Mapperly.Abstractions;
using SimpleAuth.Application.DTOs.Request;
using SimpleAuth.Application.DTOs.Response;
using SimpleAuth.Application.DTOs.Shared;
using SimpleAuth.Domain.Entities;
using SimpleAuth.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Mapper
{
    [Mapper]
    internal static partial class UserMapper
    {
        public static AuthResponse ToAuthResponse(User user, string accessToken, string refreshToken, DateTime accessExpiry, DateTime refreshExpiry)
        {
            return new AuthResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                AccessTokenExpiration = accessExpiry,
                RefreshTokenExpiration = refreshExpiry,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role
                }

            };
        }
        public static User ToUserEntity(RegisterRequest request)
        {
            return new User(request.Username, request.Email, request.Password, UserRole.User);
        }
        public static User ToAdminEntity(RegisterRequest request)
        {
            return new User(request.Username, request.Email, request.Password, UserRole.Admin);
        }
        
    }
}
