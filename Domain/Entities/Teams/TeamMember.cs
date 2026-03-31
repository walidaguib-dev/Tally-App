using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Teams
{
    public class TeamMember
    {
        public int Id { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public int TeamId { get; set; }
        public Team Team { get; set; } = null!;

        public string Role { get; set; } = null!; // "tallyman", "head_tallyman"
        public DateTime JoinedAt { get; set; } = DateTime.UtcNow;
    }
}
