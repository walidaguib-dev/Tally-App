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
    public class GetTrucksHandler(
        ITrucks _trucksService
        ) : IRequestHandler<GetAllTrucksQuery, List<TruckDto>>
    {
        private readonly ITrucks trucksService = _trucksService;
        public async Task<List<TruckDto>> Handle(GetAllTrucksQuery request, CancellationToken cancellationToken)
        {
            var result = await trucksService.GetAll();
            if (result is null) return [];
            var response = result.Select(e => e.MapToJson()).ToList();
            return response;
        }
    }
}
