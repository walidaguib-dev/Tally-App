using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactInfo { get; set; } = null!;
        public List<string> Bill_Of_Lading { get; set; } = [];

        // Relationships
        public int MerchandiseId { get; set; }
        public Merchandise Merchandise { get; set; } = null!;
        public List<TallySheetClient> TallySheetClients { get; set; } = [];
    }
}
