using SimpleAuth.Application.DTOs.Request.UserProfile;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Facades.UserProfile
{
    public interface IUserProfileFacade
    {
        public Task<IEnumerable<UserProfileResponse>> GetAll(CancellationToken ct = default);
        public Task<UserProfileResponse> GetProfile(Guid id, CancellationToken ct = default);
        public Task<UserProfileResponse> UpsertProfile(UpsertProfileRequest request, CancellationToken ct = default);
        public Task<UserProfileResponse> UploadAvatar(UploadAvatarRequest request, CancellationToken ct = default);
        public Task<bool> DeleteAvatar(Guid id, CancellationToken ct = default);
    }
}
