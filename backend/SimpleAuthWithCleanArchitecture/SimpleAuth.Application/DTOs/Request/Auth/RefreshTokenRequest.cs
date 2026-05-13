using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.DTOs.Request.Auth
{
    public class RefreshTokenRequest
    {
        public string RefreshToken { get; set; } = string.Empty;
    }
}
