using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams;
using Domain.Helpers;
using MediatR;

namespace Application.Commands.Teams
{
    public record CreateTeamCommand(CreateTeamDto Dto, string SupervisorId)
        : IRequest<TeamDto>,
            IInvalidateCache
    {
        public List<string> CacheKeys => [];

        public List<string> CacheTags => ["teams"];
    }
}
