using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Containers;
using Application.Dtos.Containers;
using Application.Mappers;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Containers
{
    public class CreateContainerHandler(IContainers containersService)
        : IRequestHandler<CreateContainerCommand, ContainersDto>
    {
        private readonly IContainers _containersService = containersService;

        public async Task<ContainersDto> Handle(
            CreateContainerCommand request,
            CancellationToken cancellationToken
        )
        {
            var item = request.Dto.MapToEntity();
            var result = await _containersService.CreateAsync(item);
            var response = result.MapToDto();
            return response;
        }
    }
}
