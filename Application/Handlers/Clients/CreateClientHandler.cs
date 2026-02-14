using Application.Commands.Clients;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Clients
{
    public class CreateClientHandler(
        IClients _clientsService
        ) : IRequestHandler<CreateClientCommand, Client>
    {
        private readonly IClients clientsService = _clientsService;
        public async Task<Client> Handle(CreateClientCommand request, CancellationToken cancellationToken)
        {
            var client = request.Dto.MapToModel();
            var result = await clientsService.CreateOne(client);
            return result;
        }
    }
}
