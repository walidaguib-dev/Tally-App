using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Contracts
{
    public interface IEmail
    {
        public Task<EmailToken?> SendEmail(string userId ,  VerificationPurpose purpose);
        public Task CreateEmailAsync(string recipientEmail, string subject, string userId , string token);
        public Task<object?> ConfirmEmailAsync(string userId, string token);
    }
}
