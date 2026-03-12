using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos.Trucks
{
    public record CreateTruckDto
    {
        public string PlateNumber { get; set; } = string.Empty;
        public double Capacity { get; set; }
    }
}
