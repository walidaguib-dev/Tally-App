using Application.Dtos.Clients;
using Application.Mappers;
using Application.Queries.Clients;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Clients
{
    public class GetAllClientsHandler(IClients _clientsService) : IRequestHandler<GetAllClientsQuery, List<ClientsDto>>
    {
        private readonly IClients clientsService = _clientsService;
        public async Task<List<ClientsDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            var paginationsParams = new Domain.Helpers.Pagination.PaginationParams
            {
                PageNumber = request.dto.PageNumber,
                PageSize = request.dto.PageSize,
                IsDescending = request.dto.IsDescending,
                SortBy = request.dto.SortBy
            };
            var result = await clientsService.GetAll(paginationsParams, request.dto.Name);
            var response = result.Select(r => r.MapToJson()).ToList();
            return response;
        }
    }
}
