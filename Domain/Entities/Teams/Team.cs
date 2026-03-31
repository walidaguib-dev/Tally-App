using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain.Entities.Teams
{
    public class Team
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!; // "Group A", "Group B"
        public string? Description { get; set; }

        // Relationships
        public string SupervisorId { get; set; } = null!;
        public User Supervisor { get; set; } = null!;

        public List<TeamMember> Members { get; set; } = [];
        public List<TallySheet> TallySheets { get; set; } = [];
    }
}
