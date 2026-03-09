using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.Dtos.Trucks
{
    public record CreateTruckDto
    {
        [Required(ErrorMessage = "Plate number is required.")]
        [MinLength(7, ErrorMessage = "Plate number must be at least 7 characters.")]
        public string PlateNumber { get; set; } = string.Empty;

        [Range(1, double.MaxValue, ErrorMessage = "Capacity must be greater than 0.")]
        public double Capacity { get; set; }
    }
}
