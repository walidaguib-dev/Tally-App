using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheets;
using Application.Dtos.TallySheets;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using MediatR;

namespace Application.Handlers.TallySheets
{
    public class CreateTallySheetHandler(
        ITallySheet tallySheetService
    ) : IRequestHandler<CreateTallySheetCommand, TallySheet>
    {
        private readonly ITallySheet _tallySheetService = tallySheetService;
        public async Task<TallySheet> Handle(CreateTallySheetCommand request, CancellationToken cancellationToken)
        {

            var tallySheet = request.dto.MapToEntity(request.UserId);
            var result = await _tallySheetService.CreateAsync(tallySheet);
            return result;
        }
    }
}