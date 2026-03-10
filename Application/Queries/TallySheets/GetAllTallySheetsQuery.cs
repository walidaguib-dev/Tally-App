using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using Domain.Helpers.Pagination;
using MediatR;

namespace Application.Queries.TallySheets
{
    public record GetAllTallySheetsQuery(TallySheetsQueryDto QueryDto) : IRequest<PagedResult<TallySheetDto>>;
}