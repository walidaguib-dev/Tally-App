using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetMerchandise;
using Application.Mappers;
using Application.Queries.TallySheetMerchandises;
using Domain.Contracts;
using MediatR;

namespace Application.Commands.TallySheetMerchandise
{
    public class GetTallySheetMerchandiseQueryHandler(
        ITallySheetMerchandise _sheetMerchandise
    ) : IRequestHandler<GetByIdQuery, TallySheetMerchandiseDto?>
    {
        private readonly ITallySheetMerchandise sheetMerchandise = _sheetMerchandise;
        public async Task<TallySheetMerchandiseDto?> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var result = await sheetMerchandise.GetById(request.TallySheetId, request.MerchandiseId);
            return result?.MapToDto();
        }
    }
}