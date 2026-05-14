using FluentValidation;
using FluentValidation.Results;
using SimpleAuth.Application.DTOs.Request.Auth;
using SimpleAuth.Application.DTOs.Response.Auth;
using SimpleAuth.Application.Interfaces.Common;
using SimpleAuth.Application.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.UseCases.Auth
{
    internal class RefreshTokenUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RefreshTokenRequest> _validator;
        private readonly ITokenGenerator _tokenGenerator;
        public RefreshTokenUseCase(IUnitOfWork unitOfWork, IValidator<RefreshTokenRequest> validator, ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponse> ExecuteAsync(RefreshTokenRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);
            
            var user = await _unitOfWork.Users.GetByRefreshTokenAsync(request.RefreshToken, ct);

            if (user is null || user.RefreshTokenExpiry <= DateTime.UtcNow)
                throw new ValidationException(new[] { new ValidationFailure(nameof(user.RefreshToken), "Invalid or expired RefreshToken")});

            var tokenResult = _tokenGenerator.GenerateToken(user);

            user.SetRefreshToken(tokenResult.RefreshToken, tokenResult.RefreshTokenExpiration);
            await _unitOfWork.SaveChangesAsync(ct);

            return Mapper.AuthMapper.ToAuthResponse(user, tokenResult);
        }
    }
}
