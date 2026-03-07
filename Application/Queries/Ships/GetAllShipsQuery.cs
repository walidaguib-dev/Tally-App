using Application.Dtos.Ships;

using Domain.Helpers.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Ships
{
    public record GetAllShipsQuery(ShipsQueryDto QueryDto) : IRequest<PagedResult<ShipDto>>;
}
