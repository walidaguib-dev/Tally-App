using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Merchandises
{
    public record CreateMerchandiseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
