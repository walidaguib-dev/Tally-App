using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Uploads
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // FK to ApplicationUser
        public string Url { get; set; } = string.Empty; // Cloudinary URL
        public string PublicId { get; set; } = string.Empty; // Cloudinary public ID
        public string FileType { get; set; } = string.Empty; // e.g., "image/png"
        public long FileSize { get; set; } // in bytes
        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
        public User User { get; set; } = null!;
        public UserProfile? profile { get; set; }
    }
}
