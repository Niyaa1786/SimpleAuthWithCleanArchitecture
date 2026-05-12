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
        public static AuthResponse ToAuthResponse(User user, TokenResult tokenResult)
        {
            return new AuthResponse
            {
                AccessToken = tokenResult.AccessToken,
                RefreshToken = tokenResult.RefreshToken,
                AccessTokenExpiration = tokenResult.AccessTokenExpiration,
                RefreshTokenExpiration = tokenResult.RefreshTokenExpiration,
                User = new UserDto
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Role = user.Role
                }

            };
        }
        public static User ToUserEntity(RegisterRequest request, string passwordHash)
        {
            return new User(request.Username, request.Email, passwordHash, UserRole.User);
        }
        public static User ToAdminEntity(RegisterRequest request)
        {
            return new User(request.Username, request.Email, request.Password, UserRole.Admin);
        }
        
    }
}
