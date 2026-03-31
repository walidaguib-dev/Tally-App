using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Teams;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Teams
{
    public class UpdateTeamHandler(ITeams _teamsService) : IRequestHandler<UpdateTeamCommand, bool?>
    {
        private readonly ITeams teamsService = _teamsService;

        public async Task<bool?> Handle(
            UpdateTeamCommand request,
            CancellationToken cancellationToken
        )
        {
            return await teamsService.UpdateTeam(
                request.Id,
                request.Dto.Name,
                request.Dto.Description
            );
        }
    }
}
