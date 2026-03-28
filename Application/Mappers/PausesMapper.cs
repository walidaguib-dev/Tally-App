using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Pauses;
using Domain.Entities;
using Domain.Enums;

namespace Application.Mappers
{
    public static class PausesMapper
    {
        public static PauseDto ToPauseDto(this Domain.Entities.Pause pause)
        {
            var duration = (pause.EndTime - pause.StartTime)?.TotalMinutes;
            return new PauseDto
            {
                Id = pause.Id,
                Reason = pause.Reason.ToString(),
                StartTime = pause.StartTime,
                EndTime = pause.EndTime ?? null, // If EndTime is null, use current time for duration calculation
                DurationMinutes = pause.EndTime > pause.StartTime ? duration : null,
                Notes = pause.Notes,
                TruckName = pause.Truck?.PlateNumber,
            };
        }

        public static Pause ToModel(this CreatePauseDto createPauseDto)
        {
            return new Pause
            {
                StartTime = createPauseDto.StartTime,
                Reason = Enum.TryParse<PauseReason>(createPauseDto.Reason, true, out var reason)
                    ? reason
                    : PauseReason.Other,
                Notes = createPauseDto.Notes,
                EndTime = null,
                TallySheetId = createPauseDto.TallySheetId,
                TruckId = createPauseDto.TruckId, // temporarily set to now, should be updated when the pause ends
            };
        }
    }
}
