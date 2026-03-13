using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Pauses;
using MediatR;

namespace Application.Commands.Pauses
{
    public record UpdatePauseCommand(int id, UpdatePauseDto Dto) : IRequest<bool?>
    {

    }
}