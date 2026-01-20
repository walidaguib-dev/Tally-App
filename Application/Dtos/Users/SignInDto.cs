using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Users
{
    public record SignInDto
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}