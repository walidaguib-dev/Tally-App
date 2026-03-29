using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Dtos.Containers;
using Application.Mappers;
using Application.Queries.Containers;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Containers
{
    public class GetContainerQueryHandler(IContainers containersService)
        : IRequestHandler<GetContainerQuery, ContainersDto?>
    {
        private readonly IContainers _containersService = containersService;

        public async Task<ContainersDto?> Handle(
            GetContainerQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await _containersService.GetAsync(request.ContainerNumber);
            return result?.MapToDto();
        }
    }
}
