using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Teams.Members;
using Application.Dtos.Teams.TeamMembers;
using Application.Mappers;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Teams.Members
{
    public class CreateTeamMemberHandler(ITeams _teamsService)
        : IRequestHandler<CreateTeamMemberCommand, TeamMemberDto>
    {
        private readonly ITeams teamsService = _teamsService;

        public async Task<TeamMemberDto> Handle(
            CreateTeamMemberCommand request,
            CancellationToken cancellationToken
        )
        {
            var item = request.Dto.MapToEntity();
            var result = await teamsService.AddMember(item);
            return result.MapToDto();
        }
    }
}
