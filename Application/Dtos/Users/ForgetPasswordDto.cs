using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Users
{
    public record ForgetPasswordDto
    {
        public string userId { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string new_password { get; set; } = string.Empty;

    }
}
