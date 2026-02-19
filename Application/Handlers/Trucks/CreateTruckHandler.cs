using Application.Commands.Trucks;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Trucks
{
    public class CreateTruckHandler(
        ITrucks _trucksService
        ) : IRequestHandler<CreateTruckCommand, Truck>
    {
        private readonly ITrucks trucksService = _trucksService;
        public async Task<Truck> Handle(CreateTruckCommand request, CancellationToken cancellationToken)
        {
            var truck = request.Dto.MapToModel();
            var result = await trucksService.CreateOne(truck);
            return result;
        }
    }
}
