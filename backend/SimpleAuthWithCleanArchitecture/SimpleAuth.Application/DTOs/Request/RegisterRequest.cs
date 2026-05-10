using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.DTOs.Request
{
    public class RegisterRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
