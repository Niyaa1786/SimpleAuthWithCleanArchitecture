using FluentValidation;
using FluentValidation.Results;
using SimpleAuth.Application.DTOs.Request;
using SimpleAuth.Application.DTOs.Response;
using SimpleAuth.Application.Interfaces.Common;
using SimpleAuth.Application.Interfaces.Shared;
using SimpleAuth.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.UseCases
{
    internal class RegisterUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IValidator<RegisterRequest> _validator;
        private readonly IPasswordHasher _passwordHasher;
        public RegisterUseCase(IUnitOfWork unitOfWork, IValidator<RegisterRequest> validator, IPasswordHasher passwordHasher, ITokenGenerator tokenGenerator)
        {
            _unitOfWork = unitOfWork;
            _validator = validator;
            _passwordHasher = passwordHasher;
        }

        public async Task<AuthResponse> ExecuteAsync(RegisterRequest request, CancellationToken ct = default)
        {
            _validator.ValidateAndThrow(request);

            var existingUser = await _unitOfWork.Users.GetByNameOrEmailAsync(request.Username, request.Email, ct);

            if(existingUser is not null)
            {
                if (existingUser.Username == request.Username)
                    throw new ValidationException(new[] { new ValidationFailure(nameof(request.Username), "Username already taken ") });
                if (existingUser.Email == request.Email)
                    throw new ValidationException(new[] { new ValidationFailure(nameof(request.Email), "Email already taken") });
            }

            var passwordHash = _passwordHasher.Hash(request.Password);
            var newUser = Mapper.UserMapper.ToUserEntity(request,passwordHash);

            _unitOfWork.Users.Add(newUser);
            await _unitOfWork.SaveChangesAsync(ct);

            return Mapper.UserMapper.ToAuthResponse(newUser,null!);
        }
    }
}
