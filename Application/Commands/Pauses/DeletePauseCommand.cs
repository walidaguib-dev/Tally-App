using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Application.Commands.Pauses
{
    public record DeletePauseCommand(int Id) : IRequest<bool?>
    {

    }
}