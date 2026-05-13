using SimpleAuth.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Domain.Entities
{ 
    public class User
    {
        public Guid Id { get; private set;  }
        public string Username { get; private set; } = string.Empty;
        public string Email { get; private set; } = string.Empty;
        public string PasswordHash { get; private set; } = string.Empty;
        public UserRole Role { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public string? RefreshToken { get; private set; }
        public DateTime RefreshTokenExpiry { get; private set; }

        public UserProfile Profile { get; private set; } = null!;

        private User() { }
        
        public User(string username, string email, string passwordHash, UserRole role = UserRole.User)
        {
            Id = Guid.NewGuid();
            Username = username;
            Email = email;
            PasswordHash = passwordHash;
            Role = role;
            CreatedAt = DateTime.UtcNow;
        }

        public void SetRefreshToken(string refreshToken, DateTime expiry)
        {
            RefreshToken = refreshToken;
            RefreshTokenExpiry = expiry;
        }

        public void RevokeRefreshToken()
        {
            RefreshToken = null;
            RefreshTokenExpiry = DateTime.MinValue;
        }
    }
}
