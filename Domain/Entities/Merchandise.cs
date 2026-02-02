using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Merchandise
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;

        // Relationships
        public ICollection<Client> Clients { get; set; } = [];
        public ICollection<TallySheetMerchandise> TallySheetMerchandises { get; set; } = [];
        public ICollection<Observation> Observations { get; set; } = [];

    }
}
