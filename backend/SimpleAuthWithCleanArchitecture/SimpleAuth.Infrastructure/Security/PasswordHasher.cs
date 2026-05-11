using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Client;
using SimpleAuth.Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Infrastructure.Security
{
    public class PasswordHasher : IPasswordHasher
    {
        public string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public bool Verify(string password, string hashPassword) => BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}
