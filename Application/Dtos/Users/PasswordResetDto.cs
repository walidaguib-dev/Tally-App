using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Users
{
    public record PasswordResetDto
    {
        public string userId { get; set; } = string.Empty;
        public string current_password { get; set; } = string.Empty;
        public string new_password { get; set; } = string.Empty;
    }
}