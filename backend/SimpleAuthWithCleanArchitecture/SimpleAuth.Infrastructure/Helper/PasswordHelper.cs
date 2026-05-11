using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Infrastructure.Helper
{
    public static class PasswordHelper
    {
        public static string Hash(string password) => BCrypt.Net.BCrypt.HashPassword(password);
        public static bool Verify(string password, string hashPassword) => BCrypt.Net.BCrypt.Verify(password, hashPassword);
    }
}
