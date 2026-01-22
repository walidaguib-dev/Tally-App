using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Tokens
{
    public record RefreshTokenRequest
    {
        public string userId { get; set; } = string.Empty;
        public string refreshTokenString { get; set; } = string.Empty;
    }
}
