using Application.Commands.ships;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Ships
{
    public class DeleteShipHandler(
        IShips shipsService
        ) : IRequestHandler<Application.Commands.ships.DeleteShipCommand, Domain.Entities.Ship?>
    {
        private readonly IShips _shipsService = shipsService;
        public async Task<Ship?> Handle(DeleteShipCommand request, CancellationToken cancellationToken)
        {
            var ship = await _shipsService.DeleteShip(request.Id);
            if(ship == null)
            {
                return null;
            }
            return ship;
        }
    }
}
