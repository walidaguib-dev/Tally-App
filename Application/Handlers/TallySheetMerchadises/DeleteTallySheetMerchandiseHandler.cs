using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.TallySheetMerchandise;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.TallySheetMerchadises
{
    public class DeleteTallySheetMerchandiseHandler(
        ITallySheetMerchandise _sheetMerchandiseService
    ) : IRequestHandler<DeleteTallySheetMerchandise, bool?>
    {
        private readonly ITallySheetMerchandise sheetMerchandiseService = _sheetMerchandiseService;
        public async Task<bool?> Handle(DeleteTallySheetMerchandise request, CancellationToken cancellationToken)
        {
            return await sheetMerchandiseService.DeleteMerchandise(request.TallySheetId, request.MerchandiseId);
        }
    }
}