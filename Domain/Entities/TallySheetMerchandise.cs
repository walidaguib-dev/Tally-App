using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class TallySheetMerchandise
    {
        public int TallySheetId { get; set; }
        public TallySheet TallySheet { get; set; } = null!;

        public int MerchandiseId { get; set; }
        public Merchandise Merchandise { get; set; } = null!;

        // Contextual attributes
        public int Quantity { get; set; }
        public string Unit { get; set; } = null!;
        public string Notes { get; set; } = null!;

        // Relationships
        public ICollection<Observation> Observations { get; set; } = [];

    }
}
