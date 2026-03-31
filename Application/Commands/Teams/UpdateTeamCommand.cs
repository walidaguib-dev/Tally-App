using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Teams
{
    public record UpdateTeamCommand(int Id, UpdateTeamDto Dto) : IRequest<bool?>, IInvalidateCache
    {
        public List<string> CacheKeys => [$"team_{Id}"];

        public List<string> CacheTags => ["teams"];
    }
}
