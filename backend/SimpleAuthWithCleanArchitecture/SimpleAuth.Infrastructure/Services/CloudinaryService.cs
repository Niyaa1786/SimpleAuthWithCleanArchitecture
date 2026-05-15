using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.Extensions.Configuration;
using Microsoft.Identity.Client;
using SimpleAuth.Application.DTOs.Shared;
using SimpleAuth.Application.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;
using ImageUploadResult = SimpleAuth.Application.DTOs.Shared.ImageUploadResult;

namespace SimpleAuth.Infrastructure.Services
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary _cloudinary;
        public CloudinaryService(IConfiguration configuration)
        {
            var cloudinarySettings = configuration.GetSection("CloudinarySettings");

            var cloudName = cloudinarySettings["CloudName"] ?? throw new Exception("Cloudinary CloudName missing");
            var apiKey = cloudinarySettings["ApiKey"] ?? throw new Exception("Cloudinary ApiKey missing");
            var apiSecret = cloudinarySettings["ApiSecret"] ?? throw new Exception("Cloudinary ApiSecret missing");

            var account = new Account(cloudName, apiKey, apiSecret);
            _cloudinary = new Cloudinary(account);
        }

        public async Task<ImageUploadResult> UploadAsync(Stream fileStream, string fileName, string contentType, string folders = "avatars", CancellationToken ct = default)
        {
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(fileName, fileStream),
                Folder = folders,
                Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
            };
            var result = await _cloudinary.UploadAsync(uploadParams, ct);

            return new ImageUploadResult
            {
                PublicId = result.PublicId,
                Url = result.SecureUrl.ToString()
            };
        }

        public async Task<bool> DeleteAsync(string publicId, CancellationToken ct = default)
        {
            var deletionParams = new DeletionParams(publicId);
            var result = await _cloudinary.DestroyAsync(deletionParams);

            if(result.Result == "ok")
            {
                return true;
            }
            return false;
        }
    }
}
