using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Trucks
{
    public record DeleteTruckCommand(int Id) : IRequest<string?>, IInvalidateCache
    {
        public List<string> CacheKeys => ["trucks", $"truck_{Id}"];
        public List<string> CacheTags => ["trucks"];
    }
}
