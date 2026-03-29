using System;
using System.Collections.Generic;
using System.Text;
using Application.Dtos.Clients;
using Application.Mappers;
using Application.Queries.Clients;
using Domain.Contracts;
using Domain.Entities;
using Domain.Helpers.Pagination;
using MediatR;

namespace Application.Handlers.Clients
{
    public class GetAllClientsHandler(IClients _clientsService)
        : IRequestHandler<GetAllClientsQuery, PagedResult<ClientsDto>>
    {
        private readonly IClients clientsService = _clientsService;

        public async Task<PagedResult<ClientsDto>> Handle(
            GetAllClientsQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await clientsService.GetAll(request.dto, request.dto.Name);

            return result?.MapToPagedResult<Client, ClientsDto>(c => c.MapToJson())
                ?? new PagedResult<ClientsDto>();
        }
    }
}
