using SimpleAuth.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Reflection;
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
        public string AvatarPublicId { get; private set; } = string.Empty;
        public Gender Gender { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdateAt { get; private set; }

        public User User { get; private set; } = null!;

        private UserProfile() { }

        public UserProfile(Guid userId, string firstName = null!, string lastName = null!, string phoneNumber = null!, Gender gender = Gender.Unknown, string avatarUrl = null!, string avatarPublicId = null!)
        {
            Id = Guid.NewGuid();
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            AvatarUrl = avatarUrl;
            AvatarPublicId = avatarPublicId;
            Gender = gender;
            CreatedAt = DateTime.UtcNow;
        }

        public void UpdateProfile(string firstName, string lastName, string phoneNumber, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            PhoneNumber = phoneNumber;
            Gender = gender;
            UpdateAt = DateTime.UtcNow;
        }

        public void SetAvatar(string avatarUrl, string avatarPublicId)
        {
            AvatarUrl = avatarUrl;
            AvatarPublicId = avatarPublicId;
            UpdateAt = DateTime.UtcNow;
        }

        public void RemoveAvatar()
        {
            AvatarUrl = null!;
            AvatarPublicId = null!;
            UpdateAt = DateTime.UtcNow;
        }
    }
}
