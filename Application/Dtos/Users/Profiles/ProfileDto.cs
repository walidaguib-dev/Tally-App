using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Users.Profiles
{
    public record ProfileDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty; // FK to ApplicationUser.Id
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }
        public int? UploadId { get; set; }
        public string url { get; set; }
        public string username { get; set; }
    }
}
