using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Observations
{
    public class ObservationsDto
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string Description { get; set; } = null!;
        public TimeOnly Timestamp { get; set; }
        public int TallySheetId { get; set; }
        public int? ClientId { get; set; }
        public string? ClientName { get; set; }
        public int? TruckId { get; set; }
        public string? PlateNumber { get; set; }
    }
}
