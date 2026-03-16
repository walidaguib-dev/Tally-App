using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetClient;

using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheetClients
{
    public class UpdateQuantityHandler(ITallySheetClient _sheetClient)
        : IRequestHandler<UpdateQuantityCommand, bool?>
    {
        private readonly ITallySheetClient sheetClient = _sheetClient;

        public async Task<bool?> Handle(
            UpdateQuantityCommand request,
            CancellationToken cancellationToken
        )
        {
            var (TallySheetId, MerchandiseId, Dto) = request;
            return await sheetClient.QueueQuantityUpdate(TallySheetId, MerchandiseId, Dto.Quantity);
        }
    }
}
