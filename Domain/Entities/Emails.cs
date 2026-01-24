using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class EmailToken
    {
        public int Id { get; set; }
        public string CodeHash { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? ConsumedAt { get; set; }

        public string UserId { get; set; } = string.Empty;
        public User User { get; set; } = null!;

        public VerificationPurpose Purpose { get; set; } = VerificationPurpose.EmailVerification;
    }

}
