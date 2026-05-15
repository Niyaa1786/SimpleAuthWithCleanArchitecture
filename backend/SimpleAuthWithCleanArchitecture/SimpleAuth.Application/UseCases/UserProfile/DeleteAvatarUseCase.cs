using FluentValidation;
using FluentValidation.Results;
using SimpleAuth.Application.DTOs.Request.UserProfile;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using SimpleAuth.Application.Interfaces.Common;
using SimpleAuth.Application.Interfaces.Shared;
using SimpleAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.UseCases.UserProfile
{
    internal class DeleteAvatarUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICloudinaryService _cloudinaryService;

        public DeleteAvatarUseCase(IUnitOfWork unitOfWork, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _cloudinaryService = cloudinaryService;
        }

        public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
        {
            var userProfile = await _unitOfWork.UserProfiles.GetByUserIdAsync(id, ct);
            if (userProfile is null)
                throw new ValidationException(new[] { new ValidationFailure("UserId", "Profile not found") });

            if (!string.IsNullOrEmpty(userProfile.AvatarPublicId))
            {
                var deleted = await _cloudinaryService.DeleteAsync(userProfile.AvatarPublicId, ct);
                if (!deleted)
                {
                    throw new Exception("Failed to delete avatar from Cloudinary");
                }
            }
            userProfile.RemoveAvatar();
            await _unitOfWork.SaveChangesAsync(ct);

            return true;
        }
    }
}
