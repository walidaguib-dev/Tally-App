using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application.Commands.Containers;
using Domain.Contracts;
using MediatR;

namespace Application.Handlers.Containers
{
    public class DeleteContainersHandler(IContainers containersService)
        : IRequestHandler<DeleteContainerCommand, bool?>
    {
        private readonly IContainers _containersService = containersService;

        public async Task<bool?> Handle(
            DeleteContainerCommand request,
            CancellationToken cancellationToken
        )
        {
            return await _containersService.DeleteAsync(request.ContainerNumber);
        }
    }
}
