using Application.Dtos.Trucks;
using Application.Mappers;
using Application.Queries.Trucks;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Trucks
{
    internal class GetTruckHandler(
        ITrucks _trucksService
        ) : IRequestHandler<GetTruckQuery, TruckDto?>
    {
        private readonly ITrucks trucksService = _trucksService;
        public async Task<TruckDto?> Handle(GetTruckQuery request, CancellationToken cancellationToken)
        {
            var result = await trucksService.GetOne(request.Id);
            if (result is null) return null;
            var response = result.MapToJson();
            return response;
        }
    }
}
