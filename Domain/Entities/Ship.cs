using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Domain.Entities
{
    public class Ship
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImoNumber { get; set; } = string.Empty;

        // Relationships
        public List<TallySheet> tallySheets { get; set; } = [];
    }
}
