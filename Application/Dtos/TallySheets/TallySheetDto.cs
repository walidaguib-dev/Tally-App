using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Dtos.TallySheets
{
    public class TallySheetDto
    {
        public int Id { get; set; }
        public DateOnly Date { get; set; }
        public int TeamsCount { get; set; }
        public ShiftType Shift { get; set; }
        public ZoneType Zone { get; set; }
        // Relationships
        public int ShipId { get; set; }
        public string UserId { get; set; } = null!;

        public ICollection<TallySheetMerchandise> TallySheetMerchandises { get; set; } = [];
        public ICollection<TallySheetTruck> TallySheetTrucks { get; set; } = [];
        public ICollection<Observation> Observations { get; set; } = [];
    }
}