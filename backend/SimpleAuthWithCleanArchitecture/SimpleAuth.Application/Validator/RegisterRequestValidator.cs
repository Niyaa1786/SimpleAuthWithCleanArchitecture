using FluentValidation;
using SimpleAuth.Application.DTOs.Request;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Validator
{
    internal class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.Username).NotEmpty().WithMessage("Username is required.")
                                    .MinimumLength(6).WithMessage("Username must be at least 6 characters long.");

            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is required.")
                                 .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                                    .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                                    .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
                                    .WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");
        }
    }
}
