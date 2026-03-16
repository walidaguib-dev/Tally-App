using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetClient;
using Domain.Entities;

namespace Application.Mappers
{
    public static class TallySheetClientMapper
    {
        public static TallySheetClientDto MapToDto(this TallySheetClient item)
        {
            return new TallySheetClientDto
            {
                TallySheetId = item.TallySheetId,
                ClientId = item.ClientId,
                ClientName = item.Client?.Name ?? "Unknown",
                Quantity = item.Quantity,
                Unit = item.Unit,
                Notes = item.Notes,
                LastUpdated = item.LastUpdated
            };
        }

        public static TallySheetClient MapToEntity(this AddClientToTallyDto dto)
        {
            return new TallySheetClient
            {
                TallySheetId = dto.TallySheetId,
                ClientId = dto.ClientId,
                Quantity = dto.Quantity,
                Unit = dto.Unit.ToLower(),
                Notes = dto.Notes!,
                LastUpdated = null
            };
        }
    }
}