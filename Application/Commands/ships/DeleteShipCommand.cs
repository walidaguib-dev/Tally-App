using Domain.Entities;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ships
{
    public record DeleteShipCommand(int Id) : IRequest<Ship?>, IInvalidateCache
    {
        public List<string> CacheKeys => ["ships", $"ship_{Id}"];
        public List<string> CacheTags => ["ships"];
    }
}
