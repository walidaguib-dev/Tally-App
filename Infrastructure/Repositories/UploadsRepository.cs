using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Domain.Contracts;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
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

        public async Task<object> DeleteUploadAsync(string publicId)
        {
            var file = await _context.uploads.FirstOrDefaultAsync(u => u.PublicId == publicId) ?? throw new Exception("file not found!");
            var deletionParams = new DeletionParams(file.PublicId);
            var result = await _cloudinaryService.DestroyAsync(deletionParams);
            if (result.Error != null || result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(result.Error!.Message);
            }
            _context.uploads.Remove(file);
            await _context.SaveChangesAsync();
            return result;

        }

        public async Task<List<Uploads>> GetAllFilesByUserAsync(string userId)
        {
            var uploads = await _context.uploads
                .Include(u => u.User)
                .AsNoTracking()
                .Where(u => u.UserId == userId).ToListAsync();
            return uploads;
        }

        public async Task<Uploads?> GetUploadByUserAsync(string userId)
        {
            var upload = await _context.uploads
                .Include(u => u.User)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.UserId == userId);
            if (upload == null)
            {
                throw new Exception("Upload not found");
            }
            return upload;
        }

        public async Task<Uploads?> UpdateAsync(string userId, IFormFile newfile, string oldPublicId)
        {
            // Step 1: Delete old file
            var file = await _context.uploads.FirstOrDefaultAsync(u => u.PublicId == oldPublicId) ?? throw new Exception("file not found!");
            var deletionParams = new DeletionParams(file.PublicId);
            var result = await _cloudinaryService.DestroyAsync(deletionParams);
            if (result.Error != null || result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(result.Error!.Message);
            }

            // Step 2: Upload new file
            ImageUploadResult uploadResult = (ImageUploadResult)await UploadImageAsync(newfile);

            // Step 3: Update DB record
            var upload = await _context.uploads.FirstOrDefaultAsync(u => u.PublicId == oldPublicId && u.UserId == userId);
            if (upload == null) throw new Exception("Upload record not found");

            upload.Url = uploadResult.SecureUrl.ToString();
            upload.PublicId = uploadResult.PublicId;
            upload.FileType = uploadResult.ResourceType;
            upload.FileSize = uploadResult.Bytes;
            upload.UploadedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return upload;

        }

        public async Task<object> UploadImageAsync(IFormFile file)
        {
            using var stream = file.OpenReadStream();
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                Transformation = new Transformation().Width(300).Height(300).Crop("fill").Gravity("face")
            };

            var result = await _cloudinaryService.UploadAsync(uploadParams);
            if (result.Error != null || result.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new Exception(result.Error!.Message);
            }
            return result;
        }

        public async Task<Uploads> UploadsAsync(string userId, IFormFile file)
        {
            var uploadResult = await UploadImageAsync(file) as ImageUploadResult;

            if (uploadResult == null || uploadResult.Error != null)
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
