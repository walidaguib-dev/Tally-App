using Application.Dtos.Ships;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Ships
{
    public record GetShipQuery(int Id) : IRequest<ShipDto?>;
}
