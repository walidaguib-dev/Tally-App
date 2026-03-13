using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Truck
    {
        public int Id { get; set; }
        public string PlateNumber { get; set; } = string.Empty;
        public double Capacity { get; set; }

        // Relationships

        public ICollection<TallySheetTruck> TallySheetTrucks { get; set; } = [];
        public List<Pause> Pauses { get; set; } = [];
        public ICollection<Observation> Observations { get; set; } = [];


    }
}
