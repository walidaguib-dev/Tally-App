using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.TallySheetTrucks
{
    public class AssignedTruckDto
    {
        public int Id { get; set; }
        public int TallySheetId { get; set; }
        public int TruckId { get; set; }
        public string TruckPlateNumber { get; set; } = string.Empty;
        public TimeOnly StartTime { get; set; }
        public TimeOnly? EndTime { get; set; }

    }
}