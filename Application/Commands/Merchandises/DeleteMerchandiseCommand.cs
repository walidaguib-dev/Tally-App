using Domain.Helpers;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.Merchandises
{
    public record DeleteMerchandiseCommand(int Id) : IRequest<string?>, IInvalidateCache
    {
        public List<string> CacheKeys => ["merchandises", $"merchandise_{Id}"];
        public List<string> CacheTags => ["merchandises"];
    }
}
