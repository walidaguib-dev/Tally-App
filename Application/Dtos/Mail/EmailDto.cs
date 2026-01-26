using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Mail
{
    public record EmailDto
    {
        public int Id { get; set; }
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ConsumedAt { get; set; } = null;
        public string Username { get; set; }
        public VerificationPurpose Purpose { get; set; } = VerificationPurpose.EmailVerification;
    }
}
