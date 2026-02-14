using Application.Commands.Clients;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Clients
{
    public class DeleteClientHandler(IClients _clientsService) : IRequestHandler<DeleteClientCommand, string?>
    {
        private readonly IClients clientsService = _clientsService;
        public async Task<string?> Handle(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var result = await clientsService.DeleteOne(request.id);
            return result is null ? null : $"client - {request.id} is deleted";
        }
    }
}
