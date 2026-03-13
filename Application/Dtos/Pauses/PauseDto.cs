using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Pauses
{
    public class PauseDto
    {
        public int Id { get; set; }
        public string Reason { get; set; } = null!;
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public double? DurationMinutes { get; set; } // computed
        public string? Notes { get; set; }
        public string? TruckName { get; set; }
    }
}