using Application.Dtos.Trucks;
using Application.Mappers;
using Application.Queries.Trucks;
using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Trucks
{
    public class GetTrucksHandler(
        ITrucks _trucksService
        ) : IRequestHandler<GetAllTrucksQuery, PagedResult<TruckDto>>
    {
        private readonly ITrucks trucksService = _trucksService;
        public async Task<PagedResult<TruckDto>> Handle(GetAllTrucksQuery request, CancellationToken cancellationToken)
        {
            var result = await trucksService.GetAll(request.Dto, request.Dto.PlateNumber);
            var response = result?.MapToPagedResult<Truck, TruckDto>(m => m.MapToJson()) ?? new PagedResult<TruckDto>();
            return response;
        }
    }
}
