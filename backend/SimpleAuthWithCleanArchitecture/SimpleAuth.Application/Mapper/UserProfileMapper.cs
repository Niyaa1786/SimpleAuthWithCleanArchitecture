using SimpleAuth.Application.DTOs.Request.UserProfile;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using SimpleAuth.Domain.Entities;
using SimpleAuth.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Mapper
{
    public static class UserProfileMapper
    {
        public static UserProfileResponse ToResponse(UserProfile userProfile)
        {
            return new UserProfileResponse
            {
                FirstName = userProfile.FirstName,
                LastName = userProfile.LastName,
                PhoneNumber = userProfile.PhoneNumber,
                Gender = userProfile.Gender
            };
        }

        public static void ApplyUpdate(UserProfile userProfile, UpsertProfileRequest request)
        {
            userProfile.UpdateProfile(request.FirstName, request.LastName, request.PhoneNumber, (Gender)Enum.Parse(typeof(Gender), request.Gender));
        }
    }
}
