using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.TallySheets;
using Domain.Entities;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.TallySheets
{
    public record CreateTallySheetCommand(CreateTallySheetDto dto, string UserId) : IRequest<TallySheet>, IInvalidateCache
    {
        public List<string> CacheKeys => ["tallySheets"];
        public List<string> CacheTags => ["tallySheets"];
    }
}