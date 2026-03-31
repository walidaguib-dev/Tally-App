using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Enums;

namespace Domain.Entities
{
    public class Cars
    {
        public int Id { get; set; }
        public string Brand { get; set; } = null!;
        public string Type { get; set; } = null!;
        public string VinNumber { get; set; } = null!;
        public string? Bill_Of_Lading { get; set; }
        public CarStatus carStatus { get; set; }
        public string UserId { get; set; } = null!;
        public User User { get; set; } = null!;
        public int TallySheetId { get; set; }
        public TallySheet TallySheet { get; set; } = null!;
        public int ShipId { get; set; }
        public Ship Ship { get; set; } = null!;
        public DateTime RecordedAt { get; set; }
    }
}
