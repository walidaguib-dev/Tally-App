using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Merchandises
{
    public record MerchandiseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
    }
}
