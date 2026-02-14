using Application.Dtos.Clients;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Clients
{
    public record CreateClientCommand(CreateClientDto Dto) : IRequest<Client>, IInvalidateCache
    {
        public List<string> CacheKeys => ["clients"];
    }
}
