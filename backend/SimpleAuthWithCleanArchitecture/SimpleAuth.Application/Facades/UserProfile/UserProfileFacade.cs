using SimpleAuth.Application.DTOs.Request.Auth;
using SimpleAuth.Application.DTOs.Request.UserProfile;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using SimpleAuth.Application.UseCases.UserProfile;

namespace SimpleAuth.Application.Facades.UserProfile
{
    internal class UserProfileFacade : IUserProfileFacade
    {
        private readonly GetAllProfileUseCase _getAll;
        private readonly GetProfileUseCase _getProfile;
        private readonly UpsertProfileUseCase _upsertProfile;
        private readonly UploadAvatarUseCase _uploadAvatar;
        private readonly DeleteAvatarUseCase _deleteAvatar;

        public UserProfileFacade(
            GetAllProfileUseCase getAll,
            GetProfileUseCase getProfile,
            UpsertProfileUseCase upsertProfile,
            UploadAvatarUseCase uploadAvatar,
            DeleteAvatarUseCase deleteAvatar
            )
        {
            _getAll = getAll;
            _getProfile = getProfile;
            _upsertProfile = upsertProfile;
            _uploadAvatar = uploadAvatar;
            _deleteAvatar = deleteAvatar;
        }
        public async Task<IEnumerable<UserProfileResponse>> GetAll(CancellationToken ct = default)
            => await _getAll.ExecuteAsync(ct);

        public async Task<UserProfileResponse> GetProfile(Guid id, CancellationToken ct = default)
            => await _getProfile.ExecuteAsync(id, ct);

        public async Task<UserProfileResponse> UpsertProfile(UpsertProfileRequest request, CancellationToken ct = default)
            => await _upsertProfile.ExecuteAsync(request, ct);

        public async Task<UserProfileResponse> UploadAvatar(UploadAvatarRequest request, CancellationToken ct = default)
            => await _uploadAvatar.ExecuteAsync(request, ct);

        public async Task<bool> DeleteAvatar(Guid id, CancellationToken ct = default)
            => await _deleteAvatar.ExecuteAsync(id, ct);
    }
}
