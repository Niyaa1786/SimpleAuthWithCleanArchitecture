using SimpleAuth.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.DTOs.Response.UserProfile
{
    public class UserProfileResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }
        public string? AvatarUrl { get; set; }
        public Gender Gender { get; set; }
    }
}
