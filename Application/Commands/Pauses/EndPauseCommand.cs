using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Pauses;
using Domain.Entities;
using MediatR;

namespace Application.Commands.Pauses
{
    public record EndPauseCommand(EndPauseDto Dto) : IRequest<Pause>;
}