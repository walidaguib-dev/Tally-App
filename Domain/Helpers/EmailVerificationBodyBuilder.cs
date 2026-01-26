using System;
using Domain.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace Domain.Helpers
{
    public class EmailVerificationBodyBuilder(
        IConfiguration configuration
        )
    {
        private readonly IConfiguration _configuration = configuration;
        public string? GenerateBody(string userId, string token, VerificationPurpose purpose)
        {
            var appUrl = _configuration.GetValue<string>("ApiBaseUrl") ?? "http://localhost:5198";
            if (purpose == VerificationPurpose.EmailVerification)
            {
                var callbackUrl = $"{appUrl}/api/emails/confirm-email?userId={userId}&token={Uri.EscapeDataString(token)}";
                return $"<p>Please confirm your account by clicking <a href='{callbackUrl}'>here</a>.</p>";
            }

            if(purpose == VerificationPurpose.PasswordReset)
            {

                return $"<p>Your password reset token is <strong>{token}</strong>.</p>";
            }
            return null;
        }
    }
}