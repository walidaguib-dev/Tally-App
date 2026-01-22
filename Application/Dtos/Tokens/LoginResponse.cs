using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Tokens
{
    public class LoginResponse
    {
        public string Access_Token { get; set; } = string.Empty;
        public string Refresh_Token { get; set; } = string.Empty;
    }
}
