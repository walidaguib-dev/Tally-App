using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Pauses;
using Domain.Entities;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Pauses
{
    public record CreatePauseCommand(CreatePauseDto Dto) : IRequest<Pause>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"pauses_{Dto.TallySheetId}"];

        public List<string> CacheTags => ["pauses", "pause"];
    }
}