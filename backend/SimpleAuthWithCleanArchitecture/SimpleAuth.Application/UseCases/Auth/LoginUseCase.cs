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
    internal class LoginUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<LoginRequest> _validator;
        private readonly IPasswordHasher _passwordHasher;
        private readonly ITokenGenerator _tokenGenerator;
        public LoginUseCase(IUnitOfWork unitOfWork ,IValidator<LoginRequest> validator, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _passwordHasher = passwordHasher;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<AuthResponse> ExecuteAsync(LoginRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var user = await _unitOfWork.Users.GetByNameOrEmailAsync(request.Username, request.Email, ct);

            if(user is null)
                throw new ValidationException(new[] {new ValidationFailure("Username", "Invalid username or password.")});

            var isValidPassword = _passwordHasher.Verify(request.Password, user.PasswordHash);
            if (!isValidPassword)
                throw new ValidationException(new[] { new ValidationFailure("Password", "Invalid username or password") });


            var tokenResult = _tokenGenerator.GenerateToken(user);
            user.SetRefreshToken(tokenResult.RefreshToken, tokenResult.RefreshTokenExpiration);
            await _unitOfWork.SaveChangesAsync(ct);

            return Mapper.UserMapper.ToAuthResponse(user, tokenResult);
        }
    }
}
