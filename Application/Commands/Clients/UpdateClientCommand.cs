using Application.Dtos.Clients;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Clients
{
    public record UpdateClientCommand(int id, UpdateClientDto Dto) : IRequest<string?>, IInvalidateCache
    {
        public List<string> CacheKeys => ["clients", $"client_{id}"];
    }
}
