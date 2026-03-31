using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Teams
{
    public class UpdateTeamDto
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
    }
}
