using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Teams.Members;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Teams.Members
{
    public class RemoveTeamMemberHandler(ITeams _teamsService)
        : IRequestHandler<RemoveTeamMemberCommand, bool?>
    {
        private readonly ITeams teamsService = _teamsService;

        public async Task<bool?> Handle(
            RemoveTeamMemberCommand request,
            CancellationToken cancellationToken
        )
        {
            return await teamsService.RemoveMember(request.teamId, request.userId);
        }
    }
}
