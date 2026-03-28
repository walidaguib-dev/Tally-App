using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Cars;
using Domain.Entities;
using Domain.Enums;
using Domain.Requests;

namespace Application.Mappers
{
    public static class CarsMapper
    {
        public static CarsDto MapToDto(this Cars item)
        {
            return new CarsDto
            {
                Id = item.Id,
                Brand = item.Brand,
                Type = item.Type,
                Bill_Of_Lading = item.Bill_Of_Lading,
                carStatus = item.carStatus.ToString(),
                VinNumber = item.VinNumber,
                UserId = item.UserId,
                Username = item.User?.UserName ?? "Unknown",
                ShipId = item.ShipId,
                ShipName = item.Ship?.Name ?? "Unknown",
                TallySheetId = item.TallySheetId,
                RecordedAt = item.RecordedAt,
            };
        }

        public static Cars MapToEntity(this CreateCarDto dto)
        {
            var carStatus = Enum.TryParse<CarStatus>(dto.carStatus, true, out var status)
                ? status
                : CarStatus.Pending;
            return new Cars
            {
                Brand = dto.Brand,
                Type = dto.Type,
                Bill_Of_Lading = dto.Bill_Of_Lading,
                carStatus = carStatus,
                VinNumber = dto.VinNumber,
                UserId = dto.UserId,
                ShipId = dto.ShipId,
                TallySheetId = dto.TallySheetId,
                RecordedAt = DateTime.UtcNow,
            };
        }

        public static UpdateCarsRequest MapToUpdateDto(this UpdateCarDto request)
        {
            return new UpdateCarsRequest
            {
                Brand = request.Brand,
                Bill_Of_Lading = request.Bill_Of_Lading,
                carStatus = request.carStatus,
                Type = request.Type,
                ShipId = request.ShipId,
                VinNumber = request.VinNumber,
            };
        }
    }
}
