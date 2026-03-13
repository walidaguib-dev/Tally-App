using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Pauses
{
    public record DeletePauseCommand(int Id) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"pause_{Id}"];

        public List<string> CacheTags => ["pauses", "pause"];
    }
}