using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Pauses
{
    public class UpdatePauseDto
    {
        public string Reason { get; set; } = null!;
        public TimeOnly StartTime { get; set; } // string not enum
        public string? Notes { get; set; }
        public int? TruckId { get; set; }
    }
}