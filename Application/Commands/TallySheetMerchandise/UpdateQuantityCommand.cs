using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetMerchandise;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetMerchandise
{
    public record UpdateQuantityCommand(int Id, UpdateQuantityDto Dto) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => throw new NotImplementedException();

        public List<string> CacheTags => throw new NotImplementedException();
    }
}