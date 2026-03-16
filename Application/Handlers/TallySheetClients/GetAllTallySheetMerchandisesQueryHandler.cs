using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetClient;
using Application.Mappers;
using Application.Queries.TallySheetClients;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheetClients
{
    public class GetAllTallySheetMerchandisesQueryHandler(ITallySheetClient _sheetClient)
        : IRequestHandler<GetAllByTallySheetIdQuery, List<TallySheetClientDto>>
    {
        private readonly ITallySheetClient sheetClient = _sheetClient;

        public async Task<List<TallySheetClientDto>> Handle(
            GetAllByTallySheetIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await sheetClient.GetByTallySheet(request.TallySheetId);
            var response = result.Select(e => e.MapToDto()).ToList();
            return response;
        }
    }
}
