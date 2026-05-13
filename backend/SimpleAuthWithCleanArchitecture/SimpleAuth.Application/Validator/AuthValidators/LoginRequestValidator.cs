using FluentValidation;
using SimpleAuth.Application.DTOs.Request.Auth;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Validator.AuthValidators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
                .When(x => !string.IsNullOrEmpty(x.Username));
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.")
                .When(x=> !string.IsNullOrEmpty(x.Email));
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required");
        }
    }
}
