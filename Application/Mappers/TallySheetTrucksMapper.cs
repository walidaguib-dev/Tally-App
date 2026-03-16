using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetTrucks;
using Domain.Entities;

namespace Application.Mappers
{
    public static class TallySheetTrucksMapper
    {
        public static AssignedTruckDto MapToJson(this TallySheetTruck sheetTruck)
        {
            return new AssignedTruckDto
            {
                TallySheetId = sheetTruck.TallySheetId,
                TruckId = sheetTruck.TruckId,
                TruckPlateNumber = sheetTruck.Truck.PlateNumber,
                StartTime = sheetTruck.StartTime,
                EndTime = sheetTruck.EndTime,
            };
        }

        public static TallySheetTruck MapToEntity(this AssignTruckDto dto)
        {
            return new TallySheetTruck
            {
                TallySheetId = dto.TallySheetId,
                TruckId = dto.TruckId,
                StartTime = dto.StartTime,
                EndTime = null,
            };
        }
    }
}
