using Application.Dtos.Merchandises;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Merchandises
{
    public record CreateMerchandiseCommand(CreateMerchandiseDto Dto) : IRequest<Merchandise>, IInvalidateCache
    {
        public List<string> CacheKeys => ["merchandises"];
    }
}
