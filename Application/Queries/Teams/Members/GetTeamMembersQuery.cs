using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams.TeamMembers;
using MediatR;

namespace Application.Queries.Teams.Members
{
    public record GetTeamMembersQuery(int teamId) : IRequest<List<TeamMemberDto>> { }
}
