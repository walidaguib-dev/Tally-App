using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Mail
{
    public class SendEmailDto
    {
        public string userId { get; set; } = string.Empty;
        public VerificationPurpose Purpose { get; set; } = VerificationPurpose.EmailVerification;
    }
}
