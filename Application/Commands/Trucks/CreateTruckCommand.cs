using Application.Dtos.Trucks;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Trucks
{
    public record CreateTruckCommand(CreateTruckDto Dto) : IRequest<Truck>, IInvalidateCache
    {
        public List<string> CacheKeys => ["trucks"];
        public List<string> CacheTags => ["trucks"];
    }
}
