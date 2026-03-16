using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Dtos.TallySheetClient
{
    public class AddClientToTallyDto
    {
        public int TallySheetId { get; set; }
        public int ClientId { get; set; }
        public int Quantity { get; set; }
        public string Unit { get; set; } = null!; // "bags", "packages", "pieces"
        public string? Notes { get; set; }
    }
}