using FluentValidation;
using SimpleAuth.Application.DTOs.Request.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Validator.AuthValidators
{
    public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
    {
        public RefreshTokenRequestValidator()
        {
            RuleFor(x => x.RefreshToken).NotEmpty().WithMessage("Refresh token is required.");
        }
    }
}
