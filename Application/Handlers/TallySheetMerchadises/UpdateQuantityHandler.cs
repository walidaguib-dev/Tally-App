using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetMerchandise;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheetMerchadises
{
    public class UpdateQuantityHandler(
        ITallySheetMerchandise _sheetMerchandiseService
    ) : IRequestHandler<UpdateQuantityCommand, bool?>
    {
        private readonly ITallySheetMerchandise sheetMerchandiseService = _sheetMerchandiseService;
        public async Task<bool?> Handle(UpdateQuantityCommand request, CancellationToken cancellationToken)
        {
            var (TallySheetId, MerchandiseId, Dto) = request;
            return await sheetMerchandiseService.QueueQuantityUpdate(TallySheetId, MerchandiseId, Dto.Quantity);
        }
    }
}