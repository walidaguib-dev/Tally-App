using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Observations;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mappers
{
    public static class ObservationsMapper
    {
        public static ObservationsDto MapToDto(this Observation item)
        {
            return new ObservationsDto
            {
                Id = item.Id,
                Type = item.Type.ToString(),
                Description = item.Description,
                TallySheetId = item.TallySheetId,
                Timestamp = item.Timestamp,
                ClientId = item.ClientId,
                ClientName = item.Client?.Name,
                TruckId = item.TruckId,
                PlateNumber = item.Truck?.PlateNumber,
            };
        }

        public static Observation MapToEntity(this CreateObservationDto dto)
        {
            var observationType = Enum.TryParse<ObservationType>(dto.Type, out var type)
                ? type
                : ObservationType.Other;
            return new Observation
            {
                Type = observationType,
                Description = dto.Description,
                Timestamp = TimeOnly.FromDateTime(DateTime.UtcNow),
                TallySheetId = dto.TallySheetId,
                ClientId = dto.ClientId,
                TruckId = dto.TruckId,
            };
        }
    }
}
