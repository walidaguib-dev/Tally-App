using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Teams.Members
{
    public record RemoveTeamMemberCommand(int teamId, string userId)
        : IRequest<bool?>,
            IInvalidateCache
    {
        public List<string> CacheKeys => [$"members_{teamId}"];

        public List<string> CacheTags => ["members", "teams"];
    }
}
