using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IUploads
    {
        public Task<object> UploadImageAsync(IFormFile file);
        public Task<Uploads> UploadsAsync(string userId , IFormFile file);
        public Task<object> DeleteUploadAsync(string publicId);
        public Task<Uploads?> UpdateAsync(string userId, IFormFile file, string oldPublicId);
        public Task<List<Uploads>> GetAllFilesByUserAsync(string userId);
        public Task<Uploads?> GetUploadByUserAsync(string userId);
    }
}
