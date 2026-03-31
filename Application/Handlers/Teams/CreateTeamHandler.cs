using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Commands.Teams;
using Application.Dtos.Teams;
using Application.Mappers;
using Domain.Contracts;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Handlers.Teams
{
    public class CreateTeamHandler(ITeams _teamsService)
        : IRequestHandler<CreateTeamCommand, TeamDto>
    {
        private readonly ITeams teamsService = _teamsService;

        public async Task<TeamDto> Handle(
            CreateTeamCommand request,
            CancellationToken cancellationToken
        )
        {
            var team = new Domain.Entities.Teams.Team
            {
                Name = request.Dto.Name,
                Description = request.Dto.Description,
                SupervisorId = request.SupervisorId,
            };
            var result = await teamsService.CreateTeam(team);
            return result.MapToDto();
        }
    }
}
