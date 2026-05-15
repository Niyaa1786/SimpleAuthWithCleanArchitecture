using FluentValidation;
using FluentValidation.Results;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using SimpleAuth.Application.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.UseCases.UserProfile
{
    internal class GetProfileUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetProfileUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserProfileResponse> ExecuteAsync(Guid userId, CancellationToken ct = default)
        {
            var profile = await _unitOfWork.UserProfiles.GetByUserIdAsync(userId, ct);
            if (profile is null)
                throw new ValidationException(new[] { new ValidationFailure("UserId", "Profile not found") });

            return Mapper.UserProfileMapper.ToResponse(profile);
        }
    }
}
