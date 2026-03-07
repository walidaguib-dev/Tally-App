using Application.Dtos.Trucks;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Trucks
{
    public record UpdateTruckCommand(int Id, UpdatetTruckDto Dto) : IRequest<string?>, IInvalidateCache
    {
        public List<string> CacheKeys => ["trucks", $"truck_{Id}"];
        public List<string> CacheTags => ["trucks"];
    }
}
