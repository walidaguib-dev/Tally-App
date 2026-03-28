using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.Cars
{
    public class UpdateCarDto
    {
        public string Brand { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string VinNumber { get; set; } = null!;
        public string? Bill_Of_Lading { get; set; }
        public string carStatus { get; set; } = null!;
        public int ShipId { get; set; }
    }
}
