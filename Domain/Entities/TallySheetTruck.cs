using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Text;

namespace Domain.Entities
{
    public class TallySheetTruck
    {

        public int TallySheetId { get; set; }
        public TallySheet TallySheet { get; set; }

        public int TruckId { get; set; }
        public Truck Truck { get; set; }

        // Usage session
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }

        // Relationships
        public ICollection<Pause> Pauses { get; set; } = [];
        public ICollection<Observation> Observations { get; set; } = [];

    }
}
