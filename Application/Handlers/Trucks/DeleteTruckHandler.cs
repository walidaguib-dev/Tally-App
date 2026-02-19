using Application.Commands.Trucks;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Trucks
{
    public class DeleteTruckHandler(
        ITrucks _trucksService
        ) : IRequestHandler<DeleteTruckCommand, string?>
    {
        private readonly ITrucks trucksService = _trucksService;
        public async Task<string?> Handle(DeleteTruckCommand request, CancellationToken cancellationToken)
        {
            var result = await trucksService.DeleteOne(request.Id);
            return result is null ? null : $"truck - {request.Id} is deleted!";
        }
    }
}
