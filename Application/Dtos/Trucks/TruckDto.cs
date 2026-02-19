using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Dtos.Trucks
{
    public record TruckDto
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public double Capacity { get; set; }
    }
}
