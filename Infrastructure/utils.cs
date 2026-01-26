using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure
{
    public class Utils
    {
        public static async Task<EmailToken?> GetEmail(string userId , VerificationPurpose purpose , UserManager<User> userManager) {
            switch (purpose)
            {
                case VerificationPurpose.EmailVerification:
                    var user = await userManager.FindByIdAsync(userId) ?? throw new InvalidOperationException("User not found!");
                    string? token = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    if (token == null) return null;
                    return new EmailToken
                    {
                        CodeHash = token,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                        CreatedAt = DateTime.UtcNow,
                        UserId = userId,
                        Purpose = purpose,
                        ConsumedAt = null
                    };
                case VerificationPurpose.PasswordReset:
                    var myUser = await userManager.FindByIdAsync(userId) ?? throw new InvalidOperationException("User not found!");
                    
                    var PasswordResetToken = await userManager.GeneratePasswordResetTokenAsync(myUser);
                    if (PasswordResetToken == null) return null;
                    return new EmailToken
                    {
                        CodeHash = PasswordResetToken,
                        ExpiresAt = DateTime.UtcNow.AddMinutes(15),
                        CreatedAt = DateTime.UtcNow,
                        UserId = userId,
                        Purpose = purpose,
                        ConsumedAt = null
                    };
                default:
                    return null;


            }
        }
    }
}
