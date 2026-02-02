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
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string? Notes { get; set; } = string.Empty;

        // Relationships
        public int TallySheetTruckId { get; set; }
        public TallySheetTruck TallySheetTruck { get; set; } = null!;

    }
}
