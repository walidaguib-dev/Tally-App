using Domain.Contracts;
using Domain.Entities;
using Domain.Enums;
using Domain.Helpers;
using FluentEmail.Core;
using Hangfire;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories
{
    public class EmailsRepository(
        UserManager<User> _userManager,
        ApplicationDbContext applicationDbContext,
        IFluentEmail fluentEmail,
        IBackgroundJobClient _jobClient,
        IConfiguration configuration
        ) : IEmail
    {
        private readonly IBackgroundJobClient jobClient = _jobClient;
        private readonly UserManager<User> userManager = _userManager;
        private readonly IFluentEmail FluentEmail = fluentEmail;
        private readonly ApplicationDbContext db = applicationDbContext;
        private readonly IConfiguration _configuration = configuration;


        public async Task<object?> ConfirmEmailAsync(string userId, string token)
        {
            var user = await userManager.FindByIdAsync(userId);
            if (user == null) return null;

            var result = await userManager.ConfirmEmailAsync(user, token);
            if (!result.Succeeded) throw new Exception("Invalid or expired token");

            return "Done";
        }

        public async Task CreateEmailAsync(string recipientEmail, string subject, string userId , string token , VerificationPurpose purpose)
        {
            var _bodyBuilder = new EmailVerificationBodyBuilder(_configuration);
            var result = _bodyBuilder.GenerateBody(userId, token,purpose);

            // Send email using FluentEmail
            await FluentEmail
                .To(recipientEmail)
                .Subject(subject)
                .Body(result, isHtml: true)
                .SendAsync();

        }

        public async Task<EmailToken?> SendEmail(string userId, VerificationPurpose purpose)
        {
            User? user = await userManager.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return null;

            var emailToken = await Utils.GetEmail(userId, purpose, userManager) ?? throw new InvalidOperationException("User not found!");
            await db.emailTokens.AddAsync(emailToken);
            await db.SaveChangesAsync();
            jobClient.Enqueue<IEmail>(emailRepo => emailRepo.CreateEmailAsync(
                user.Email!,
                purpose == 0 ? "Email verification" : "Password Reset",
                userId,
                emailToken.CodeHash,
                purpose
            ));
            return emailToken;
        }
    }
}
