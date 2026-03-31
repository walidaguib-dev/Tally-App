using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Teams
{
    public class CreateTeamDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
