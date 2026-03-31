using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams.TeamMembers;

namespace Application.Dtos.Teams
{
    public class TeamDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string SupervisorId { get; set; } = null!;
        public string SupervisorName { get; set; } = null!;
        public int MembersCount { get; set; }
        public List<TeamMemberDto> Members { get; set; } = [];
    }
}
