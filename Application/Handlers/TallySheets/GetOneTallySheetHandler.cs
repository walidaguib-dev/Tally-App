using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using Application.Mappers;
using Application.Queries.TallySheets;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheets
{
    public class GetOneTallySheetHandler(
        ITallySheet tallySheetService
    ) : IRequestHandler<GetTallySheetQuery, TallySheetDto?>
    {
        private readonly ITallySheet _tallySheetService = tallySheetService;
        public async Task<TallySheetDto?> Handle(GetTallySheetQuery request, CancellationToken cancellationToken)
        {
            var result = await _tallySheetService.GetOneAsync(request.id);
            return result?.MapToDto();
        }
    }
}