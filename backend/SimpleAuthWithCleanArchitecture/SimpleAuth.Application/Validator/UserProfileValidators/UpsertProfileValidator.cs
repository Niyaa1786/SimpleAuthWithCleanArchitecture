using FluentValidation;
using SimpleAuth.Application.DTOs.Request.UserProfile;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace SimpleAuth.Application.Validator.UserProfileValidators
{
    public class UpsertProfileValidator : AbstractValidator<UpsertProfileRequest>
    {
        public UpsertProfileValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is required")
                .MaximumLength(50).WithMessage("First name must not exceed 50 characters");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is required")
                .MaximumLength(50).WithMessage("Last name must not exceed 50 characters");
            RuleFor(x => x.PhoneNumber).MaximumLength(20).WithMessage("Phone number max 20 characters")
                .Matches("^\\+?[0-9\\s\\-\\(\\)]+$").When(x => !string.IsNullOrEmpty(x.PhoneNumber)).WithMessage("Invalid phone number format");
            RuleFor(x => x.Gender).IsInEnum().WithMessage("Invalid gender value");

        }
    }
}
