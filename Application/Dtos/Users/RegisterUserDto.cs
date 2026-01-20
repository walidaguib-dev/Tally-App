using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Users
{
    public record RegisterUserDto
    {
        public string email { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
        public List<string> Roles { get; set; } = [];
    }
}