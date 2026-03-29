using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Containers;
using Application.Mappers;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Containers
{
    public class UpdateContainerHandler(IContainers containersService)
        : IRequestHandler<UpdateContainerCommand, bool?>
    {
        private readonly IContainers _containersService = containersService;

        public async Task<bool?> Handle(
            UpdateContainerCommand request,
            CancellationToken cancellationToken
        )
        {
            var (Id, Dto) = request;
            return await _containersService.UpdateAsync(Id, Dto.MapToRequest());
        }
    }
}
