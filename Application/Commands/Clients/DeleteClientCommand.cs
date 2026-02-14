using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Clients
{
    public record DeleteClientCommand(int id) : IRequest<string?>, IInvalidateCache
    {
        public List<string> CacheKeys => ["clients", $"client_{id}"];
    }
}
