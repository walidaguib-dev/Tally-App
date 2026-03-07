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
    public record UpdateTallySheetCommand(int id, UpdateTallySheetDto dto) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => ["tallySheets", $"tallySheet_{id}"];
        public List<string> CacheTags => ["tallySheets"];
    }
}