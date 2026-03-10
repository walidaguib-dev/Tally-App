using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheets;
using Domain.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.TallySheets
{
    public class UpdateTallySheetHandler(
        ITallySheet tallySheetService
    ) : IRequestHandler<UpdateTallySheetCommand, bool?>
    {
        private readonly ITallySheet _tallySheetService = tallySheetService;
        public async Task<bool?> Handle(UpdateTallySheetCommand request, CancellationToken cancellationToken)
        {
            var (id, dto) = request;
            var result = await _tallySheetService.UpdateOneAsync(id, dto.TeamsCount, dto.Shift, dto.Zone, dto.ShipId);
            return result;
        }
    }
}