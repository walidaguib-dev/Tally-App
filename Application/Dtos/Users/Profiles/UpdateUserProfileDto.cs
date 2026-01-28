using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Users.Profiles
{
    public class UpdateUserProfileDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string? Bio { get; set; }
        public DateTime? UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
