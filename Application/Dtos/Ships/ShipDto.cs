using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Ships
{
    public record ShipDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string ImoNumber { get; set; } = string.Empty;
    }
}
