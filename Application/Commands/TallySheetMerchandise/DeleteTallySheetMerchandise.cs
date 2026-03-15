using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetMerchandise
{
    public record DeleteTallySheetMerchandise(int TallySheetId, int MerchandiseId) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"Operation_{TallySheetId}_{MerchandiseId}", $"Operations_{TallySheetId}"];

        public List<string> CacheTags => throw new NotImplementedException();
    }
}