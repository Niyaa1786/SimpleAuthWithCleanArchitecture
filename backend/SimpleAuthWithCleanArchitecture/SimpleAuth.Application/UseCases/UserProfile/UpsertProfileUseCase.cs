using FluentValidation;
using FluentValidation.Results;
using SimpleAuth.Application.DTOs.Request.UserProfile;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using SimpleAuth.Application.Interfaces.Common;
using SimpleAuth.Application.Interfaces.Shared;
using SimpleAuth.Application.Validator.UserProfileValidators;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.UseCases.UserProfile
{
    internal class UpsertProfileUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<UpsertProfileRequest> _validator;

        public UpsertProfileUseCase(IUnitOfWork unitOfWork, IValidator<UpsertProfileRequest> validator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
        }

        public async Task<UserProfileResponse> ExecuteAsync(UpsertProfileRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var existing = await _unitOfWork.UserProfiles.GetByUserIdAsync(request.UserId, ct);
            if (existing is null)
            {
                var newProfile = Mapper.UserProfileMapper.ToEntity(request);
                _unitOfWork.UserProfiles.Add(newProfile);
                await _unitOfWork.SaveChangesAsync(ct);
                return Mapper.UserProfileMapper.ToResponse(newProfile);
            }
            else
            {
                Mapper.UserProfileMapper.ApplyUpdate(existing, request);
                await _unitOfWork.SaveChangesAsync(ct);
                return Mapper.UserProfileMapper.ToResponse(existing);
            }
        }
    }
}
