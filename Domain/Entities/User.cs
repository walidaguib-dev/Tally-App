using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public RefreshToken refreshToken { get; set; }
        public UserProfile profile { get; set; }
        public List<EmailToken> EmailTokens { get; set; } = new List<EmailToken>();
        public Uploads Upload { get; set; }
    }
}
