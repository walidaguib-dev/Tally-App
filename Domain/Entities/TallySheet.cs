using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TallySheet
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int TeamsCount { get; set; }
        public ShiftType Shift { get; set; }
        public ZoneType Zone { get; set; }

        // Relationships
        public int ShipId { get; set; }
        public Ship Ship { get; set; } = null!;

        public ICollection<TallySheetMerchandise> TallySheetMerchandises { get; set; } = [];
        public ICollection<TallySheetTruck> TallySheetTrucks { get; set; } = [];
        public ICollection<Observation> Observations { get; set; } = [];

    }
}
