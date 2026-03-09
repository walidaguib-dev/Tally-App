using Application.Dtos.Ships;
using Application.Mappers;
using Application.Queries.Ships;
using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Ships
{
    public class GetAllShipsHandler(
        IShips shipsService
        ) : IRequestHandler<GetAllShipsQuery, PagedResult<ShipDto>>
    {
        private readonly IShips _shipsService = shipsService;
        public async Task<PagedResult<ShipDto>> Handle(GetAllShipsQuery request, CancellationToken cancellationToken)
        {
            var result = await _shipsService.GetAllShips(request.QueryDto, request.QueryDto.Name);

            return result?.MapToPagedResult<Ship, ShipDto>(s => s.MapToJson())
                    ?? new PagedResult<ShipDto>();
        }
    }
}
