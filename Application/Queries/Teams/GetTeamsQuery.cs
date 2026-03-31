using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Teams;
using MediatR;

namespace Application.Queries.Teams
{
    public record GetTeamsQuery : IRequest<List<TeamDto>> { }
}
