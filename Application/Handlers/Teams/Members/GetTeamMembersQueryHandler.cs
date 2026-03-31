using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams.TeamMembers;
using Application.Mappers;
using Application.Queries.Teams.Members;
using Domain.Contracts;
using Domain.Entities.Teams;
using MediatR;

namespace Application.Handlers.Teams.Members
{
    public class GetTeamMembersQueryHandler(ITeams _teamsService)
        : IRequestHandler<GetTeamMembersQuery, List<TeamMemberDto>>
    {
        private readonly ITeams teamsService = _teamsService;

        public async Task<List<TeamMemberDto>> Handle(
            GetTeamMembersQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await teamsService.GetMembers(request.teamId);
            var response = result.Select(e => e.MapToDto()).ToList();
            return response;
        }
    }
}
