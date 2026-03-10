using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using Application.Mappers;
using Application.Queries.TallySheets;
using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
using MediatR;

namespace Application.Handlers.TallySheets
{
    public class GetAllTallySheetsHandler(
        ITallySheet tallySheetService
    ) : IRequestHandler<GetAllTallySheetsQuery, PagedResult<TallySheetDto>>
    {
        private readonly ITallySheet _tallySheetService = tallySheetService;
        public async Task<PagedResult<TallySheetDto>> Handle(GetAllTallySheetsQuery request, CancellationToken cancellationToken)
        {
            var result = await _tallySheetService.GetAllAsync(
                request.QueryDto, request.QueryDto.ShipName, request.QueryDto.Shift, request.QueryDto.Zone, request.QueryDto.Date);
            return result!.MapToPagedResult<TallySheet, TallySheetDto>(c => c.MapToDto()) ?? new PagedResult<TallySheetDto>();
        }
    }
}