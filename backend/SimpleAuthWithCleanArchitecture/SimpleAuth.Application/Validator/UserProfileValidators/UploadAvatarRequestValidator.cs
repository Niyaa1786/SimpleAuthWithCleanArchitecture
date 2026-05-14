using FluentValidation;
using SimpleAuth.Application.DTOs.Request.UserProfile;
using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.Validator.UserProfileValidators
{
    public class UploadAvatarRequestValidator : AbstractValidator<UploadAvatarRequest>
    {
        private const long MaxFileSize = 5 * 1024 * 1024;
        public UploadAvatarRequestValidator()
        {
            RuleFor(x => x.FileStream)
                .NotNull().WithMessage("Stream data must not be empty")
                .Must(s => s.CanRead).WithMessage("Cannot read stream")
                .Must(s => s.Length > 0).WithMessage("File is required")
                .Must(s => s.Length <= MaxFileSize).WithMessage("Maximum file is 5MB");
        }
    }
}
