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
    public class GetShipHandler(
        IShips shipsService
        ) : IRequestHandler<GetShipQuery, ShipDto?>
    {
        private readonly IShips _shipsService = shipsService;
        public async Task<ShipDto?> Handle(GetShipQuery request, CancellationToken cancellationToken)
        {
            var ship = await _shipsService.GetShipById(request.Id);
            if (ship == null)
            {
                return null;

            }

            return ship.MapToJson();
        }
    }
}
