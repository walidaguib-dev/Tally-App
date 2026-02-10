using Application.Dtos.Ships;
using Application.Mappers;
using Application.Queries.Ships;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Ships
{
    public class GetAllShipsHandler(
        IShips shipsService
        ) : IRequestHandler<GetAllShipsQuery, List<ShipDto>>
    {
        private readonly IShips _shipsService = shipsService;
        public async Task<List<ShipDto>> Handle(GetAllShipsQuery request, CancellationToken cancellationToken)
        {
            var ships = await _shipsService.GetAllShips();

            return [.. ships.Select(s => s.MapToJson())];
        }
    }
}
