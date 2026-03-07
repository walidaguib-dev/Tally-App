using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Domain.Enums;

namespace Application.Dtos.TallySheets
{
    public class CreateTallySheetDto
    {
        public DateTime Date { get; set; }
        public int TeamsCount { get; set; }
        public ShiftType Shift { get; set; }
        public ZoneType Zone { get; set; }
        // Relationships
        public int ShipId { get; set; }

    }
}