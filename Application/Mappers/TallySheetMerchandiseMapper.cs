using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetMerchandise;
using Domain.Entities;

namespace Application.Mappers
{
    public static class TallySheetMerchandiseMapper
    {
        public static TallySheetMerchandiseDto MapToDto(this TallySheetMerchandise item)
        {
            return new TallySheetMerchandiseDto
            {
                TallySheetId = item.TallySheetId,
                MerchandiseId = item.MerchandiseId,
                MerchandiseName = item.Merchandise?.Name ?? "Unknown",
                Quantity = item.Quantity,
                Unit = item.Unit,
                Notes = item.Notes,
                LastUpdated = item.LastUpdated
            };
        }

        public static TallySheetMerchandise MapToEntity(this AddMerchandiseToTallyDto dto)
        {
            return new TallySheetMerchandise
            {
                TallySheetId = dto.TallySheetId,
                MerchandiseId = dto.MerchandiseId,
                Quantity = dto.Quantity,
                Unit = dto.Unit.ToLower(),
                Notes = dto.Notes!,
                LastUpdated = null
            };
        }
    }
}