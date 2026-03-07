using Application.Dtos.Ships;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ships
{
    public record CreateShipCommand(CreateShipDto Dto) : IRequest<Ship>, IInvalidateCache
    {
        public List<string> CacheKeys => ["ships"];
        public List<string> CacheTags => ["ships"];
    }
}
