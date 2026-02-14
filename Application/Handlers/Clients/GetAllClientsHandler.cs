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
            var result = await clientsService.GetAll();
            var response = result.Select(r => r.MapToJson()).ToList();
            return response;
        }
    }
}
