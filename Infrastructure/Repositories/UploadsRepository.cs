using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class UploadsRepository(
        Cloudinary cloudinaryService,
        ApplicationDbContext context
        ) : IUploads
    {
        private readonly Cloudinary _cloudinaryService = cloudinaryService;
        private readonly ApplicationDbContext _context = context;
        public async Task<object> UploadImageAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
            };

            var result = await _cloudinaryService.UploadAsync(uploadParams);
            if(result.Error != null || result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(result.Error!.Message);
            }
            return result;
        }

        public async Task<Uploads> UploadsAsync(string userId, IFormFile file)
        {
            var uploadResult = await UploadImageAsync(file) as ImageUploadResult;

            if(uploadResult == null || uploadResult.Error != null)
            {
                throw new Exception("Image upload failed");
            }

            User? user = await _context.Users.FindAsync(userId) ?? throw new Exception("User not found");
            var upload = new Uploads
            {
                UserId = userId,
                Url = uploadResult!.SecureUrl.ToString(),
                PublicId = uploadResult.PublicId,
                FileType = uploadResult.ResourceType,
                FileSize = uploadResult.Bytes,
                UploadedAt = DateTime.UtcNow,
            };

            await _context.uploads.AddAsync(upload);
            await _context.SaveChangesAsync();
            return upload;
        }
    }
}
