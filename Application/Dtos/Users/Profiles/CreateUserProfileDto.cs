using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Users.Profiles
{
    public class CreateUserProfileDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // FK to ApplicationUser.Id
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        // Audit fields
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public int? UploadId { get; set; }
    }
}
