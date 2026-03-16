using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TallySheetClient
    {
        public int TallySheetId { get; set; }
        public TallySheet TallySheet { get; set; } = null!;
        public int ClientId { get; set; }
        public Client Client { get; set; } = null!;
        public DateTime? LastUpdated { get; set; }

        // Contextual attributes
        public int Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public string? Notes { get; set; } = null!;
        public List<Observation> Observations { get; set; } = [];

    }
}
