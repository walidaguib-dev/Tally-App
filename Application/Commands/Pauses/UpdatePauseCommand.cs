using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Pauses;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Pauses
{
    public record UpdatePauseCommand(int id, UpdatePauseDto Dto) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"pause_{id}"];

        public List<string> CacheTags => ["pauses", "pause"];
    }
}