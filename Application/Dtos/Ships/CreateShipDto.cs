using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Ships
{
    public record CreateShipDto
    {
        public string Name { get; set; } = string.Empty;
        public string ImoNumber { get; set; } = string.Empty;
    }
}
