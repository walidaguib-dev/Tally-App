using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.TallySheetTrucks
{
    public class AssignTruckDto
    {
        public int TallySheetId { get; set; }
        public int TruckId { get; set; }
        public TimeOnly StartTime { get; set; }
    }
}