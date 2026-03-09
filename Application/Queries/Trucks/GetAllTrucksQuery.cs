using Application.Dtos.Trucks;
using Domain.Helpers.Pagination;
using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Queries.Trucks
{
    public record GetAllTrucksQuery(TrucksQueryDto Dto) : IRequest<PagedResult<TruckDto>>;
}
