using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Application.Dtos.Observations
{
    public class CreateObservationDto
    {
        public string Type { get; set; } = null!;
        public string Description { get; set; } = string.Empty;

        // Relationships (flexible)
        public int TallySheetId { get; set; }
        public int? ClientId { get; set; }
        public int? TruckId { get; set; }
    }
}
