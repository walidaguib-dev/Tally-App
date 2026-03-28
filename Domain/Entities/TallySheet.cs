using System;
using System.Collections.Generic;
using System.Text;
using Domain.Enums;

namespace Domain.Entities
{
    public class TallySheet
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int TeamsCount { get; set; }
        public ShiftType Shift { get; set; }
        public ZoneType Zone { get; set; }

        // Relationships
        public int ShipId { get; set; }
        public Ship Ship { get; set; } = null!;

        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;

        public ICollection<TallySheetClient> TallySheetClients { get; set; } = [];
        public ICollection<TallySheetTruck> TallySheetTrucks { get; set; } = [];
        public ICollection<Observation> Observations { get; set; } = [];
        public List<Cars> CarsList { get; set; } = [];
    }
}
