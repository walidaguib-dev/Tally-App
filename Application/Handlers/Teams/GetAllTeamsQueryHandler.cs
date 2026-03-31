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
    public class GetAllTeamsQueryHandler(ITeams _teamsService)
        : IRequestHandler<GetTeamsQuery, List<TeamDto>>
    {
        private readonly ITeams teamsService = _teamsService;

        public async Task<List<TeamDto>> Handle(
            GetTeamsQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await teamsService.GetAll();
            var response = result.Select(e => e.MapToDto()).ToList();
            return response;
        }
    }
}
