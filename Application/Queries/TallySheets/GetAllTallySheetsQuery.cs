using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using MediatR;

namespace Application.Queries.TallySheets
{
    public record GetAllTallySheetsQuery : IRequest<List<TallySheetDto>>;
}