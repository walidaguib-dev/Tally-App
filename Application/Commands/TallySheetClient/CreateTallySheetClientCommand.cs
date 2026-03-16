using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetClient;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetClient
{
    public record CreateTallySheetClientCommand(AddClientToTallyDto Dto)
        : IRequest<TallySheetClientDto>,
            IInvalidateCache
    {
        public List<string> CacheKeys => [$"Operations_{Dto.TallySheetId}"];

        public List<string> CacheTags => [""];
    }
}
