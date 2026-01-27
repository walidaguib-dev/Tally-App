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
    }
}
