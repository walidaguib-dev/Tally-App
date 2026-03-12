using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;

namespace Application.Mappers
{
    public static class TallySheetsMapper
    {
        public static TallySheetDto MapToDto(this Domain.Entities.TallySheet tallySheet)
        {
            return new TallySheetDto
            {
                Id = tallySheet.Id,
                Date = tallySheet.Date,
                Shift = tallySheet.Shift,
                TeamsCount = tallySheet.TeamsCount,
                ShipId = tallySheet.ShipId,
                Zone = tallySheet.Zone,
                UserId = tallySheet.UserId,
                Observations = tallySheet.Observations ?? [],
                TallySheetMerchandises = tallySheet.TallySheetMerchandises ?? [],

            };
        }

        public static Domain.Entities.TallySheet MapToEntity(this CreateTallySheetDto tallySheetDto, string userId)
        {
            return new Domain.Entities.TallySheet
            {
                Date = DateOnly.FromDateTime(DateTime.UtcNow),
                Shift = tallySheetDto.Shift,
                TeamsCount = tallySheetDto.TeamsCount,
                ShipId = tallySheetDto.ShipId,
                Zone = tallySheetDto.Zone,
                UserId = userId
            };
        }
    }
}