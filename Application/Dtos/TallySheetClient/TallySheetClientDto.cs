using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.TallySheetClient
{
    public class TallySheetClientDto
    {
        public int Id { get; set; }
        public int TallySheetId { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; } = null!;
        public int Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public string? Notes { get; set; }
        public DateTime? LastUpdated { get; set; }

    }

}