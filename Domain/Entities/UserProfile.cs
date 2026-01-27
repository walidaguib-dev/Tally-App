using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // FK to ApplicationUser.Id
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        // Navigation
        public int? UploadId { get; set; }
        public Uploads? Upload { get; set; }
        public User User { get; set; } = null!;

    }
}
