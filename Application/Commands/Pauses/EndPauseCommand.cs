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
    public record EndPauseCommand(int Id, EndPauseDto Dto) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"pause_{Id}"];

        public List<string> CacheTags => ["pauses", "pause"];
    }
}