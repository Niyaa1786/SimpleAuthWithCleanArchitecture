using FluentValidation;
using FluentValidation.Results;
using SimpleAuth.Application.DTOs.Request.UserProfile;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using SimpleAuth.Application.Interfaces.Common;
using SimpleAuth.Application.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using ValidationException = FluentValidation.ValidationException;

namespace SimpleAuth.Application.UseCases.UserProfile
{
    internal class UploadAvatarUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UploadAvatarRequest> _validator;
        private readonly ICloudinaryService _cloudinaryService;

        public UploadAvatarUseCase(IUnitOfWork unitOfWork,IValidator<UploadAvatarRequest> validator, ICloudinaryService cloudinaryService)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _cloudinaryService = cloudinaryService;
        }

        public async Task <UserProfileResponse> ExecuteAsync(UploadAvatarRequest request,CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var userProfile = await _unitOfWork.UserProfiles.GetByUserIdAsync(request.UserId, ct);
            if (userProfile is null)
                throw new ValidationException(new[] { new ValidationFailure("UserId", "Profile not found")});

            var uploadResult = await _cloudinaryService.UploadAsync(request.FileStream, request.FileName, request.ContentType, ct: ct);
            userProfile.SetAvatar(uploadResult.Url!, uploadResult.PublicId!);
            await _unitOfWork.SaveChangesAsync(ct);

            return Mapper.UserProfileMapper.ToResponse(userProfile);
        }
    }
}
