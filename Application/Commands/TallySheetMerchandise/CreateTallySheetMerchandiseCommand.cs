using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheetMerchandise;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheetMerchandise
{
    public record CreateTallySheetMerchandiseCommand(AddMerchandiseToTallyDto Dto)
                        : IRequest<TallySheetMerchandiseDto>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"Operations_{Dto.TallySheetId}"];

        public List<string> CacheTags => throw new NotImplementedException();
    }
}