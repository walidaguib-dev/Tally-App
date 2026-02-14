using Application.Commands.ships;
using Application.Dtos.Ships;
using Domain.Contracts;
using Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Ships
{
    public class UpdateShipHandler(
        IShips shipsService
        ) : IRequestHandler<UpdateShipCommand, Ship>
    {
        private readonly IShips _shipsService = shipsService;

        public async Task<Ship> Handle(UpdateShipCommand request, CancellationToken cancellationToken)
        {
            var ship = await _shipsService.UpdateShip(request.id,request.Dto.Name,request.Dto.ImoNumber);
            return ship ?? throw new Exception("Ship not found");
        }
    }
}
