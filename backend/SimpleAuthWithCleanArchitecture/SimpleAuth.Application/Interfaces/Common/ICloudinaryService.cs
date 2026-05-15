using SimpleAuth.Application.DTOs.Shared;
using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace SimpleAuth.Application.Interfaces.Common
{
    public interface ICloudinaryService
    {
        public Task<ImageUploadResult> UploadAsync(Stream fileStream, string fileName, string contentType, string folders = "avatars", CancellationToken ct = default);
        public Task<bool> DeleteAsync(string publicId, CancellationToken ct = default);
    }
}
