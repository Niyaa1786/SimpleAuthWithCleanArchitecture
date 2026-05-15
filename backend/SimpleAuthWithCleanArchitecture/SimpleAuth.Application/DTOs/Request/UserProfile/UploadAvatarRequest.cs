using System;
using System.Collections.Generic;
using System.Text;

namespace SimpleAuth.Application.DTOs.Request.UserProfile
{
    public class UploadAvatarRequest
    {
        public Guid UserId { get; set; }
        public Stream FileStream { get; set; } = null!;
        public string FileName { get; set; } = string.Empty;
        public string ContentType { get; set; } = string.Empty;
    }
}
