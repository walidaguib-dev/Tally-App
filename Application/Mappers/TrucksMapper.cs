using Application.Dtos.Trucks;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappers
{
    public static class TrucksMapper
    {
        public static Truck MapToModel(this CreateTruckDto dto) {
            return new Truck
            {
                PlateNumber = dto.PlateNumber,
                Capacity = dto.Capacity
            };
        }

        public static TruckDto MapToJson(this Truck truck) {
            return new TruckDto
            {
                Id = truck.Id,
                PlateNumber = truck.PlateNumber,
                Capacity = truck.Capacity
            };
        }
    }
}
