using FluentValidation;
using FluentValidation.Results;
using SimpleAuth.Application.DTOs.Response.UserProfile;
using SimpleAuth.Application.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.UseCases.UserProfile
{
    internal class GetAllProfileUseCase
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAllProfileUseCase(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<UserProfileResponse>> ExecuteAsync(CancellationToken ct = default)
        {
            var profile = await _unitOfWork.UserProfiles.GetAllAsync(ct);
            return profile.Select(p => Mapper.UserProfileMapper.ToResponse(p));
        }
    }
}
