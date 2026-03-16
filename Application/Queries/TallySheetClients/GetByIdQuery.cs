using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetClient;
using MediatR;

namespace Application.Queries.TallySheetClients
{
    public record GetByIdQuery(int TallySheetId, int MerchandiseId)
        : IRequest<TallySheetClientDto?>;
}
