using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Pause
    {
        public int Id { get; set; }
        public PauseReason Reason { get; set; }
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }
        public string? Notes { get; set; } = string.Empty;

        // Relationships
        public int? TruckId { get; set; }
        public Truck? Truck { get; set; } = null!;

        public int TallySheetId { get; set; }
        public TallySheet TallySheet { get; set; } = null!;


    }
}
