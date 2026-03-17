using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Domain.Entities
{
    public class Observation
    {
        public int Id { get; set; }
        public ObservationType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public TimeOnly Timestamp { get; set; } = TimeOnly.FromDateTime(DateTime.UtcNow);

        // Relationships (flexible)
        public int TallySheetId { get; set; }
        public TallySheet TallySheet { get; set; } = null!;

        public int? ClientId { get; set; }
        public Client? Client { get; set; }
        public int? TruckId { get; set; }
        public Truck? Truck { get; set; }
    }
}
