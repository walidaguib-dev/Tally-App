using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ships
{
    public record DeleteShipCommand(int Id) : IRequest<Ship?>;
   
}
