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
    public class GetContainersByTallySessionQueryHandler(IContainers containersService)
        : IRequestHandler<GetContainersByTallySessionIdQuery, List<ContainersDto>>
    {
        private readonly IContainers _containersService = containersService;

        public async Task<List<ContainersDto>> Handle(
            GetContainersByTallySessionIdQuery request,
            CancellationToken cancellationToken
        )
        {
            var result = await _containersService.GetAllByTallySession(request.TallySessionId);
            var response = result.Select(e => e.MapToDto()).ToList();
            return response;
        }
    }
}
