using Application.Commands.Trucks;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Trucks
{
    public class UpdateTruckHandler(
        ITrucks _trucksService
        ) : IRequestHandler<UpdateTruckCommand, string?>
    {
        private readonly ITrucks trucksService = _trucksService;
        public async Task<string?> Handle(UpdateTruckCommand request, CancellationToken cancellationToken)
        {
            var result = await trucksService.UpdateOne(request.Id, request.Dto.PlateNumber, request.Dto.Capacity);
            if (result is null) return null;
            return $"truck - {request.Id} is updated!";
        }
    }
}
