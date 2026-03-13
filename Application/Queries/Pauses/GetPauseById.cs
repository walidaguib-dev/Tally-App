using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Pauses;
using MediatR;

namespace Application.Queries.Pauses
{
    public record GetPauseByIdQuery(int Id) : IRequest<PauseDto?>
    {

    }
}