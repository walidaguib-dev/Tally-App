using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Users
{
    public record UserDto
    {
        public string Id { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string username { get; set; } = string.Empty;
        public bool email_confirmed { get; set; }
    }
}