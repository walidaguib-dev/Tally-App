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
    public class GetClientHandler(IClients _clientsService) : IRequestHandler<GetClientQuery, ClientsDto?>
    {
        private readonly IClients clientsService = _clientsService;
        public async Task<ClientsDto?> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            var result = await clientsService.Get(request.id);
            return result?.MapToJson();
        }
    }
}
