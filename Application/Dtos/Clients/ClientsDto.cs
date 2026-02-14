using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Clients
{
    public  record ClientsDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string ContactInfo { get; set; } = null!;
        public List<string> Bill_Of_Lading { get; set; } = [];
        public int MerchandiseId { get; set; }
        public string Merchandise_Name { get; set; } = null!;
    }
}
