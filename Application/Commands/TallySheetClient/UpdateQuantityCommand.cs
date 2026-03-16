using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetClient;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetClient
{
    public record UpdateQuantityCommand(int TallySheetId, int ClientId, UpdateQuantityDto Dto)
        : IRequest<bool?>,
            IInvalidateCache
    {
        public List<string> CacheKeys =>
            [$"Operation_{TallySheetId}_{ClientId}", $"Operations_{TallySheetId}"];

        public List<string> CacheTags => [""];
    }
}
