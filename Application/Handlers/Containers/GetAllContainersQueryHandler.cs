using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Containers;
using Application.Mappers;
using Application.Queries.Containers;
using Domain.Contracts;
using Domain.Helpers.Pagination;
using MediatR;

namespace Application.Handlers.Containers
{
    public class GetAllContainersQueryHandler(IContainers containersService)
        : IRequestHandler<GetAllContainersQuery, PagedResult<ContainersDto>>
    {
        private readonly IContainers _containersService = containersService;

        public async Task<PagedResult<ContainersDto>> Handle(
            GetAllContainersQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await _containersService.GetAll(
                request.Dto,
                request.Dto.ContainerNumber,
                request.Dto.Bill_of_lading,
                request.Dto.ClientName
            );

            return result!.MapToPagedResult(e => e.MapToDto()) ?? new PagedResult<ContainersDto>();
        }
    }
}
