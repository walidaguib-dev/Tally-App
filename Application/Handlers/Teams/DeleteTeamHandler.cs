using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Teams;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Teams
{
    public class DeleteTeamHandler(ITeams _teamsService) : IRequestHandler<DeleteTeamCommand, bool?>
    {
        private readonly ITeams teamsService = _teamsService;

        public async Task<bool?> Handle(
            DeleteTeamCommand request,
            CancellationToken cancellationToken
        )
        {
            return await teamsService.DeleteTeam(request.Id);
        }
    }
}
