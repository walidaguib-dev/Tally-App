using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Containers;
using Domain.Helpers.Pagination;
using MediatR;

namespace Application.Queries.Containers
{
    public record GetAllContainersQuery(ContainersFilterDto Dto)
        : IRequest<PagedResult<ContainersDto>> { }
}
