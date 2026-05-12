using FluentValidation;
using SimpleAuth.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Validator
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Refresh token is required.");
        }
    }
}
