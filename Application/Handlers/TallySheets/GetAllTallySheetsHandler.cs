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
    public class GetAllTallySheetsHandler(
        ITallySheet tallySheetService
    ) : IRequestHandler<GetAllTallySheetsQuery, List<TallySheetDto>>
    {
        private readonly ITallySheet _tallySheetService = tallySheetService;
        public async Task<List<TallySheetDto>> Handle(GetAllTallySheetsQuery request, CancellationToken cancellationToken)
        {
            var result = await _tallySheetService.GetAllAsync();
            return [.. result.Select(t => t.MapToDto())];
        }
    }
}