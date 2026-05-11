using SimpleAuth.Application.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.DTOs.Response
{
    public class AuthResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public DateTime AccessTokenExpiration { get; set; }
        public DateTime RefreshTokenExpiration { get; set; }

        public UserDto User { get; set; } = null!;
    }
}
