using Application.Dtos.Clients;
using Application.Mappers;
using Application.Queries.Clients;
using Domain.Contracts;
using Domain.Helpers.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Clients
{
    public class GetAllClientsHandler(IClients _clientsService) : IRequestHandler<GetAllClientsQuery, PagedResult<ClientsDto>>
    {
        private readonly IClients clientsService = _clientsService;
        public async Task<PagedResult<ClientsDto>> Handle(GetAllClientsQuery request, CancellationToken cancellationToken)
        {
            // var paginationsParams = new Domain.Helpers.Pagination.PaginationParams
            // {
            //     PageNumber = request.dto.PageNumber,
            //     PageSize = request.dto.PageSize,
            //     IsDescending = request.dto.IsDescending,
            //     SortBy = request.dto.SortBy
            // };
            var result = await clientsService.GetAll(request.dto, request.dto.Name);
            var response = result;
            return response is null ? new PagedResult<ClientsDto>() : new PagedResult<ClientsDto>
            {
                Items = [.. result!.Items.Select(c => c.MapToJson())],
                TotalCount = result.TotalCount,
                PageNumber = request.dto.PageNumber,
                PageSize = request.dto.PageSize
            };
        }
    }
}
