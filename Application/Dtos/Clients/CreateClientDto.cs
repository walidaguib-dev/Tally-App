using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Clients
{
    public record CreateClientDto
    {
        public string Name { get; set; } = null!;
        public string ContactInfo { get; set; } = null!;
        public List<string> Bill_Of_Lading { get; set; } = [];
        public int MerchandiseId { get; set; }
    }
}
