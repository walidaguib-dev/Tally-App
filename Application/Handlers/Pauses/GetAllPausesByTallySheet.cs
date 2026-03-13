using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Pauses;
using Application.Mappers;
using Application.Queries.Pauses;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Pauses
{
    public class GetAllPausesByTallySheet(
        IPauses pausesService
    ) : IRequestHandler<GetAllPausesQuery, List<PauseDto>>
    {
        private readonly IPauses _pausesService = pausesService;
        public async Task<List<PauseDto>> Handle(GetAllPausesQuery request, CancellationToken cancellationToken)
        {
            var result = await _pausesService.GetPausesByTallySession(request.tallySheetId);
            var response = result.Select(e => e.ToPauseDto()).ToList();
            return response;
        }
    }
}