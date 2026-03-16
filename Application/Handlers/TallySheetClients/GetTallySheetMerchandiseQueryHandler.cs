using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetClient;
using Application.Mappers;
using Application.Queries.TallySheetClients;
using Domain.Contracts;
using MediatR;

namespace Application.Commands.TallySheetClients
{
    public class GetTallySheetMerchandiseQueryHandler(ITallySheetClient _sheetClient)
        : IRequestHandler<GetByIdQuery, TallySheetClientDto?>
    {
        private readonly ITallySheetClient sheetClient = _sheetClient;

        public async Task<TallySheetClientDto?> Handle(
            GetByIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await sheetClient.GetById(request.TallySheetId, request.MerchandiseId);
            return result?.MapToDto();
        }
    }
}
