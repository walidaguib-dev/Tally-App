using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetMerchandise;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetMerchandise
{
    public record UpdateQuantityCommand(int TallySheetId, int MerchandiseId, UpdateQuantityDto Dto) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"Operation_{TallySheetId}_{MerchandiseId}", $"Operations_{TallySheetId}"];

        public List<string> CacheTags => throw new NotImplementedException();
    }
}