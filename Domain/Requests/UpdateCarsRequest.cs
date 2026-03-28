using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Requests
{
    public class UpdateCarsRequest
    {
        public string Brand { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string VinNumber { get; set; } = null!;
        public string? Bill_Of_Lading { get; set; }
        public string carStatus { get; set; } = null!;
        public int ShipId { get; set; }
    }
}
