using Application.Commands.Clients;
using Domain.Contracts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Handlers.Clients
{
    public class UpdateClientHandler(IClients _clientsService) : IRequestHandler<UpdateClientCommand, string?>
    {
        private readonly IClients clientsService = _clientsService;
        public async Task<string?> Handle(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var result = await clientsService.UpdateOne(
                request.id,
                request.Dto.Name,
                request.Dto.ContactInfo,
                request.Dto.Bill_Of_Lading,
                request.Dto.MerchandiseId
                );
            return result is null ? null : $"client-{request.id} is updated";
            
        }
    }
}
