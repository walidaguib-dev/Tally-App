using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams.TeamMembers;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Teams.Members
{
    public record CreateTeamMemberCommand(AddMemberDto Dto)
        : IRequest<TeamMemberDto>,
            IInvalidateCache
    {
        public List<string> CacheKeys => [];

        public List<string> CacheTags => ["members", "teams"];
    }
}
