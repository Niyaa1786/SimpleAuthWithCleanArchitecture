using FluentValidation;
using FluentValidation.Results;
using SimpleAuth.Application.DTOs.Request;
using SimpleAuth.Application.DTOs.Response;
using SimpleAuth.Application.Interfaces.Common;
using SimpleAuth.Application.Interfaces.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.UseCases.Auth
{
    internal class LogoutUseCase
    {
        private readonly IUnitOfWork _unitOfWork;
        public LogoutUseCase(IUnitOfWork unitOfWork) => _unitOfWork = unitOfWork;

        public async Task<bool> ExecuteAsync(Guid id, CancellationToken ct = default)
        {
            var user = await _unitOfWork.Users.GetByIdAsync(id, ct);
            if(user is null)
                throw new ValidationException(new[] { new ValidationFailure("Id", "User not found")});

            user.RevokeRefreshToken();
            await _unitOfWork.SaveChangesAsync(ct);

            return true;
        }
    }
}
