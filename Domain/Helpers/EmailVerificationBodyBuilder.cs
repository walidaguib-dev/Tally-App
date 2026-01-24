using System;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Domain.Helpers
{
    public class EmailVerificationBodyBuilder()
    {
        public string GenerateBody(string userId, string token)
        {
            var callbackUrl = $"http://localhost:5198/api/emails/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";
            return $"<p>Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.</p>";
        }
    }
}