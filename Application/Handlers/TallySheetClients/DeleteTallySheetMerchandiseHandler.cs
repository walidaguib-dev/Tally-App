using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetClient;

using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheetClients
{
    public class DeleteTallySheetMerchandiseHandler(ITallySheetClient _sheetClient)
        : IRequestHandler<DeleteTallySheetClient, bool?>
    {
        private readonly ITallySheetClient sheetClient = _sheetClient;

        public async Task<bool?> Handle(
            DeleteTallySheetClient request,
            CancellationToken cancellationToken
        )
        {
            return await sheetClient.DeleteMerchandise(request.TallySheetId, request.ClientId);
        }
    }
}
