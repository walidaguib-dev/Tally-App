using Application.Commands.ships;
using Application.Dtos.Ships;
using Application.Mappers;
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
    internal class CreateShipHandler(
        IShips shipsService,
        [FromKeyedServices("createShip")] IValidator<CreateShipDto> validator
        ) : IRequestHandler<CreateShipCommand, Ship>
    {
        private readonly IShips _shipsService = shipsService;
        private readonly IValidator<CreateShipDto> _validator = validator;
        public async Task<Ship> Handle(CreateShipCommand request, CancellationToken cancellationToken)
        {
            var ValidationResult = await _validator.ValidateAsync(request.Dto);
            if(!ValidationResult.IsValid)
            {
                throw new ValidationException(ValidationResult.Errors);
            }

            var ship = request.Dto.MapToModel();

            return await _shipsService.CreateShip(ship);
        }
    }
}
