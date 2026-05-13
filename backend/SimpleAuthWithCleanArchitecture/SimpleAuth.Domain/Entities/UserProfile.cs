using SimpleAuth.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace SimpleAuth.Domain.Entities
{
    public class UserProfile
    {
        public Guid Id { get; private set; }
        public Guid UserId { get; private set; }
        public string FirstName { get; private set; } = string.Empty;
        public string LastName { get; private set; } = string.Empty;
        public string PhoneNumber { get; private set; } = string.Empty ;
        public string AvatarUrl { get; private set; } = string.Empty;
        public Gender Gender { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        public User User { get; private set; } = null!;

        private UserProfile() { }
        
        public UserProfile(Guid userId)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string firstName, string lastName, string phoneNumber, string avatarUrl, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            AvatarUrl = avatarUrl;
            Gender = gender;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
