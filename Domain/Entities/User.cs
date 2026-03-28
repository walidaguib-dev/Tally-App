using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public RefreshToken refreshToken { get; set; } = null!;
        public UserProfile? profile { get; set; }
        public List<EmailToken> EmailTokens { get; set; } = [];
        public List<Uploads> Upload { get; set; } = [];
        public List<Cars> CarsList { get; set; } = [];
    }
}
