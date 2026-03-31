using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Teams.TeamMembers
{
    public class AddMemberDto
    {
        public int TeamId { get; set; }
        public string UserId { get; set; } = null!;
        public string Role { get; set; } = null!;
    }
}
