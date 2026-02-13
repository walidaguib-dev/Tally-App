using Application.Dtos.Merchandises;
using Domain.Entities;
using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Merchandises
{
    public record UpdateMerchandiseCommand(int Id, UpdateMerchandiseDto Dto) : IRequest<string?>, IInvalidateCache
    {
        public List<string> CacheKeys => ["merchandises", $"merchandise_{Id}"];
    }
}
