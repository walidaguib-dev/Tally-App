using Application.Dtos.Ships;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Commands.ships
{
    public record UpdateShipCommand(int id ,UpdateShipDto Dto) : IRequest<Ship>;
}
