using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetMerchandise;
using MediatR;

namespace Application.Queries.TallySheetMerchandises
{
    public record GetAllByTallySheetIdQuery(int TallySheetId) : IRequest<List<TallySheetMerchandiseDto>>;
}