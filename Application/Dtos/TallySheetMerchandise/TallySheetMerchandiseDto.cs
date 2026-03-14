using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.TallySheetMerchandise
{
    public class TallySheetMerchandiseDto
    {
        public int Id { get; set; }
        public int TallySheetId { get; set; }
        public int MerchandiseId { get; set; }
        public string MerchandiseName { get; set; } = null!;
        public int Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime? LastUpdated { get; set; }

    }

}