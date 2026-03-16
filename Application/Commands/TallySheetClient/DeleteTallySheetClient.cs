using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetClient
{
    public record DeleteTallySheetClient(int TallySheetId, int ClientId)
        : IRequest<bool?>,
            IInvalidateCache
    {
        public List<string> CacheKeys =>
            [$"Operation_{TallySheetId}_{ClientId}", $"Operations_{TallySheetId}"];

        public List<string> CacheTags => [""];
    }
}
