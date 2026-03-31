using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams;
using Application.Mappers;
using Application.Queries.Teams;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Teams
{
    public class GetTeamQueryHandler(ITeams _teamsService) : IRequestHandler<GetTeamQuery, TeamDto?>
    {
        private readonly ITeams teamsService = _teamsService;

        public async Task<TeamDto?> Handle(
            GetTeamQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await teamsService.GetById(request.Id);
            return result?.MapToDto();
        }
    }
}
