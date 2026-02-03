using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Observation
    {
        public int Id { get; set; }
        public ObservationType Type { get; set; }
        public string Description { get; set; } = string.Empty;
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        // Relationships (flexible)
        public int? TallySheetId { get; set; }
        public TallySheet? TallySheet { get; set; } 

        public int? TallySheetMerchandiseId { get; set; }
        public TallySheetMerchandise? TallySheetMerchandise { get; set; } 

        public int? TallySheetTruckId { get; set; }
        public TallySheetTruck? TallySheetTruck { get; set; } 

    }
}
