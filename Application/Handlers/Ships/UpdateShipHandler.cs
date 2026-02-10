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
        IShips shipsService,
        [FromKeyedServices("updateShip")] IValidator<UpdateShipDto> validator
        ) : IRequestHandler<UpdateShipCommand, Ship>
    {
        private readonly IShips _shipsService = shipsService;
        private readonly IValidator<UpdateShipDto> _validator = validator;
        public async Task<Ship> Handle(UpdateShipCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _validator.ValidateAsync(request.Dto);
            if (!ValidationResult.IsValid)
            {
                throw new ValidationException(ValidationResult.Errors);
            }

            var ship = await _shipsService.UpdateShip(request.id,request.Dto.Name,request.Dto.ImoNumber);
            if (ship == null)
            {
                throw new Exception("Ship not found");
            }
            return ship;
        }
    }
}
