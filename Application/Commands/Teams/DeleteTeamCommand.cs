using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Teams
{
    public record DeleteTeamCommand(int Id) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"team_{Id}"];

        public List<string> CacheTags => ["teams"];
    }
}
