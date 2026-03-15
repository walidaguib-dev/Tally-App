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
    public class GetAllTallySheetMerchandisesQueryHandler(
        ITallySheetMerchandise _sheetMerchandise
    ) : IRequestHandler<GetAllByTallySheetIdQuery, List<TallySheetMerchandiseDto>>
    {
        private readonly ITallySheetMerchandise sheetMerchandise = _sheetMerchandise;
        public async Task<List<TallySheetMerchandiseDto>> Handle(GetAllByTallySheetIdQuery request, CancellationToken cancellationToken)
        {
            var result = await sheetMerchandise.GetByTallySheet(request.TallySheetId);
            var response = result.Select(e => e.MapToDto())
                .ToList();
            return response;
        }
    }
}