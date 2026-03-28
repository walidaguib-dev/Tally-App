using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Cars
{
    public class CarsDto
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string VinNumber { get; set; } = null!;
        public string? Bill_Of_Lading { get; set; }
        public string carStatus { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public string Username { get; set; } = null!;
        public int TallySheetId { get; set; }
        public int ShipId { get; set; }
        public string ShipName { get; set; } = string.Empty;
        public DateTime RecordedAt { get; set; }
    }
}
