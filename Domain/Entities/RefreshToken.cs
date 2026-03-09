using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class RefreshToken
    {
        public int Id { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime ExpiresAt { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? RevokedAt { get; set; }
        public string userId { get; set; } = null!;
        public User user { get; set; } = null!;
    }
}
